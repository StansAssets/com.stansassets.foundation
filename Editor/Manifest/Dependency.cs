using System;
using System.Collections.Generic;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// Representation of the manifest file "dependency" entry.
    /// </summary>
    public class Dependency
    {
        /// <summary>
        /// The <see cref="Dependency"/> name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The <see cref="Dependency"/> version.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// `true` if the <see cref="Dependency"/> has <see cref="SemanticVersion"/>; otherwise, `false`.
        /// </summary>
        public bool HasSemanticVersion { get; private set; }

        /// <summary>
        /// The <see cref="Dependency"/> semantic version.
        /// </summary>
        public SemanticVersion SemanticVersion { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dependency"/> class with provided properties.
        /// </summary>
        /// <param name="fullName"><see cref="Dependency"/> full name which contains name and version (e.g. 'com.company.package@1.0.0').</param>
        public Dependency(string fullName)
        {
            if (TryGetNameAndVersion(fullName, out string name, out string version))
            {
                Name = name;
                Version = version;
                TryAssignSemanticVersion();
            }
            else
                throw new ArgumentException("Dependency fullName has wrong format");
        }

        /// <summary>
        /// Initializes a new instance of the  <see cref="Dependency"/> class with provided properties.
        /// </summary>
        /// <param name="name">Dependency name.</param>
        /// <param name="version">Dependency version.</param>
        public Dependency(string name, string version)
        {
            Name = name;
            Version = version;
            TryAssignSemanticVersion();
        }

        internal static bool TryGetNameAndVersion(string fullName, out string name, out string version)
        {
            name = version = null;
            var dependencyData = fullName.Split('@');
            if (dependencyData.Length == 2)
            {
                name = dependencyData[0];
                version = dependencyData[1];
                return true;
            }
            return false;
        }

        void TryAssignSemanticVersion()
        {
            HasSemanticVersion = SemanticVersion.TryCreateSemanticVersion(Version, out var semanticVersion);
            if (HasSemanticVersion)
                SemanticVersion = semanticVersion;
        }

        /// <summary>
        /// Sets new dependency version.
        /// </summary>
        /// <param name="version">The version to be set for this dependency</param>
        public void SetVersion(string version)
        {
            Version = version;
            TryAssignSemanticVersion();
        }

        /// <summary>
        /// Creates a dictionary from this object.
        /// </summary>
        /// <returns>Dependency object representation as Dictionary&lt;string, object&gt;.</returns>
        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string,object> result = new Dictionary<string, object>();
            result.Add(Name, Version);
            return result;
        }
    }
}
