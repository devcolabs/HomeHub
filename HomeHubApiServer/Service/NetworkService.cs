using Insteon.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace HomeHubApiServer.Service
{
    public class NetworkService
    {
        #region Fields

        private readonly Subject<ConnectionStatus> _connectionStatusSubject = new Subject<ConnectionStatus>();

        private readonly Subject<InsteonDeviceList> _devicesSubject = new Subject<InsteonDeviceList>();

        #endregion

        #region Creationg 

        public NetworkService(InsteonNetwork insteonNetwork)
        {
            Network = insteonNetwork;
        }

        #endregion

        #region Properties

        private void Devices_DeviceAdded(object sender, InsteonDeviceEventArgs data)
        {
            _devicesSubject.OnNext(Network.Devices);
        }

        public IObservable<ConnectionStatus> Connection
        {
            get { return _connectionStatusSubject; }
        }

        public InsteonNetwork Network { get; private set; }

        public InsteonDeviceLinkRecord[] Links { get; private set; }

        public IObservable<InsteonDeviceList> DevicesObserable { get { return _devicesSubject.AsObservable(); } }

        public bool TryCommand(InsteonAddress address, InsteonDeviceCommands command, byte data)
        {
            var device = Network.Devices.FirstOrDefault(a => a.Address.Equals(address));
            return device?.TryCommand(command, data) ?? false;
        }

        public bool TryGroupCommand(InsteonControllerGroupCommands command, byte group, byte data)
        {
            return Network.Controller.TryGroupCommand(command, group, data);
        }

        public bool TryGetOnLevel(InsteonAddress address, out byte level)
        {
            level = 0;
            var device = Network.Devices.FirstOrDefault(a => a.Address.Equals(address));
            return device?.TryGetOnLevel(out level) ?? false;
        }

        #endregion

        #region Methods

        public void Connect(string comPort = null)
        {
            if(comPort == null)
            {
                comPort = "COM5";
            }

            var addr = new InsteonAddress(0x49, 0xFA, 0xED);
            var plm = new InsteonConnection(InsteonConnectionType.Serial, comPort, "POWDERHORN", addr);


            Network.AutoAdd = true;

            bool ok = Network.TryConnect(plm);

            Links = Network.Controller.GetLinks();

            // add devices
            Links.ToList().ForEach(link =>
            {
                if (!Network.Devices.ContainsKey(link.Address))
                {
                    Network.Devices.Add(new InsteonAddress(link.Address.Value), new InsteonIdentity());
                }
            });

            // fire connection status
            var status = new ConnectionStatus() { Connected = ok, Links = Links };
            _connectionStatusSubject.OnNext(status);

            // fire device list
            _devicesSubject.OnNext(Network.Devices);

            // monitor for changes to device list
            Network.Devices.DeviceAdded += Devices_DeviceAdded;
        }

        public IObservable<bool> GetLightOnOffObservable(InsteonAddress address, byte group = 1)
        {
            var device = DeviceFromAddress(address);
            return device.InsteonMessageObservable
                .Where(
                    m =>
                        (m.MessageType == InsteonMessageType.OnCleanup ||
                        m.MessageType == InsteonMessageType.OffCleanup) &&
                        m.Properties[PropertyKey.Group] == group)
                .Select(m => m.MessageType == InsteonMessageType.OnCleanup);
        }

        public IObservable<bool> GetLightFastOnOffObservable(InsteonAddress address, byte group = 1)
        {
            var device = DeviceFromAddress(address);
            return device.InsteonMessageObservable
                .Where(
                    m =>
                        (m.MessageType == InsteonMessageType.FastOnCleanup ||
                        m.MessageType == InsteonMessageType.FastOffCleanup) &&
                        m.Properties[PropertyKey.Group] == group)
                .Select(m => m.MessageType == InsteonMessageType.FastOffBroadcast);
        }

        public InsteonDevice DeviceFromAddress(InsteonAddress address)
        {
            return Network.Devices.Find(address);
        }

        #endregion

        #region Helpers



        #endregion
    }

    public class ConnectionStatus
    {
        public bool Connected { get; set; }

        public InsteonDeviceLinkRecord[] Links { get; set; }
    }
}
