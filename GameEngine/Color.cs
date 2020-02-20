using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
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
			this.R = r;
			this.G = g;
			this.B = b;
		}

		//
		// Summary:
		//     Represents a color that is null.
		public static readonly Color Empty;
    }
}
