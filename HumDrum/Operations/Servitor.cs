using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

using HumDrum.Collections;

namespace HumDrum.Operations
{
	//TODO: Allow responses to be sent to the server
	/// <summary>
	/// Servitor is a wrapper of TcpListener. It buffers incoming requests
	/// so that another thread can read them when they'd ready.
	/// </summary>
	public class Servitor
	{
		/// <summary>
		/// A list of all the input that this server has ever gotten.
		/// </summary>
		List<string> AllInput {get; set;}

		/// <summary>
		/// The port to listen to (defaults to 4206)
		/// </summary>
		int Port { get; set; }

		/// <summary>
		/// The address to listen for (defaults to loopback)
		/// </summary>
		IPAddress Address { get; set; }

		/// <summary>
		/// A boolean set up to determine whether input has
		/// been given since the last time it has been collected.
		/// </summary>
		public bool Changed { get; set; }

		/// <summary>
		/// The last time output was collected
		/// </summary>
		int LastIndex { get; set; }

		/// <summary>
		/// Set up the servitor with the default settings
		/// </summary>
		public Servitor()
		{
			Address = IPAddress.Loopback;
			AllInput = new List<string>();
			LastIndex = 0;
			Changed = false;
			Port = 4206;
		}

		/// <summary>
		/// Create a new servitor given an IPAdress and a port.
		/// </summary>
		/// <param name="address">The IP address to listen to</param>
		/// <param name="port">The port to listen on</param>
		public Servitor(IPAddress address, int port){
			Address = address;
			AllInput = new List<string> ();
			LastIndex = 0;
			Port = port;
		}

		/// <summary>
		/// Gets all the input that the servitor has received since the last
		/// time it got any.
		/// </summary>
		/// <returns>The list of the new input</returns>
		public List<string> Collect()
		{
			lock (AllInput)
			{
				// It has not changed, because we just looked.
				Changed = false;
				// The index when this was triggered
				int oldIndex = LastIndex;
				// The last index is now. We just accessed it.
				LastIndex = AllInput.Count - 1;
				return Transformations.Subsequence(AllInput, oldIndex, AllInput.Count);
			}
		}
		/// <summary>
		/// Start the Servitor, listening on its thread.
		/// </summary>
		public void Start()
		{
			TcpListener listener = new TcpListener(Address, Port);
			listener.Start();

			// Continuously accept clients
			for(;/*ever*/;){
				TcpClient client = listener.AcceptTcpClient();
				NetworkStream nwStream = client.GetStream();

				var buffer = new byte[client.ReceiveBufferSize];
				int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
				string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);

				lock (AllInput)
				{
					AllInput.Add(dataReceived);
				}

				Changed = true;
			}
		}
	}
}