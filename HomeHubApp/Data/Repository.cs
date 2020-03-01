using HomeHubApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHubApp.Data
{
    public class DeviceTag
    {
        public const string GardenTag = "GARDEN";
        public const string UpperTag = "UPPER";
        public const string MainTag = "MAIN";
    }

    public class Repository
    {
        private List<DeviceControlDto> _deviceControls;
        private const string GardenTag = DeviceTag.GardenTag;
        private const string UpperTag = DeviceTag.UpperTag;
        private const string MainTag = DeviceTag.MainTag;

        public Repository()
        {
            InitializeDeviceControls();
        }

        public DeviceControlDto[] GetDeviceControls(string tag = null)
        {
            if(tag == null)
            {
                return _deviceControls.ToArray();
            }

            return _deviceControls.Where(d => d.Tags.Contains(tag)).ToArray();
        }

        public string[] GetTags()
        {
            return new string[] { GardenTag, UpperTag, MainTag };
        }

        private void InitializeDeviceControls()
        {

            _deviceControls = new List<DeviceControlDto>()
            {
                new DeviceControlDto(HubDeviceType.InsteonDevice, "28.FE.9D", "Office",GardenTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "28.EC.6A", "Media Spots", GardenTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "19.3A.DB", "Media Center", GardenTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "07.D5.93", "Garden Stairs Bottom", GardenTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1A.4E.54", "Garden Stairs Top", GardenTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "29.00.79", "Guest Room", GardenTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1B.34.BF", "Loose", GardenTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "18.90.AC", "Garden Floor Lamp", GardenTag),

                new DeviceControlDto(HubDeviceType.InsteonDevice, "24.22.27", "Master overhead", UpperTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "14.CB.FB", "Master fan", UpperTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "29.00.BE", "Master bath", UpperTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "19.40.0F", "Master", UpperTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1A.5B.2F", "Main stair top", UpperTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "19.39.25", "Main stair pendant", UpperTag),

                new DeviceControlDto(HubDeviceType.InsteonDevice, "29.4D.7E", "Main stair bottom", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1B.3B.7D", "Living room cans", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1B.37.DC", "Kitchen Spots", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1C.9A.9E", "Kitchen Floods", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "24.7E.9C", "Garage Hallway", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "29.0C.4E", "Garage", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1C.A6.61", "Family Room Spots", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "29.0D.3C", "Family Room Sconce", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "12.23.F6", "Family Room Cans", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1B.5E.0A", "Family Room", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1A.5B.8B", "Entry Hanging Lamp", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1A.4D.45", "Entry Hallway", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1A.5D.AC", "Dining Spots", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1B.32.47", "Dining Table Lamp", MainTag),
                new DeviceControlDto(HubDeviceType.InsteonDevice, "1B.89.91", "Colored Lights", MainTag),


            };
        }

    }
}
