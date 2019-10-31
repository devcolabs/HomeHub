using Insteon.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHubApiServer.Service
{
    public class HubService
    {
        private NetworkService _networkService;

        public HubService(NetworkService networkService)
        {
            _networkService = networkService;
        }

        public bool ProcessCommand(string addressText, string commandText, string dataText)
        {
            // allowable command strings:
            // EnterLinkingMode
            // EnterUnlinkingMode
            // IDRequest
            // On
            // FastOn
            // Off
            // FastOff
            // Brighten
            // Dim
            // StartDimming
            // StopDimming
            // StatusRequest

            string message = string.Empty;

            // parse parameters
            InsteonAddress address;
            var addresOk = TryParserAddress(addressText, out address);

            if (!addresOk)
            {
                message = $"Address={addressText} is invalid";
                return false;
            }

            InsteonDeviceCommands command;
            var idOk = TryParseCommand( commandText, out command);

            if (!idOk)
            {
                message = $"Id={commandText} is invalid";
                return false;
            }

            byte data;
            var dataOk = TryParseData(dataText, out data);

            if (!dataOk)
            {
                message = $"Data={dataText} is invalid";
                return false;
            }

            // send command
            var sendOk = _networkService.TryCommand(address, command, data);

            if (!sendOk)
            {
                message = "Failed to send command";
            }

            return sendOk;

        }

        public bool ProcessGroup(string groupText, string commandText, string dataText)
        {
            var message = string.Empty;

            // parse parameters
            byte group;
            bool groupOk = TryParseGroup(groupText, out group);

            if (!groupOk)
            {
                message = $"Group={groupText} is invalid";
                return false;
            }

            InsteonControllerGroupCommands command;
            var idOk = TryParseGroupCommand(commandText, out command);

            if (!idOk)
            {
                message = $"Id={commandText} is invalid";
                return false;
            }

            byte data;
            var dataOk = TryParseData(dataText, out data);

            if (!dataOk)
            {
                message = $"Data={dataText} is invalid";
                return false;
            }

            // send command
            var sendOk = _networkService.TryGroupCommand(command, group, data);

            if (!sendOk)
            {
                message = "Failed to send group command";
            }

            return sendOk;

        }

        public int ProcessStatus(string addressText)
        {
            var message = string.Empty;

            // parse parameters
            InsteonAddress address;
            var addresOk = TryParserAddress(addressText, out address);

            if (!addresOk)
            {
                message = $"Address={addressText} is invalid";
                return -1;
            }

            // send command
            byte level;
            var sendOk = _networkService.TryGetOnLevel(address, out level);
            message = sendOk ? $"value={level}" : "Failed to send command";

            return sendOk ? level : -1;
        }

        private bool TryParseGroup(string dataText, out byte group)
        {
            return byte.TryParse(dataText, out group);
        }

        private bool TryParserAddress(string addressText, out InsteonAddress address)
        {
            return InsteonAddress.TryParse(addressText, out address);
        }

        private bool TryParseGroupCommand(string idText, out InsteonControllerGroupCommands command)
        {
            return Enum.TryParse(idText, out command);
        }

        private bool TryParseCommand(string idText, out InsteonDeviceCommands command)
        {
            return Enum.TryParse(idText, out command);
        }

        private bool TryParseData(string dataText, out byte data)
        {
            return byte.TryParse(dataText, out data);
        }
    }


}
