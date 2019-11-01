using HomeHubApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHubApp.Data
{
    public class Repository
    {
        private List<DeviceControlDto> _deviceControls;

        public Repository()
        {
            InitializeDeviceControls();
        }

        private void InitializeDeviceControls()
        {
            var tag = "GARDEN";
            _deviceControls = new List<DeviceControlDto>()
            {
                new DeviceControlDto(HubDeviceType.InsteonDevice, "28.FE.9D", "Office",tag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "28.EC.6A", "Media Spots", tag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "07.D5.93", "Garden Stairs Buttom", tag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1A.4E.54", "Garden Stairs Top", tag)
            };
        }

        public DeviceControlDto[] GetDeviceControls()
        {
            return _deviceControls.ToArray();
        }
    }
}
