using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    public class Color
    {
		/// <summary> Red component. </summary>
		public float R { get; set; }
		/// <summary> Green component. </summary>
		public float G { get; set; }
		/// <summary> Bkue component. </summary>
		public float B { get; set; }
		// <summary> Alpha component </summary>
		public float A { get; set; }

		/// <summary> Creates a new Color from rgb. </summary>
		public Color(float r, float g, float b, float a)
		{
			this.R = r;
			this.G = g;
			this.B = b;
			this.A = a;
		}

		//
		// Summary:
		//     Represents a color that is null.
		public static readonly Color Empty;
		public Color32 ToColor32 => new Color32(R * 255, G * 255, B * 255, A * 255);
	}
}
