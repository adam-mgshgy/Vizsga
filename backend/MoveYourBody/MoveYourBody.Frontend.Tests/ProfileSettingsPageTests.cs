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
                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).WaitForDisplayed(5);

                Assert.Equal("Edz� J�zsef", driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Text);

                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Click();
                driver.FindElement(By.XPath("//a[contains(@href, '/profile')]")).WaitForDisplayed(5);
                driver.FindElement(By.XPath("//a[contains(@href, '/profile')]")).Click();
                driver.FindElement(By.ClassName("container")).WaitForDisplayed(20);
                driver.FindElement(By.Id("username")).WaitForDisplayed(5);
                Assert.Equal("Edz� J�zsef", driver.FindElement(By.Id("username")).Text);

                driver.FindElement(By.Id("email")).WaitForDisplayed(5);
                Assert.Equal("jozsiedzo@email.com", driver.FindElement(By.Id("email")).Text);

                driver.FindElement(By.Id("phoneNumber")).WaitForDisplayed(5);
                Assert.Equal("+36701234567", driver.FindElement(By.Id("phoneNumber")).Text);

                driver.FindElement(By.Id("county")).WaitForDisplayed(5);
                Assert.Equal("Gy�r-Moson-Sopron", driver.FindElement(By.Id("county")).Text);

                driver.FindElement(By.Id("city")).WaitForDisplayed(5);
                Assert.Equal("Csorna", driver.FindElement(By.Id("city")).Text);

                driver.FindElement(By.Id("cancelSubscriptionButton")).WaitForDisplayed(5);
                Assert.Equal("Edz�s�g lemond�sa", driver.FindElement(By.Id("cancelSubscriptionButton")).Text);

                //logout
                driver.FindElement(By.LinkText("Edz� J�zsef")).Click();
                driver.FindElement(By.LinkText("Kijelentkez�s")).Click();
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
                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).WaitForDisplayed(5);

                Assert.Equal("Edz� J�zsef", driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Text);

                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Click();
                driver.FindElement(By.XPath("//a[contains(@href, '/profile')]")).WaitForDisplayed(5);
                driver.FindElement(By.XPath("//a[contains(@href, '/profile')]")).Click();
                driver.FindElement(By.ClassName("container")).WaitForDisplayed(20);

                driver.FindElement(By.Id("passwordButton")).WaitForDisplayed(5);
                Assert.Equal("Jelsz� m�dos�t�sa", driver.FindElement(By.Id("passwordButton")).Text);
                driver.FindElement(By.Id("passwordButton")).Click();

                driver.FindElement(By.Id("cancelbtn")).WaitForDisplayed(5);
                Assert.Equal("X", driver.FindElement(By.Id("cancelbtn")).Text);

                driver.FindElement(By.Name("password")).WaitForDisplayed(5);
                Assert.Equal("", driver.FindElement(By.Name("password")).Text);

                driver.FindElement(By.Name("password2")).WaitForDisplayed(5);
                Assert.Equal("", driver.FindElement(By.Name("password2")).Text);


                //logout
                driver.FindElement(By.LinkText("Edz� J�zsef")).Click();
                driver.FindElement(By.LinkText("Kijelentkez�s")).Click();
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
                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).WaitForDisplayed(5);

                Assert.Equal("Edz� J�zsef", driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Text);

                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Click();
                driver.FindElement(By.XPath("//a[contains(@href, '/profile')]")).WaitForDisplayed(5);
                driver.FindElement(By.XPath("//a[contains(@href, '/profile')]")).Click();
                driver.FindElement(By.ClassName("container")).WaitForDisplayed(20);

                driver.FindElement(By.Id("cancelSubscriptionButton")).WaitForDisplayed(5);
                Assert.Equal("Edz�s�g lemond�sa", driver.FindElement(By.Id("cancelSubscriptionButton")).Text);
                driver.FindElement(By.Id("cancelSubscriptionButton")).Click();

                driver.FindElement(By.Id("subscribeButton")).WaitForDisplayed(5);
                Assert.Equal("Edz�v� v�lok!", driver.FindElement(By.Id("subscribeButton")).Text);
                driver.FindElement(By.Id("subscribeButton")).Click();

                //logout
                driver.FindElement(By.LinkText("Edz� J�zsef")).Click();
                driver.FindElement(By.LinkText("Kijelentkez�s")).Click();
            }
        }

    }
}
