using Mypaddocks.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaddocksTest
{
    [TestFixture]
    public class FarmRepositoryTest
    {
        private FarmDimensionRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new FarmDimensionRepository();
        }

        [Test]
        public void SetFarmDimensions_SetsValuesCorrectly()
        {
            // Act
            _repo.SetFarmDimensions(250, 350);

            // Assert
            var dims = _repo.GetFarmDimensions();
            Assert.That(dims.Length, Is.EqualTo(250));
            Assert.That(dims.Width, Is.EqualTo(350));
        }

        [Test]
        public void GetFarmDimensions_ReturnsDefaultValuesInitially()
        {
            // Act
            var dims = _repo.GetFarmDimensions();

            // Assert
            Assert.That(dims.Length, Is.EqualTo(0));
            Assert.That(dims.Width, Is.EqualTo(0));
        }

        [TestCase(210, 310, true)]
        [TestCase(209, 310, false)]
        [TestCase(210, 309, false)]
        [TestCase(500, 500, true)]
        [TestCase(0, 0, false)]
        public void CalculateArea_ReturnsExpectedResult(int length, int width, bool expected)
        {
            // Arrange
            _repo.SetFarmDimensions(length, width);

            // Act
            var result = _repo.CalculateArea();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(210, true)]
        [TestCase(209, false)]
        [TestCase(500, true)]
        [TestCase(0, false)]
        public void ValidateLength_ReturnsExpectedResult(int length, bool expected)
        {
            // Arrange
            _repo.SetFarmDimensions(length, 400);

            // Act
            var result = _repo.ValidateLength();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(310, true)]
        [TestCase(309, false)]
        [TestCase(500, true)]
        [TestCase(0, false)]
        public void ValidateWidth_ReturnsExpectedResult(int width, bool expected)
        {
            // Arrange
            _repo.SetFarmDimensions(400, width);

            // Act
            var result = _repo.ValidateWidth();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
