using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GuiXml
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> typeNames = new List<string>();
            bool types = false;
            int pathEnd = args.Length;
            
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                
                if (types)
                {
                    typeNames.Add(arg);
                    continue;
                }
                
                if (arg == "..")
                {
                    pathEnd = i;
                    types = true;
                    continue;
                }
            }
            
            if (pathEnd == 0)
            {
                Console.Error.WriteLine($"Missing path argument");
                return;
            }
            
            foreach (string path in args.AsSpan(0, pathEnd))
            {
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine($"{path} is not a file");
                    continue;
                }
                
                Console.WriteLine($"Starting {path}");
                FileStream input = new FileStream(path, FileMode.Open);
                FileStream output = new FileStream(path + ".cs", FileMode.Create);
                RunFile(input, output, Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path), typeNames);
                Console.WriteLine($"Finished");
            }
        }
        
        static void RunFile(Stream stream, Stream output, string dir, string name, List<string> typeNames)
        {
            string csproj = FindCSPROJ(dir);
            if (csproj == null)
            {
                Console.Error.WriteLine("Could not find parent csproj");
                return;
            }
            
            Assembly a = LoadAssembly(csproj);
            if (a == null) { return; }
            
            string rootspace = a.DefinedTypes.FindType("Program").Namespace;
            
            try
            {
                Xml xml = new Xml(a, typeNames);
                Type[] insts = xml.EventTypes.Where(t => !t.IsAbstract || !t.IsSealed).ToArray();
                string args = GenArgs(insts);
                
                CSWriter csw = new CSWriter(output);
                csw.WriteLine("using System");
                csw.WriteLine("using Zene.Structs");
                csw.WriteLine("using Zene.Graphics");
                csw.WriteLine("using Zene.Windowing");
                csw.WriteLine("using Zene.GUI");
                csw.WriteLine();
                // TODO: use folders for subspaces
                csw.WriteLine($"namespace {rootspace}");
                csw.OpenContext();
                csw.WriteLine($"public static class {name}");
                csw.OpenContext();
                csw.WriteLine($"public static void LoadGUI(ElementList el{args})");
                csw.OpenContext();
                csw.WriteLine("RootElement root = el.Source");
                csw.WriteLine();
                
                xml.TranscribeXml(stream, csw);
                
                csw.CloseContext();
                csw.CloseContext();
                csw.CloseContext();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return;
            }
        }
        
        static string GenArgs(Type[] types)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Type t in types)
            {
                sb.Append($", {t.Name} {t.Name.ToLower()}");
            }
            return sb.ToString();
        }
        
        static string FindCSPROJ(string dir)
        {
            while (dir != null)
            {
                foreach (string path in Directory.EnumerateFiles(dir))
                {
                    if (Path.GetExtension(path) != ".csproj") { continue; }
                    
                    return path;
                }
                
                dir = Directory.GetParent(dir).Parent.FullName;
            }
            
            return null;
        }
        static Assembly LoadAssembly(string csproj)
        {
            string name = Path.GetFileNameWithoutExtension(csproj);
            string dir = Path.GetDirectoryName(csproj);
            string dll = Path.Combine(dir, "bin", "Debug", "net8.0", $"{name}.dll");
            if (!File.Exists(dll))
            {
                Console.Error.WriteLine("Project has not been compiled or has an invalid bin structure.");
                return null;
            }
            
            try
            {
                return Assembly.LoadFrom(dll);
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Failed to load project assembly.");
                return null;
            }
        }
    }
}
