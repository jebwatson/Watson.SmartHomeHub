using MediatR;

namespace Watson.SmartHomeHub.Features.GetDeviceDetails
{
  public record GetDeviceDetailsQuery : IRequest<DeviceDetails>
  {
    public string DeviceId { get; } = "";

    public GetDeviceDetailsQuery(string deviceId)
    {
      DeviceId = deviceId;
    }
  }
}