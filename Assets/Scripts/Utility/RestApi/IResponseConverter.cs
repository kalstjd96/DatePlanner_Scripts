/**
 * @author minsung.kim@naviworks.com
 * 
 * @brief
 * @version 0.1
 * @date 2023-07-18 09:48:19Z
 *
 * @copyright Copyright 2023 Naviworks, Co., LTD. All rights reserved.
 *
 */
namespace Utility.RestApi
{
    using static Defines;
    public interface IResponseTransfer
    {
        public T ConvertTo<T>();
        public string Body { get; }
        public ResponseCode ResponseCode { get; }
        public long ResponseCodeRaw { get; }
    }
}
