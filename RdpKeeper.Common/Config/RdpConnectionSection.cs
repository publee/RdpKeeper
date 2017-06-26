using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RdpKeeper.Common.Config
{
	public class RdpConnectionSection : ConfigurationSection
	{

		[ConfigurationProperty("", IsDefaultCollection = true)]
		[ConfigurationCollection(typeof(RdpConnectionCollection), AddItemName = "rdpConnection")]
		public RdpConnectionCollection RdpConnections
		{
			get
			{
				RdpConnectionCollection coll = (RdpConnectionCollection)base[""];
				return coll;
			}
		}
	}

	public class RdpConnectionCollection : ConfigurationElementCollection
	{

		protected override ConfigurationElement CreateNewElement()
		{
			return new RdpConnectionElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			RdpConnectionElement connectionElement = element as RdpConnectionElement;

			var ret = string.Format(CultureInfo.InvariantCulture, "{0}|{1}", connectionElement.HostName, connectionElement.UserName);
			return ret;
		}
	}

	public class RdpConnectionElement : ConfigurationElement
	{
		[ConfigurationProperty("hostname", IsRequired = true)]
		public string HostName
		{
			get { return (string) this["hostname"]; }
			set { this["hostname"] = value; }
		}

		[ConfigurationProperty("username", IsRequired = false)]
		public string UserName
		{
			get { return (string)this["username"]; }
			set { this["username"] = value; }
		}

		[ConfigurationProperty("domain", IsRequired = false)]
		public string Domain
		{
			get { return (string)this["domain"]; }
			set { this["domain"] = value; }
		}

		[ConfigurationProperty("password", IsRequired = false)]
		public string Password
		{
			get { return (string)this["password"]; }
			set { this["password"] = value; }
		}

		[ConfigurationProperty("port", DefaultValue = "3389", IsRequired = false)]
		public int Port
		{
			get { return (int)this["port"]; }
			set { this["port"] = value; }
		}

		[ConfigurationProperty("desktopWidth", DefaultValue = "1024", IsRequired = false)]
		public int DesktopWidth
		{
			get { return (int) this["desktopWidth"]; }
			set { this["desktopWidth"] = value; }
		}

		[ConfigurationProperty("desktopHeight", DefaultValue = "768", IsRequired = false)]
		public int DesktopHeight
		{
			get { return (int)this["desktopHeight"]; }
			set { this["desktopHeight"] = value; }
		}

		[ConfigurationProperty("sendEnterAfterLogon", DefaultValue = false, IsRequired = false)]
		public bool SendEnterAfterLogon
		{
			get { return (bool)this["sendEnterAfterLogon"]; }
			set { this["sendEnterAfterLogon"] = value; }
		}

		[ConfigurationProperty("reconnectTimeoutMins", DefaultValue = "5", IsRequired = false)]
		public int ReconnectTimeoutMins
		{
			get { return (int)this["reconnectTimeoutMins"]; }
			set { this["reconnectTimeoutMins"] = value; }
		}

		[ConfigurationProperty("sendKeysTimeoutMins", DefaultValue = "1", IsRequired = false)]
		public int SendKeysTimeoutMins
		{
			get { return (int)this["sendKeysTimeoutMins"]; }
			set { this["sendKeysTimeoutMins"] = value; }
		}
	}

}
