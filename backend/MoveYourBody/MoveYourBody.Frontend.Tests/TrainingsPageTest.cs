using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using Xunit;

namespace MoveYourBody.Frontend.Tests
{
    public class TrainingsPageTest
    {
        [Fact]
        public void TextOfTrainings()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();
                System.Threading.Thread.Sleep(300);

                driver.FindElement(By.XPath("//a[contains(text(),'Edzések')]")).Click();
                
                System.Threading.Thread.Sleep(300);

                driver.FindElement(By.XPath("//h3[contains(.,'GézaFitt')]"));
                Assert.Equal("GézaFitt", driver.FindElement(By.XPath("//h3[contains(.,'GézaFitt')]")).Text);
                Assert.Equal(7, driver.FindElements(By.XPath("//div[@id='training']")).Count);
                Assert.Equal("Erõnléti", driver.FindElement(By.XPath("(//mat-chip[@id='tag'])[14]")).Text);
                Assert.Equal("Minden Edzés", driver.FindElement(By.XPath("//h1[@id='allTrainings']")).Text);
            }
        }

        [Fact]
        public void FilterByTrainer()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();                

                driver.Navigate().GoToUrl("http://localhost:4200/trainings");

                System.Threading.Thread.Sleep(300);

                driver.FindElement(By.XPath("//h6[contains(.,'Edzõ József')]")).Click();
                Assert.Equal("Edzõ József edzései", driver.FindElement(By.XPath("//h1[@id='filteredByTrainer']")).Text);
                Assert.Equal(2, driver.FindElements(By.XPath("//div[@id='training']")).Count);
            }
        }

        [Fact]
        public void FilterByCounty()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();

                driver.Navigate().GoToUrl("http://localhost:4200/trainings/county/Somogy");

                System.Threading.Thread.Sleep(300);

                Assert.Equal("Somogy megyei edzések", driver.FindElement(By.XPath("//h1[@id='filteredByCounty']")).Text);
                Assert.Equal(4, driver.FindElements(By.XPath("//div[@id='training']")).Count);
            }
        }

        [Fact]
        public void FilterByCity()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();

                driver.Navigate().GoToUrl("http://localhost:4200/trainings/city/Csurgó");

                System.Threading.Thread.Sleep(300);

                Assert.Equal("Csurgó város edzései", driver.FindElement(By.XPath("//h1[@id='filteredByCity']")).Text);
                Assert.Equal(4, driver.FindElements(By.XPath("//div[@id='training']")).Count);
            }
        }

        [Fact]
        public void FilterByTag()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();

                driver.Navigate().GoToUrl("http://localhost:4200/trainings");

                System.Threading.Thread.Sleep(300);

                driver.FindElement(By.XPath("//mat-chip[contains(.,'Csoportos')]")).Click();
                string url = driver.Url;
                Assert.Equal("http://localhost:4200/trainings/tag/1", url);
                Assert.Equal("Csoportos edzések", driver.FindElement(By.XPath("//h1[@id='filteredByTag']")).Text);
                Assert.Equal(5, driver.FindElements(By.XPath("//div[@id='training']")).Count);

            }
        }

        [Fact]
        public void FilterByCategory()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();

                driver.Navigate().GoToUrl("http://localhost:4200/trainings");

                System.Threading.Thread.Sleep(300);

                driver.FindElement(By.XPath("//button[contains(.,'Kézilabda')]")).Click();
                string url = driver.Url;
                Assert.Equal("http://localhost:4200/trainings/category/5", url);
                Assert.Equal("Kézilabda edzések", driver.FindElement(By.XPath("//h1[@id='filteredByCategory']")).Text);
                Assert.Equal(2, driver.FindElements(By.XPath("//div[@id='training']")).Count);

            }
        }

        [Fact]
        public void FilterBySearch()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();

                driver.Navigate().GoToUrl("http://localhost:4200/trainings");

                System.Threading.Thread.Sleep(300);

                driver.FindElement(By.XPath("//input[@name='trainingName']")).SendKeys("Edzés");
                driver.FindElement(By.XPath("//button[@id='searchBtn']")).Click();

                string url = driver.Url;
                Assert.Equal("http://localhost:4200/trainings/name/Edz%C3%A9s", url);
                Assert.Equal("\"Edzés\" nevû edzések", driver.FindElement(By.XPath("//h1[@id='filteredBySearch']")).Text);
                Assert.Equal(7, driver.FindElements(By.XPath("//div[@id='training']")).Count);

            }
        }

    }
}
