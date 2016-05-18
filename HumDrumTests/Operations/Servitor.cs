using System;
using System.Net;
using System.Threading;

using HumDrum.Collections;

using System.Collections.Generic;

using NUnit.Framework;

using SV = HumDrum.Operations.Servitor;

namespace HumDrumTests.Operations
{
	/// <summary>
	/// Servitor.
	/// </summary>
	[TestFixture]
	public class Servitor
	{
		/// <summary>
		/// This will test all of the servitor's functions
		/// in one method.
		/// </summary>
		[Test]
		public async void TestServitor()
		{

			SV _testServitor;


			Thread _servitorThread;

			_testServitor = new SV (IPAddress.Loopback, 4206);
			_servitorThread = new Thread(new ThreadStart (_testServitor.Start));

			_servitorThread.Start ();

			Assert.False (_testServitor.Changed);

			SV.Send ("This", "127.0.0.1", 4206);
			SV.Send ("Is", "127.0.0.1", 4206);
			SV.Send ("Test", "127.0.0.1", 4206);
			SV.Send ("Data", "127.0.0.1", 4206);


			IEnumerable<string> modified = await _testServitor.Collect ();

			lock( modified)
			{
			Assert.True (_testServitor.Changed);

			Assert.AreEqual ("This", modified.Get (0));
			Assert.AreEqual ("Is", modified.Get (1));
			Assert.AreEqual ("Test", modified.Get (2));
			Assert.AreEqual ("Data", modified.Get (3));
			}

		}
	}
}

