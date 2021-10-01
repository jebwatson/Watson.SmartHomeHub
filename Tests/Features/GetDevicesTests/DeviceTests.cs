using NUnit.Framework;
using Watson.SmartHomeHub.Features.GetDevices;

namespace Tests.Features.GetDevicesTests
{
    [TestFixture]
    public class DeviceTests
    {
        [Test]
        public void ToStringReturnsExpectedValue()
        {
            var device = new Device { Id = "1", Name = "test", Label = "test", Type = "test", };
            var result = device.ToString();
            
            Assert.AreEqual(result, "Device\nId: 1\nName: test\nLabel: test\nType: test\n");
        }
    }
}