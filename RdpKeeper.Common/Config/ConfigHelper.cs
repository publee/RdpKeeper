using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RdpKeeper.Common.Config
{
	public static class ConfigHelper
	{
		public static RdpConnectionElement[] GetRdpConnectionConfiguration()
		{
			var rdpConnectionConfiguration = (RdpConnectionSection)System.Configuration.ConfigurationManager.GetSection("rdpConnections");
			return rdpConnectionConfiguration.RdpConnections.Cast<RdpConnectionElement>().ToArray();
		}
	}
}
