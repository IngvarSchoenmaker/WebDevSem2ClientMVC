using System.ComponentModel.DataAnnotations;
using WebDevSem2ClientMVC.Models;

namespace UnitTest
{
    public class ContactFormTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Email_Required_ShouldHaveErrorMessage()
        {
            // Arrange
            var contactForm = new ContactForm();

            // Act
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(contactForm, null, null);
            var result = Validator.TryValidateObject(contactForm, context, validationResults, true);

            // Assert
            Assert.IsFalse(result);
            Assert.IsTrue(validationResults.Count > 0);
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("E-mailadres is verplicht"));
        }
    }
}