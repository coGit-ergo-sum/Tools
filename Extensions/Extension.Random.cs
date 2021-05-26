using System;
using System.Collections.Generic;
using System.Text;
using Vi.Tools.Extensions.Float;

namespace Vi.Tools.Extensions.Random
{
	/// <summary>
	/// Collection of extention methods for the 'System.Random' Object.
	/// </summary>
	public static class Methods
	{
		/// <summary>
		/// Returns a non-negative random integer between 'min' and 'max'.
		/// </summary>
		/// <param name="rnd">The current instance of the 'System.Random' object.</param>
		/// <param name="min">The min value for the resulting value.</param>
		/// <param name="max">The max value for the resulting value.</param>
		/// <param name="iterations">Set the distribution 1: Linear (omogeneous) 2: Triangular; ... Gaussian</param>
		/// <returns>A  random value between min and max with a specified distribution.</returns>
		public static int Next(this System.Random rnd, int min, int max, byte iterations)
		{
			int tot = 0;
			for (byte i = 0; i < iterations; i++)
			{
				tot += rnd.Next(min, max);
			}
			return tot / iterations;
		}

		/// <summary>
		/// Compare a random value between 0 and 100 against the value of 'probability'
		/// </summary>
		/// <param name="rnd"></param>
		/// <param name="grade">The grade of the polinomial distribution.</param>
		/// <param name="probability">a number between 0 and 100. Grater becom 100 smallest becom 0</param>
		/// <returns>A true if 'brobability is grater than a random value (rnd.next > probability)(</returns>
		public static bool Bet(this System.Random rnd, byte grade, Vi.Tools.Types.Percent probability)
		{
			return rnd.Next(0, 1, grade) > probability.Value;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="rnd">The current instance of the 'System.Random object.</param>
		/// <param name="probability">A value between 0 and 100. The expected probability an event is successfull.</param>
		/// <returns>A random value (rnd.next > probability)</returns>
		public static bool Bet(this System.Random rnd, Vi.Tools.Types.Percent probability)
		{
			return rnd.Bet(1, probability);
		}

	}



}
