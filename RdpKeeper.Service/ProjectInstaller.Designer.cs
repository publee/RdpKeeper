namespace RdpKeeper
{
	partial class ProjectInstaller
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.RdpKeeperServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this.RdpKeeperServiceInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// RdpKeeperServiceProcessInstaller
			// 
			this.RdpKeeperServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
			this.RdpKeeperServiceProcessInstaller.Password = null;
			this.RdpKeeperServiceProcessInstaller.Username = null;
			// 
			// RdpKeeperServiceInstaller
			// 
			this.RdpKeeperServiceInstaller.Description = "Service to keep open RDP connections for UI automation testing";
			this.RdpKeeperServiceInstaller.DisplayName = "RDP Keeper";
			this.RdpKeeperServiceInstaller.ServiceName = "RDPKeeper";
			this.RdpKeeperServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.RdpKeeperServiceProcessInstaller,
            this.RdpKeeperServiceInstaller});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller RdpKeeperServiceProcessInstaller;
		private System.ServiceProcess.ServiceInstaller RdpKeeperServiceInstaller;
	}
}