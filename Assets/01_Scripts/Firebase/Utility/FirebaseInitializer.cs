using System;
using Firebase;
using Firebase.Extensions;
using UnityEngine;

namespace Firebase.Utility
{
    /// <summary>
    /// Firebase SDK √ ±‚»≠
    /// </summary>
    public class FirebaseInitializer
    {
        public static void InitializeFirebase(Action<DependencyStatus> onInitialized)
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                onInitialized?.Invoke(task.Result);
            });
        }
    }

}
