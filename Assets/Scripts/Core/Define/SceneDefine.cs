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
        { SceneName.Login, "1_Login" },
        { SceneName.Main, "2_Main" },
        { SceneName.Detail, "3_Detail" },
        { SceneName.Settings, "4_Settings" }
    };
    }
}
