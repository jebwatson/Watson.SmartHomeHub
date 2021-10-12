using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Watson.SmartHomeHub.Features.GetDevices
{
    public class DeviceHub : Hub
    {
        private readonly IMediator _mediator;

        public static string Route => "/deviceHub";

        public DeviceHub(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Sends a query to all clients to get a list of available devices.
        /// </summary>
        public async Task SendDeviceQuery()
        {
            // Get the devices
            var devices = await _mediator.Send(new GetDevicesQuery());

            // Send results to clients
            await Clients.All.SendAsync("GetDevices", devices);
        }
    }
}