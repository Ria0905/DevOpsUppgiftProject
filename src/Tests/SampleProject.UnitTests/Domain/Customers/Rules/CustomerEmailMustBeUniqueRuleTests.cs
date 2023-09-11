using Moq;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Rules;
using Xunit;
using Assert = Xunit.Assert;

namespace SampleProject.UnitTests.Domain.Customers.Rules
{
    public class CustomerEmailMustBeUniqueRuleTests
    {
        [Fact]
        public void IsBroken_WhenEmailIsUnique_IsBrokenShouldBeFalse()
        {
            //  Arrange
            string email = "test@test.test";
            var customerUniquenessChecker = new Mock<ICustomerUniquenessChecker>();
            customerUniquenessChecker.Setup(x => x.IsUnique(It.IsAny<string>())).Returns(true);

            //Act
            var sut = new CustomerEmailMustBeUniqueRule(customerUniquenessChecker.Object, email);
            var result = sut.IsBroken();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsBroken_EmailIsNotUnique_IsBrokenShouldBeTrue()
        {
            //  Arrange
            string email = "test@test.test";
            var customerUniquenessChecker = new Mock<ICustomerUniquenessChecker>();
            customerUniquenessChecker.Setup(x => x.IsUnique(It.IsAny<string>())).Returns(false);

            //Act
            var sut = new CustomerEmailMustBeUniqueRule(customerUniquenessChecker.Object, email);
            var result = sut.IsBroken();

            //Assert
            Assert.True(result);
        }

    }
}
