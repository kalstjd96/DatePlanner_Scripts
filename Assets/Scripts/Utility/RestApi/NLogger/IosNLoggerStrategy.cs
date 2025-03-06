/**
 * @author minsung.kim@naviworks.com
 * @brief
 * @version 0.1
 * @date 2023-10-14 12:49:04Z
 *
 * @copyright Copyright 2023 Naviworks, Co., LTD. All rights reserved.
 *
 */
using System.Runtime.InteropServices;

namespace Utility.RestApi.NLogger
{
    using static NLoggerDefines;
    public class IosNLoggerStrategy : INLoggerStrategy
    {
        #region Non-Public Fields
#if UNITY_IOS
        [DllImport("__Internal")]
        private extern static void nloggerConfigure();
#endif

#if UNITY_IOS
        [DllImport("__Internal")]    
        private extern static void nloggerEvent(string message);
#endif
        #endregion

        #region Public Methods
#if UNITY_IOS
        public IosNLoggerStrategy()
        {
            nloggerConfigure();
        }
#endif
        public void SendLog(string type, string path)
        {
#if UNITY_IOS        
            var message = $"type={type}, uri={path}";
            nloggerEvent(message);
#endif
        }

        public void SendLogPost(string path) => SendLog(MethodStringSendLog, path);
        #endregion
    }
}