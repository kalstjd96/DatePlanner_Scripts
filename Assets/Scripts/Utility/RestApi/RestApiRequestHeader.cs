/**
* @author minsung.kim@naviworks.com
* @brief
* @version 0.1
* @date 2023-07-18 01:15:36Z
*
* @copyright Copyright 2023 Naviworks, Co., LTD. All rights reserved.
*
*/
namespace Utility.RestApi
{
    using static Defines;
    public class RestApiRequestHeader
    {
        public string Authorization { get => authorization; set => authorization = value; }
        public string CacheControl { get => cacheControl; set => cacheControl = value; }
        public Header.ContentType ContentType { get => contentType; set => contentType = value; }
        public string UserAgent { get => userAgent; set => userAgent = value; }
        public string Connection { get => connection; set => connection = value; }

        #region Non-Public Fields
        private string authorization;
        private string cacheControl;
        private Header.ContentType contentType;
        private string userAgent;
        private string connection;
        private const string BearerTokenPrefix = "Bearer ";
        #endregion

        #region Public Methods
        public RestApiRequestHeader SetAuthorization(string authorization)
        {
            this.authorization = BearerTokenPrefix + authorization;
            return this;
        }

        public RestApiRequestHeader SetCacheControl(string cacheControl)
        {
            this.cacheControl = cacheControl;
            return this;
        }

        public RestApiRequestHeader SetContentType(Header.ContentType contentType)
        {
            this.contentType = contentType;
            return this;
        }

        public RestApiRequestHeader SetUserAgent(string userAgent)
        {
            this.userAgent = userAgent;
            return this;
        }

        public RestApiRequestHeader SetConnection(string connection)
        {
            this.connection = connection;
            return this;
        }
        #endregion
    }
}
