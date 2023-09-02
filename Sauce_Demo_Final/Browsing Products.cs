using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sauce_Demo2
{
    public class Browsing_Products
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public object ExpectedConditions { get; private set; }

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            IWebElement Username = driver.FindElement(By.Id("user-name"));
            IWebElement password = driver.FindElement(By.Id("password"));
            Username.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            IWebElement Loginbtn = driver.FindElement(By.Id("login-button"));
            Loginbtn.Click();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("inventory_container")));

            //Thread.Sleep(5000);


        }

        [Test]
        public void TestCatalogue()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/inventory.html");

            // Find the product containers
            var productContainers = driver.FindElements(By.ClassName("inventory_item"));

            // Loop through the product containers and print the product names
            foreach (var container in productContainers)
            {
                var productTitle = container.FindElement(By.ClassName("inventory_item_name")).Text;
                Console.WriteLine(productTitle);
            }

            // Assert that there are at least 6 product containers
            Assert.That(productContainers.Count, Is.GreaterThanOrEqualTo(6));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }



    }
}
