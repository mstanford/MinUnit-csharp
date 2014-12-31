// ------------------------------------------------------------------------
// 
// This is free and unencumbered software released into the public domain.
// 
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
// 
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org/>
// 
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnit.Framework
{
	public class Assert
	{

		public static void AreEqual(double expected, double actual)
		{
			AreEqual(expected, actual, "N3");
		}

		public static void AreEqual(double[] expected, double[] actual)
		{
			if (expected.Length != actual.Length)
				throw new AssertionException("Assert.AreEqual([Arrays are different sizes]);");

			for (int i = 0; i < expected.Length; i++)
				AreEqual(expected[i], actual[i], "N3");
		}

		public static void AreEqual(double[] expected, double[] actual, string precision)
		{
			if (expected.Length != actual.Length)
				throw new AssertionException("Assert.AreEqual([Arrays are different sizes]);");

			for (int i = 0; i < expected.Length; i++)
				AreEqual(expected[i], actual[i], precision);
		}

		public static void AreEqual(double[,] expected, double[,] actual)
		{
			int n = expected.GetLength(0);
			int m = expected.GetLength(1);

			if (n != actual.GetLength(0) || m != actual.GetLength(1))
				throw new AssertionException("Assert.AreEqual([Arrays are different sizes]);");

			for (int i = 0; i < n; i++)
				for (int j = 0; j < m; j++)
					AreEqual(expected[i, j], actual[i, j], "N3");
		}

		public static void AreEqual(double[,] expected, double[,] actual, string precision)
		{
			int n = expected.GetLength(0);
			int m = expected.GetLength(1);

			if (n != actual.GetLength(0) || m != actual.GetLength(1))
				throw new AssertionException("Assert.AreEqual([Arrays are different sizes]);");

			for (int i = 0; i < n; i++)
				for (int j = 0; j < m; j++)
					AreEqual(expected[i, j], actual[i, j], precision);
		}

		public static void AreEqual(double[, ,] expected, double[, ,] actual)
		{
			int n = expected.GetLength(0);
			int m = expected.GetLength(1);
			int o = expected.GetLength(2);

			if (n != actual.GetLength(0) || m != actual.GetLength(1) || o != actual.GetLength(2))
				throw new AssertionException("Assert.AreEqual([Arrays are different sizes]);");

			for (int i = 0; i < n; i++)
				for (int j = 0; j < m; j++)
					for (int k = 0; k < o; k++)
						AreEqual(expected[i, j, k], actual[i, j, k], "N3");
		}

		public static void AreEqual(double expected, double actual, string precision)
		{
			AreEqual2(expected.ToString(precision), actual.ToString(precision));
		}

		public static void AreEqual(int expected, int actual)
		{
			AreEqual2(expected, actual);
		}

		public static void AreEqual(string expected, string actual)
		{
			AreEqual2(expected, actual);
		}

		public static void AreEqual(object expected, object actual)
		{
			if (expected is System.Array && actual is System.Array)
				AreEqual((System.Array)expected, (System.Array)actual);
			else
				AreEqual2(expected, actual);
		}

		public static void AreEqual(System.Array expected, System.Array actual)
		{
			if (expected.Length != actual.Length)
				throw new AssertionException("Assert.AreEqual([Arrays are different sizes]);");

			if (expected.Rank == 1)
			{
				for (int i = 0; i < expected.Length; i++)
					AreEqual(expected.GetValue(i), actual.GetValue(i));
			}
			else if (expected.Rank == 2)
			{
				int n = expected.GetLength(0);
				int m = expected.GetLength(1);

				for (int i = 0; i < n; i++)
					for (int j = 0; j < m; j++)
						AreEqual(expected.GetValue(i, j), actual.GetValue(i, j));
			}
			else
			{
				throw new System.Exception();
			}
		}

		private static void AreEqual2(object expected, object actual)
		{
			if (expected == null && actual == null)
				return;

			if (expected == null)
				throw new AssertionException("Assert.AreEqual(null, " + actual + ");");

			if (actual == null)
				throw new AssertionException("Assert.AreEqual(" + expected + ", null);");

			if (!expected.Equals(actual))
				throw new AssertionException("Assert.AreEqual(" + expected + ", " + actual + ");");
		}

	}
}
