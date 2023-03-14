using CreditCardApplications;
using Microsoft.AspNetCore.Builder;

namespace Moq.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Evaluate_ExpiredLicensekey_ReturnsReferToHuman()
        {
            // MockBehavior.Strict will ensure that all methods in this interface have been setup 
            // else the unit test will fail
            //var mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);
           
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Loose);
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            mockValidator.Setup(i => i.LicenseData.License.LicenseKey).Returns("EXPIRED");

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication
            {
                Age = 42
            };

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }


        [Fact]
        public void TrackingChangesInAMockedobject()
        {
            int expectedDiscount = 30;
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Loose);

            // if a property is not mocked then it will get its default value. check readme file
            mockValidator.DefaultValue = DefaultValue.Mock;

            // without setting this property the mock will not remember any changes made to it
            // since we are checking the value for this property while asserting it is necessary 
            // that we make sure that changes made to this property of the mock object are 
            // retained
            mockValidator.SetupProperty(i => i.AgeDiscount);
            
            // use this method to setup all properties, this will overwrite the previously setup
            // property
            //mockValidator.SetupAllProperties();


            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication
            {
                Age = 42
            };

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(mockValidator.Object.AgeDiscount, expectedDiscount);
        }
    }
}