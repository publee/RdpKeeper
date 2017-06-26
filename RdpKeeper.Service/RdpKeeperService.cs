using System.ServiceProcess;
using System.Diagnostics;
using RdpKeeper.Common;

namespace RdpKeeper
{
	public sealed partial class RdpKeeperService : ServiceBase
	{
		private RdpManager _rdpManager;

		public RdpKeeperService()
		{
			InitializeComponent();
			_rdpManager = new RdpManager(new EventLogWrapper(this.EventLog));
		}

		protected override void OnStart(string[] args)
		{
			_rdpManager.StartAll();
		}

		protected override void OnStop()
		{
			_rdpManager.StopAll();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_rdpManager.Dispose();
				components?.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
