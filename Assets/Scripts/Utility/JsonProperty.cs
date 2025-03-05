using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public enum JsonPropertyValueType
    {
        Undefined = 0,
        Boolean = 1,
        Int = 2,
        Long = 3,
        Float = 4,
        String = 5,
        Vector3 = 6,
        Color = 7,
    }

    public class JsonProperty : KeyValueProperty, IJsonConvertable
    {
        public JsonPropertyValueType fixedJsonType = JsonPropertyValueType.Undefined;



        public void FromJson(JSONObject _json)
        {
            key = _json.GetField("key")?.str ?? string.Empty;
            if (string.IsNullOrEmpty(key))
            {
                value = null;
                return;
            }

            fixedJsonType = (JsonPropertyValueType)(_json.GetField("type")?.i ?? 0);
            JSONObject valueJson = _json.GetField("value");
            if (valueJson == null)
            {
                value = null;
                return;
            }

            switch (fixedJsonType)
            {
                case JsonPropertyValueType.Boolean:
                    value = valueJson.b;
                    break;

                case JsonPropertyValueType.Int:
                    value = (int)valueJson.i;
                    break;

                case JsonPropertyValueType.Long:
                    value = valueJson.i;
                    break;

                case JsonPropertyValueType.Float:
                    value = valueJson.f;
                    break;

                case JsonPropertyValueType.String:
                    value = valueJson.str;
                    break;

                case JsonPropertyValueType.Vector3:
                    value = JSONTemplates.ToVector3(valueJson);
                    break;

                case JsonPropertyValueType.Color:
                    value = JSONTemplates.ToColor(valueJson);
                    break;

                case JsonPropertyValueType.Undefined:
                default:
                    value = null;
                    break;
            }
        }

        public JSONObject ToJson()
        {
            JSONObject resJson = new JSONObject();

            resJson.AddField("key", key ?? string.Empty);
            resJson.AddField("type", (int)fixedJsonType);
            JSONTemplates.AddFieldObject(resJson, "value", value);

            return resJson;
        }
    }
}