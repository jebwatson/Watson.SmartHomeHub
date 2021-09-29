using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Watson.SmartHomeHub.Features.GetDevices
{
    public class DeviceHub : Hub
    {
        private readonly IMediator _mediator;

        public DeviceHub(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Sends a query to all clients to get a list of available devices.
        /// </summary>
        /// <param name="id">If id is specified, device list will be filtered on id.</param>
        public async Task SendDeviceQuery(string id = "")
        {
            // Get the devices
            var devices = await _mediator.Send(new GetDevicesQuery(id));

            // Send results to clients
            await Clients.All.SendAsync("GetDevices", devices);
        }
    }
}