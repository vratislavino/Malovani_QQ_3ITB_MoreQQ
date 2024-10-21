using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Malovani_QQ_3ITB_MoreQQ
{
    internal class FileManager
    {
        public void SaveShapes(string path, IEnumerable<Shape> shapes) {
            var content = JsonConvert.SerializeObject(shapes.Select(s => s.GetDTO()));
            File.WriteAllText(path, content);
        }

        public IEnumerable<Shape> LoadShapes(string path) {
            var content = File.ReadAllText(path);
            var dtos = JsonConvert.DeserializeObject<IEnumerable<Shape.ShapeDTO>>(content);
      
            return dtos.Select(dto => {
                var type = Type.GetType(dto.shapeType);
                return Activator.CreateInstance(type, dto) as Shape;
            });
        }
    }
}
