using Newtonsoft.Json;
using System.Collections.Generic;
using Utility;

namespace Google
{
    public class GoogleSTTResponseDto
    {
        [JsonProperty("audioContent")]
        public string AudioContent { get; set; }

        [JsonProperty("results")]
        public ResultDto[] Results { get; set; }
    }

    public class ResultDto
    {
        [JsonProperty("alternatives")]
        public AlternativeDto[] Alternatives { get; set; }
    }

    public class AlternativeDto
    {
        [JsonProperty("transcript")]
        public string Transcript { get; set; }
    }
}