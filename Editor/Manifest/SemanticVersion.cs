using System;
using System.Text;
using System.Text.RegularExpressions;

namespace StansAssets.Foundation.Editor
{
    public class SemanticVersion
    {
        // Regular Expression to check a Semantic Version string. With numbered capture groups
        // (so cg1 = major, cg2 = minor, cg3 = patch, cg4 = prerelease and cg5 = buildmetadata)
        // that is compatible with ECMA Script (JavaScript),
        // PCRE (Perl Compatible Regular Expressions, i.e. Perl, PHP and R), Python and Go.
        // See: https://regex101.com/r/vkijKf/1/
        static readonly Regex s_Matcher = new Regex(@"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$");

        public readonly int Major;
        public readonly int Minor;
        public readonly int Patch;

        public readonly bool HasPreRelease;
        public readonly string PreRelease;

        public readonly bool HasBuildMetadata;
        public readonly string BuildMetadata;

        string m_StringRepresentation;

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

        public static bool TryCreateSemanticVersion(string versionString, out SemanticVersion semanticVersion)
        {
            semanticVersion = null;
            var match = s_Matcher.Match(versionString);
            if (match.Success)
            {
                var major = Int32.Parse(match.Groups[1].Value);
                var minor = Int32.Parse(match.Groups[2].Value);
                var patch = Int32.Parse(match.Groups[3].Value);
                var preRelease = match.Groups[4].Value;
                var buildMetadata = match.Groups[5].Value;

                semanticVersion = new SemanticVersion(major, minor, patch, preRelease, buildMetadata);
                return true;
            }

            return false;
        }

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

        public static bool operator <(SemanticVersion semanticVersion, SemanticVersion other)
        {
            return !(semanticVersion > other);
        }

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
