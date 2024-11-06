using EmailOTPModuleAssignment;
using System.Text;

namespace EmailOTPModuleAssignmentTest
{
    public class EmailOTPModuleTest
    {
        EmailOTPModule emailOTPModule;

        [SetUp]
        public void Setup()
        {
            emailOTPModule = new EmailOTPModule();
        }

        [TearDown]
        public void TearDown()
        {
            // remove any unwanted resource after test
        }

        [Test]
        public async Task InvalidEmail_ShouldReturn_StatusEmailInvalid()
        {
            //Arrange
            string invalidEmail = "test@dso.org.sg";
            //Act
            var result = await emailOTPModule.GenerateOTPEmail(invalidEmail);
            //Assert
            Assert.That(result, Is.EqualTo("STATUS_EMAIL_INVALID: email address is invalid."));

        }
        [Test]
        public async Task ValidEmail_ShouldReturn_StatusEmailOk()
        {
            //Arrange
            string validEmail = "test@.dso.org.sg";
            //Act
            var result = await emailOTPModule.GenerateOTPEmail(validEmail);
            //Assert
            Assert.That(result, Is.EqualTo("STATUS_EMAIL_OK: email containing OTP has been sent successfully."));
        }
        [Test]
        public void CheckOTP_ShoudReturn_Success_For_Valid_OTP()
        {
            //Arrange
            emailOTPModule.MaxTries = 10;
            emailOTPModule.NewOTP = new Random().Next(0, 1000000).ToString("D6");
            emailOTPModule.OtpExpiryTime = DateTime.Now.AddMinutes(1);
            byte[] byteArray = Encoding.UTF8.GetBytes(emailOTPModule.NewOTP);
            MemoryStream stream = new(byteArray);
            //Act
            var result = emailOTPModule.CheckOTP(stream);
            //Assert
            Assert.That(result, Is.EqualTo("STATUS_OTP_OK: OTP is valid and checked"));
        }
        [Test]
        public void CheckOTP_ShouldReturn_Fail_After_Ten_Tries()
        {
            //Arrange
            emailOTPModule.MaxTries = 10;
            emailOTPModule.AttemptCount = 11;
            emailOTPModule.NewOTP = "123456";
            emailOTPModule.OtpExpiryTime = DateTime.Now.AddMinutes(1);
            byte[] byteArray = Encoding.UTF8.GetBytes("654321");
            MemoryStream stream = new(byteArray);
            //Act
            var result = emailOTPModule.CheckOTP(stream);
            //Assert
            Assert.That(result, Is.EqualTo("STATUS_OTP_FAIL: OTP is wrong after 10 tries"));
        }
        [Test]
        public void CheckOTP_ShoudReturn_Fail_After_Timeout()
        {
            //Arrange
            emailOTPModule.MaxTries = 10;
            emailOTPModule.AttemptCount = 0;
            emailOTPModule.NewOTP = "123456";
            emailOTPModule.OtpExpiryTime = DateTime.Now.AddMinutes(-1);
            byte[] byteArray = Encoding.UTF8.GetBytes(emailOTPModule.NewOTP);
            MemoryStream stream = new(byteArray);
            //Act
            var result = emailOTPModule.CheckOTP(stream);
            //Assert
            Assert.That(result, Is.EqualTo("STATUS_OTP_TIMEOUT: timeout after 1 min"));
        }
        [Test]
        public async Task SendEmail_ShoudReturn_True_For_Success()
        {
            //Arrange
            string email = "test@dso.org.sg";
            var emailBody = "You OTP Code is 123456. The code is valid for 1 minute";
            //Act
            var result = await emailOTPModule.SendEmail(email, emailBody);
            //Assert
            Assert.That(result, Is.True);
        }

    }
}