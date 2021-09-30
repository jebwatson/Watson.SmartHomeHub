using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Watson.SmartHomeHub.Features.GetDevices
{
    public record DeviceCollection
    {
        public IEnumerable<Device> Devices { get; init; } = Enumerable.Empty<Device>();

        public string ResultMessage { get; init; } = "No Devices Found";

        public static DeviceCollection Empty() => new DeviceCollection();
        
        public static DeviceCollection FromJson(string json)
        {
            var devices = JsonConvert.DeserializeObject<List<Device>>(json);
            var collection = new DeviceCollection { Devices = devices ?? Enumerable.Empty<Device>(), };
            return collection;
        }
    }
}