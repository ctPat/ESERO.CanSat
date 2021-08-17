using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESERO.CanSat
{
	//***********************************
	// Implement Singleton design pattern
	//***********************************
	class SingletonMB
	{
		private SingletonMB() { }

		private static SingletonMB _instance;

		private static readonly object _lock = new();

		public static SingletonMB GetInstance(fMicroBit mb)
		{
			if (_instance == null)
			{
				// Monitor Class
				lock (_lock)
				{
					if (_instance == null)
					{
						_instance = new SingletonMB();
					}
				}
			}
			return _instance;
		}		
	}
}
