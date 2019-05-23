using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modularity.Hardware
{
    public interface IHardwareManager
    {
        HardwareStatus LedState { get; set; }
        HardwareStatus HandsetState { get; }
        HardwareStatus BarcodeScannerState { get; set; }
        bool SpeakersDisabled { get; set; }
        void StartRinging();
        void StopRinging();
        void SetLcdBacklight(HardwareStatus status);
        void SimulateHandsetState(HardwareStatus status);
        void Restart();
        event EventHandler<HardwareStatus> HandsetStateChanged;
        event EventHandler<HardwareStatus> LcdButtonChange;
        event EventHandler<HardwareStatus> PowerButtonChange;
    }
}
