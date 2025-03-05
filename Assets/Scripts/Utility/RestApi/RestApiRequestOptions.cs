/**
 * @author minsung.kim@naviworks.com
 * @brief
 * @version 0.1
 * @date 2023-07-18 09:20:47Z
 *
 * @copyright Copyright 2023 Naviworks, Co., LTD. All rights reserved.
 *
 */
namespace Utility.RestApi
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Drawing;
    using System.Text;
    using static Defines;
    public class RestApiRequestOptions
    {
        #region Public Fields
        public string Path => path;
        public string BaseUrl { get; }
        public JSONObject Body => body;
        public RequestMethod Method => method;
        public RestApiRequestHeader Header => header;
        public bool IsMock { get; }

        #endregion

        #region Non-Public Fields
        private string path = string.Empty;
        private JSONObject body = null;
        private RequestMethod method = RequestMethod.Undefined;
        private RestApiRequestHeader header = null;
        #endregion

        #region Public Fields
        public RestApiRequestOptions(string path, RequestMethod method) : this(path, method, false) { }

        /// <summary>
        /// ! 주의 !<br/>
        /// mock server는 비용이 청구됩니다. 기능 완료 후 테스트가 아닐 경우 사용을 엄금합니다.
        /// <see href="https://naviworks-postman.postman.co/billing/add-ons/overview"/>를 확인하고 사용 바랍니다.
        /// 사용량이 8000을 넘어가면 관리자(<see href="admin.postman@naviworks.com"/>)에게 문의바랍니다.
        /// </summary>
        /// <param name="path">api path</param>
        /// <param name="method">api 방식</param>
        /// <param name="isMock">mock 인지 여부</param>
        public RestApiRequestOptions(string path, RequestMethod method, bool isMock)
        {
            this.path = path;
            this.method = method;
            IsMock = isMock;
        }        

        public RestApiRequestOptions SetBody(JSONObject body)
        {
            this.body = body;
            return this;
        }

        public RestApiRequestOptions SetHeader(RestApiRequestHeader header)
        {
            this.header = header;
            return this;
        }
        #endregion
    }
}