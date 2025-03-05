/**
 * @author minsung.kim@naviworks.com
 * @brief
 * @version 0.1
 * @date 2023-07-18 02:02:16Z
 *
 * @copyright Copyright 2023 Naviworks, Co., LTD. All rights reserved.
 *
 */

namespace Utility.RestApi.NLogger
{
    public interface INLoggerStrategy 
    {

        public void SendLogPost(string path);
        public void SendLog(string type, string path);
    }
}
