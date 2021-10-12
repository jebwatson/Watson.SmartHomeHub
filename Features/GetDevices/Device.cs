using Newtonsoft.Json;

namespace Watson.SmartHomeHub.Features.GetDevices
{
    public record Device
    {
        public string? Id { get; init; }
        public string? Name { get; init; }
        public string? Label { get; init; }
        public string? Type { get; init; }

        public override string ToString()
        {
            return $"Device\nId: {Id}\nName: {Name}\nLabel: {Label}\nType: {Type}\n";
        }

        public static Device FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Device>(json) ?? new Device();
        }
    }
}