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
            var dtos = JsonConvert.DeserializeObject<IEnumerable<Shape.ShapeDTO>>(content);

            // Some fixes here
            return dtos.Select(dto =>
            {
                if(loadedAssemblies.ContainsKey(dto.shapeType))
                {
                    var type = loadedAssemblies[dto.shapeType].GetType(dto.shapeType);
                    return Activator.CreateInstance(type, dto) as Shape;
                } else
                {
                    return null;
                }
            });
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

        public void CacheDll(string path)
        {
            string filename = Path.GetFileName(path);
            var targetPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        
            targetPath = Path.Combine(targetPath, "MalovaniQQ");
            
            if(!Directory.Exists(targetPath))
                Directory.CreateDirectory(targetPath);

            targetPath = Path.Combine(targetPath, filename);

            File.Copy(path, targetPath, true);
            
            Debug.WriteLine(targetPath);
        }

        public List<Assembly> GetAllCachedDlls()
        {
            var targetPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            targetPath = Path.Combine(targetPath, "MalovaniQQ");
            
            if(!Directory.Exists(targetPath))
                return new List<Assembly>();

            var dlls = Directory.GetFiles(targetPath, "*.dll");
            List<Assembly> result = new List<Assembly>();
            foreach (var dll in dlls)
            {
                var ass = LoadAssemblyFromFile(dll);
                if(ass != null)
                {
                    result.Add(ass);
                }
            }
            return result;
        }

        public void AddAssembly(Type t, Assembly ass)
        {
            loadedAssemblies.Add(t.FullName, ass);
        }
    }
}
