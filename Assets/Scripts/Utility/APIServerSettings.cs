using System.Collections.Generic;

public class APIServerSettings
{
    private static APIServerSettings _instance;
    public static APIServerSettings Instance => _instance ??= new APIServerSettings();

    // API 엔드포인트 관리
    private readonly Dictionary<ApiType, string> _apiUrls = new()
    {
        { ApiType.Default, "https://api.default.com/" },
        { ApiType.GoogleTTS, "https://texttospeech.googleapis.com/" },
        { ApiType.GoogleSTT, "https://speech.googleapis.com/" },
    };

    public string RootURL => GetApiBaseUrl(ApiType.Default);
    public string MockServerUrl => "https://mockserver.com/";

    public bool IsLogEnabled { get; set; } = true;

    // API 타입에 따라 주소 반환
    public string GetApiBaseUrl(ApiType apiType)
    {
        return _apiUrls.TryGetValue(apiType, out var url) ? url : _apiUrls[ApiType.Default];
    }
}

// API 타입을 관리하는 Enum
public enum ApiType
{
    Default,   // 기본 API 서버
    GoogleTTS, // Google TTS API
    GoogleSTT, // Google STT API
}
