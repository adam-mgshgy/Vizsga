using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using Xunit;

namespace MoveYourBody.Frontend.Tests
{
    public class MyTrainingsPageTest
    {
        [Fact]
        public void UserViewApplications()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("evi@email.com");
                driver.FindElement(By.Name("password")).SendKeys("evi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();

                By.XPath("//ul[@id='userDropdown']/li/a").WaitForExists(driver, 3);                

                driver.Navigate().GoToUrl("http://localhost:4200/mytrainings/applied");

                Assert.Equal(3, driver.FindElements(By.XPath("//div[@id='training']")).Count);
                driver.FindElement(By.XPath("//button[@id='sessions']")).Click();
                By.XPath("//button[@id='cancel1']").WaitForExists(driver, 10);
                driver.FindElement(By.XPath("//button[@id='cancel1']")).Click();
                driver.Navigate().GoToUrl("http://localhost:4200/training/1");

                By.XPath("//td[@id='apply1']/button").WaitForExists(driver, 3);
                driver.FindElement(By.XPath("//td[@id='apply1']/button")).Click();
            }
        }

        [Fact]
        public void TrainerViewApplications()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();

                By.XPath("//ul[@id='userDropdown']/li/a").WaitForExists(driver, 3);

                driver.Navigate().GoToUrl("http://localhost:4200/mytrainings/applied");
                Assert.Equal("Még nem jelentkezett egy edzésre sem!", driver.FindElement(By.XPath("//h2[@id='notApplied']")).Text);
                Assert.Equal("Megnézem az összes kategóriát!", driver.FindElement(By.XPath("//h2[@id='categories']/a")).Text);

                driver.FindElement(By.XPath("//h2[@id='categories']/a")).Click();
                string url = driver.Url;
                Assert.Equal("http://localhost:4200/categories", url);

                driver.Navigate().GoToUrl("http://localhost:4200/training/3");
                By.XPath("//td[@id='apply6']/button").WaitForExists(driver, 3);
                driver.FindElement(By.XPath("//td[@id='apply6']/button")).Click();

                driver.Navigate().GoToUrl("http://localhost:4200/mytrainings/applied");

                Assert.Equal(1, driver.FindElements(By.XPath("//div[@id='training']")).Count);
                Assert.Equal("Edzés 3", driver.FindElement(By.CssSelector(".card-title")).Text);

                driver.FindElement(By.XPath("//button[@id='sessions']")).Click();
                By.XPath("//button[@id='cancel6']").WaitForExists(driver, 5);
                driver.FindElement(By.XPath("//button[@id='cancel6']")).Click();                               
            }
        }

        [Fact]
        public void TrainerViewTrainings()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();

                By.XPath("//ul[@id='userDropdown']/li/a").WaitForExists(driver, 3);

                driver.Navigate().GoToUrl("http://localhost:4200/mytrainings/trainer");
                Assert.Equal(2, driver.FindElements(By.XPath("//div[@id='training']")).Count);

                driver.FindElement(By.XPath("//button[@id='newTraining']")).Click();
                string url = driver.Url;
                Assert.Equal("http://localhost:4200/createtraining", url);

                driver.Navigate().GoToUrl("http://localhost:4200/mytrainings/trainer");
                driver.FindElement(By.XPath("//button[@id='modify']")).Click();
                url = driver.Url;
                Assert.Equal("http://localhost:4200/createtraining/1", url);

                driver.Navigate().GoToUrl("http://localhost:4200/mytrainings/trainer");
                driver.FindElement(By.XPath("//button[@id='newSession']")).Click();
                url = driver.Url;
                Assert.Equal("http://localhost:4200/addsession/1/0", url);

                driver.Navigate().GoToUrl("http://localhost:4200/mytrainings/trainer");
                driver.FindElement(By.XPath("(//button[@id='sessions'])[2]")).Click();
                driver.FindElement(By.XPath("//button[contains(.,'Duplikálás')]")).Click();
                url = driver.Url;
                Assert.Equal("http://localhost:4200/addsession/2/4", url);
            }
        }

    }
}
