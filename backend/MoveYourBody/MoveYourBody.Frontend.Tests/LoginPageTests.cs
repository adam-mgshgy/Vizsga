using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace MoveYourBody.Frontend.Tests
{
    public class LoginPageTests
    {
        [Fact]
        public void LoginTest()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).WaitForDisplayed(50);

                System.Threading.Thread.Sleep(3000);

                Assert.Equal("Edzõ József", driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Text);
                driver.FindElement(By.LinkText("Edzõ József")).Click();
                driver.FindElement(By.LinkText("Kijelentkezés")).Click();

            }
        }
    }
}
