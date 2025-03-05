
/**
* @author minsung.kim@naviworks.com
* @brief
* @version 0.1
* @date 2023-07-18 01:16:43Z
*
* @copyright Copyright 2023 Naviworks, Co., LTD. All rights reserved.
*
*/
namespace Utility.RestApi
{
    public static class Defines
    {
        public enum RequestMethod
        {
            Undefined,
            Get,
            Post,
            Put,
            Delete,
        }

        public enum ResponseCode
        {
            Undefined = 0,

            /// <summary>
            /// successful responses.<br/>
            /// see <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/200"/>
            /// <list type="bullet">
            ///     <item>
            ///        <term>GET</term>
            ///        <description>The resource has been fetched and transmitted in the message body.</description>
            ///     </item>
            ///     <item>
            ///        <term>HEAD</term>
            ///        <description>The representation headers are included in the response without any message body.</description>
            ///     </item>
            ///     <item>
            ///        <term>PUT or POST</term>
            ///        <description>The resource describing the result of the action is transmitted in the message body.</description>
            ///     </item>
            ///     <item>
            ///        <term>TRACE</term>
            ///        <description>The message body contains the request message as received by the server.</description>
            ///     </item>
            /// </list>
            /// </summary>
            Ok = 200,

            /// <summary>
            /// client error, server cannot or will not process the request due to something that is perceived to be a client error.<br/>
            /// see <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/400"/>
            /// </summary>
            BadRequest = 400,

            /// <summary>
            /// client error, the server understands the request but refuses to authorize it.<br/>
            /// see <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/403"/>
            /// </summary>
            Forbidden = 403,

            /// <summary>
            /// client error, the server cannot find the requested resource.<br/>
            /// see <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/404"/>
            /// </summary>
            NotFound = 404,
        }


        public static class Header
        {
            public enum Types
            {
                Authorization,
                CacheControl,
                ContentType,
                UserAgent,
                Connection,
            }

            public enum ContentType
            {
                Application_JavaArchive,
                Application_EdiX12,
                Application_Edifact,
                Application_Javascript,
                Application_OctetStream,
                Application_Ogg,
                Application_Pdf,
                Application_XhtmlXml,
                Application_XShockwaveFlash,
                Application_Json,
                Application_LdJson,
                Application_Xml,
                Application_Zip,
                Application_XWwwFormUrlencoded,
                Audio_Mpeg,
                Audio_XMsWma,
                Audio_XWav,
                Image_Gif,
                Image_Jpeg,
                Image_Png,
                Image_Tiff,
                Image_XIcon,
                Image_SvgXml,
                Multipart_Mixed,
                Multipart_Alternative,
                Multipart_FormData,
                Text_Css,
                Text_Csv,
                Text_Html,
                Text_Plain,
                Text_Xml,
            }
        }
    }





}