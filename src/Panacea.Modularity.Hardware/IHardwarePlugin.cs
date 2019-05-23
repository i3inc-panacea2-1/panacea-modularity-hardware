using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modularity.Hardware
{
    public interface IHardwarePlugin : IPlugin
    {
        IHardwareManager GetHardwareManager();
    }
}
