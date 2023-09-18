using Moq;
using SampleProject.Domain.Customers.Rules;
using SampleProject.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SampleProject.Domain.Customers.Orders;
using AutoFixture;
using NSubstitute;
using SampleProject.Domain.Products;
using SampleProject.Domain.SharedKernel;
using SampleProject.Domain.ForeignExchange;
using Castle.Core.Resource;

namespace SampleProject.UnitTests.Domain.Customers
{
    public class CustomerTests
    {
        private readonly Fixture _fixture;

        public CustomerTests() => _fixture = new Fixture();

        [Fact]
        public void ChangeOrder_xx_xx()
        {
            // Arrange
            var customerUniquenessChecker = Substitute.For<ICustomerUniquenessChecker>();
            var email = "hermi.seprianihakefjall@yh.nackademin.se";
            customerUniquenessChecker.IsUnique(Arg.Any<string>()).Returns(true);
            //customerUniquenessChecker.IsUnique(email).Returns(true);

            string currency = "EUR";
            var orderProductData = _fixture.Build<OrderProductData>()
                //.With(x => x.Quantity,45)
                .CreateMany(3).ToList();

            var productPriceData = new List<ProductPriceData>
            {
                new ProductPriceData(orderProductData.First().ProductId, new MoneyValue(1, currency)),
                new ProductPriceData(orderProductData.Skip(1).First().ProductId, new MoneyValue(10, currency)),
                new ProductPriceData(orderProductData.Skip(2).First().ProductId, new MoneyValue(100, currency))

            };

            var conversionRates = _fixture.Create<List<ConversionRate>>();

            var order = Order.CreateNew(orderProductData, productPriceData, currency, conversionRates);
            var customer = Customer.CreateRegistered(email, "Hermi", customerUniquenessChecker);
            customer._orders.Add(order);

            var orderPreviousValue = order.GetValue();
            var orderWithRemovedProduct = orderProductData.Take(2).ToList();

            // Act
            customer.ChangeOrder(order.Id, productPriceData, orderWithRemovedProduct, conversionRates, currency);
            var newOrderValue = order.GetValue();

            //Assert
            Assert.NotEqual(orderPreviousValue.Value, newOrderValue.Value);
        }
    }
}