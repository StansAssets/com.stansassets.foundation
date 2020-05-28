using System.Collections.Generic;
using System.IO;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// Representation of Manifest JSON file.
    /// Can be used for adding dependencies, scopeRegistries, etc to .json file
    /// </summary>
    public class Manifest
    {
        const string k_ProjectManifestPath = "Packages/manifest.json";
        const string k_DependenciesKey = "dependencies";
        const string k_ScopedRegistriesKey = "scopedRegistries";

        /// <summary>
        /// Path to manifest file.
        /// </summary>
        public string Path { get; }

        readonly Dictionary<string, ScopeRegistry> m_ScopeRegistries;
        readonly Dictionary<string, Dependency> m_Dependencies;

        Dictionary<string, object> m_RawContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="Manifest"/> class.
        /// </summary>
        /// <param name="pathToFile">Path to manifest file.</param>
        public Manifest(string pathToFile = k_ProjectManifestPath)
        {
            Path = pathToFile;
            m_ScopeRegistries = new Dictionary<string, ScopeRegistry>();
            m_Dependencies = new Dictionary<string,Dependency>();
        }

        /// <summary>
        /// Read the Manifest file and deserialize its content from JSON.
        /// </summary>
        public void Fetch()
        {
            var manifestText = File.ReadAllText(Path);
            m_RawContent = (Dictionary<string, object>)Json.Deserialize(manifestText);

            if (m_RawContent.TryGetValue(k_ScopedRegistriesKey, out var registriesBlob))
            {
                if (registriesBlob is List<object> registries)
                {
                    foreach (var registry in registries)
                    {
                        var registryDict = (Dictionary<string, object>)registry;
                        var scopeRegistry = new ScopeRegistry(registryDict);
                        m_ScopeRegistries.Add(scopeRegistry.Url, scopeRegistry);
                    }
                }
            }

            if (m_RawContent.TryGetValue(k_DependenciesKey, out var dependenciesBlob))
            {
                if (dependenciesBlob is Dictionary<string, object> dependencies)
                {
                    foreach (var dependencyData in dependencies)
                    {
                        var dependency = new Dependency(dependencyData.Key, dependencyData.Value.ToString());
                        m_Dependencies.Add(dependency.Name, dependency);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="Dependency"/> associated with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="Dependency"/> to get.</param>
        /// <param name="dependency">When this method returns, contains the <see cref="Dependency"/> associated with the specified name,
        /// if the name is found; otherwise, null. This parameter is passed uninitialized.</param>
        /// <returns>true if the <see cref="Manifest"/> contains a <see cref="Dependency"/> with the specified name; otherwise, false.</returns>
        public bool TryGetDependency(string name, out Dependency dependency)
        {
            return m_Dependencies.TryGetValue(name, out dependency);
        }

        /// <summary>
        /// Gets the <see cref="ScopeRegistry"/> associated with the specified url.
        /// </summary>
        /// <param name="url">The url of the <see cref="ScopeRegistry"/> to get.</param>
        /// <param name="registry">When this method returns, contains the <see cref="ScopeRegistry"/> associated with the specified url,
        /// if the url is found; otherwise, null. This parameter is passed uninitialized.</param>
        /// <returns>true if the <see cref="Manifest"/> contains a <see cref="ScopeRegistry"/> with the specified url; otherwise, false.</returns>
        public bool TryGetScopeRegistry(string url, out ScopeRegistry registry)
        {
            return m_ScopeRegistries.TryGetValue(url, out registry);
        }

        /// <summary>
        /// Returns dependencies of the manifest
        /// </summary>
        /// <returns>Dependencies of the manifest</returns>
        public IEnumerable<Dependency> GetDependencies()
        {
            return m_Dependencies.Values;
        }

        /// <summary>
        /// Returns scope registries of the manifest
        /// </summary>
        /// <returns>Scope registries of the manifest</returns>
        public IEnumerable<ScopeRegistry> GetScopeRegistries()
        {
            return m_ScopeRegistries.Values;
        }

        /// <summary>
        /// Returns dependency by a provided name.
        /// </summary>
        /// <param name="name">Name of the dependency.</param>
        /// <returns>Dependency with given name.</returns>
        public Dependency GetDependency(string name)
        {
            return m_Dependencies[name];
        }

        /// <summary>
        /// Returns scope registry by a provided url.
        /// </summary>
        /// <param name="url">Scope registry url.</param>
        /// <returns>Scope registry with the given url.</returns>
        public ScopeRegistry GetScopeRegistry(string url)
        {
            return m_ScopeRegistries[url];
        }

        /// <summary>
        /// Adds scope registry.
        /// </summary>
        /// <param name="registry">An entry to add.</param>
        public void AddScopeRegistry(ScopeRegistry registry)
        {
            if (!IsRegistryExists(registry.Url))
            {
                m_ScopeRegistries.Add(registry.Url, registry);
            }
        }

        /// <summary>
        /// Adds dependency.
        /// </summary>
        /// <param name="name">Dependency name.</param>
        /// <param name="version">Dependency version.</param>
        public void AddDependency(string name, string version)
        {
            if (!IsDependencyExists(name))
            {
                var dependency = new Dependency(name, version);
                m_Dependencies.Add(dependency.Name, dependency);
            }
        }

        /// <summary>
        /// Writes changes back to the manifest file.
        /// </summary>
        public void ApplyChanges()
        {
            var registries = new List<object>();
            foreach (var registry in m_ScopeRegistries.Values)
            {
                registries.Add(registry.ToDictionary());
            }
            m_RawContent[k_ScopedRegistriesKey] = registries;

            // Remove 'scopedRegistries' key from raw content if we have zero scope registries.
            // Because we don't need an empty 'scopedRegistries' key in the manifest
            if (registries.Count == 0)
                m_RawContent.Remove(k_ScopedRegistriesKey);

            Dictionary<string,object> dependencies = new Dictionary<string, object>();
            foreach (var dependency in m_Dependencies.Values)
            {
                dependencies.Add(dependency.Name, dependency.Version);
            }
            m_RawContent[k_DependenciesKey] = dependencies;

            string manifestText = Json.Serialize(m_RawContent, true);
            File.WriteAllText(Path,manifestText);
        }

        /// <summary>
        /// Searches for ScopeRegistry with the provided Url.
        /// </summary>
        /// <param name="url">ScopeRegistry url to search for.</param>
        /// <returns>`true` if scoped registry found, `false` otherwise.</returns>
        public bool IsRegistryExists(string url)
        {
            return m_ScopeRegistries.ContainsKey(url);
        }

        /// <summary>
        /// Searches for a specific dependency by the provided name.
        /// </summary>
        /// <param name="name">The dependency name to search for.</param>
        /// <returns>`true` if found, `false` otherwise.</returns>
        public bool IsDependencyExists(string name)
        {
            return m_Dependencies.ContainsKey(name);
        }
    }
}
