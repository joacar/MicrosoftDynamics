using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicrosoftDynamics.Tests
{
    [TestClass]
    public class AxTimeZoneMapper2012Fixture
    {
        [TestMethod]
        public void Convert_Exists()
        {
            // Arrange
            var mapper = DynamicsAxTimeZoneManager.Current.Create(DynamicsAxVersion.Ax2012);
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            // Act
            var obj = mapper.ConvertToAx(timeZone.DisplayName);
            // Assert
            Assert.IsNotNull(obj);
            Assert.AreEqual(79, obj.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Convert_NotExists()
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
