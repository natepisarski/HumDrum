using System;
using NUnit.Framework;
using HumDrum.Structures;

namespace HumDrumTests
{
	public class TestClass {
		public string X;
		public int Y;

		public TestClass(string x, int y) {
			X =  x;
			Y =  y;
		}
	}

	[TestFixture]
	public class TestObjectBuilder
	{
		[Test]
		public void TestSetup() {
			ObjectBuilder obj = new ObjectBuilder (typeof(TestClass));

			obj = obj [0, "x", "X-Value"] [0, "y", 0];
			TestClass t = (TestClass) obj.Instantiate (0);

			Assert.AreEqual (
				"X-Value",
				t.X);

			Assert.AreEqual (
				0,
				t.Y);
		}
	}
}

