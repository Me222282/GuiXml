using System;

namespace GuiXml
{
    public static class XmlTypeParser
    {
        public static string CursorParser(string str)
        {
            bool _ = str switch
            {
                "Arrow" or "CrossHair" or "Default" or "Hand" or "IBeam" or "NotAllowed" or
                "ResizeAll" or "ResizeBottomLeft" or "ResizeBottomRight" or "ResizeHorizontal" or
                "ResizeTopLeft" or "ResizeTopRight" or "ResizeVertical" => true,
                _ => throw new Exception("Invalid Cursor syntax")
            };
            
            return $"Cursor.{str}";
        }
        public static string ColourParser(string str)
        {
            bool _ = str switch
            {
                "MediumVioletRed" or "DeepPink" or "PaleVioletRed" or
                "HotPink" or "LightPink" or "Pink" or

                "DarkRed" or "Red" or "Firebrick" or "Crimson" or
                "IndianRed" or "LightCoral" or "Salmon" or
                "DarkSalmon" or "LightSalmon" or

                "OrangeRed" or "Tomato" or "DarkOrange" or
                "Coral" or "Orange" or

                "DarkKhaki" or "Gold" or "Khaki" or "PeachPuff" or
                "Yellow" or "PaleGoldenrod" or "Moccasin" or
                "PapayaWhip" or "LightGoldenrodYellow" or
                "LemonChiffon" or "LightYellow" or

                "Maroon" or "Brown" or "SaddleBrown" or
                "Sienna" or "Chocolate" or "DarkGoldenrod" or
                "Peru" or "RosyBrown" or "Goldenrod" or "SandyBrown" or
                "Tan" or "Burlywood" or "Wheat" or "NavajoWhite" or
                "Bisque" or "BlanchedAlmond" or "Cornsilk" or

                "Indigo" or "Purple" or "DarkMagenta" or
                "DarkViolet" or "DarkSlateBlue" or "BlueViolet" or
                "DarkOrchid" or "Fuchsia" or "Magenta" or
                "SlateBlue" or "MediumSlateBlue" or
                "MediumOrchid" or "MediumPurple" or "Orchid" or
                "Violet" or "Plum" or "Thistle" or "Lavender" or

                "MidnightBlue" or "Navy" or "DarkBlue" or "MediumBlue" or
                "Blue" or "RoyalBlue" or "SteelBlue" or "DodgerBlue" or
                "DeepSkyBlue" or "CornflowerBlue" or "SkyBlue" or
                "LightSkyBlue" or "LightSteelBlue" or "LightBlue" or
                "PowderBlue" or

                "Teal" or "DarkCyan" or "LightSeaGreen" or
                "CadetBlue" or "DarkTurquoise" or "MediumTurquoise" or
                "Turquoise" or "Aqua" or "Cyan" or"Aquamarine" or
                "PaleTurquoise" or "LightCyan" or

                "DarkGreen" or "Green" or"DarkOliveGreen" or
                "ForestGreen" or"SeaGreen" or"Olive" or
                "OliveDrab" or "MediumSeaGreen" or"LimeGreen" or
                "Lime" or "SpringGreen" or"MediumSpringGreen" or
                "DarkSeaGreen" or "MediumAquamarine" or
                "YellowGreen" or "LawnGreen" or "Chartreuse" or
                "LightGreen" or "GreenYellow" or "PaleGreen" or

                "MistyRose" or "AntiqueWhite" or "Linen" or
                "Beige" or "WhiteSmoke" or "LavenderBlush" or
                "OldLace" or "AliceBlue" or "Seashell" or
                "GhostWhite" or "Honeydew" or "FloralWhite" or
                "Azure" or "MintCream" or "Snow" or
                "Ivory" or "White" or

                "Black" or "DarkSlateGrey" or "DimGrey" or
                "SlateGrey" or "DarkGrey" or "LightSlateGrey" or
                "Grey" or "Silver" or "LightGrey" or "Gainsboro" => true,
                _ => throw new Exception("Invalid Colour syntax")
            };
            
            return $"Colour.{str}";
        }
        public static string ColourFParser(string str)
        {
            bool _ = str switch
            {
                "MediumVioletRed" or "DeepPink" or "PaleVioletRed" or
                "HotPink" or "LightPink" or "Pink" or

                "DarkRed" or "Red" or "Firebrick" or "Crimson" or
                "IndianRed" or "LightCoral" or "Salmon" or
                "DarkSalmon" or "LightSalmon" or

                "OrangeRed" or "Tomato" or "DarkOrange" or
                "Coral" or "Orange" or

                "DarkKhaki" or "Gold" or "Khaki" or "PeachPuff" or
                "Yellow" or "PaleGoldenrod" or "Moccasin" or
                "PapayaWhip" or "LightGoldenrodYellow" or
                "LemonChiffon" or "LightYellow" or

                "Maroon" or "Brown" or "SaddleBrown" or
                "Sienna" or "Chocolate" or "DarkGoldenrod" or
                "Peru" or "RosyBrown" or "Goldenrod" or "SandyBrown" or
                "Tan" or "Burlywood" or "Wheat" or "NavajoWhite" or
                "Bisque" or "BlanchedAlmond" or "Cornsilk" or

                "Indigo" or "Purple" or "DarkMagenta" or
                "DarkViolet" or "DarkSlateBlue" or "BlueViolet" or
                "DarkOrchid" or "Fuchsia" or "Magenta" or
                "SlateBlue" or "MediumSlateBlue" or
                "MediumOrchid" or "MediumPurple" or "Orchid" or
                "Violet" or "Plum" or "Thistle" or "Lavender" or

                "MidnightBlue" or "Navy" or "DarkBlue" or "MediumBlue" or
                "Blue" or "RoyalBlue" or "SteelBlue" or "DodgerBlue" or
                "DeepSkyBlue" or "CornflowerBlue" or "SkyBlue" or
                "LightSkyBlue" or "LightSteelBlue" or "LightBlue" or
                "PowderBlue" or

                "Teal" or "DarkCyan" or "LightSeaGreen" or
                "CadetBlue" or "DarkTurquoise" or "MediumTurquoise" or
                "Turquoise" or "Aqua" or "Cyan" or"Aquamarine" or
                "PaleTurquoise" or "LightCyan" or

                "DarkGreen" or "Green" or"DarkOliveGreen" or
                "ForestGreen" or"SeaGreen" or"Olive" or
                "OliveDrab" or "MediumSeaGreen" or"LimeGreen" or
                "Lime" or "SpringGreen" or"MediumSpringGreen" or
                "DarkSeaGreen" or "MediumAquamarine" or
                "YellowGreen" or "LawnGreen" or "Chartreuse" or
                "LightGreen" or "GreenYellow" or "PaleGreen" or

                "MistyRose" or "AntiqueWhite" or "Linen" or
                "Beige" or "WhiteSmoke" or "LavenderBlush" or
                "OldLace" or "AliceBlue" or "Seashell" or
                "GhostWhite" or "Honeydew" or "FloralWhite" or
                "Azure" or "MintCream" or "Snow" or
                "Ivory" or "White" or

                "Black" or "DarkSlateGrey" or "DimGrey" or
                "SlateGrey" or "DarkGrey" or "LightSlateGrey" or
                "Grey" or "Silver" or "LightGrey" or "Gainsboro" => true,
                _ => throw new Exception("Invalid ColourF syntax")
            };
            
            return $"ColourF.{str}";
        }
        public static string Colour3Parser(string str)
        {
            bool _ = str switch
            {
                "MediumVioletRed" or "DeepPink" or "PaleVioletRed" or
                "HotPink" or "LightPink" or "Pink" or

                "DarkRed" or "Red" or "Firebrick" or "Crimson" or
                "IndianRed" or "LightCoral" or "Salmon" or
                "DarkSalmon" or "LightSalmon" or

                "OrangeRed" or "Tomato" or "DarkOrange" or
                "Coral" or "Orange" or

                "DarkKhaki" or "Gold" or "Khaki" or "PeachPuff" or
                "Yellow" or "PaleGoldenrod" or "Moccasin" or
                "PapayaWhip" or "LightGoldenrodYellow" or
                "LemonChiffon" or "LightYellow" or

                "Maroon" or "Brown" or "SaddleBrown" or
                "Sienna" or "Chocolate" or "DarkGoldenrod" or
                "Peru" or "RosyBrown" or "Goldenrod" or "SandyBrown" or
                "Tan" or "Burlywood" or "Wheat" or "NavajoWhite" or
                "Bisque" or "BlanchedAlmond" or "Cornsilk" or

                "Indigo" or "Purple" or "DarkMagenta" or
                "DarkViolet" or "DarkSlateBlue" or "BlueViolet" or
                "DarkOrchid" or "Fuchsia" or "Magenta" or
                "SlateBlue" or "MediumSlateBlue" or
                "MediumOrchid" or "MediumPurple" or "Orchid" or
                "Violet" or "Plum" or "Thistle" or "Lavender" or

                "MidnightBlue" or "Navy" or "DarkBlue" or "MediumBlue" or
                "Blue" or "RoyalBlue" or "SteelBlue" or "DodgerBlue" or
                "DeepSkyBlue" or "CornflowerBlue" or "SkyBlue" or
                "LightSkyBlue" or "LightSteelBlue" or "LightBlue" or
                "PowderBlue" or

                "Teal" or "DarkCyan" or "LightSeaGreen" or
                "CadetBlue" or "DarkTurquoise" or "MediumTurquoise" or
                "Turquoise" or "Aqua" or "Cyan" or"Aquamarine" or
                "PaleTurquoise" or "LightCyan" or

                "DarkGreen" or "Green" or"DarkOliveGreen" or
                "ForestGreen" or"SeaGreen" or"Olive" or
                "OliveDrab" or "MediumSeaGreen" or"LimeGreen" or
                "Lime" or "SpringGreen" or"MediumSpringGreen" or
                "DarkSeaGreen" or "MediumAquamarine" or
                "YellowGreen" or "LawnGreen" or "Chartreuse" or
                "LightGreen" or "GreenYellow" or "PaleGreen" or

                "MistyRose" or "AntiqueWhite" or "Linen" or
                "Beige" or "WhiteSmoke" or "LavenderBlush" or
                "OldLace" or "AliceBlue" or "Seashell" or
                "GhostWhite" or "Honeydew" or "FloralWhite" or
                "Azure" or "MintCream" or "Snow" or
                "Ivory" or "White" or

                "Black" or "DarkSlateGrey" or "DimGrey" or
                "SlateGrey" or "DarkGrey" or "LightSlateGrey" or
                "Grey" or "Silver" or "LightGrey" or "Gainsboro" => true,
                _ => throw new Exception("Invalid Colour3 syntax")
            };
            
            return $"Colour3.{str}";
        }
        public static string ColourF3Parser(string str)
        {
            bool _ = str switch
            {
                "MediumVioletRed" or "DeepPink" or "PaleVioletRed" or
                "HotPink" or "LightPink" or "Pink" or

                "DarkRed" or "Red" or "Firebrick" or "Crimson" or
                "IndianRed" or "LightCoral" or "Salmon" or
                "DarkSalmon" or "LightSalmon" or

                "OrangeRed" or "Tomato" or "DarkOrange" or
                "Coral" or "Orange" or

                "DarkKhaki" or "Gold" or "Khaki" or "PeachPuff" or
                "Yellow" or "PaleGoldenrod" or "Moccasin" or
                "PapayaWhip" or "LightGoldenrodYellow" or
                "LemonChiffon" or "LightYellow" or

                "Maroon" or "Brown" or "SaddleBrown" or
                "Sienna" or "Chocolate" or "DarkGoldenrod" or
                "Peru" or "RosyBrown" or "Goldenrod" or "SandyBrown" or
                "Tan" or "Burlywood" or "Wheat" or "NavajoWhite" or
                "Bisque" or "BlanchedAlmond" or "Cornsilk" or

                "Indigo" or "Purple" or "DarkMagenta" or
                "DarkViolet" or "DarkSlateBlue" or "BlueViolet" or
                "DarkOrchid" or "Fuchsia" or "Magenta" or
                "SlateBlue" or "MediumSlateBlue" or
                "MediumOrchid" or "MediumPurple" or "Orchid" or
                "Violet" or "Plum" or "Thistle" or "Lavender" or

                "MidnightBlue" or "Navy" or "DarkBlue" or "MediumBlue" or
                "Blue" or "RoyalBlue" or "SteelBlue" or "DodgerBlue" or
                "DeepSkyBlue" or "CornflowerBlue" or "SkyBlue" or
                "LightSkyBlue" or "LightSteelBlue" or "LightBlue" or
                "PowderBlue" or

                "Teal" or "DarkCyan" or "LightSeaGreen" or
                "CadetBlue" or "DarkTurquoise" or "MediumTurquoise" or
                "Turquoise" or "Aqua" or "Cyan" or"Aquamarine" or
                "PaleTurquoise" or "LightCyan" or

                "DarkGreen" or "Green" or"DarkOliveGreen" or
                "ForestGreen" or"SeaGreen" or"Olive" or
                "OliveDrab" or "MediumSeaGreen" or"LimeGreen" or
                "Lime" or "SpringGreen" or"MediumSpringGreen" or
                "DarkSeaGreen" or "MediumAquamarine" or
                "YellowGreen" or "LawnGreen" or "Chartreuse" or
                "LightGreen" or "GreenYellow" or "PaleGreen" or

                "MistyRose" or "AntiqueWhite" or "Linen" or
                "Beige" or "WhiteSmoke" or "LavenderBlush" or
                "OldLace" or "AliceBlue" or "Seashell" or
                "GhostWhite" or "Honeydew" or "FloralWhite" or
                "Azure" or "MintCream" or "Snow" or
                "Ivory" or "White" or

                "Black" or "DarkSlateGrey" or "DimGrey" or
                "SlateGrey" or "DarkGrey" or "LightSlateGrey" or
                "Grey" or "Silver" or "LightGrey" or "Gainsboro" => true,
                _ => throw new Exception("Invalid ColourF3 syntax")
            };
            
            return $"ColourF3.{str}";
        }

        public static StringParser Vector2Parser(bool useFloat)
        {
            if (useFloat)
            {
                return str =>
                {
                    str = str.Trim();

                    if (str[0] != '{' || str[^1] != '}')
                    {
                        throw new Exception("Invlaid Vector2 syntax.");
                    }

                    str = str[1..^1];

                    float x = float.Parse(str.Remove(str.IndexOf(',')));
                    float y = float.Parse(str.Remove(0, str.IndexOf(',') + 1));

                    return $"new Vector2({x}f, {y}f)";
                };
            }
            
            return str =>
            {
                str = str.Trim();

                if (str[0] != '{' || str[^1] != '}')
                {
                    throw new Exception("Invlaid Vector2 syntax.");
                }

                str = str[1..^1];

                double x = double.Parse(str.Remove(str.IndexOf(',')));
                double y = double.Parse(str.Remove(0, str.IndexOf(',') + 1));

                return $"new Vector2({x}d, {y}d)";
            };
        }
        public static string Vector2IParser(string str)
        {
            str = str.Trim();

            if (str[0] != '{' || str[^1] != '}')
            {
                throw new Exception("Invlaid Vector2I syntax.");
            }

            str = str[1..^1];

            int x = int.Parse(str.Remove(str.IndexOf(',')));
            int y = int.Parse(str.Remove(0, str.IndexOf(',') + 1));

            return $"new Vector2I({x}, {y})";
        }
        public static StringParser Vector3Parser(bool useFloat)
        {
            if (useFloat)
            {
                return str =>
                {
                    str = str.Trim();

                    if (str[0] != '{' || str[^1] != '}')
                    {
                        throw new Exception("Invlaid Vector3 syntax.");
                    }

                    str = str[1..^1];

                    float x = float.Parse(str.Remove(str.IndexOf(',')));
                    str = str.Remove(0, str.IndexOf(',') + 1);
                    float y = float.Parse(str.Remove(str.IndexOf(',')));
                    float z = float.Parse(str.Remove(0, str.IndexOf(',') + 1));

                    return $"new Vector3({x}f, {y}f, {z}f)";
                };
            }
            
            return str =>
            {
                str = str.Trim();

                if (str[0] != '{' || str[^1] != '}')
                {
                    throw new Exception("Invlaid Vector3 syntax.");
                }

                str = str[1..^1];

                double x = double.Parse(str.Remove(str.IndexOf(',')));
                str = str.Remove(0, str.IndexOf(',') + 1);
                double y = double.Parse(str.Remove(str.IndexOf(',')));
                double z = double.Parse(str.Remove(0, str.IndexOf(',') + 1));

                return $"new Vector3({x}d, {y}d, {z}d)";
            };
        }
        public static string Vector3IParser(string str)
        {
            str = str.Trim();

            if (str[0] != '{' || str[^1] != '}')
            {
                throw new Exception("Invlaid Vector3I syntax.");
            }

            str = str[1..^1];

            int x = int.Parse(str.Remove(str.IndexOf(',')));
            str = str.Remove(0, str.IndexOf(',') + 1);
            int y = int.Parse(str.Remove(str.IndexOf(',')));
            int z = int.Parse(str.Remove(0, str.IndexOf(',') + 1));

            return $"new Vector3I({x}, {y}, {z})";
        }
        public static StringParser Vector4Parser(bool useFloat)
        {
            if (useFloat)
            {
                return str =>
                {
                    str = str.Trim();

                    if (str[0] != '{' || str[^1] != '}')
                    {
                        throw new Exception("Invlaid Vector4 syntax.");
                    }

                    str = str[1..^1];

                    float x = float.Parse(str.Remove(str.IndexOf(',')));
                    str = str.Remove(0, str.IndexOf(',') + 1);
                    float y = float.Parse(str.Remove(str.IndexOf(',')));
                    str = str.Remove(0, str.IndexOf(',') + 1);
                    float z = float.Parse(str.Remove(str.IndexOf(',')));
                    float w = float.Parse(str.Remove(0, str.IndexOf(',') + 1));

                    return $"new Vector4({x}f, {y}f, {z}f, {w}f)";
                };
            }
            
            return str =>
            {
                str = str.Trim();

                if (str[0] != '{' || str[^1] != '}')
                {
                    throw new Exception("Invlaid Vector4 syntax.");
                }

                str = str[1..^1];

                double x = double.Parse(str.Remove(str.IndexOf(',')));
                str = str.Remove(0, str.IndexOf(',') + 1);
                double y = double.Parse(str.Remove(str.IndexOf(',')));
                str = str.Remove(0, str.IndexOf(',') + 1);
                double z = double.Parse(str.Remove(str.IndexOf(',')));
                double w = double.Parse(str.Remove(0, str.IndexOf(',') + 1));

                return $"new Vector4({x}d, {y}d, {z}d, {w}d)";
            };
        }
        public static string Vector4IParser(string str)
        {
            str = str.Trim();

            if (str[0] != '{' || str[^1] != '}')
            {
                throw new Exception("Invlaid Vector4I syntax.");
            }

            str = str[1..^1];

            int x = int.Parse(str.Remove(str.IndexOf(',')));
            str = str.Remove(0, str.IndexOf(',') + 1);
            int y = int.Parse(str.Remove(str.IndexOf(',')));
            str = str.Remove(0, str.IndexOf(',') + 1);
            int z = int.Parse(str.Remove(str.IndexOf(',')));
            int w = int.Parse(str.Remove(0, str.IndexOf(',') + 1));

            return $"new Vector4I({x}, {y}, {z}, {w})";
        }
    }
}
