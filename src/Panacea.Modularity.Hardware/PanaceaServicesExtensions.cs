using Panacea.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modularity.Hardware
{
    public static class PanaceaServicesExtensions
    {
        static IHardwareManager _instance;
        static object _lock = new object();
        public static IHardwareManager GetHardwareManager(this PanaceaServices core)
        {
            if (_instance != null)
            {
                lock (_lock)
                {
                    if (_instance != null)
                    {
                        _instance = new HardwarePluginWrapper(core);
                    }
                }
            }
            return _instance;
        }
    }
}
