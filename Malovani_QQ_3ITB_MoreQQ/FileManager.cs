using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Malovani_QQ_3ITB_MoreQQ
{
    internal class FileManager
    {
        Dictionary<string, Assembly> loadedAssemblies = new Dictionary<string, Assembly>();
        public void SaveShapes(string path, IEnumerable<Shape> shapes)
        {
            var content = JsonConvert.SerializeObject(shapes.Select(s => s.GetDTO()));
            File.WriteAllText(path, content);
        }

        public IEnumerable<Shape> LoadShapes(string path)
        {
            var content = File.ReadAllText(path);
            var dtos = JsonConvert.DeserializeObject<IEnumerable<Shape.ShapeDTO>>(content) ?? Enumerable.Empty<Shape.ShapeDTO>();

            return dtos.Select(dto =>
            {
                var type = Type.GetType(dto.shapeType);
                return (Shape?)Activator.CreateInstance(type, dto);
            }).Where(s => s != null)!;
        }

        public Assembly LoadAssemblyFromFile(string path)
        {
            try
            {
                return Assembly.LoadFrom(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void ChacheDll(string path)
        {
            string filename = Path.GetFileName(path);
            var tagretPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            tagretPath = Path.Combine(tagretPath, "MalovaniQQ");
            if (!Directory.Exists(tagretPath))
            {
                Directory.CreateDirectory(tagretPath);
            }

            tagretPath = Path.Combine(tagretPath, filename);
            File.Copy(path, tagretPath, true);
            Debug.WriteLine(tagretPath);
        }

        public List<Assembly> GetAllCachedDlls()
        {
            var targetPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            targetPath = Path.Combine(targetPath, "MalovaniQQ");

            if (!Directory.Exists(targetPath))
            {
                return new List<Assembly>();
            }

            var ddls = Directory.GetFiles(targetPath, "*.dll");
            List<Assembly> result = new List<Assembly>();

            foreach (var ddl in ddls)
            {
                try
                {
                    var ass = LoadAssemblyFromFile(ddl);
                    if (ass != null)
                    {
                        result.Add(ass);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result;
        }

        internal void AddAssembly(Type t, Assembly ass)
        {
            loadedAssemblies.Add(t.FullName, ass);
        }
    }
}
