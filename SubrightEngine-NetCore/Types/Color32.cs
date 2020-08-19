using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    public class Color32
    {
        private uint _packedValue;

        [CLSCompliant(false)]
        public Color32(uint packedValue)
        {
            _packedValue = packedValue;
        }

        public Color32(Vector4 color) : this((int)(color.x * 255), (int)(color.y * 255), (int)(color.z * 255), (int)(color.w * 255))
        {}

        public Color32(Color32 color, int alpha)
        {
            if((alpha & 0xFFFFFF00) != 0)
            {
                var clampedA = (uint)Mathf.Clamp(alpha, Byte.MinValue, Byte.MaxValue);
                _packedValue = (color._packedValue & 0x00FFFFFF) | (clampedA << 24);
            }
            else
            {
                _packedValue = (color._packedValue & 0x00FFFFFF) | ((uint)alpha << 24);
            }
        }

        public Color32(Color32 color, float alpha) : this(color, (int)(alpha * 255))
        {}


        public Color32(float r, float g, float b) : this((int)(r * 255), (int)(g * 255), (int)(b * 255)) { }

        public Color32(float r, float g, float b, float alpha) : this((int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(alpha * 255)) 
        {}

        public Color32(int r, int g, int b)
        {
            _packedValue = 0xFF000000;

            if(((r | g | b) & 0xFFFFFF00) != 0)
            {
                var clampedR = (uint)Mathf.Clamp(r, Byte.MinValue, Byte.MaxValue);
                var clampedG = (uint)Mathf.Clamp(g, Byte.MinValue, Byte.MaxValue);
                var clampedB = (uint)Mathf.Clamp(b, Byte.MinValue, Byte.MaxValue);

                _packedValue |= (clampedB << 16) | (clampedG << 8) | (clampedR);
            }
            else
            {
                _packedValue |= ((uint)b << 16) | ((uint)g << 8) | ((uint)r);
            }
        }

        public Color32(int r, int g, int b, int alpha)
        {
            if(((r | g | b | alpha) & 0xFFFFFF00) != 0)
            {
                var clampedR = (uint)Mathf.Clamp(r, Byte.MinValue, Byte.MaxValue);
                var clampedG = (uint)Mathf.Clamp(g, Byte.MinValue, Byte.MaxValue);
                var clampedB = (uint)Mathf.Clamp(b, Byte.MinValue, Byte.MaxValue);
                var clampedA = (uint)Mathf.Clamp(alpha, Byte.MinValue, Byte.MaxValue);

                _packedValue = (clampedA << 24) | (clampedB << 16) | (clampedG << 8) | (clampedR);
            }
            else
            {
                _packedValue = ((uint)alpha << 24) | ((uint)b << 16) | ((uint)g << 8) | ((uint)r);
            }
        }

        public Color32(byte r, byte g, byte b, byte alpha)
        {
            _packedValue = ((uint)alpha << 24) | ((uint)b << 16) | ((uint)g << 8) | (r);
        }

        [DataMember]
        public byte B
        {
            get
            {
                unchecked
                {
                    return (byte)(this._packedValue >> 16);
                }
            }
            set
            {
                this._packedValue = (this._packedValue & 0xff00ffff) | ((uint)value << 16);
            }
        }

        [DataMember]
        public byte G
        {
            get
            {
                unchecked
                {
                    return (byte)(this._packedValue >> 8);
                }
            }
            set
            {
                this._packedValue = (this._packedValue & 0xffff00ff) | ((uint)value << 8);
            }
        }

        [DataMember]
        public byte R
        {
            get
            {
                unchecked
                {
                    return (byte)this._packedValue;
                }
            }
            set
            {
                this._packedValue = (this._packedValue & 0xffffff00) | value;
            }
        }

        [DataMember]
        public byte A
        {
            get
            {
                unchecked
                {
                    return (byte)(this._packedValue >> 24);
                }
            }
            set
            {
                this._packedValue = (this._packedValue & 0x00ffffff) | ((uint)value << 24);
            }
        }

        public override bool Equals(object obj)
        {
            return ((obj is Color32) && this.Equals((Color32)obj));
        }

        //
        // Summary:
        //     Represents a color that is null.
        public static readonly Color32 Empty;

		public Color ToColor => new Color(R / 255, G / 255, B / 255, A / 255);

		public static readonly Color32 White = new Color32(uint.MaxValue);
		public static readonly Color32 Black = new Color32(0, 0, 0, 255);
        public static readonly Color32 TransparentBlack = new Color32(0);
        public static readonly Color32 Transparent = new Color32(0);
        public static readonly Color32 AliceBlue = new Color32(0xfffff8f0);
        public static readonly Color32 AntiqueWhite = new Color32(0xffd7ebfa);
        public static readonly Color32 Aqua = new Color32(0xffffff00);
        public static readonly Color32 Aquamarine = new Color32(0xffd4ff7f);
        public static readonly Color32 Azure = new Color32(0xfffffff0);
        public static readonly Color32 Beige = new Color32(0xffdcf5f5);
        public static readonly Color32 Bisque = new Color32(0xffc4e4ff);
        public static readonly Color32 BlanchedAlmond = new Color32(0xffcdebff);
        public static readonly Color32 Blue = new Color32(0xffff0000);
        public static readonly Color32 BlueViolet = new Color32(0xffe22b8a);
        public static readonly Color32 Brown = new Color32(0xff2a2aa5);
        public static readonly Color32 BurlyWood = new Color32(0xff87b8de);
        public static readonly Color32 CadetBlue = new Color32(0xffa09e5f);
        public static readonly Color32 Chartreuse = new Color32(0xff00ff7f);
        public static readonly Color32 Chocolate = new Color32(0xff1e69d2);
        public static readonly Color32 Coral = new Color32(0xff507fff);
        public static readonly Color32 CornflowerBlue = new Color32(0xffed9564);
        public static readonly Color32 Cornsilk = new Color32(0xffdcf8ff);
        public static readonly Color32 Crimson = new Color32(0xff3c14dc);
        public static readonly Color32 Cyan = new Color32(0xffffff00);
        public static readonly Color32 DarkBlue = new Color32(0xff8b0000);
        public static readonly Color32 DarkCyan = new Color32(0xff8b8b00);
        public static readonly Color32 DarkGoldenrod = new Color32(0xff0b86b8);
        public static readonly Color32 DarkGray = new Color32(0xffa9a9a9);
        public static readonly Color32 DarkGreen = new Color32(0xff006400);
        public static readonly Color32 DarkKhaki = new Color32(0xff6bb7bd);
        public static readonly Color32 DarkMagenta = new Color32(0xff8b008b);
        public static readonly Color32 DarkOliveGreen = new Color32(0xff2f6b55);
        public static readonly Color32 DarkOrange = new Color32(0xff008cff);
        public static readonly Color32 DarkOrchid = new Color32(0xffcc3299);
        public static readonly Color32 DarkRed = new Color32(0xff00008b);
        public static readonly Color32 DarkSalmon = new Color32(0xff7a96e9);
        public static readonly Color32 DarkSeaGreen = new Color32(0xff8bbc8f);
        public static readonly Color32 DarkSlateBlue = new Color32(0xff8b3d48);
        public static readonly Color32 DarkSlateGray = new Color32(0xff4f4f2f);
        public static readonly Color32 DarkTurquoise = new Color32(0xffd1ce00);
        public static readonly Color32 DarkViolet = new Color32(0xffd30094);
        public static readonly Color32 DeepPink = new Color32(0xff9314ff);
        public static readonly Color32 DeepSkyBlue = new Color32(0xffffbf00);
        public static readonly Color32 DimGray = new Color32(0xff696969);
        public static readonly Color32 DodgerBlue = new Color32(0xffff901e);
        public static readonly Color32 Firebrick = new Color32(0xff2222b2);
        public static readonly Color32 FloralWhite = new Color32(0xfff0faff);
        public static readonly Color32 ForestGreen = new Color32(0xff228b22);
        public static readonly Color32 Fuchsia = new Color32(0xffff00ff);
        public static readonly Color32 Gainsboro = new Color32(0xffdcdcdc);
        public static readonly Color32 GhostWhite = new Color32(0xfffff8f8);
        public static readonly Color32 Gold = new Color32(0xff00d7ff);
        public static readonly Color32 Goldenrod = new Color32(0xff20a5da);
        public static readonly Color32 Gray = new Color32(0xff808080);
        public static readonly Color32 Green = new Color32(0xff008000);
        public static readonly Color32 GreenYellow = new Color32(0xff2fffad);
        public static readonly Color32 Honeydew = new Color32(0xfff0fff0);
        public static readonly Color32 HotPink = new Color32(0xffb469ff);
        public static readonly Color32 IndianRed = new Color32(0xff5c5ccd);
        public static readonly Color32 Indigo = new Color32(0xff82004b);
        public static readonly Color32 Ivory = new Color32(0xfff0ffff);
        public static readonly Color32 Khaki = new Color32(0xff8ce6f0);
        public static readonly Color32 Lavender = new Color32(0xfffae6e6);
        public static readonly Color32 LavenderBlush = new Color32(0xfff5f0ff);
        public static readonly Color32 LawnGreen = new Color32(0xff00fc7c);
        public static readonly Color32 LemonChiffon = new Color32(0xffcdfaff);
        public static readonly Color32 LightBlue = new Color32(0xffe6d8ad);
        public static readonly Color32 LightCoral = new Color32(0xff8080f0);
        public static readonly Color32 LightCyan = new Color32(0xffffffe0);
        public static readonly Color32 LightGoldenrodYellow = new Color32(0xffd2fafa);
        public static readonly Color32 LightGray = new Color32(0xffd3d3d3);
        public static readonly Color32 LightGreen = new Color32(0xff90ee90);
        public static readonly Color32 LightPink = new Color32(0xffc1b6ff);
        public static readonly Color32 LightSalmon = new Color32(0xff7aa0ff);
        public static readonly Color32 LightSeaGreen = new Color32(0xffaab220);
        public static readonly Color32 LightSkyBlue = new Color32(0xffface87);
        public static readonly Color32 LightSlateGray = new Color32(0xff998877);
        public static readonly Color32 LightSteelBlue = new Color32(0xffdec4b0);
        public static readonly Color32 LightYellow = new Color32(0xffe0ffff);
        public static readonly Color32 Lime = new Color32(0xff00ff00);
        public static readonly Color32 LimeGreen = new Color32(0xff32cd32);
        public static readonly Color32 Linen = new Color32(0xffe6f0fa);
        public static readonly Color32 Magenta = new Color32(0xffff00ff);
        public static readonly Color32 Maroon = new Color32(0xff000080);
        public static readonly Color32 MediumAquamarine = new Color32(0xffaacd66);
        public static readonly Color32 MediumBlue = new Color32(0xffcd0000);
        public static readonly Color32 MediumOrchid = new Color32(0xffd355ba);
        public static readonly Color32 MediumPurple = new Color32(0xffdb7093);
        public static readonly Color32 MediumSeaGreen = new Color32(0xff71b33c);
        public static readonly Color32 MediumSlateBlue = new Color32(0xffee687b);
        public static readonly Color32 MediumSpringGreen = new Color32(0xff9afa00);
        public static readonly Color32 MediumTurquoise = new Color32(0xffccd148);
        public static readonly Color32 MediumVioletRed = new Color32(0xff8515c7);
        public static readonly Color32 MidnightBlue = new Color32(0xff701919);
        public static readonly Color32 MintCream = new Color32(0xfffafff5);
        public static readonly Color32 MistyRose = new Color32(0xffe1e4ff);
        public static readonly Color32 Moccasin = new Color32(0xffb5e4ff);
        public static readonly Color32 MonoGameOrange = new Color32(0xff003ce7);
        public static readonly Color32 NavajoWhite = new Color32(0xffaddeff);
        public static readonly Color32 Navy = new Color32(0xff800000);
        public static readonly Color32 OldLace = new Color32(0xffe6f5fd);
        public static readonly Color32 Olive = new Color32(0xff008080);
        public static readonly Color32 OliveDrab = new Color32(0xff238e6b);
        public static readonly Color32 Orange = new Color32(0xff00a5ff);
        public static readonly Color32 OrangeRed = new Color32(0xff0045ff);
        public static readonly Color32 Orchid = new Color32(0xffd670da);
        public static readonly Color32 PaleGoldenrod = new Color32(0xffaae8ee);
        public static readonly Color32 PaleGreen = new Color32(0xff98fb98);
        public static readonly Color32 PaleTurquoise = new Color32(0xffeeeeaf);
        public static readonly Color32 PaleVioletRed = new Color32(0xff9370db);
        public static readonly Color32 PapayaWhip = new Color32(0xffd5efff);
        public static readonly Color32 PeachPuff = new Color32(0xffb9daff);
        public static readonly Color32 Peru = new Color32(0xff3f85cd);
        public static readonly Color32 Pink = new Color32(0xffcbc0ff);
        public static readonly Color32 publicPlum = new Color32(0xffdda0dd);
        public static readonly Color32 PowderBlue = new Color32(0xffe6e0b0);
        public static readonly Color32 Purple = new Color32(0xff800080);
        public static readonly Color32 Red = new Color32(0xff0000ff);
        public static readonly Color32 RosyBrown = new Color32(0xff8f8fbc);
        public static readonly Color32 RoyalBlue = new Color32(0xffe16941);
        public static readonly Color32 SaddleBrown = new Color32(0xff13458b);
        public static readonly Color32 Salmon= new Color32(0xff7280fa);
        public static readonly Color32 SandyBrown = new Color32(0xff60a4f4);
        public static readonly Color32 SeaGreen = new Color32(0xff578b2e);
        public static readonly Color32 SeaShell = new Color32(0xffeef5ff);
        public static readonly Color32 Sienna = new Color32(0xff2d52a0);
        public static readonly Color32 Silver  = new Color32(0xffc0c0c0);
        public static readonly Color32 SkyBlue  = new Color32(0xffebce87);
        public static readonly Color32 SlateBlue= new Color32(0xffcd5a6a);
        public static readonly Color32 SlateGray= new Color32(0xff908070);
        public static readonly Color32 Snow= new Color32(0xfffafaff);
        public static readonly Color32 SpringGreen= new Color32(0xff7fff00);
        public static readonly Color32 SteelBlue= new Color32(0xffb48246);
        public static readonly Color32 Tan= new Color32(0xff8cb4d2);
        public static readonly Color32 Teal= new Color32(0xff808000);
        public static readonly Color32 Thistle= new Color32(0xffd8bfd8);
        public static readonly Color32 Tomato= new Color32(0xff4763ff);
        public static readonly Color32 Turquoise= new Color32(0xffd0e040);
        public static readonly Color32 Violet= new Color32(0xffee82ee);
        public static readonly Color32 Wheat= new Color32(0xffb3def5);
        public static readonly Color32 WhiteSmoke= new Color32(0xfff5f5f5);
        public static readonly Color32 Yellow = new Color32(0xff00ffff);
        public static readonly Color32 YellowGreen = new Color32(0xff32cd9a);
    }
}
