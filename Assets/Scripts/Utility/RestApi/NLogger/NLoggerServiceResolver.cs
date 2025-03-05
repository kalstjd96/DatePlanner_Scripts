/**
 * @author minsung.kim@naviworks.com
 * @brief
 * @version 0.1
 * @date 2023-10-14 13:23:49Z
 *
 * @copyright Copyright 2023 Naviworks, Co., LTD. All rights reserved.
 *
 */
using UnityEngine;

namespace Utility.RestApi.NLogger
{
    public class NLoggerServiceResolver
    {
        #region Public Fields
        public static NLoggerServiceResolver Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public INLoggerStrategy CurrentNLoggerStrategy => nLoggerStrategy;
        #endregion

        #region Non-Public Fields
        private static NLoggerServiceResolver instance;
        private INLoggerStrategy nLoggerStrategy;
        #endregion

        #region Non-Public Methods
        private NLoggerServiceResolver(RuntimePlatform platform)
        {
            nLoggerStrategy = 
#if UNITY_ANDROID
                new AndroidNLoggerStrategy();
#elif UNITY_IOS
                new IosNLoggerStrategy();
#else 
                null;
#endif
        }

        private NLoggerServiceResolver() : this(Application.platform) 
        {

        }
        #endregion
    }
}