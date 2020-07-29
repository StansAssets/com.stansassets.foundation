using System;
using System.Text;
using System.Text.RegularExpressions;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// Representation of the <see cref="Dependency"/> semantic version.
    /// </summary>
    public class SemanticVersion
    {
        // Regular Expression to check a Semantic Version string. With numbered capture groups
        // (so cg1 = major, cg2 = minor, cg3 = patch, cg4 = prerelease and cg5 = buildmetadata)
        // that is compatible with ECMA Script (JavaScript),
        // PCRE (Perl Compatible Regular Expressions, i.e. Perl, PHP and R), Python and Go.
        // See: https://regex101.com/r/vkijKf/1/
        static readonly Regex s_Matcher = new Regex(@"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$");

        /// <summary>
        /// The major version number.
        /// </summary>
        public int Major { get; }

        /// <summary>
        /// The minor version number.
        /// </summary>
        public int Minor { get; }

        /// <summary>
        /// The patch version number.
        /// </summary>
        public int Patch { get; }

        /// <summary>
        /// True if the <see cref="SemanticVersion"/> has pre-release data string set; otherwise, false.
        /// </summary>
        public bool HasPreRelease { get; }

        /// <summary>
        /// The pre-release data string.
        /// </summary>
        public string PreRelease { get; }

        /// <summary>
        /// True if the <see cref="SemanticVersion"/> has build metadata string set; otherwise, false.
        /// </summary>
        public bool HasBuildMetadata { get; }

        /// <summary>
        /// The build metadata string.
        /// </summary>
        public string BuildMetadata { get; }

        string m_StringRepresentation;

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticVersion"/> class with provided data.
        /// </summary>
        /// <param name="versionString">String which contains <see cref="SemanticVersion"/> data.</param>
        /// <exception cref="ArgumentException">Thrown when versionString has incorrect format</exception>
        public SemanticVersion(string versionString)
        {
            if (ValidateVersionFormat(versionString, out var regexMatch))
            {
                Major = Int32.Parse(regexMatch.Groups[1].Value);
                Minor = Int32.Parse(regexMatch.Groups[2].Value);
                Patch = Int32.Parse(regexMatch.Groups[3].Value);

                PreRelease = regexMatch.Groups[4].Value;
                HasPreRelease = !string.IsNullOrEmpty(PreRelease);

                BuildMetadata = regexMatch.Groups[5].Value;
                HasBuildMetadata = !string.IsNullOrEmpty(BuildMetadata);
            }
            else
                throw new ArgumentException("Version string has incorrect format");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticVersion"/> class with provided properties.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        /// <param name="patch">The patch version number.</param>
        /// <param name="preRelease">The pre-release data string.</param>
        /// <param name="buildMetadata">The build metadata string.</param>
        public SemanticVersion(int major, int minor, int patch, string preRelease, string buildMetadata)
        {
            Major = major;
            Minor = minor;
            Patch = patch;

            PreRelease = preRelease;
            HasPreRelease = !string.IsNullOrEmpty(PreRelease);

            BuildMetadata = buildMetadata;
            HasBuildMetadata = !string.IsNullOrEmpty(BuildMetadata);
        }

        static bool ValidateVersionFormat(string versionString, out Match regexMatch)
        {
            regexMatch = s_Matcher.Match(versionString);
            return regexMatch.Success;
        }

        /// <summary>
        /// Tries to create a new instance of the <see cref="SemanticVersion"/> class with provided data.
        /// </summary>
        /// <param name="versionString">String which contains <see cref="SemanticVersion"/> data.</param>
        /// <param name="semanticVersion">When this method returns, contains the <see cref="SemanticVersion"/> object with provided data,
        /// if versionString has correct format; otherwise, null. This parameter is passed uninitialized.</param>
        /// <returns>'true' if the <see cref="SemanticVersion"/> has been successfully created; otherwise, 'false'.</returns>
        public static bool TryCreateSemanticVersion(string versionString, out SemanticVersion semanticVersion)
        {
            semanticVersion = null;
            if (ValidateVersionFormat(versionString, out var regexMatch))
            {
                var major = Int32.Parse(regexMatch.Groups[1].Value);
                var minor = Int32.Parse(regexMatch.Groups[2].Value);
                var patch = Int32.Parse(regexMatch.Groups[3].Value);
                var preRelease = regexMatch.Groups[4].Value;
                var buildMetadata = regexMatch.Groups[5].Value;

                semanticVersion = new SemanticVersion(major, minor, patch, preRelease, buildMetadata);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Compares two <see cref="SemanticVersion"/> objects. Major, Minor and Patch version numbers will be compared one by one respectively.
        /// </summary>
        /// <param name="semanticVersion"><see cref="SemanticVersion"/> object which you want to compare.</param>
        /// <param name="other"><see cref="SemanticVersion"/> object which you want to compare to.</param>
        public static bool operator >(SemanticVersion semanticVersion, SemanticVersion other)
        {
            if (semanticVersion.Major > other.Major)
                return true;
            if (semanticVersion.Minor > other.Minor)
                return true;
            if (semanticVersion.Patch > other.Patch)
                return true;

            return false;
        }

        /// <summary>
        /// Compares two <see cref="SemanticVersion"/> objects. Major, Minor and Patch version numbers will be compared one by one respectively.
        /// </summary>
        /// <param name="semanticVersion"><see cref="SemanticVersion"/> object which you want to compare.</param>
        /// <param name="other"><see cref="SemanticVersion"/> object which you want to compare to.</param>
        public static bool operator <(SemanticVersion semanticVersion, SemanticVersion other)
        {
            return !(semanticVersion > other);
        }

        /// <summary>
        /// Generates a hash of this object data.
        /// </summary>
        /// <returns>Hash of this object.</returns>
        public override int GetHashCode() {
            int hash = 0;
            hash ^= Major;
            hash ^= Minor;
            hash ^= Patch;
            if (HasPreRelease)
                hash ^= PreRelease.GetHashCode();
            if (HasBuildMetadata)
                hash ^= BuildMetadata.GetHashCode();

            return hash;
        }

        /// <summary>
        /// Determines whether two <see cref="SemanticVersion"/> instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current <see cref="SemanticVersion"/>.</param>
        /// <returns>'true' if the specified object is equal to the current <see cref="SemanticVersion"/>; otherwise, 'false'.</returns>
        public override bool Equals(object obj)
        {
            if (obj is SemanticVersion other)
            {
                bool sameVersions = Major == other.Major &&
                    Minor == other.Minor &&
                    Patch == other.Patch;
                if (sameVersions && HasPreRelease && other.HasPreRelease)
                {
                    if (PreRelease.Equals(other.PreRelease) && HasBuildMetadata && other.HasBuildMetadata)
                    {
                        return BuildMetadata.Equals(other.BuildMetadata);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="SemanticVersion"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="SemanticVersion"/>.</returns>
        public override string ToString()
        {
            if (m_StringRepresentation == null)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append($"{Major}.");
                builder.Append($"{Minor}.");
                builder.Append(Patch);

                if (HasPreRelease)
                    builder.Append($"-{PreRelease}");

                if (HasBuildMetadata)
                    builder.Append($"+{BuildMetadata}");

                m_StringRepresentation = builder.ToString();
            }

            return m_StringRepresentation;
        }
    }
}
