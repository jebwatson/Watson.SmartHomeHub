using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Watson.SmartHomeHub.Features.GetDevices
{
    public sealed record DeviceCollection
    {
        public IEnumerable<Device> Devices { get; init; } = Enumerable.Empty<Device>();

        public string ResultMessage { get; init; } = "No Devices Found";

        public static DeviceCollection Empty() => new();
        
        public static DeviceCollection FromJson(string json)
        {
            var devices = JsonConvert.DeserializeObject<List<Device>>(json);
            var collection = new DeviceCollection { Devices = devices ?? Enumerable.Empty<Device>(), };
            return collection;
        }
        
        public bool Equals(DeviceCollection? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.Devices.Count() != Devices.Count()) return false;
            
            var theseDevices = Devices.ToArray();
            var thoseDevices = other.Devices.ToArray();
            
            for (var i = 0; i < Devices.Count(); i++)
            {
                if (!theseDevices[i].Equals(thoseDevices[i])) return false;
            }
            
            return ResultMessage == other.ResultMessage;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Devices, ResultMessage);
        }
    }
}