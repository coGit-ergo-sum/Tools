using System;
using System.Collections.Generic;
using System.Text;

namespace Vi.Tools.Extensions.Random
{
	public static class Methods
	{
		public static int Next(this System.Random rnd, int min, int max, byte iterations)
		{
			int tot = 0;
			for (byte i = 0; i < iterations; i++)
			{
				tot += rnd.Next(min, max);
			}
			return tot / iterations;
		}

	}
}
