using Mypaddocks.Models;
using Mypaddocks.Repository;

namespace MyPaddocksTest
{
    [TestFixture]
    public class CalculationRepositoryTest
    {
        private CalculationRepository _calculationRepository;

        [SetUp]
        public void SetUp()
        {
            _calculationRepository = new CalculationRepository();
            AppState.FarmLength = 100; // Ensure AppState is defined in the correct namespace
            AppState.FarmWidth = 50;
            AppState.FarmArea = 5000;
            AppState.CowsPerPaddock = 10;
        }

        [Test]
        public void CalculatePaddockLayout_WithValidInput_ReturnsExpectedResult()
        {
            // Arrange
            var repo = new CalculationRepository();
            var inputDimensions = new FarmDimensions();
            var inputConfig = new PaddockConfiguration();

            // Act
            var result = repo.CalculatePaddockLayout(inputDimensions, inputConfig);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.FarmDimensions.Length, Is.EqualTo(AppState.FarmLength));
            Assert.That(result.FarmDimensions.Width, Is.EqualTo(AppState.FarmWidth));
            Assert.That(result.PaddockConfiguration.CowsPerPaddock, Is.EqualTo(AppState.CowsPerPaddock));
            Assert.That(result.PaddockConfiguration.FarmArea, Is.EqualTo(AppState.FarmArea));

            // These depend on FindOptimalPaddockDimensions implementation
            Assert.That(result.PaddockConfiguration.PaddockLength, Is.GreaterThan(0));
            Assert.That(result.PaddockConfiguration.PaddockWidth, Is.GreaterThan(0));
            Assert.That(result.PaddockConfiguration.TotalFittingPaddocks, Is.GreaterThanOrEqualTo(0));
            Assert.That(result.PaddockConfiguration.PaddockNames, Is.Not.Null);
            Assert.That(result.PaddockCoordinates, Is.Not.Null);
        }

        [Test]
        public void CalculatePaddockLayout_WithInvalidInput_ReturnsDefaultResult()
        {
            // Arrange
            AppState.FarmLength = 0;
            AppState.FarmWidth = 0;
            AppState.FarmArea = 0;
            AppState.CowsPerPaddock = 0;

            var repo = new CalculationRepository();
            var inputDimensions = new FarmDimensions();
            var inputConfig = new PaddockConfiguration();

            // Act
            var result = repo.CalculatePaddockLayout(inputDimensions, inputConfig);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PaddockConfiguration.PaddockLength, Is.EqualTo(0));
            Assert.That(result.PaddockConfiguration.PaddockWidth, Is.EqualTo(0));
            Assert.That(result.PaddockConfiguration.LayoutOption, Is.Null);
            Assert.That(result.PaddockConfiguration.PaddockArea, Is.EqualTo(0));
            Assert.That(result.PaddockConfiguration.TotalFittingPaddocks, Is.EqualTo(0));
            Assert.That(result.PaddockConfiguration.PaddockNames, Is.Null);
            Assert.That(result.PaddockCoordinates, Is.Null);
        }
    }
}
