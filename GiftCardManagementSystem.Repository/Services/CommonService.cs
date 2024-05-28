using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GiftCardManagementSystem.Repository.Services
{
    public static class CommonService
    {
        public static T ToObject<T>(this string jsonStr)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<T>(jsonStr,
                    new JsonSerializerSettings { DateParseHandling = DateParseHandling.DateTimeOffset });
                return result;
            }
            catch
            {
                return (T)Convert.ChangeType(jsonStr, typeof(T));
            }
        }

        public static object? ToObject(this string? jsonStr)
        {
            return ToObject<object>(jsonStr);
        }

        public static JObject ToJObject(this object? obj)
        {
            return JObject.FromObject(obj!);
        }

        public static string? ToJson<T>(this T? obj, bool format = false)
        {
            if (obj == null) return string.Empty;
            string? result;
            if (obj is string)
            {
                result = obj.ToString();
                goto Result;
            }

            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.sssZ" };
            result = format
                ? JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, settings)
                : JsonConvert.SerializeObject(obj, settings);
        Result:
            return result;
        }
    }
}
