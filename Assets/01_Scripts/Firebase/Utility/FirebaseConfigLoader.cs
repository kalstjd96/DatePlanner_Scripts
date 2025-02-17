using UnityEngine;

namespace Firebase.Utility
{
    public static class FirebaseConfigLoader
    {
        public static string LoadGoogleClientID()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("secureData");
            if (jsonFile == null)
            {
                Debug.LogError("securedata.json not found in Resources folder!");
                return null;
            }

            SecureData config = JsonUtility.FromJson<SecureData>(jsonFile.text);
            if (config == null)
            {
                Debug.LogError("Failed to parse securedata.json!");
                return null;
            }

            return config.googleClientID;
        }
    }
}
