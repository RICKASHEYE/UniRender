using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    public class Color32
    {
		/// <summary> Red component. </summary>
		public float R { get; set; }
		/// <summary> Green component. </summary>
		public float G { get; set; }
		/// <summary> Bkue component. </summary>
		public float B { get; set; }
		/// <summary> Alpha component. </summary>
		public float A { get; set; }

		/// <summary> Creates a new Color from rgb. </summary>
		public Color32(float r, float g, float b, float a)
		{
			this.R = r;
			this.G = g;
			this.B = b;
			this.A = a;
		}

		//
		// Summary:
		//     Represents a color that is null.
		public static readonly Color32 Empty;

		public Color ToColor => new Color(R / 255, G / 255, B / 255, A / 255);

		public static readonly Color32 White = new Color32(255, 255, 255, 255);
		public static readonly Color32 Black = new Color32(0, 0, 0, 255);
		public static readonly Color32 CatalinaBlue = new Color32(43, 51, 95, 255);
		public static readonly Color32 VividViolet = new Color32(126, 32, 144, 255);
		public static readonly Color32 LightSeaGreen = new Color32(25, 149, 156, 255);
		public static readonly Color32 SolidPink = new Color32(139, 72, 82, 255);
		public static readonly Color32 Mariner = new Color32(57, 92, 152, 255);
		public static readonly Color32 CottonCandy = new Color32(169, 193, 255, 255);
		public static readonly Color32 WhiteSmoke = new Color32(238, 238, 238, 255);
		public static readonly Color32 Ruby = new Color32(212, 24, 108, 255);
		public static readonly Color32 RawSienna = new Color32(211, 132, 65, 255);
		public static readonly Color32 Ranchi = new Color32(233, 195, 91, 255);
		public static readonly Color32 MonteCarlo = new Color32(112, 198, 169, 255);
		public static readonly Color32 JordyBlue = new Color32(118, 150, 222, 255);
		public static readonly Color32 DarkGray = new Color32(163, 163, 163, 255);
		public static readonly Color32 MonaLisa = new Color32(255, 151, 152, 255);
		public static readonly Color32 DesertSand = new Color32(237, 199, 176, 255);
	}
}
