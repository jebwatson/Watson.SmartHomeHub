using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Watson.SmartHomeHub.Features.HubitatHttpConnection;

namespace Watson.SmartHomeHub.Features.GetDevices
{
    /// <summary>
    /// Gets a collection of devices from hubitat.
    /// </summary>
    public class GetDevicesHandler : IRequestHandler<GetDevicesQuery, DeviceCollection>
  {
    public GetDevicesHandler(IMediator mediator)
    {
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

#region Public Methods

    public async Task<DeviceCollection> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
    {
      var requestUri = new StringBuilder()
                      .Append("apps/2/devices")
                      .ToString();

      var response = await _mediator.Send(new HubitatHttpQuery { Uri = requestUri, }, cancellationToken);

      if (response.Response?.StatusCode != HttpStatusCode.OK)
      {
        return new DeviceCollection { ResultMessage = "No Devices Found", };
      }

      var parsedResponse = await response.Response.Content.ReadAsStringAsync(cancellationToken);

      return DeviceCollection.FromJson(parsedResponse);
    }

#endregion

#region Fields

    private readonly IMediator _mediator;

#endregion
  }
}