using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NUnit.Framework;
using Watson.SmartHomeHub.Features.GetDevices;
using Watson.SmartHomeHub.Features.HubitatHttpConnection;

namespace Watson.SmartHomeHub.Tests.GetDevicesTests
{
    [TestFixture]
    public class GetDevicesHandlerTests
    {
        private Mock<IMediator> _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task HandleReturnsEmptyDeviceCollectionOnNonOkStatusCode()
        {
            Debug.Assert(_mediator != null, nameof(_mediator) + " != null");

            var uut = new GetDevicesHandler(_mediator.Object);
            var request = new GetDevicesQuery();
            var response = new HubitatHttpResponse { Response = new HttpResponseMessage(HttpStatusCode.Accepted), };
            var collection = DeviceCollection.Empty();
            var tokenSource = new CancellationTokenSource();

            _mediator
               .Setup(m => m.Send(It.IsAny<HubitatHttpQuery>(), tokenSource.Token))
               .ReturnsAsync(response);

            var result = await uut.Handle(request, tokenSource.Token);

            Assert.AreEqual(result, collection);
        }
    }
}