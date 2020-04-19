using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine
{
    public class Color
    {
		/// <summary> Red component. </summary>
		public int R { get; set; }
		/// <summary> Green component. </summary>
		public int G { get; set; }
		/// <summary> Bkue component. </summary>
		public int B { get; set; }

		/// <summary> Creates a new Color from rgb. </summary>
		public Color(int r, int g, int b)
		{
			this.R = r / 255;
			this.G = g / 255;
			this.B = b / 255;
		}

		//
		// Summary:
		//     Represents a color that is null.
		public static readonly Color Empty;

		public static readonly Color White = new Color(255, 255, 255);
		public static readonly Color Black = new Color(0, 0, 0);
		public static readonly Color CatalinaBlue = new Color(43, 51, 95);
		public static readonly Color VividViolet = new Color(126, 32, 144);
		public static readonly Color LightSeaGreen = new Color(25, 149, 156);
		public static readonly Color SolidPink = new Color(139, 72, 82);
		public static readonly Color Mariner = new Color(57, 92, 152);
		public static readonly Color CottonCandy = new Color(169, 193, 255);
		public static readonly Color WhiteSmoke = new Color(238, 238, 238);
		public static readonly Color Ruby = new Color(212, 24, 108);
		public static readonly Color RawSienna = new Color(211, 132, 65);
		public static readonly Color Ranchi = new Color(233, 195, 91);
		public static readonly Color MonteCarlo = new Color(112, 198, 169);
		public static readonly Color JordyBlue = new Color(118, 150, 222);
		public static readonly Color DarkGray = new Color(163, 163, 163);
		public static readonly Color MonaLisa = new Color(255, 151, 152);
		public static readonly Color DesertSand = new Color(237, 199, 176);
    }
}
