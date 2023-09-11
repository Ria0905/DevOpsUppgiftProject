using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Customers.Rules;
using SampleProject.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleProject.UnitTests.Domain.Customers.Rules
{
    public class OrderMustHaveAtLeastOneProductRuleTests
    {
        [Fact]
        public void IsBroken_WhenOrderProductsGreaterThanZero_IsBrokenShouldBeFalse()
        {
            // Arrange
            int productCount = 3;
            var orderProducts = new List<OrderProductData> { new OrderProductData(new ProductId(Guid.NewGuid()), productCount) };

            // Act
            var sut = new OrderMustHaveAtLeastOneProductRule(orderProducts);
            var result = sut.IsBroken();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsBroken_WhenOrderProductsEmpty_IsBrokenShouldBeTrue()
        {
            // Arrange
            var orderProducts = new List<OrderProductData>();

            // Act
            var sut = new OrderMustHaveAtLeastOneProductRule(orderProducts);
            var result = sut.IsBroken();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsBroken_WhenOrderProductsIsNull_IsBrokenShouldBeTrue()
        {
            // Arrange & Act
            var sut = new OrderMustHaveAtLeastOneProductRule(null);
            var result = sut.IsBroken();

            // Assert
            Assert.True(result);
        }
    }
}
