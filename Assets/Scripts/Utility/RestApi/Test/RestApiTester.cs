/**
 * @author minsung.kim@naviworks.com
 * @brief
 * @version 0.1
 * @date 2023-07-18 03:13:10Z
 *
 * @copyright Copyright 2023 Naviworks, Co., LTD. All rights reserved.
 *
 */

using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility.RestApi.Defines;

namespace Utility.RestApi.Test
{
    public class RestApiTester : MonoBehaviour
    {
        #region Unity Methods
        private async UniTaskVoid Start()
        {
            var body = new JSONObject(new Dictionary<string, string>{
                 {"emlAddr", "hy1"},
                 {"pswdCn", "11"}
            });
            var options = new RestApiRequestOptions("accounts/login", RequestMethod.Post)
                .SetHeader(new () { ContentType =  Header.ContentType.Application_Json })
                .SetBody(body);
                
            var client = new RestApiClient(options);
            var transfer = await client.RequestToApiServer();


            var loginResult = transfer.ConvertTo<LoginResult>();
            Debug.Log(loginResult.user.nickNm);
            Debug.Log(loginResult.user.lastAvtSn);
        }
        #endregion

        [Serializable]
        public class Avatar
        {
            public int hatSn;
            public int socksSn;
            public int bagSn;
            public int skclSn;
            public int shoesSn;
            public int acsrSn;
            public int avtSn;
            public int topSn;
            public int faceSn;
            public int hairSn;
            public int btmSn;
        }

        [Serializable]
        public class User
        {
            public int acumltAtndCnt;
            public int milgAmt;
            public string sttsMsgCn;
            public string prfImgUrlAddr;
            public int rprsAvtSn;
            public string nickNm;
            public string lastAtndDt;
            public int lastAvtSn;
            public string[] authrNm;
        }
        private class LoginResult
        {
            public string errorMsg;
            public string token;
            public Avatar avatar;
            public User user;
        }
    }
}