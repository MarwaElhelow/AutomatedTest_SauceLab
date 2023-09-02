using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sauce_Demo2
{
    public class Add_To_Cart
    {
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            IWebElement username = driver.FindElement(By.Id("user-name"));
            IWebElement Password = driver.FindElement(By.Id("password"));
            username.SendKeys("standard_user");
            Password.SendKeys("secret_sauce");
            IWebElement Loginbtn = driver.FindElement(By.Id("login-button"));
            Loginbtn.Click();
            Thread.Sleep(5000);

        }

        [Test]
        public void TestAddToCart()
        {

            //Random rnd = new Random();
            //int ProductsToAdd = rnd.Next(1, 7);
            //for (int i = 0; i < ProductsToAdd; i++)
            //{
            //  int ProductCount = rnd.Next(1, 7);
            //    string productID = $"add-to-cart-sauce-labs-backpack-{ProductCount}";
            //    driver.FindElement(By.Id(productID)).Click();
            //}
            driver.FindElement(By.Id("add-to-cart-sauce-labs-bolt-t-shirt")).Click();
            Assert.That(driver.FindElement(By.ClassName("shopping_cart_link")).Text, Is.EqualTo("1"));
            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/cart.html");
            Thread.Sleep(5000);

            //driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();

            IWebElement Cart_Page_Name = driver.FindElement(By.CssSelector("#header_container > div.header_secondary_container > span"));
            Assert.That(Cart_Page_Name.Text, Is.EqualTo("Your Cart"));

            //int ExpectedAddedProducts = ProductsToAdd;
            //int ActualAddedProducts = int.Parse(driver.FindElement(By.ClassName("shopping_cart_badge")).Text);
            //Assert.AreEqual(ExpectedAddedProducts, ActualAddedProducts);



        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }


}

