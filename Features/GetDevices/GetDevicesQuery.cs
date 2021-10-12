using MediatR;

namespace Watson.SmartHomeHub.Features.GetDevices
{
  public record GetDevicesQuery : IRequest<DeviceCollection> { }
}