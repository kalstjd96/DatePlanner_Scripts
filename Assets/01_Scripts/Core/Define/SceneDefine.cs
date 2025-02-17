using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DateApp.Platform.Intro
{
    public static class SceneDefine
    {
        public enum SceneName
        {
            Intro,
            Main,
            Login,
            GamePlay,
            Settings
        }

        public static readonly Dictionary<SceneName, string> SceneNames = new Dictionary<SceneName, string>
    {
        { SceneName.Intro, "0_Intro" },
        { SceneName.Main, "1_Main" },
        { SceneName.Login, "Login" },
        { SceneName.GamePlay, "2_GamePlay" },
        { SceneName.Settings, "3_Settings" }
    };
    }
}
