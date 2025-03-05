/**
 * @author minsung.kim@naviworks.com
 * @brief
 * @version 0.1
 * @date 2023-10-14 12:22:18Z
 *
 * @copyright Copyright 2023 Naviworks, Co., LTD. All rights reserved.
 *
 */

using UnityEngine;
namespace Utility.RestApi.NLogger
{
    using static NLoggerDefines;
    public class AndroidNLoggerStrategy : INLoggerStrategy
    {
        #region Non-Public Fields
        private const string NLoggerPluginUrl = "com.example.plugin.NLoggerPlugin";
        private readonly AndroidJavaObject nLoggerPluginInstance;
        #endregion

        #region Public Methods
        public AndroidNLoggerStrategy()
        {
            var nLoggerPlugin = new AndroidJavaClass(NLoggerPluginUrl);
            nLoggerPluginInstance = nLoggerPlugin.CallStatic<AndroidJavaObject>("instance");
        }

        public void SendLogPost(string path) => SendLog(MethodStringTypePost, path);

        public void SendLog(string type, string path)
        {
            var message = $"type={type}, uri={path}";
            nLoggerPluginInstance.Call(MethodStringSendLog, message);
        }
        #endregion
    }
}