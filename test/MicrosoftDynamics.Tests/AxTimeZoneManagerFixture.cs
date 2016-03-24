using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicrosoftDynamics.Tests
{
    /// <summary>
    /// Summary description for Manager
    /// </summary>
    [TestClass]
    public class AxTimeZoneManagerFixture
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Factory_InvalidVersion()
        {
            // Arrange
            const string invalidVersion = "_";
            // Act
            DynamicsAxTimeZoneManager.Current.Create(invalidVersion);
            // Assert
        }

        [TestMethod]
        public void Factory_ValidVersion()
        {
            // Arrange
            // Act
            var mapper = DynamicsAxTimeZoneManager.Current.Create(DynamicsAxVersion.Ax2012);
            // Assert
            Assert.IsNotNull(mapper);
            Assert.AreEqual(DynamicsAxVersion.Ax2012, mapper.AxVersion);
        }
    }
}
