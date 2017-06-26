using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RdpKeeper.Common.Config;

namespace RdpKeeper.Common
{
	public class RdpManager : IDisposable
	{
		private readonly IEventLogWrapper _eventLogWrapper;
		private RdpConnection[] _rdpConnections;
		private Thread _timerThread;
		private ManualResetEvent _stopThread = new ManualResetEvent(false);

		public RdpManager(IEventLogWrapper eventLogWrapper)
		{
			if (eventLogWrapper == null) throw new ArgumentNullException(nameof(eventLogWrapper));
			_eventLogWrapper = eventLogWrapper;
			_rdpConnections = ConfigHelper.GetRdpConnectionConfiguration().Select(c => new RdpConnection(c, eventLogWrapper)).ToArray();
		}

		public void StartAll()
		{
			var tasks = _rdpConnections.Select(x =>
				Task.Factory.StartNew(x.Start)).ToArray();

			Task.WaitAll(tasks);

			_stopThread.Reset();
			_timerThread = new Thread(TimerJob);
			_timerThread.Start();
		}

		private void TimerJob()
		{
			while (!_stopThread.WaitOne(1000))
			{
				foreach (var rdpConnection in _rdpConnections)
				{
					rdpConnection.DoEvents();
				}
			}
		}

		public void StopAll()
		{
			_stopThread.Set();
			_timerThread.Join();
			_stopThread.Dispose();

			foreach (var rdpConnection in _rdpConnections)
			{
				rdpConnection.Stop();
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~RdpManager()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// free managed resources
				foreach (var rdpConnection in _rdpConnections)
				{
					rdpConnection.Dispose();
				}
				_stopThread.Dispose();
			}
			// free native resources if there are any.  
		}
	}
}
