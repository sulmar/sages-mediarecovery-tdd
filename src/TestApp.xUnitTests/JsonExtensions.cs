using Newtonsoft.Json;

namespace TestApp.xUnitTests;

public static class JsonExtensions
{
    // Metoda rozszerzająca (Extension Method)
    public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);
    public static T FromJson<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
}