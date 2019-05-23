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
        public static IHardwareManager GetHardwareManager(this PanaceaServices core)
        {
            if (_instance != null)
            {
                lock (_instance)
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
