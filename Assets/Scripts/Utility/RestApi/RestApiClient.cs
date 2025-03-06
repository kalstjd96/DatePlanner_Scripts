/**
 * @author minsung.kim@naviworks.com
 * @brief
 * @version 0.1
 * @date 2023-07-18 01:06:25Z
 *
 * @copyright Copyright 2023 Naviworks, Co., LTD. All rights reserved.
 *
 */
using Cysharp.Threading.Tasks;
using Utility;
using System;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

//Utility.RestApi
namespace Utility.RestApi
{
    using static Defines;
    using NLogger;

    public class RestApiClient
    {
        #region Non-Public Fields
        protected static float TimeoutSecond = 10.0f;
        protected INLoggerStrategy nLoggerStrategy;

        private string path;
        private RequestMethod method;
        private RestApiRequestHeader header;
        private JSONObject requestBody;
        private RestApiRequestOptions options;
        
        private const string ErrorMsg = "<color=red>Error occurred</color>";

        #endregion

        #region Public Methods
        public RestApiClient(RestApiRequestOptions options)
        {
            this.options = options;
            this.path = options.Path;
            this.method = options.Method;
            this.header = options.Header;
            this.requestBody = options.Body;
            this.nLoggerStrategy = NLoggerServiceResolver.Instance.CurrentNLoggerStrategy;
        }

        public async UniTask<IResponseTransfer> RequestToApiServer(ApiType apiType = ApiType.Default, IResponseTransfer transfer = null)
        {
            await CheckNetwork();
            var baseUrl = 
#if DEVELOPMENT_BUILD || UNITY_EDITOR
                options.IsMock ? APIServerSettings.Instance.MockServerUrl : 
#endif
            APIServerSettings.Instance.GetApiBaseUrl(apiType);
            var url = baseUrl + path;
            var cts = new CancellationTokenSource();
            cts.CancelAfterSlim(TimeSpan.FromSeconds(TimeoutSecond));
            var bodyString = requestBody?.ToString() ?? string.Empty;

            using (var request = new UnityWebRequest(url, method.ToUpper()))
            {
                nLoggerStrategy?.SendLogPost(path);

                if (!string.IsNullOrEmpty(bodyString))
                {
                    var bodyRaw = Encoding.UTF8.GetBytes(bodyString);
                    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                }

                SetHeaders(request, header);
                request.downloadHandler = new DownloadHandlerBuffer();
                try
                {
                    var operation = await request.SendWebRequest().WithCancellation(cts.Token);
                    var responseBody = operation.downloadHandler.text;
                    PrintResponse(responseBody);
                    transfer ??= new DefaultResponseTransfer(responseBody, request.responseCode);
                    return transfer;
                }
                catch (OperationCanceledException ex)
                {
                    var timeoutMessage = ex.CancellationToken == cts.Token ? "timeout," : String.Empty;
                    Debug.Log($"{nameof(RestApiClient)} failed:{timeoutMessage}{ex.Message}");
                    transfer ??= new DefaultResponseTransfer(string.Empty, request.responseCode);
                    PrintResponse(ErrorMsg);
                    return transfer;
                }
                catch (Exception e)
                {
                    Debug.Log($"{nameof(RestApiClient)} failed: {e.Message}");
                    transfer ??= new DefaultResponseTransfer(string.Empty, request.responseCode);
                    PrintResponse(ErrorMsg);
                    return transfer;
                }
            }
        }
        #endregion

        #region Non-Public Methods
        private async UniTask CheckNetwork()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
                return;

            await UniTask.WaitUntil(() => Application.internetReachability != NetworkReachability.NotReachable);
        }

        private void SetHeaders(UnityWebRequest request, RestApiRequestHeader header)
        {
            if (header == null)
                return;

            request.SetRequestHeader(Header.Types.ContentType.ToKey(), header.ContentType.ToValue());
            if (!string.IsNullOrEmpty(header.Authorization))
                request.SetRequestHeader(Header.Types.Authorization.ToKey(), header.Authorization);
            if (!string.IsNullOrEmpty(header.CacheControl))
                request.SetRequestHeader(Header.Types.CacheControl.ToKey(), header.CacheControl);
            if (!string.IsNullOrEmpty(header.Connection))
                request.SetRequestHeader(Header.Types.Connection.ToKey(), header.Connection);
        }

        private void PrintResponse(string body)
        {
            if (!APIServerSettings.Instance.IsLogEnabled)
                return;
            StringBuilder sb = new();
            sb.Append($"<color=#7CFC00>REST API CALL</color> path: {path}\n");
            sb.Append(options);
            sb.Append("<color=#7CFC00>Response</color>\n");
            if (body != null)
            {
                sb.Append("  Response Body:\n");
                sb.Append(body);
                sb.Append("\n");
            }
            Debug.Log(sb.ToString());
        }


        #endregion
    }
}
