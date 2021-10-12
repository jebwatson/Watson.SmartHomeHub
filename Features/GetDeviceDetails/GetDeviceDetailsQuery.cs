using MediatR;

namespace Watson.SmartHomeHub.Features.GetDeviceDetails
{
  public record GetDeviceDetailsQuery : IRequest<DeviceDetails>
  {
    public int DeviceId { get; }

    public GetDeviceDetailsQuery(int deviceId)
    {
      DeviceId = deviceId;
    }
  }
}