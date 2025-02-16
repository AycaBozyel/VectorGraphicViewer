using Newtonsoft.Json;
using System.IO;
using VectorGraphicViewer.Interface;

namespace VectorGraphicViewer.Util
{
    public static class ShapeReader
    {
        public static List<IShape> LoadShapes(string fileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"JSON File could not find: {filePath}");
            }

            var json = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<IShape>>(json, new JsonSerializerSettings
            {
                Converters = { new ShapeConverter() }
            });
        }
    }
}
