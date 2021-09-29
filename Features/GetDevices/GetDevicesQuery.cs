using MediatR;

namespace Watson.SmartHomeHub.Features.GetDevices
{
    public record GetDevicesQuery : IRequest<DeviceCollection>
    {
        public string Id { get; }

        public GetDevicesQuery(string id = "")
        {
            Id = id;
        }
    }
}