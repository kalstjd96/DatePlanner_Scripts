using Cysharp.Threading.Tasks;
using Utility.RestApi;
using Newtonsoft.Json.Linq;
using Utility;
using UnityEngine;

namespace Google
{
    public class GoogleCloudServiceImpl : IGoogleCloudService
    {
        public async UniTask<string> CallSTT(
            string json,
            string sttAPIUrl,
            string googleCloudAPIKey)
        {
            string requestUri = $"{googleCloudAPIKey}";

            var headers = new RestApiRequestHeader()
                .SetContentType(Defines.Header.ContentType.Application_Json);

            var jsonObject = new JSONObject(json); // 혹은 직접 생성자 사용
            var options = new RestApiRequestOptions(requestUri, Defines.RequestMethod.Post)
                .SetBody(jsonObject)
                .SetHeader(headers);

            RestApiClient client = new RestApiClient(options);
            var transfer = await client.RequestToApiServer();
            var responseText = transfer.ConvertTo<GoogleSTTResponseDto>();

            if (transfer.ResponseCode != Defines.ResponseCode.Ok)
            {
                Debug.LogError(transfer.ResponseCode + responseText.ToString());
            }

            foreach (var result in responseText.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    return alternative.Transcript;
                }
            }

            return null;
        }
    }

}