using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.UI;
using log4net;
using log4net.Config;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace Sauce_Demo2
{
    public class LoginTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        // private static readonly ILog log = LogManager.GetLogger(typeof(LoginTests));
        private static readonly log4net.ILog
        log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //2-Creating a logger

        private ExtentReports extent = new ExtentReports();
        private ExtentTest test;

        [SetUp]
        public void Setup()
        {
            try
            {
                XmlConfigurator.Configure(new FileInfo("log4net.config"));
                log.Debug("Setting up the test at" + DateTime.Now.ToString());
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://www.saucedemo.com/");

                // Set up ExtentReports
                string reportPath = @"C:\logs\Reports";
                var htmlReporter = new ExtentHtmlReporter(reportPath);
                //extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);
            }

            catch (Exception ex)
            {
                log.Error("An Error occured while setup" + ex.Message);
                throw;
            }


        }

        [Test]
        public void TestLogin1() //Invalid Username and Valid Password
        {
            // Create ExtentTest instance
            test = extent.CreateTest("TestLogin1", "Invalid Username and Valid Password");

            try
            {
                log.Info("Starting TestLogin1 at " + DateTime.Now.ToString());
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement Username = driver.FindElement(By.Id("user-name"));
                Username.SendKeys("Marwa_Yahia");
                IWebElement Password = driver.FindElement(By.Id("password"));
                Password.SendKeys("secret_sauce");
                IWebElement Loginbtn = driver.FindElement(By.Id("login-button"));
                Loginbtn.Click();

                // Find the error message element
                IWebElement errorMessage = driver.FindElement(By.XPath("//h3[@data-test='error']"));

                // Wait for the error message text to be non-empty
                wait.Until(driver => !string.IsNullOrEmpty(errorMessage.Text));

                // Verify that the error message text is correct
                Assert.That(errorMessage.Text, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));
            }
            catch (Exception ex)
            {
                log.Error("An error occurred while executing TestLogin1: " + ex.Message);

            }

        }

        [Test]
        public void TestLogin2() //Valid Username and Invalid Password
        {
            // Create ExtentTest instance
            test = extent.CreateTest("TestLogin2", "Valid Username and Invalid Password");

            try
            {
                log.Info("Starting TestLogin2 at " + DateTime.Now.ToString());
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement Username = driver.FindElement(By.Id("user-name"));
                Username.SendKeys("standard_user");
                IWebElement Password = driver.FindElement(By.Id("password"));
                Password.SendKeys("Test123");
                IWebElement Loginbtn = driver.FindElement(By.Id("login-button"));
                Loginbtn.Click();

                // Find the error message element
                IWebElement errorMessage = driver.FindElement(By.XPath("//h3[@data-test='error']"));

                // Wait for the error message text to be non-empty
                wait.Until(driver => !string.IsNullOrEmpty(errorMessage.Text));

                // Verify that the error message text is correct
                Assert.That(errorMessage.Text, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));

            }
            catch (Exception ex)
            {
                log.Error("An error occurred while executing TestLogin2: " + ex.Message);

            }


        }

        [Test]
        public void TestLogin3() //InValid Username and Invalid Password
        {
            // Create ExtentTest instance
            test = extent.CreateTest("TestLogin3", "InValid Username and Invalid Password");

            try
            {
                log.Info("Starting TestLogin3 at " + DateTime.Now.ToString());
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement Username = driver.FindElement(By.Id("user-name"));
                Username.SendKeys("Marwa_Elhelow");
                IWebElement Password = driver.FindElement(By.Id("password"));
                Password.SendKeys("Test123");
                IWebElement Loginbtn = driver.FindElement(By.Id("login-button"));
                Loginbtn.Click();

                // Find the error message element
                IWebElement errorMessage = driver.FindElement(By.XPath("//h3[@data-test='error']"));

                // Wait for the error message text to be non-empty
                wait.Until(driver => !string.IsNullOrEmpty(errorMessage.Text));

                // Verify that the error message text is correct
                Assert.That(errorMessage.Text, Is.EqualTo("Epic sadface: failed "));

            }
            catch (Exception ex)
            {
                log.Error("An error occurred while executing TestLogin3: " + ex.Message);

            }
        }

        [Test]
        public void TestLogin4() //Valid Username and Valid Password
        {
            // Create ExtentTest instance
            test = extent.CreateTest("TestLogin4", "Valid Username and Valid Password");
            try
            {
                log.Info("Starting TestLogin4 at " + DateTime.Now.ToString());
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement Username = driver.FindElement(By.Id("user-name"));
                Username.SendKeys("standard_user");
                IWebElement Password = driver.FindElement(By.Id("password"));
                Password.SendKeys("secret_sauce");
                IWebElement Loginbtn = driver.FindElement(By.Id("login-button"));
                Loginbtn.Click();

                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

                IWebElement ProductsPage_title = driver.FindElement(By.XPath("//*[@id=\\\"header_container\\\"]/div[2]/span\""));
                Assert.That(ProductsPage_title.Text, Is.EqualTo("Products"));

                //        // // Find the error message element
                //        //IWebElement errorMessage = driver.FindElement(By.XPath("//h3[@data-test='error']"));

                //        // // Wait for the error message text to be non-empty
                //        // wait.Until(driver => !string.IsNullOrEmpty(errorMessage.Text));

                //        // // Verify that the error message text is correct
                //        // Assert.AreEqual("Epic sadface: failed ", errorMessage.Text);

                //        // Assert.Fail("Login should have failed");
                //    }
                //    catch(Exception ex)
                //    {
                //        log.Error("An error occurred while executing TestLogin4: " + ex.Message);
                //    }

                //}

            }
            catch (Exception ex)
            {
                log.Error("An error occurred while executing TestLogin3: " + ex.Message);

            }
        }


        [TearDown]

        public void TearDown()
        {
            try
            {
                log.Info("Tearing down the Tests at " + DateTime.Now.ToString());
                driver.Quit();

                // Log test status and report to ExtentReports
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stackTrace = TestContext.CurrentContext.Result.StackTrace;
                var errorMessage = TestContext.CurrentContext.Result.Message;

                if (status == TestStatus.Failed)
                {
                    log.Error("Test failed: " + errorMessage);
                    test.Fail(errorMessage);
                }
                else if (status == TestStatus.Passed)
                {
                    log.Info("Test passed");
                    test.Pass("Test passed");
                }
                else if (status == TestStatus.Skipped)
                {
                    log.Info("Test skipped");
                    test.Skip("Test skipped");
                }

                // End the test
                extent.Flush();
            }
            catch (Exception ex)
            {
                log.Error("An error occurred while teardown: " + ex.Message);
            }
        }
    }
}


