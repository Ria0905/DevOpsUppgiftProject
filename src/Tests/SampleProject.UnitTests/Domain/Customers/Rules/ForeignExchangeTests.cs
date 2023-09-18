using System;
using System.Collections.Generic;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Infrastructure.Caching;
using SampleProject.Infrastructure.Domain.ForeignExchanges;
using Xunit;
using Moq;
using Shouldly;
using AutoFixture;

namespace SampleProject.UnitTests.Domain.Customers.Rules
{
    public class ForeignExchangeTests
    {
        [Fact]
        public void GetConversionRates_WhenCacheNotAvailable_ShouldReturnTwoValues()
        {

            // Arrange
            var cacheStoreMock = new Mock<ICacheStore>();
            var foreignExchange = new ForeignExchange(cacheStoreMock.Object);

            var conversionRates = new List<ConversionRate>
            {
                new ConversionRate("USD", "EUR", (decimal)0.88),
                new ConversionRate("EUR", "USD", (decimal)1.13)
            };

            cacheStoreMock.Setup(c => c.Get<List<ConversionRate>>(It.IsAny<ICacheKey<List<ConversionRate>>>()))
                .Returns((List<ConversionRate>)null); // Cache tidak tersedia

            cacheStoreMock.Setup(c => c.Add(It.IsAny<ConversionRatesCache>(), It.IsAny<ICacheKey<ConversionRatesCache>>(), It.IsAny<DateTime?>()))
                .Callback<ConversionRatesCache, ICacheKey<ConversionRatesCache>, DateTime?>((cache, key, expirationTime) =>
                {
                    cache.Rates = conversionRates;
                });


            // Act
            var result = foreignExchange.GetConversionRates();

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeEquivalentTo(conversionRates);



        }


        [Fact]
        public void GetConversionRates_WhenCacheNotAvailable_ShouldReturnOneValue()
        {


            //Arrange
            var fixture = new Fixture();

            var cachedConversionRate = new ConversionRate("USD", "EUR", (decimal)0.88);
            var cachedRates = new List<ConversionRate> { cachedConversionRate };

            var cacheStoreMock = new Mock<ICacheStore>();
            cacheStoreMock.Setup(c => c.Get(It.IsAny<ConversionRatesCacheKey>())).Returns(new ConversionRatesCache(cachedRates));

            var foreignExchange = new ForeignExchange(cacheStoreMock.Object);

            //Act
            var result = foreignExchange.GetConversionRates();


            //Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);

            result[0].SourceCurrency.ShouldBe(cachedConversionRate.SourceCurrency);
            result[0].TargetCurrency.ShouldBe(cachedConversionRate.TargetCurrency);
        }
    }

}
    

