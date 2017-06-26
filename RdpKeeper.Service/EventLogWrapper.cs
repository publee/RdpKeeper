using System;
using System.Diagnostics;
using RdpKeeper.Common;

namespace RdpKeeper
{
	public class EventLogWrapper : IEventLogWrapper
	{
		private readonly EventLog _eventLog;

		public EventLogWrapper(EventLog eventLog)
		{
			if (eventLog == null) throw new ArgumentNullException(nameof(eventLog));
			_eventLog = eventLog;
		}

		public void WriteEntry(string message, EventLogEntryType type)
		{
			_eventLog.WriteEntry(message, type);
		}
	}
}