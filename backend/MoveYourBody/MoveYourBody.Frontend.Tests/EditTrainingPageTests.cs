using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace MoveYourBody.Frontend.Tests
{
    public class UnitTest1
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
                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).WaitForDisplayed(5);
               

                Assert.Equal("Edz� J�zsef", driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Text);
                driver.FindElement(By.LinkText("Edz� J�zsef")).Click();
                driver.FindElement(By.LinkText("Kijelentkez�s")).Click();

            }
        }
    }
}
