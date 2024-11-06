# Project Name

EmailOTPModule 

## About

EmailOTPModule is a .NET CORE application module desgined to use for an enterprise application.

## Application Specs

- *Language/Framework*: C# (.NET 8.0)
- *Development Environment*: Windows, using .NET Core SDK version 8.0
- *Projects*:
  - EmailOTPModule (Main Application)
  - EmailOTPModuleTest (Test Project using NUnit)

## Assumptions

1.SendEmail method is implemented.
2.OtpExpiryTime,MaxTries and AttemptCount are public properties with default values (as per in the task) and can be changed by the caller
3.Console log is used as a logger for the application.

## Running the Application

1. Open the solution (Inside the unzipped folder, EmailOTPSln file) using Visual Studio 2022 or Visual Studio Code (ensure the correct SDK version is installed).
2. In the solution explorer window (right side), right click on the solution name and choose build solution
3. Run the tests as below mentioned

## Running the Tests

1.Ensure that the following NuGet packages installed/restored:
	-NUnit
	-TestAdapter
2.Right click on the test project (EmailOTPModuleTest) and select 'Run Tests' from context menu.

## Testing the module with description for each tests

As this is a standard library implementation, the testing is mainly based on unit tests which checks and ensures the methods are correctly implemented

As such the following six unit tests are written for testing EmailOTPModule.

1.InvalidEmail_ShouldReturn_StatusEmailInvalid - Test for checking invalid email address
2.ValidEmail_ShouldReturn_StatusEmailOk - Test for checking valid email address
3.CheckOTP_ShoudReturn_Success_For_Valid_OTP - Test for checking valid OTP
4.CheckOTP_ShouldReturn_Fail_After_Ten_Tries - Test for checking failure after ten tries
5.CheckOTP_ShoudReturn_Fail_After_Timeout - Test for checking failure after 1 min timeout 
6.SendEmail_ShoudReturn_True_For_Success - Test for checking successful email sending 
