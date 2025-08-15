using System;
using System.Collections.Generic;
using System.Xml;
using System.Reflection;
using System.Linq;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace GuiXml
{
    public delegate string StringParser(string value);

    public class Xml
    {
        public Xml(Assembly a)
        {
            _types = a.DefinedTypes;
            Type iElement = _types.FirstOrDefault(ti => ti.FullName == "Zene.GUI.IElement").AsType();
            _elementTypes = _types.Where(ti =>
            {
                Type type = ti.AsType();
                return type.IsAssignableTo(iElement);
            });
            
            Type cursor = _types.FirstOrDefault(ti => ti.FullName == "Zene.Windowing.Cursor").AsType();
            Type colour = _types.FirstOrDefault(ti => ti.FullName == "Zene.Structs.Colour").AsType();
            Type colour3 = _types.FirstOrDefault(ti => ti.FullName == "Zene.Structs.Colour3").AsType();
            Type colourF = _types.FirstOrDefault(ti => ti.FullName == "Zene.Structs.ColourF").AsType();
            Type colourF3 = _types.FirstOrDefault(ti => ti.FullName == "Zene.Structs.ColourF3").AsType();
            
            Type vector2 = _types.FirstOrDefault(ti => ti.FullName == "Zene.Structs.Vector2").AsType();
            Type vector2I = _types.FirstOrDefault(ti => ti.FullName == "Zene.Structs.Vector2I").AsType();
            Type vector3 = _types.FirstOrDefault(ti => ti.FullName == "Zene.Structs.Vector3").AsType();
            Type vector3I = _types.FirstOrDefault(ti => ti.FullName == "Zene.Structs.Vector3I").AsType();
            Type vector4 = _types.FirstOrDefault(ti => ti.FullName == "Zene.Structs.Vector4").AsType();
            Type vector4I = _types.FirstOrDefault(ti => ti.FullName == "Zene.Structs.Vector4I").AsType();
            
            // all properties of vector2 are floatv - tells us if using float or double
            bool useFloat = vector2.GetProperties()[0].PropertyType == typeof(float);
            
            AddParser(XmlTypeParser.CursorParser, cursor);

            AddParser(XmlTypeParser.ColourParser, colour);
            AddParser(XmlTypeParser.Colour3Parser, colour3);
            AddParser(XmlTypeParser.ColourFParser, colourF);
            AddParser(XmlTypeParser.ColourF3Parser, colourF3);

            AddParser(XmlTypeParser.Vector2Parser(useFloat), vector2);
            AddParser(XmlTypeParser.Vector2IParser, vector2I);
            AddParser(XmlTypeParser.Vector3Parser(useFloat), vector3);
            AddParser(XmlTypeParser.Vector3IParser, vector3I);
            AddParser(XmlTypeParser.Vector4Parser(useFloat), vector4);
            AddParser(XmlTypeParser.Vector4IParser, vector4I);
            
            _rootType = _types.FirstOrDefault(ti => ti.FullName == "Zene.GUI.RootElement").AsType();
        }

        private readonly IEnumerable<TypeInfo> _types;
        private readonly IEnumerable<TypeInfo> _elementTypes;
        private readonly Dictionary<Type, StringParser> _stringParses = new Dictionary<Type, StringParser>();
        private readonly Dictionary<Type, int> _typeCounts = new Dictionary<Type, int>();

        public void AddParser(StringParser func, Type returnType) => _stringParses.Add(returnType, func);
        private int GetCount(Type t)
        {
            bool exist = _typeCounts.TryGetValue(t, out int v);
            if (!exist)
            {
                _typeCounts.Add(t, 1);
                return 1;
            }
            v++;
            _typeCounts[t] = v;
            return v;
        }
        
        private object _events;
        private Type _eventType;
        private Type _rootType;
        
        public void TranscribeXml(Stream xml, CSWriter output)
        {
            XmlDocument root = new XmlDocument();
            try
            {
                root.Load(xml);
            }
            catch (Exception)
            {
                throw new Exception("Invalid XML syntax");
            }
            
            if (root.ChildNodes.Count == 0)
            {
                throw new Exception($"No elements in xml");
            }

            XmlNodeList xnl = root.ChildNodes[0].ChildNodes;

            if (root.ChildNodes[0].Name.ToLower() == "xml")
            {
                if (root.ChildNodes.Count == 1)
                {
                    throw new Exception($"No elements in xml");
                }

                xnl = root.ChildNodes[1].ChildNodes;
            }

            foreach (XmlNode node in xnl)
            {
                string name = ParseNode(node, new Field("root", _rootType), output);
                // Property node
                if (name == null) { continue; }

                // re.Add(e);
                output.WriteLine($"root.AddChild({name})");
                output.WriteLine();
            }
        }

        private string ParseNode(XmlNode node, Field parent, CSWriter output)
        {
            if (node.Name == "Property")
            {
                ParseProperty(node, parent, output);
                return null;
            }

            return ParseElement(node, output);
        }
        private string ParseElement(XmlNode node, CSWriter output)
        {
            Type t = _elementTypes.FindType(node.Name);
            if (t == null)
            {
                throw new Exception($"Tag name {node.Name} does not exist");
            }

            ConstructorInfo constructor = t.GetConstructor(Array.Empty<Type>());
            if (constructor == null)
            {
                throw new Exception($"Type {node.Name} does not have a parameterless constructor");
            }
            
            // construct element
            int count = GetCount(t);
            string vName = t.Name.ToLower() + count.ToString();
            output.WriteLine($"{t.Name} {vName} = new {t.Name}()");
            
            // IElement element = constructor.Invoke(null) as IElement;

            foreach (XmlAttribute a in node.Attributes)
            {
                ParseAttribute(a.Name, a.Value, new Field(vName, t), output);
            }

            foreach (XmlNode child in node.ChildNodes)
            {
                // Set text
                if (child.NodeType == XmlNodeType.Text)
                {
                    PropertyInfo pi = t.GetProperty("Text");
                    if (pi == null || !pi.CanWrite)
                    {
                        throw new Exception($"{node.Name} doesn't have a Text property");
                    }
                    
                    output.WriteLine($"{vName}.Text = {child.Value.Trim()}");
                    // pi.SetValue(element, child.Value.Trim());
                    continue;
                }

                if (child.NodeType != XmlNodeType.Element) { continue; }

                string childName = ParseNode(child, new Field(vName, t), output);
                // Property node
                if (childName == null) { continue; }

                // if (!element.IsParent)
                // {
                //     throw new Exception($"{node.Name} cannot have child elements.");
                // }

                output.WriteLine($"{vName}.Children.Add({childName})");
                output.WriteLine();
            }

            return vName;
        }

        private void ParseAttribute(string name, string value, Field parent, CSWriter output)
        {
            PropertyInfo p = parent.Type.GetPropertyUnambiguous(name, BindingFlags.Public | BindingFlags.Instance);

            if (p == null || !p.CanWrite)
            {
                ParseEventAttribute(name, value, parent.Type, parent.Name, output);
                return;
            }

            string set = ParseString(value, p.PropertyType);
            // p.SetValue(e, o);
            output.WriteLine($"{parent.Name}.{name} = {set}");
        }
        private void ParseEventAttribute(string name, string value, Type type, string vName, CSWriter output)
        {
            EventInfo ei = type.GetEvent(name);
            if (ei == null)
            {
                throw new Exception($"{type.Name} doesn't have attribute {name}");
            }

            int delegateParamterCount = ei.EventHandlerType.GetMethod("Invoke").GetParameters().Length;

            Delegate d;
            try
            {
                d = ParseEventString(value, ei.EventHandlerType, delegateParamterCount, _window, _window.GetType()); ;
            }
            catch
            {
                if (_eventType == null) { throw; }

                d = ParseEventString(value, ei.EventHandlerType, delegateParamterCount, _events, _eventType);
            }

            ei.AddEventHandler(e, d);
        }

        private static Delegate ParseEventString(string value, Type delegateType, int paramCount, object methodSource, Type sourceType)
        {
            if (sourceType == null)
            {
                throw new Exception("No method source");
            }

            if (value[^1] == ')' && value[^2] == '(')
            {
                value = value.Remove(value.Length - 2);
            }

            MethodInfo mi = sourceType.GetMethod(value, paramCount, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            if (mi == null)
            {
                throw new Exception($"Method {value} is not accessible");
            }

            if (mi.IsStatic)
            {
                return mi.CreateDelegate(delegateType);
            }

            return mi.CreateDelegate(delegateType, methodSource);
        }

        private string ParseString(string value, Type returnType)
        {
            value = value.Trim();

            try
            {
                TypeConverter tc = TypeDescriptor.GetConverter(returnType);
                object o = tc.ConvertFromInvariantString(value);
                // If null - parser not valid
                if (o != null)
                {
                    if (returnType == typeof(float))
                    {
                        return value + "f";
                    }
                    
                    return value;
                }
            }
            catch (Exception) { }

            if (_stringParses.TryGetValue(returnType, out StringParser parser) && parser != null)
            {
                try
                {
                    return parser(value);
                }
                catch (Exception) { }
            }

            return StringByType(value, returnType);
        }

        private string StringByType(string src, Type assign)
        {
            string[] constructor = SplitConstructor(src);
            if (constructor.Length == 0)
            {
                throw new Exception("Invalid constructor syntax");
            }

            // Find type
            Type type = _types.FindType(constructor[0], assign);
            if (type == null)
            {
                throw new Exception("Constructor type is invalid or doesn't exist");
            }

            // Find all constructors
            ConstructorInfo[] cinfos = type.GetConstructors();
            if (cinfos.Length == 0)
            {
                throw new Exception("Constructor doesn't exist");
            }

            // FInd valid constructors
            IEnumerable<ConstructorInfo> validConstructors = cinfos.Where(ci => ci.GetParameters().Length == constructor.Length - 1);
            if (!validConstructors.Any())
            {
                throw new Exception("Invalid number of parameters in constructor");
            }

            // No parameters
            if (constructor.Length == 1)
            {
                // return validConstructors.First().Invoke(null);
                return $"new {type.Name}()";
            }

            string[] parameters = new string[constructor.Length - 1];
            ConstructorInfo constructorMethod = null;

            foreach (ConstructorInfo ci in validConstructors)
            {
                ParameterInfo[] pinfos = ci.GetParameters();

                int i;
                for (i = 0; i < pinfos.Length; i++)
                {
                    Type pt = pinfos[i].ParameterType;

                    try
                    {
                        parameters[i] = ParseString(constructor[i + 1], pt);
                    }
                    catch (Exception) { break; }
                }

                // Not correct parameter
                if (i != pinfos.Length) { continue; }

                constructorMethod = ci;
                break;
            }

            if (constructorMethod == null)
            {
                throw new Exception("Invalid constructor parameters");
            }
            
            StringBuilder sb = new StringBuilder(20);
            
            sb.Append($"new {type.Name}(");
            for (int i = 0; i < parameters.Length; i++)
            {
                sb.Append(parameters[i]);
                if (i < parameters.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(')');
            
            return sb.ToString();
        }
        private static string[] SplitConstructor(string src)
        {
            if (src == null || src.Length == 0)
            {
                throw new Exception("Invalid constructor syntax");
            }

            src = src.Trim().TrimBrackets();
            if (src[^1] != ')')
            {
                throw new Exception("Invalid constructor syntax");
            }

            List<string> strings = new List<string>();

            bool inBrakets = false;
            bool inConstructor = false;

            int strRef = 0;

            for (int i = 0; i < src.Length; i++)
            {
                if (!inConstructor)
                {
                    // Continue until constructor
                    if (src[i] != '(') { continue; }

                    strings.Add(src[0..i].Trim());
                    strRef = i + 1;
                    inConstructor = true;
                    continue;
                }

                if (inBrakets && src[i] == ')')
                {
                    inBrakets = false;
                    continue;
                }

                if (src[i] == '(')
                {
                    inBrakets = true;
                    continue;
                }

                if (inBrakets) { continue; }

                if (src[i] == ',' || src[i] == ')')
                {
                    strings.Add(src[strRef..i].Trim());
                    strRef = i + 1;
                }
            }

            if (strRef != src.Length)
            {
                throw new Exception("Invalid constructor syntax");
            }

            if (strings[^1] == "")
            {
                strings.RemoveAt(strings.Count - 1);
            }

            return strings.ToArray();
        }

        private void ParseProperty(XmlNode node, Field parent, CSWriter output)
        {
            // if (parentName == null)
            // {
            //     throw new Exception("No parent to set property");
            // }

            XmlAttributeCollection xac = node.Attributes;

            if (xac.Count > 2 || xac.Count == 0)
            {
                throw new Exception("Invalid number of attributes on Property node");
            }

            string name;

            if (xac[0].Name == "Name")
            {
                name = xac[0].Value;
            }
            else if (xac.Count == 2 && xac[1].Name == "Name")
            {
                name = xac[1].Value;
            }
            else
            {
                throw new Exception("Property node needs Name attribute");
            }

            string value;

            if (xac[0].Name == "Value")
            {
                value = xac[0].Value;
            }
            else if (xac.Count == 2 && xac[1].Name == "Value")
            {
                value = xac[1].Value;
            }
            else if (node.ChildNodes.Count > 0 &&
                    node.ChildNodes[0].NodeType != XmlNodeType.Text)
            {
                ParseAttributeObject(name, node.ChildNodes[0], parent, output);
                return;
            }
            else
            {
                value = node.InnerText;
            }

            ParseAttribute(name, value, parent, output);
        }
        private void ParseAttributeObject(string name, XmlNode value, Field parent, CSWriter output)
        {
            PropertyInfo p;
            p = parent.Type.GetPropertyUnambiguous(name, BindingFlags.Public | BindingFlags.Instance);
            Type t = p.PropertyType;
            
            // construct element
            int count = GetCount(t);
            string vName = t.Name.ToLower() + count.ToString();
            
            ParseObjectProp(value, new Field(vName, t), output);
            // p.SetValue(e, o);
            output.WriteLine($"{parent.Name}.{name} = {vName}");
        }
        private void ParseObjectProp(XmlNode node, Field parent, CSWriter output)
        {
            if (node.Name == "Property")
            {
                ParseProperty(node, parent, output);
                return;
            }

            Type t = _types.FindType(node.Name, parent.Type);
            if (t == null)
            {
                throw new Exception("Tag name does not match expected type or does not exist");
            }

            ConstructorInfo constructor = t.GetConstructor(Array.Empty<Type>());
            if (constructor == null)
            {
                throw new Exception("Type does not have a parameterless constructor");
            }

            // object obj = constructor.Invoke(null);
            output.WriteLine($"{t.Name} {parent.Name} = new {t.Name}()");

            foreach (XmlAttribute a in node.Attributes)
            {
                ParseAttribute(a.Name, a.Value, parent, output);
            }
        }
    }
}
