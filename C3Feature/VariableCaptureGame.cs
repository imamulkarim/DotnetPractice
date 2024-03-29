﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.C3Feature
{
	internal class VariableCaptureGame
	{
		internal Action<int>? updateCapturedLocalVariable;
		internal Func<int, bool>? isEqualToCapturedLocalVariable;

		public void Run(int input)
		{
			int j = 0;

			updateCapturedLocalVariable = x =>
			{
				j = x;
				bool result = j > input;
				Console.WriteLine($"{j} is greater than {input}: {result}");
			};

			isEqualToCapturedLocalVariable = x => x == j;

			Console.WriteLine($"Local variable before lambda invocation: {j}");
			updateCapturedLocalVariable(10);
			Console.WriteLine($"Local variable after lambda invocation: {j}");
		}
	}
}
