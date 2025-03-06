/**
 * @author minsung.kim@naviworks.com
 * @brief
 * @version 0.1
 * @date 2023-07-19 22:27:02Z
 *
 * @copyright Copyright 2023 Naviworks, Co., LTD. All rights reserved.
 *
 */
using UnityEngine;

namespace Utility.RestApi
{
    using static Defines;
    public static class Utility
    {
        public static string ToUpper(this RequestMethod method) => method switch
        {
            RequestMethod.Get => "GET",
            RequestMethod.Post => "POST",
            RequestMethod.Put => "PUT",
            RequestMethod.Delete => "DELETE",
            _ => string.Empty
        };

        public static string ToKey(this Header.Types headerTypes) => headerTypes switch
        {
            Header.Types.Authorization => "Authorization",
            Header.Types.CacheControl => "Cache-Control",
            Header.Types.ContentType => "Content-Type",
            Header.Types.UserAgent => "User-Agent",
            Header.Types.Connection => "Connection",
            _ => string.Empty
        };

        public static string ToValue(this Header.ContentType contentType) => contentType switch
        {
            Header.ContentType.Application_JavaArchive => "application/java-archive",
            Header.ContentType.Application_EdiX12 => "application/EDI-X12",
            Header.ContentType.Application_Edifact => "application/EDIFACT",
            Header.ContentType.Application_Javascript => "application/javascript",
            Header.ContentType.Application_OctetStream => "application/octet-stream",
            Header.ContentType.Application_Ogg => "application/ogg",
            Header.ContentType.Application_Pdf => "application/pdf",
            Header.ContentType.Application_XhtmlXml => "application/xhtml+xml",
            Header.ContentType.Application_XShockwaveFlash => "application/x-shockwave-flash",
            Header.ContentType.Application_Json => "application/json",
            Header.ContentType.Application_LdJson => "application/ld+json",
            Header.ContentType.Application_Xml => "application/xml",
            Header.ContentType.Application_Zip => "application/zip",
            Header.ContentType.Application_XWwwFormUrlencoded => "application/x-www-form-urlencoded",
            Header.ContentType.Audio_Mpeg => "audio/mpeg",
            Header.ContentType.Audio_XMsWma => "audio/x-ms-wma",
            Header.ContentType.Audio_XWav => "audio/x-wav",
            Header.ContentType.Image_Gif => "image/gif",
            Header.ContentType.Image_Jpeg => "image/jpeg",
            Header.ContentType.Image_Png => "image/png",
            Header.ContentType.Image_Tiff => "image/tiff",
            Header.ContentType.Image_XIcon => "image/x-icon",
            Header.ContentType.Image_SvgXml => "image/svg+xml",
            Header.ContentType.Multipart_Mixed => "multipart/mixed",
            Header.ContentType.Multipart_Alternative => "multipart/alternative",
            Header.ContentType.Multipart_FormData => "multipart/form-data",
            Header.ContentType.Text_Css => "text/css",
            Header.ContentType.Text_Csv => "text/csv",
            Header.ContentType.Text_Html => "text/html",
            Header.ContentType.Text_Plain => "text/plain",
            Header.ContentType.Text_Xml => "text/xml",
            _ => string.Empty
        };

        public static ResponseCode ToResponseCode(this long code) => code switch
        {
            200 => ResponseCode.Ok,
            400 => ResponseCode.BadRequest,
            403 => ResponseCode.Forbidden,
            404 => ResponseCode.NotFound,
            _ => ResponseCode.Undefined
        };
    }
}