using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EDMInternal.MiniJSON;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// Can be used for adding dependencies, scopeRegistries to .json file 
    /// </summary>
    public class ManifestModifier
    {
        public static readonly string MANIFEST_PATH = "Packages/manifest.json";
        public static readonly string MANIFEST_KEY_DEPENDENCIES = "dependencies";
        public static readonly string MANIFEST_KEY_REGISTRIES = "scopedRegistries";
        
        /// <summary>
        /// Path to manifest file
        /// </summary>
        public string PathToManifestFile
        {
            get;
            private set;
        }

        /// <summary>
        /// Manifest's content deserializes to this field
        /// </summary>
        public object ManifestDictionary;
        
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="pathToFile">By default path to manifest</param>
        public ManifestModifier(string pathToFile = "Packages/manifest.json")
        {
            //read manifest file
            PathToManifestFile = pathToFile;
            string manifestText = File.ReadAllText(pathToFile);
            //deserialize 
            ManifestDictionary = Json.Deserialize(manifestText);
        }

        /// <summary>
        /// Method for adding scope registries. Adds "scopeRegistry" key if it not present 
        /// </summary>
        /// <param name="registry">Entry to add</param>
        /// <returns>True if registry added or exists, false otherwise</returns>
        public bool AddScopeRegistry(ScopeRegistry registry)
        {
            if (CheckIfRegistryExists(registry)) return true;
            try
            {
                List<object> registries =
                    (List<object>) ((Dictionary<string, object>) ManifestDictionary)[MANIFEST_KEY_REGISTRIES];
                registries.Add(registry.ToDictionary());
            }
            catch (KeyNotFoundException)
            {
                ((Dictionary<string, object>) ManifestDictionary).Add(MANIFEST_KEY_REGISTRIES, new List<object>());
                return AddScopeRegistry(registry);
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Method for adding dependencies. Adds "dependencies" key if it not present
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="version"></param>
        /// <returns>True if dependency added, or exists, false otherwise</returns>
        public bool AddDependency(string dependency, string version)
        {
            if (CheckIfDependencyExists(dependency, version)) return true;
            try
            {
                Dictionary<string,object> dependencies = (Dictionary<string, object>) ((Dictionary<string, object>) ManifestDictionary)[MANIFEST_KEY_DEPENDENCIES];
                dependencies.Add(dependency,version);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Writes changes back to the file
        /// </summary>
        /// <returns></returns>
        public bool ApplyChanges()
        {
            string manifestText = Json.Serialize(ManifestDictionary,true);
            File.WriteAllText(PathToManifestFile,manifestText);
            return true;
        }

        /// <summary>
        /// Searches for specific scopeRegistry
        /// Uses ScopeRegistry.Equal method to compare entries
        /// Name matching is ignored
        /// </summary>
        /// <param name="registry">Entry to find</param>
        /// <returns>True if registry found, false otherwise</returns>
        public bool CheckIfRegistryExists(ScopeRegistry registry)
        {
            List<ScopeRegistry> registries;
            try
            {
                registries = new List<ScopeRegistry>();
                foreach (var dictionary in (List<object>)((Dictionary<string, object>) ManifestDictionary)[MANIFEST_KEY_REGISTRIES])
                {
                    registries.Add(new ScopeRegistry((Dictionary<string, object>) dictionary));
                }
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            return registries.Contains(registry);
        }

        /// <summary>
        /// Searches for specific dependency
        /// Name and version must match
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <returns>True if found, false otherwise</returns>
        public bool CheckIfDependencyExists(string name, string version)
        {
            Dictionary<string, object> dependencies;
            try
            {
                dependencies = (Dictionary<string, object>) ((Dictionary<string, object>) ManifestDictionary)[MANIFEST_KEY_DEPENDENCIES];
            }
            catch (Exception)
            {
                return false;
            }
            
            return dependencies.Keys.Contains(name) && (string)dependencies[name] == version;
        }
    }

    /// <summary>
    /// Representation for "scopeRegistries" entries 
    /// </summary>
    public class ScopeRegistry
    {
        public static readonly string KEY_NAME = "name";
        public static readonly string KEY_URL = "url";
        public static readonly string KEY_SCOPES = "scopes";

        public string Name;
        public string Url;
        public HashSet<string> Scopes ;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="scopes"></param>
        public ScopeRegistry(string name, string url, HashSet<string> scopes)
        {
            this.Name = name;
            this.Url = url;
            this.Scopes = scopes;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dictionary">To fill this object. Must contain <see cref="KEY_NAME">name</see>,
        /// <see cref="KEY_URL">url</see> and <see cref="KEY_SCOPES">scopes</see>
        /// </param>
        public ScopeRegistry(Dictionary<string, object> dictionary)
        {
            Name = (string) dictionary[KEY_NAME];
            Url = (string) dictionary[KEY_URL];
            var scopes = (List<object>) dictionary[KEY_SCOPES];
            Scopes = new HashSet<string>();
            foreach (var scope in scopes)
            {
                Scopes.Add((string) scope);
            }
        }

        /// <summary>
        /// Method for matching entries
        /// name matching is not necessary  
        /// </summary>
        /// <param name="obj">To compare with</param>
        /// <returns>True if url and scopes are match</returns>
        public override bool Equals(object obj)
        {
            var other = obj as ScopeRegistry;
            return other != null &&
                   Url == other.Url &&
                   Scopes != null && other.Scopes != null &&
                   (new HashSet<string>(Scopes)).SetEquals(other.Scopes);

        }

        /// <summary>
        /// Creates dictionary from this object
        /// </summary>
        /// <returns>Dictionary of this object</returns>
        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string,object> result = new Dictionary<string, object>();
            result.Add(KEY_NAME,Name);
            result.Add(KEY_URL,Url);
            result.Add(KEY_SCOPES,Scopes.ToList());
            return result;
        }
    }

}