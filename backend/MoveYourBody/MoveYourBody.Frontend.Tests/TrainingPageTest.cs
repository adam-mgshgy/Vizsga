using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using Xunit;

namespace MoveYourBody.Frontend.Tests
{
    public class TrainingPageTest
    {
        [Fact]
        public void TextOfTrainings()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jani@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jani");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();

                System.Threading.Thread.Sleep(300);

                driver.Navigate().GoToUrl("http://localhost:4200/training/1");

                System.Threading.Thread.Sleep(3000);

                driver.FindElement(By.XPath("//td[@id='apply1']/button")).Click();
                Assert.Equal("5/10", driver.FindElement(By.XPath("//td[@id='numberOfApplicants1']")).Text);
                Assert.Equal("Erre az edzésre már jelentkezett!", driver.FindElement(By.XPath("//p[@id='error']")).Text);

                driver.FindElement(By.XPath("//td[@id='apply3']/button")).Click();
                System.Threading.Thread.Sleep(300);
                Assert.Equal("3/10", driver.FindElement(By.XPath("//td[@id='numberOfApplicants3']")).Text);
                System.Threading.Thread.Sleep(300);

                Assert.Equal("Sikeres jelentkezés!", driver.FindElement(By.XPath("//p[@id='success']")).Text);


            }
        }

       

    }
}
