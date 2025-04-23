using System.Text.Json;
using System.Text.Json.Serialization;

namespace TeamWebShop.Extensions.MySessionExtensions
{
    public static class MySessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve, // 
                //ReferenceHandler = ReferenceHandler.IgnoreCycles, 
                WriteIndented = true, // to have proper spaces inside
            };
            string srt = JsonSerializer.Serialize(value, options);
            session.SetString(key, srt); // Writing data to session
        }

        public static T? Get<T>(this ISession session, string key)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                //ReferenceHandler = ReferenceHandler.IgnoreCycles, 
                WriteIndented = true,
            };
            T? item = default;
            string? str = session.GetString(key);
            if(str!=null)
                item = JsonSerializer.Deserialize<T>(str, options);
            return item;          
        }
    }
}
