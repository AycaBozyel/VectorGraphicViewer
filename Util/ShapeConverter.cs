using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VectorGraphicViewer.Interface;
using VectorGraphicViewer.Model;

namespace VectorGraphicViewer.Util
{
    public class ShapeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IShape);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            string type = obj["type"]?.ToString();

            if (string.IsNullOrEmpty(type))
            {
                throw new JsonSerializationException("Missing 'type' property in JSON.");
            }

            IShape shape;
            switch (type.ToLower())
            {
                case "line":
                    shape = new LineObject();
                    break;
                case "circle":
                    shape = new CircleObject();
                    break;
                case "triangle":
                    shape = new TriangleObject();
                    break;
                case "rectangle":
                    shape = new RectangleObject();
                    break;
                default:
                    throw new JsonSerializationException($"Unknown shape type: {type}");
            }

            serializer.Populate(obj.CreateReader(), shape);
            return shape;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
