using System;
using System.Diagnostics;
using RdpKeeper.Common;

namespace RdpKeeper.Console
{
	public class ConsoleEventLogWrapper : IEventLogWrapper
	{
		public void WriteEntry(string message, EventLogEntryType type)
		{
			System.Console.WriteLine("{0}\t{1}\t{2}", DateTime.Now, type, message);
		}
	}
}