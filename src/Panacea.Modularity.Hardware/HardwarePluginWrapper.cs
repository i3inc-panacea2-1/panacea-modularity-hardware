using Panacea.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modularity.Hardware
{
    class HardwarePluginWrapper : IHardwareManager
    {
        private readonly PanaceaServices _core;
        IHardwareManager _manager;
        public HardwarePluginWrapper(PanaceaServices core)
        {
            _core = core;
            _core.PluginLoader.PluginLoaded += PluginLoader_PluginLoaded;
            _core.PluginLoader.PluginUnloaded += PluginLoader_PluginUnloaded;
            foreach (var p in core.PluginLoader.LoadedPlugins.Where(p => p.Value is IHardwarePlugin).Select(p => p.Value))
            {
                PluginLoader_PluginLoaded(core.PluginLoader, p);
            }
        }

        private void PluginLoader_PluginUnloaded(object sender, IPlugin e)
        {
            if (e is IHardwarePlugin)
            {
                var manager = (e as IHardwarePlugin).GetHardwareManager();
                Detach(manager);
                _manager = null;
            }
        }

        void Detach(IHardwareManager manager)
        {
            manager.HandsetStateChanged -= Manager_HandsetStateChanged;
            manager.LcdButtonChange -= Manager_LcdButtonChange;
            manager.PowerButtonChange -= Manager_PowerButtonChange;
        }

        private void PluginLoader_PluginLoaded(object sender, IPlugin e)
        {
            if (e is IHardwarePlugin)
            {
                if (_manager != null)
                {
                    Detach(_manager);
                }
                var manager = (e as IHardwarePlugin).GetHardwareManager();
                manager.HandsetStateChanged += Manager_HandsetStateChanged;
                manager.LcdButtonChange += Manager_LcdButtonChange;
                manager.PowerButtonChange += Manager_PowerButtonChange;
                _manager = manager;
            }
        }

        private void Manager_PowerButtonChange(object sender, HardwareStatus e)
        {
            PowerButtonChange?.Invoke(this, e);
        }

        private void Manager_LcdButtonChange(object sender, HardwareStatus e)
        {
            LcdButtonChange?.Invoke(this, e);
        }

        private void Manager_HandsetStateChanged(object sender, HardwareStatus e)
        {
            HandsetStateChanged?.Invoke(sender, e);
        }

        public HardwareStatus LedState
        {
            get => _manager?.LedState ?? HardwareStatus.Off;
            set
            {
                if (_manager != null)
                {
                    _manager.LedState = value;
                }
            }
        }

        public HardwareStatus HandsetState
        {
            get => _manager?.HandsetState ?? HardwareStatus.On;
        }

        public HardwareStatus BarcodeScannerState
        {
            get => _manager?.BarcodeScannerState ?? HardwareStatus.Off;
            set
            {
                if (_manager != null)
                {
                    _manager.BarcodeScannerState = value;
                }
            }
        }

        public bool SpeakersDisabled
        {
            get => _manager?.SpeakersDisabled == true;
            set
            {
                if (_manager != null)
                {
                    _manager.SpeakersDisabled = value;
                }
            }
        }

        public event EventHandler<HardwareStatus> HandsetStateChanged;
        public event EventHandler<HardwareStatus> LcdButtonChange;
        public event EventHandler<HardwareStatus> PowerButtonChange;

        public void Restart()
        {

        }

        public void SetLcdBacklight(HardwareStatus status)
        {
            _manager?.SetLcdBacklight(status);
        }

        public void SimulateHandsetState(HardwareStatus status)
        {
            _manager?.SimulateHandsetState(status);
        }

        public void StartRinging()
        {
            _manager?.StartRinging();
        }

        public void StopRinging()
        {
            _manager.StopRinging();
        }
    }
}
