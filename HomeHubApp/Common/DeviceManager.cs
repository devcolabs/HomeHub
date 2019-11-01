using HomeHubApp.Data;
using HomeHubApp.Dto;
using Insteon.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PubSub.Extension;

namespace HomeHubApp.Common
{
    public class DeviceManagerInitDonePub
    {
    }

    public class DeviceManager
    {
        private DeviceControlDto[] _devices;
        private Repository _repository;
        private HubClientService _hubClientService;

        public DeviceManager(Repository repository, HubClientService hubClientService )
        {
            _repository = repository;
            _hubClientService = hubClientService;
        }

        public async Task ToggleAsync(DeviceControlDto device)
        {
            if( device.DeviceStatus == DeviceStatus.On )
            {
                await _hubClientService.SendCommnadRequestAsync(device.Identifier, "20");
                device.DeviceStatus = DeviceStatus.Off;
            }
            else
            {
                await _hubClientService.SendCommnadRequestAsync(device.Identifier, "18");
                device.DeviceStatus = DeviceStatus.On;
            }
        }


        public async Task InitializeAsync()
        {
            _devices = _repository.GetDeviceControls();
            foreach(var d in _devices)
            {
                await ReadAndUpdateStatus(d);
            }

            await this.PublishAsync<DeviceManagerInitDonePub>();
        }

        public async Task ReadAndUpdateStatus(DeviceControlDto device)
        {
            if(device.DeviceType == HubDeviceType.InsteonDevice)
            {
                var status = await _hubClientService.SendStatusRequestAsync(device.Identifier);
                device.DeviceStatus = (status.Level == 0) ? DeviceStatus.Off : DeviceStatus.On;
                device.Level = status.Level;
            }
        }
    }
}
