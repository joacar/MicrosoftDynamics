using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicrosoftDynamics.Tests
{
    [TestClass]
    public class AxTimeZoneMapper2012Fixture
    {
        [TestMethod]
        public void ConvertToAx_Exists()
        {
            // Arrange
            var mapper = DynamicsAxTimeZoneManager.Current.Create(DynamicsAxVersion.Ax2012);
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            // Act
            var obj = mapper.ConvertToAx(timeZone.StandardName);
            // Assert
            Assert.IsNotNull(obj);
            Assert.AreEqual(79, obj.Value);
        }

        [TestMethod]
        public void ConveryFromAx_Exists()
        {
            // Arrange
            var mapper = DynamicsAxTimeZoneManager.Current.Create(DynamicsAxVersion.Ax2012);
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            // Act
            var obj = mapper.ConvertFromAx("GMTPLUS0100_AMSTERDAM_BERLIN_BERN_ROME");
            // Assert
            Assert.IsNotNull(obj);
            Assert.AreEqual(obj.Id, timeZone.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConvertToAx_NotExists()
        {
            // Arrange
            var mapper = DynamicsAxTimeZoneManager.Current.Create(DynamicsAxVersion.Ax2012);
            const string displayName = "Something ridiculous";
            // Act
            mapper.ConvertToAx(displayName);
            // Assert
            // Exception thrown
        }
    }
}
