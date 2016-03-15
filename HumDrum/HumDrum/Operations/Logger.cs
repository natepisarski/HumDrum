using System;
using System.IO;

namespace HumDrum.Operations
{
	/// <summary>
	/// Logger is the class responsible for logging output to a 
	/// stream, which is usually a file or standard output.
	/// </summary>
	public class Logger
	{
		/// <summary>
		/// The place where this logger will writer its information
		/// </summary>
		Stream OutputStream;

		/// <summary>
		/// Create a file from a stream
		/// </summary>
		/// <param name="outputStream">Any stream. Usually StandardOutput or a file stream</param>
		public Logger (Stream outputStream)
		{
			this.OutputStream = outputStream;
		}

		/// <summary>
		/// Create a logger with a specified file as its output stream.
		/// </summary>
		/// <param name="logFile">The path to the file to write to</param>
		public Logger(string path){
			OutputStream = new FileStream (path, FileMode.Append);
		}

		/// <summary>
		/// Log a piece of data in its raw, exact format
		/// </summary>
		/// <param name="data">The data to log to the output stream</param>
		public void LogRaw(string data){
			var writer = new StreamWriter (OutputStream);
			writer.Write (data);
			writer.Flush ();
		}

		/// <summary>
		/// Log a particular moment in time
		/// </summary>
		/// <param name="data">The data to write to the log</param>
		/// <param name="time">The time to use for the timestamp</param>
		public void Log(string data, DateTime time){
			LogRaw ("["+time.ToLongTimeString () + "]: " + data+"\n");
		}

		/// <summary>
		/// Log the data, using the current time as the timestamp
		/// </summary>
		/// <param name="data">The data to write to the log file</param>
		public void Log(string data){
			Log (data, DateTime.Now);
		}

		/// <summary>
		/// Create a warning with the given data
		/// </summary>
		/// <param name="data">The data to warn about</param>
		public void Warn(string data){
			Log ("WARNING: " + data);
		}
	}
}
