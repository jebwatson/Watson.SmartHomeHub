using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Watson.SmartHomeHub.Features.GetDeviceDetails
{
  public class DeviceDetailsHub : Hub
  {
#region Properties

    public static string Route => "/deviceDetailsHub";

#endregion

    public DeviceDetailsHub(IMediator mediator)
    {
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

#region Public Methods

    public async Task SendDeviceDetailsQuery(int deviceId)
    {
      // Get device details
      var deviceDetails = await _mediator.Send(new GetDeviceDetailsQuery(deviceId));

      // Send results to clients
      await Clients.All.SendAsync(nameof(GetDeviceDetailsQuery), deviceDetails);
    }

#endregion

#region Fields

    private readonly IMediator _mediator;

#endregion
  }
}