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

                driver.FindElement(By.XPath("//a[contains(text(),'Edz�sek')]")).Click();
                
                System.Threading.Thread.Sleep(300);

                driver.FindElement(By.XPath("//h3[contains(.,'G�zaFitt')]"));
                Assert.Equal("G�zaFitt", driver.FindElement(By.XPath("//h3[contains(.,'G�zaFitt')]")).Text);
                Assert.Equal(7, driver.FindElements(By.XPath("//div[@id='training']")).Count);
                Assert.Equal("Er�nl�ti", driver.FindElement(By.XPath("(//mat-chip[@id='tag'])[14]")).Text);
                Assert.Equal("Minden Edz�s", driver.FindElement(By.XPath("//h1[@id='allTrainings']")).Text);
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

                driver.FindElement(By.XPath("//h6[contains(.,'Edz� J�zsef')]")).Click();
                Assert.Equal("Edz� J�zsef edz�sei", driver.FindElement(By.XPath("//h1[@id='filteredByTrainer']")).Text);
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

                Assert.Equal("Somogy megyei edz�sek", driver.FindElement(By.XPath("//h1[@id='filteredByCounty']")).Text);
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

                driver.Navigate().GoToUrl("http://localhost:4200/trainings/city/Csurg�");

                System.Threading.Thread.Sleep(300);

                Assert.Equal("Csurg� v�ros edz�sei", driver.FindElement(By.XPath("//h1[@id='filteredByCity']")).Text);
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
                Assert.Equal("Csoportos edz�sek", driver.FindElement(By.XPath("//h1[@id='filteredByTag']")).Text);
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

                driver.FindElement(By.XPath("//button[contains(.,'K�zilabda')]")).Click();
                string url = driver.Url;
                Assert.Equal("http://localhost:4200/trainings/category/5", url);
                Assert.Equal("K�zilabda edz�sek", driver.FindElement(By.XPath("//h1[@id='filteredByCategory']")).Text);
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

                driver.FindElement(By.XPath("//input[@name='trainingName']")).SendKeys("Edz�s");
                driver.FindElement(By.XPath("//button[@id='searchBtn']")).Click();

                string url = driver.Url;
                Assert.Equal("http://localhost:4200/trainings/name/Edz%C3%A9s", url);
                Assert.Equal("\"Edz�s\" nev� edz�sek", driver.FindElement(By.XPath("//h1[@id='filteredBySearch']")).Text);
                Assert.Equal(7, driver.FindElements(By.XPath("//div[@id='training']")).Count);

            }
        }

    }
}
