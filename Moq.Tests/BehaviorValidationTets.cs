using Castle.Components.DictionaryAdapter.Xml;
using CreditCardApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moq.Tests
{
    public class BehaviorValidationTets
    {
        [Fact]
        public void Evaluate_WithExpiredLicenseKey_DoesntInvokeAgeDiscountProperty()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            mockValidator.Setup(i => i.LicenseData.License.LicenseKey).Returns("EXPIRED");
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);


            sut.Evaluate(new CreditCardApplication
            {
                GrossAnnualIncome= 200_000
            });

            // Test that this property was never invoked
            mockValidator.Verify(i=> i.AgeDiscount,times: Times.Never);
        }

        [Fact]
        public void Evaluate_WithNotExpiredLicenseKey_InvokesAgeDiscountPropertyOnce()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            mockValidator.Setup(i => i.LicenseData.License.LicenseKey).Returns("NOT_EXPIRED");
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            mockValidator.SetupProperty(i => i.AgeDiscount);

            sut.Evaluate(new CreditCardApplication
            {
                GrossAnnualIncome = 100
            });

            // Test that this property was invoked and the value was set to 50
            mockValidator.VerifySet(i =>
            {
                i.AgeDiscount = 50;
            });
        }

        [Fact]
        public void Evaluate_WithNotExpiredLicenseKey_InvokesAgeDiscountPropertyGetter()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            mockValidator.Setup(i => i.LicenseData.License.LicenseKey).Returns("NOT_EXPIRED");
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            mockValidator.SetupProperty(i => i.AgeDiscount);

            sut.Evaluate(new CreditCardApplication
            {
                GrossAnnualIncome = 100
            });

            // Test that this property was invoked and the value was set to 50
            mockValidator.VerifyGet(i =>i.AgeDiscount);
        }
    }
}
