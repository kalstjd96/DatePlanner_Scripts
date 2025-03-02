using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DateApp.Platform.Define
{
    public static class SceneDefine
    {
        public enum SceneName
        {
            Intro,
            Main,
            Login,
            Detail,
            Settings
        }

        public static readonly Dictionary<SceneName, string> SceneNames = new Dictionary<SceneName, string>
    {
        { SceneName.Intro, "0_Intro" },
        { SceneName.Main, "1_Main" },
        { SceneName.Login, "Login" },
        { SceneName.Detail, "Detail" },
        { SceneName.Settings, "3_Settings" }
    };
    }
}
