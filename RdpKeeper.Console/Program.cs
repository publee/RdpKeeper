using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RdpKeeper.Common;
using RdpKeeper.Common.Config;

namespace RdpKeeper.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var rdpManager = new RdpManager(new ConsoleEventLogWrapper());
			rdpManager.StartAll();

			System.Console.WriteLine("Press any key to stop all connections");
			System.Console.ReadKey();

			rdpManager.StopAll();

			System.Console.WriteLine("Press any key to quit");
			System.Console.ReadKey();
		}
	}
}
