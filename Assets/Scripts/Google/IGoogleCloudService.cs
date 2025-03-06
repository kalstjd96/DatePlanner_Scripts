using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Google
{
    public interface IGoogleCloudService
    {
        UniTask<string> CallSTT(string json, string sttAPIUrl, string googleCloudAPIKey);
    }
}