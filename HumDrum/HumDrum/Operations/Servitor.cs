using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;

using HumDrum.Collections;
using HumDrum.Structures;

namespace HumDrum.Operations
{
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
		/// Binds the string that a client transfered to the servitor to the client itself.
		/// </summary>
		/// <value>The logbook</value>
		List<Tuple<string, TcpClient>> Logbook {get; set;}

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
		/// Gets or sets the IO table.
		/// The IO Table determines, given an input, what to send to the server.
		/// </summary>
		/// <value>The IO table</value>
		public BindingsTable<string, string> IOTable{ get; set;}

		/// <summary>
		/// The default port to that Servitor listens to
		/// </summary>
		public const int DEFAULT_PORT = 4206;

		/// <summary>
		/// Set up the servitor with the default settings.
		/// </summary>
		public Servitor()
		{
			Address = IPAddress.Loopback;
			AllInput = new List<string>();
			LastIndex = 0;
			Changed = false;
			Port = DEFAULT_PORT;
			IOTable = new BindingsTable<string, string> ();
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
			IOTable = new BindingsTable<string, string> ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Operations.Servitor"/> class.
		/// This allows the creation of a Servitor instance with an IOTable
		/// </summary>
		/// <param name="address">The address to listen to</param>
		/// <param name="port">The port to listen to</param>
		/// <param name="table">The table of inputs and outputs</param>
		public Servitor(IPAddress address, int port, BindingsTable<string, string> table) : this(address, port)
		{
			IOTable = table;
		}

		/// <summary>
		/// Gets all the input that the servitor has received since the last
		/// time it got any.
		/// </summary>
		/// <returns>The list of the new input</returns>
		public async Task<IEnumerable<string>> Collect()
		{
			lock (AllInput)
			{
				// It has not changed, because we just looked.
				Changed = false;

				// The index when this was triggered
				int oldIndex = LastIndex;

				// The last index is now. We just accessed it.
				LastIndex = AllInput.Count - 1;

				var r = Transformations.Subsequence(AllInput, oldIndex, AllInput.Count);
				return r;
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

				// Something is connecting
				TcpClient client = listener.AcceptTcpClient();

				// Make a stream of it so we can IO with it
				NetworkStream nwStream = client.GetStream();

				// Make an array equal to the message size
				var buffer = new byte[client.ReceiveBufferSize];

				// NetworkStream.Read returns the size of the message
				int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

				// Convert this buffer to a string so we can work with it
				string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);

				// If the IOTable defines this interaction, reply how it wants.
				if (IOTable.Has (dataReceived)) {
					var clientWriter = new StreamWriter (nwStream);
					foreach (string s in IOTable.Lookup(dataReceived)) {
						clientWriter.WriteLine (s);
					}
					clientWriter.Close ();
				}

				nwStream.Close ();

				lock (AllInput)
				{
					AllInput.Add(dataReceived);
				}

				Changed = true;
			}
		}

		/// <summary>
		/// Sends the specified data to the port at address
		/// </summary>
		/// <param name="data">The data to send as a string</param>
		/// <param name="address">The address to send the data to</param>
		/// <param name="port">The port to which the data should be sent</param>
		public static void Send(string data, string host, int port)
		{
			TcpClient client = new TcpClient (host, port);
			NetworkStream ns = client.GetStream ();
			StreamWriter s = new StreamWriter (ns);
			s.WriteLine (data);
			s.Close ();
		}

		/// <summary>
		/// Turns a TcpClient into text
		/// </summary>
		/// <returns>The text encoded in the stream</returns>
		/// <param name="ns">The TcpClient</param>
		public static string LineFromClient(TcpClient client)
		{
			NetworkStream nwStream = client.GetStream ();
			// Make an array equal to the message size
			var buffer = new byte[client.ReceiveBufferSize];

			// NetworkStream.Read returns the size of the message
			int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

			// Convert this buffer to a string so we can work with it
			string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);

			return dataReceived;
		}
	}
}