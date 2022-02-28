using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace MoveYourBody.Frontend.Tests
{
    public class ProfileSettingsPageTests
    {
        [Fact]
        public void DataLoadedTest()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();

                By.XPath("//ul[@id='userDropdown']/li/a").WaitForExists(driver, 25);

                Assert.Equal("Edzõ József", driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Text);

                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Click();
                By.XPath("//a[contains(@href, '/profile')]").WaitForExists(driver, 5);
                driver.FindElement(By.XPath("//a[contains(@href, '/profile')]")).Click();
                By.ClassName("container").WaitForExists(driver, 5);


                Assert.Equal("Edzõ József", driver.FindElement(By.Id("username")).GetAttribute("value"));
                Assert.Equal("jozsiedzo@email.com", driver.FindElement(By.Id("email")).GetAttribute("value"));
                Assert.Equal("+36701234567", driver.FindElement(By.Id("phoneNumber")).GetAttribute("value"));
                Assert.Equal("Gyõr-Moson-Sopron", driver.FindElement(By.Id("county")).GetAttribute("value"));
                Assert.Equal("Csorna", driver.FindElement(By.Id("city")).GetAttribute("value"));
                Assert.Equal("Edzõség lemondása", driver.FindElement(By.Id("cancelSubscriptionButton")).Text);

            }
        }

        [Fact]
        public void PasswordChangeTest()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();
                By.XPath("//ul[@id='userDropdown']/li/a").WaitForExists(driver, 25);

                Assert.Equal("Edzõ József", driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Text);

                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Click();
                driver.FindElement(By.XPath("//a[contains(@href, '/profile')]")).WaitForDisplayed(5);
                driver.FindElement(By.XPath("//a[contains(@href, '/profile')]")).Click();

                By.ClassName("container").WaitForExists(driver, 25);
                Assert.Equal("Jelszó módosítása", driver.FindElement(By.Id("passwordButton")).Text);
                driver.FindElement(By.Id("passwordButton")).Click();
               

                driver.FindElement(By.Id("cancelbtn")).WaitForDisplayed(5);
                Assert.Equal("X", driver.FindElement(By.Id("cancelbtn")).Text);

                Assert.Equal("", driver.FindElement(By.Name("password")).Text);

                driver.FindElement(By.Name("password2")).WaitForDisplayed(5);
                Assert.Equal("", driver.FindElement(By.Name("password2")).Text);

                //logout
                driver.FindElement(By.LinkText("Edzõ József")).Click();
                driver.FindElement(By.LinkText("Kijelentkezés")).Click();
            }
        }
        [Fact]
        public void TrainerChangeTest()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();
                By.XPath("//ul[@id='userDropdown']/li/a").WaitForExists(driver, 25);
                Assert.Equal("Edzõ József", driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Text);

                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Click();
                driver.FindElement(By.XPath("//a[contains(@href, '/profile')]")).WaitForDisplayed(5);
                driver.FindElement(By.XPath("//a[contains(@href, '/profile')]")).Click();
                By.ClassName("container").WaitForExists(driver, 5);

                driver.FindElement(By.Id("cancelSubscriptionButton")).WaitForDisplayed(5);
                Assert.Equal("Edzõség lemondása", driver.FindElement(By.Id("cancelSubscriptionButton")).Text);
                driver.FindElement(By.Id("cancelSubscriptionButton")).Click();

                driver.FindElement(By.Id("subscribeButton")).WaitForDisplayed(5);
                Assert.Equal("Edzõvé válok!", driver.FindElement(By.Id("subscribeButton")).Text);
                driver.FindElement(By.Id("subscribeButton")).Click();

                //logout
                driver.FindElement(By.LinkText("Edzõ József")).Click();
                driver.FindElement(By.LinkText("Kijelentkezés")).Click();
            }
        }

    }
}
