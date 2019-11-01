using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHubApp.Dto
{
    public enum HubDeviceType {
        InsteonDevice,
        InsteonGroup,
    }

    public enum DeviceStatus
    {
        On,
        Off,
    }

    public class DeviceControlDto
    {
        public DeviceControlDto(HubDeviceType deviceType, string identifier, string name, string tags, string description=null )
        {
            DeviceType = deviceType;
            Identifier = identifier;
            Name = name;
            Description = description == null ? name : description;
            Tags = tags;
        }

        public HubDeviceType DeviceType { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public DeviceStatus DeviceStatus { get; set; } = DeviceStatus.Off;
        public int Level { get; set; }
    }
}

