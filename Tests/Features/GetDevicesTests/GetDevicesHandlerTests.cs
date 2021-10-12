using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NUnit.Framework;
using Watson.SmartHomeHub.Features.GetDevices;
using Watson.SmartHomeHub.Features.HubitatHttpConnection;

namespace Tests.Features.GetDevicesTests
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
        public async Task WhenNonOkStatusCode_HandleReturnsDeviceCollection_WithEmptyDevices()
        {
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

        [Test]
        public async Task WhenDeviceNotSpecified_HandleReturnsDeviceCollection_WithAllDevices()
        {
            var uut = new GetDevicesHandler(_mediator.Object);
            var request = new GetDevicesQuery();
            HttpContent responseContent = new StringContent(
                "[{\"id\":\"1\",\"name\":\"hueBridge\",\"label\":\"Hue Bridge (6A7E0F)\",\"type\":\"hueBridge\",\"attributes\":[{\"name\":\"status\",\"currentValue\":\"Online\",\"dataType\":\"STRING\"}],\"capabilities\":[\"Refresh\"],\"commands\":[\"refresh\"]},{\"id\":\"2\",\"name\":\"hueBridge\",\"label\":\"Hue Bridge (6A7E0F)\",\"type\":\"hueBridge\",\"attributes\":[{\"name\":\"status\",\"currentValue\":\"Online\",\"dataType\":\"STRING\"}],\"capabilities\":[\"Refresh\"],\"commands\":[\"refresh\"]}]");

            var httpResponse = new HttpResponseMessage { Content = responseContent, };
            var response = new HubitatHttpResponse { Response = httpResponse, };
            var devices = new Device[]
            {
                new() { Id = "1", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
                new() { Id = "2", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
            };

            var collection = new DeviceCollection { Devices = devices, };
            var tokenSource = new CancellationTokenSource();

            _mediator
               .Setup(m => m.Send(It.IsAny<HubitatHttpQuery>(), tokenSource.Token))
               .ReturnsAsync(response);

            var result = await uut.Handle(request, tokenSource.Token);

            Assert.AreEqual(result, collection);
        }
    }
}