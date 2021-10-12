using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Watson.SmartHomeHub.Features.HubitatHttpConnection;

namespace Watson.SmartHomeHub.Features.GetDeviceDetails
{
  public class GetDeviceDetailsHandler : IRequestHandler<GetDeviceDetailsQuery, DeviceDetails>
  {
    private readonly IMediator _mediator;

    public GetDeviceDetailsHandler(IMediator mediator)
    {
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
#region Public Methods

    public async Task<DeviceDetails> Handle(GetDeviceDetailsQuery request, CancellationToken cancellationToken)
    {
      var requestUri = new StringBuilder()
                      .Append("apps/2/devices/")
                      .Append($"{request.DeviceId}/")
                      .ToString();

      var response = await _mediator.Send(new HubitatHttpQuery { Uri = requestUri, }, cancellationToken);

      if (response.Response?.StatusCode != HttpStatusCode.OK)
      {
        return new DeviceDetails();
      }

      var parsedResponse = await response.Response.Content.ReadAsStringAsync(cancellationToken);

      return DeviceDetails.FromJson(parsedResponse);
    }

#endregion
  }
}