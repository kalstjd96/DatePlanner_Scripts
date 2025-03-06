/**
 * @author minsung.kim@naviworks.com
 * @brief
 * @version 0.1
 * @date 2023-07-18 09:50:15Z
 *
 * @copyright Copyright 2023 Naviworks, Co., LTD. All rights reserved.
 *
 */
using Newtonsoft.Json;

namespace Utility.RestApi
{
    using static Defines;
    public class DefaultResponseTransfer : IResponseTransfer
    {
        #region Public Fields
        public string Body => body;
        public long ResponseCodeRaw => responseCodeRaw;
        public ResponseCode ResponseCode => responseCode;
        
        #endregion

        #region Non-Public Fields
        private string body;
        private long responseCodeRaw;
        private ResponseCode responseCode;
        
        #endregion

        #region Public Fields
        public DefaultResponseTransfer(string body, long responseCodeRaw)
        {
            this.body = body;
            this.responseCodeRaw = responseCodeRaw;
            this.responseCode = responseCodeRaw.ToResponseCode();
        }

        public T ConvertTo<T>() 
        { 
            if (string.IsNullOrEmpty(body))
                return default;

            return JsonConvert.DeserializeObject<T>(body);
        }
        #endregion
    }
}
