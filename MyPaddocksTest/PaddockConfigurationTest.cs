using Mypaddocks.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaddocksTest
{
    [TestFixture]
    public class PaddockConfigurationTest
    {
        private PaddockConfigurationRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new PaddockConfigurationRepository();
        }

        [Test]
        public void CalculateNumberOfPaddocks_ValidInput_ReturnsCorrectResult()
        {
            // Arrange
            int area = 300;
            int cowsPerPaddock = 10; // Each paddock needs 30m²

            // Act
            int result = _repo.CalculateNumberOfPaddocks(area, cowsPerPaddock);

            // Assert
            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void CalculateNumberOfPaddocks_AreaNotDivisible_ReturnsFlooredResult()
        {
            // Arrange
            int area = 305;
            int cowsPerPaddock = 10; // Each paddock needs 30m²

            // Act
            int result = _repo.CalculateNumberOfPaddocks(area, cowsPerPaddock);

            // Assert
            Assert.That(result, Is.EqualTo(10)); // 305 / 30 = 10.16, integer division floors to 10
        }

        [Test]
        public void CalculateNumberOfPaddocks_ZeroArea_ReturnsZero()
        {
            // Arrange
            int area = 0;
            int cowsPerPaddock = 10;

            // Act
            int result = _repo.CalculateNumberOfPaddocks(area, cowsPerPaddock);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNumberOfPaddocks_ZeroCowsPerPaddock_ThrowsDivideByZeroException()
        {
            // Arrange
            int area = 300;
            int cowsPerPaddock = 0;

            // Act & Assert
            Assert.Throws<System.DivideByZeroException>(() =>
                _repo.CalculateNumberOfPaddocks(area, cowsPerPaddock));
        }

        [Test]
        public void CalculateNumberOfPaddocks_NegativeArea_ReturnsNegativeResult()
        {
            // Arrange
            int area = -300;
            int cowsPerPaddock = 10;

            // Act
            int result = _repo.CalculateNumberOfPaddocks(area, cowsPerPaddock);

            // Assert
            Assert.That(result, Is.EqualTo(-10));
        }

        [Test]
        public void CalculateNumberOfPaddocks_NegativeCowsPerPaddock_ReturnsNegativeResult()
        {
            // Arrange
            int area = 300;
            int cowsPerPaddock = -10;

            // Act
            int result = _repo.CalculateNumberOfPaddocks(area, cowsPerPaddock);

            // Assert
            Assert.That(result, Is.EqualTo(-10));
        }
    }
}
