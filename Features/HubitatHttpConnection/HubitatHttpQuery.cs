using MediatR;

namespace Watson.SmartHomeHub.Features.HubitatHttpConnection
{
    public class HubitatHttpQuery : IRequest<HubitatHttpResponse>
    {
        public string? Uri { get; init; }
    }
}