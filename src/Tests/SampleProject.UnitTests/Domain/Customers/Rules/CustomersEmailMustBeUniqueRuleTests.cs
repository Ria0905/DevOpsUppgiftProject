using Moq;
using SampleProject.Domain.Customers;
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
    public class CustomersEmailMustBeUniqueRuleTests
    {
        [Fact]
        public void IsBroken_WhenEmailIsUnique_IsBrokenShouldBeFalse()
        {
            // Arrange
            string email = "test@test.test";
            var customerUniquenessChecker = new Mock<ICustomerUniquenessChecker>();
            customerUniquenessChecker.Setup(x => x.IsUnique(It.IsAny<string>())).Returns(true);

            // Act
            var sut = new CustomerEmailMustBeUniqueRule(customerUniquenessChecker.Object, email);
            var result = sut.IsBroken();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsBroken_EmailIsNotUnique_IsBrokenShouldBeTrue()
        {
            // Arrange
            string email = "test@test.test";
            var customerUniquenessChecker = new Mock<ICustomerUniquenessChecker>();
            customerUniquenessChecker.Setup(x => x.IsUnique(It.IsAny<string>())).Returns(false);

            // Act
            var sut = new CustomerEmailMustBeUniqueRule(customerUniquenessChecker.Object, email);
            var result = sut.IsBroken();

            // Assert
            Assert.True(result);
        }


    }
}