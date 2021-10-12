using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Watson.SmartHomeHub.Features.GetDeviceDetails
{
  public class DeviceDetailsHub : Hub
  {
    private readonly IMediator _mediator;

    public static string Route => "/deviceDetailsHub";

    public DeviceDetailsHub(IMediator mediator)
    {
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task SendDeviceDetailsQuery(string deviceId)
    {
      // Get device details
      var deviceDetails = await _mediator.Send(new GetDeviceDetailsQuery(deviceId));
      
      // Send results to clients
      await Clients.All.SendAsync(nameof(GetDeviceDetailsQuery), deviceDetails);
    }
  }
}