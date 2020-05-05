using System.Text;
using UnityEngine;

namespace StansAssets.Foundation
{
    /// <summary>
    /// IdFactory is the static class with the helper methods to generate unique Ids.
    /// </summary>
    public static class IdFactory
    {
        const string k_PlayerPrefsFactoryKey = "IdFactory.Key";

        /// <summary>
        /// Generates unique <see cref="PlayerPrefs"/> based incremental Id.
        /// <see cref="PlayerPrefs"/> is used to store previous id.
        /// </summary>
        public static int NextId
        {
            get
            {
                var id = 1;
                if (PlayerPrefs.HasKey(k_PlayerPrefsFactoryKey))
                {
                    id = PlayerPrefs.GetInt(k_PlayerPrefsFactoryKey);
                    id++;
                }

                PlayerPrefs.SetInt(k_PlayerPrefsFactoryKey, id);
                return id;
            }
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        public static string RandomString
        {
            get
            {
                const string chars = "0123456789abcdefghijklmnopqrstuvwxABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var builder = new StringBuilder();
                for (var i = 0; i < 20; i++)
                {
                    var a = Random.Range(0, chars.Length);
                    builder.Append(chars[a]);
                }
                return builder.ToString();
            }
        }
    }
}
