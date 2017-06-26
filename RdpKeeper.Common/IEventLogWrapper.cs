using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RdpKeeper.Common
{
	public interface IEventLogWrapper
	{
		void WriteEntry(string message, EventLogEntryType type);
	}
}
