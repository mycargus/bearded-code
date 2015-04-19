using System;
using System.Resources;

namespace SquareRoot
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			const double Accuracy = 0.1;

			// test cases: 4, 16, 64, 8, 10, 12.5
			var testCase = 1000;
			var expectedRoot = Math.Sqrt(testCase);


			Console.WriteLine(String.Format("Test case:\t{0}", testCase));

			// estimate root

			double high = testCase;
			double middle = high;
			double low = 0;
			double previousMiddle = -1;


			while (Math.Abs(previousMiddle - middle) >= Accuracy)
			{
				previousMiddle = middle;
				middle = (high + low) / 2;

				var middleSquared = middle * middle;

				if (middleSquared > testCase)
				{
					// too high
					high = middle;
				}
				else if (middleSquared < testCase)
				{
					// too low
					low = middle;
				}
			}

			double actualRoot = middle;

			Console.WriteLine(String.Format("Expected:\t{0}", expectedRoot));
			Console.WriteLine(String.Format("Actual:\t{0}", actualRoot));
			Console.ReadLine();
		}

	}
}
