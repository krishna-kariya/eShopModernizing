using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace eShopLegacy.Utilities
{
    public class Serializing
    {
        public static Stream SerializeBinary(object input)
        {
            var stream = new MemoryStream();
            var json = JsonSerializer.Serialize(input);
            var writer = new StreamWriter(stream);
            writer.Write(json);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public static object DeserializeBinary(Stream stream)
        {
            var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            var result = JsonSerializer.Deserialize<object>(json);
            return result;
        }
    }
}