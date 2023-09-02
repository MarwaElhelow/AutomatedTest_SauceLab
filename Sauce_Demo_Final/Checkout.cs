using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sauce_Demo2
{
    public class Checkout
    {
        private IWebDriver driver;


        [SetUp]
        public void SetUp()
        {

            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            IWebElement Username = driver.FindElement(By.Id("user-name"));
            IWebElement Password = driver.FindElement(By.Id("password"));
            Username.SendKeys("standard_user");
            Password.SendKeys("secret_sauce");
            IWebElement LoginButon = driver.FindElement(By.Id("login-button"));
            LoginButon.Click();
            Thread.Sleep(5000);
        }

        [Test]
        public void Test1() //All Valid Data
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/cart.html");
            driver.FindElement(By.Id("checkout")).Click();
            Thread.Sleep(3000);
            IWebElement Firstname = driver.FindElement(By.Id("first-name"));
            Firstname.SendKeys("Marwa");
            IWebElement Lastname = driver.FindElement(By.Id("last-name"));
            Lastname.SendKeys("Elhelow");
            IWebElement Postal_Code = driver.FindElement(By.Id("postal-code"));
            Postal_Code.SendKeys("12553");
            IWebElement Continue = driver.FindElement(By.Id("continue"));
            //Thread.Sleep(1000);
            Continue.Click();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            IWebElement FinalPageTitle = driver.FindElement(By.XPath("//*[@id=\"header_container\"]/div[2]/span"));
            Assert.That(FinalPageTitle.Text, Is.EqualTo("Checkout: Overview"));


            //Assert that number of items displayed are equal to the added items to cart
            //int Page_Items_Count = GetActualItemCount();
            // int Cart_Items_Count = GetExpectedItemCount();
            //Assert.AreEqual(Page_Items_Count, Cart_Items_Count);

            IWebElement Finish_Checkout_btn = driver.FindElement(By.CssSelector("#finish"));
            Finish_Checkout_btn.Click();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            IWebElement CompletePage = driver.FindElement(By.CssSelector("#header_container > div.header_secondary_container > span"));
            Assert.That(CompletePage.Text, Is.EqualTo("Checkout: Complete!"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IWebElement BackToHome = driver.FindElement(By.Id("back-to-products"));
            BackToHome.Click();
            driver.Navigate().Refresh();

        }

        //private int GetExpectedItemCount()
        //{
        //   IWebElement cartItemsNum = driver.FindElement(By.CssSelector("#shopping_cart_badge"));
        //    string cartItemsNumText = cartItemsNum.Text;
        //   return int.Parse(cartItemsNumText);
        //}

        //private int GetActualItemCount()
        //{
        //   // Find the element that contains the list of items on the checkout page
        //  IWebElement checkoutItemsContainer = driver.FindElement(By.CssSelector(".cart_list"));

        //   // Find all the elements that represent individual items on the checkout page
        //  IReadOnlyCollection<IWebElement> checkoutItems = checkoutItemsContainer.FindElements(By.CssSelector(".cart_item"));

        //  // Return the number of items on the checkout page
        //  return checkoutItems.Count;
        //}

        [Test]
        public void Test2() //No First Name
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/cart.html");
            driver.FindElement(By.Id("checkout")).Click();
            Thread.Sleep(3000);
            IWebElement Firstname = driver.FindElement(By.Id("first-name"));
            Firstname.SendKeys("");
            IWebElement Lastname = driver.FindElement(By.Id("last-name"));
            Lastname.SendKeys("Elhelow");
            IWebElement Postal_Code = driver.FindElement(By.Id("postal-code"));
            Postal_Code.SendKeys("12553");
            IWebElement Continue = driver.FindElement(By.Id("continue"));
            Thread.Sleep(1000);
            Continue.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IWebElement errorMessage = driver.FindElement(By.XPath("//*[@data-test='error']"));
            Assert.That(errorMessage.Displayed, Is.True);

        }

        [Test]
        public void Test3() //No Last Name
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/cart.html");
            driver.FindElement(By.Id("checkout")).Click();
            Thread.Sleep(3000);
            IWebElement Firstname = driver.FindElement(By.Id("first-name"));
            Firstname.SendKeys("Marwa");
            IWebElement Lastname = driver.FindElement(By.Id("last-name"));
            Lastname.SendKeys("");
            IWebElement Postal_Code = driver.FindElement(By.Id("postal-code"));
            Postal_Code.SendKeys("12553");
            IWebElement Continue = driver.FindElement(By.Id("continue"));
            Thread.Sleep(1000);
            Continue.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IWebElement errorMessage = driver.FindElement(By.XPath("//*[@data-test='error']"));
            Assert.That(errorMessage.Displayed, Is.True);
        }

        [Test]
        public void Test4() //No postalCode
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/cart.html");
            driver.FindElement(By.Id("checkout")).Click();
            Thread.Sleep(3000);
            IWebElement Firstname = driver.FindElement(By.Id("first-name"));
            Firstname.SendKeys("Marwa");
            IWebElement Lastname = driver.FindElement(By.Id("last-name"));
            Lastname.SendKeys("Elhelow");
            IWebElement Postal_Code = driver.FindElement(By.Id("postal-code"));
            Postal_Code.SendKeys("");
            IWebElement Continue = driver.FindElement(By.Id("continue"));
            Thread.Sleep(1000);
            Continue.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IWebElement errorMessage = driver.FindElement(By.XPath("//*[@data-test='error']"));
            Assert.That(errorMessage.Displayed, Is.True);
        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

    }
}
