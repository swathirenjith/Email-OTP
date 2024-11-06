using System.Text.RegularExpressions;

namespace EmailOTPModuleAssignment
{
    public class EmailOTPModule
    {
        private readonly string pattern = @"^[a-zA-Z0-9._%+-]+@([a-zA-Z0-9-]+\.)*\.dso\.org\.sg$";
        public DateTime OtpExpiryTime { get; set; } = DateTime.Now.AddMinutes(1);
        public int MaxTries { get; set; } = 10;
        public int AttemptCount { get; set; } = 0;
        public string NewOTP { get; set; }

        public void Start()
        {
            Console.WriteLine("Start | Usage of EmailOTPModule");
        }
        public void Close()
        {
            Console.WriteLine("End | Usage of EmailOTPModule");
            //remove related unmanaged resource if any!
        }

        public async Task<string> GenerateOTPEmail(string emailAddress)
        {
            if (Regex.IsMatch(emailAddress, pattern))
            {
                NewOTP = new Random().Next(0, 1000000).ToString("D6");
                string emailBody = $"Your OTP Code is {NewOTP}. The code is valid for 1 minute.";
                var isSendEmailSuccess = await SendEmail(emailAddress, emailBody);
                if (isSendEmailSuccess)
                {
                    return "STATUS_EMAIL_OK: email containing OTP has been sent successfully.";
                }
                else
                {
                    return "STATUS_EMAIL_FAIL: email address does not exist or sending to the email has failed.";
                }
            }
            else
            {
                return "STATUS_EMAIL_INVALID: email address is invalid.";
            }

        }
        public async Task<bool> SendEmail(string emailAddress, string emailBody)
        {
            try
            {
                //assuming sent email is success, log and returns true
                await Task.Delay(0);
                return true;
            }
            catch (Exception ex)
            {
                //log and return false if any exception occurs
                Console.WriteLine($"Failed to send email: {ex.Message}");
                return false;
            }
        }
        public string CheckOTP(Stream OTP)
        {
            var userInput = new StreamReader(OTP).ReadLine();

            if (DateTime.Now < OtpExpiryTime && AttemptCount < MaxTries)
            {
                if (NewOTP == userInput)
                {
                    return "STATUS_OTP_OK: OTP is valid and checked";
                }
                AttemptCount++;
            }
            if (DateTime.Now >= OtpExpiryTime)
            {
                return "STATUS_OTP_TIMEOUT: timeout after 1 min";
            }
            return "STATUS_OTP_FAIL: OTP is wrong after 10 tries";
        }
    }
}
