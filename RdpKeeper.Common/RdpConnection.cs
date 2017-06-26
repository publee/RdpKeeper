using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreeRDP;
using FreeRDP.Core;
using RdpKeeper.Common.Config;

namespace RdpKeeper.Common
{
    public class RdpConnection : IDisposable
    {
	    private readonly RdpConnectionElement _configuration;
	    private readonly IEventLogWrapper _eventLogWrapper;
	    private readonly RDP _rdp = new RDP();
	    private DateTime _disconnectTime;
		private DateTime _screensaverTime;

		public RdpConnection(RdpConnectionElement configuration, IEventLogWrapper eventLogWrapper)
	    {
		    if (configuration == null) throw new ArgumentNullException(nameof(configuration));
		    if (eventLogWrapper == null) throw new ArgumentNullException(nameof(eventLogWrapper));
		    _configuration = configuration;
		    _eventLogWrapper = eventLogWrapper;
		    _rdp.ErrorInfo += RdpOnErrorInfo;
			_rdp.Terminated += RdpOnTerminated;
		}

	    private void RdpOnErrorInfo(object sender, ErrorInfoEventArgs errorInfoEventArgs)
	    {
		    WriteLog(
			    string.Format(CultureInfo.CurrentCulture, "RDP Error {0} Info: {1}", errorInfoEventArgs.ErrorCode,
				    errorInfoEventArgs.ErrorInfoMessage),
			    EventLogEntryType.Error);
	    }

	    private void RdpOnTerminated(object sender, EventArgs eventArgs)
	    {
			_disconnectTime = DateTime.Now;
			WriteLog("RDP connection terminated", EventLogEntryType.Information);
		}



		public void Start()
	    {
			_screensaverTime = _disconnectTime = DateTime.Now;

			WriteLog(
				string.Format(CultureInfo.CurrentCulture, "RDP Connection start DesktopWidth:{0} DesktopHeight:{1}",
					_configuration.DesktopHeight, _configuration.DesktopWidth),
				EventLogEntryType.Information);

			try
			{
				_rdp.Connect(_configuration.HostName, _configuration.Domain, _configuration.UserName, _configuration.Password,
					_configuration.Port, new ConnectionSettings()
					{
						ColorDepth = 32,
						DesktopHeight = _configuration.DesktopHeight,
						DesktopWidth = _configuration.DesktopWidth
					});

				if (_configuration.SendEnterAfterLogon)
				{
					Thread.Sleep(2000);
					_rdp.SendInputKeyboardEvent(KeyboardFlags.KBD_FLAGS_DOWN, 28);
					Thread.Sleep(200);
					_rdp.SendInputKeyboardEvent(KeyboardFlags.KBD_FLAGS_RELEASE, 28);
				}
				WriteLog("RDP connection successful", EventLogEntryType.Information);
			}
			catch (Exception ex)
			{
				WriteLog("RDP connection failed: " + ex.ToString(), EventLogEntryType.Error);
			}
		}


		public void DoEvents()
		{
			if (!_rdp.Connected && (DateTime.Now - _disconnectTime).TotalMinutes >= _configuration.ReconnectTimeoutMins)
			{
				Start();
			}
			if (_rdp.Connected && (DateTime.Now - _screensaverTime).TotalMinutes >= _configuration.SendKeysTimeoutMins)
			{
				_screensaverTime = DateTime.Now;
				try
				{
					//send F15 to prevent screensaver
					_rdp.SendInputKeyboardEvent(KeyboardFlags.KBD_FLAGS_DOWN, 0x59);
					Thread.Sleep(200);
					_rdp.SendInputKeyboardEvent(KeyboardFlags.KBD_FLAGS_RELEASE, 0x59);
				}
				catch (Exception ex)
				{
					WriteLog("RDP send keys failed: " + ex.ToString(), EventLogEntryType.Error);
				}
			}
		}

		public void Stop()
	    {
		    if (_rdp.Connected)
		    {
			    WriteLog("RDP disconnecting", EventLogEntryType.Information);
			    try
			    {
				    _rdp.Disconnect();
			    }
			    catch (Exception ex)
			    {
				    WriteLog("RDP disconnect failed: " + ex.ToString(), EventLogEntryType.Error);
			    }
		    }
	    }

	    private void WriteLog(string message, EventLogEntryType type)
		{
			_eventLogWrapper.WriteEntry(
				string.Format(CultureInfo.CurrentCulture, "Host: {0} User:{1}\\{2} {3}", _configuration.HostName,
					_configuration.Domain, _configuration.UserName, message),
				type);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~RdpConnection()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// free managed resources
				_rdp.Dispose();
			}
			// free native resources if there are any.  
		}
    }
}
