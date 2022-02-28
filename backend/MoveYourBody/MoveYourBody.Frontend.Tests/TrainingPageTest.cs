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

                By.XPath("//ul[@id='userDropdown']/li/a").WaitForExists(driver, 5);                

                driver.Navigate().GoToUrl("http://localhost:4200/training/1");
                
                By.XPath("//td[@id='apply1']/button").WaitForExists(driver, 5);
                driver.FindElement(By.XPath("//td[@id='apply1']/button")).Click();
                Assert.Equal("5/10", driver.FindElement(By.XPath("//td[@id='numberOfApplicants1']")).Text);
                Assert.Equal("Erre az edzésre már jelentkezett!", driver.FindElement(By.XPath("//p[@id='error']")).Text);

                driver.FindElement(By.XPath("//td[@id='apply3']/button")).Click();
                
                Assert.Equal("3/10", driver.FindElement(By.XPath("//td[@id='numberOfApplicants3']")).Text);
                

                Assert.Equal("Sikeres jelentkezés!", driver.FindElement(By.XPath("//p[@id='success']")).Text);


            }
        }

       

    }
}
