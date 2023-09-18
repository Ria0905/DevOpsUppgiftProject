using AutoFixture;
using Moq;
using SampleProject.Infrastructure.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace SampleProject.Infrastructure.Domain.ForeignExchanges
{
    internal class FXTest
    {
        [Fact]
        public void GetConversionRates_WhenCacheNotAvailable_ShouldReturnTwoValues()
        {
            //Arrange
            var fixture = new Fixture();
            var cacheStoreMock = new Mock<ICacheStore>();
            cacheStoreMock.Setup(c => c.Get(It.IsAny<ConversionRatesCacheKey>())).Returns((ConversionRatesCache)null);

            var foreignExchange = new ForeignExchange(cacheStoreMock.Object);

            //Act
            var conversionRates = foreignExchange.GetConversionRates();

            //Assert
           
            conversionRates.ShouldBeNull();
            conversionRates.Count.ShouldBe(2);


        }
    }
}
