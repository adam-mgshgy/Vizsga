using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using Xunit;

namespace MoveYourBody.Frontend.Tests
{
    public class CategoriesPageTest
    {
        [Fact]
        public void TextOfCategories()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();

                driver.Navigate().GoToUrl("http://localhost:4200/categories");
                driver.Manage().Window.Maximize();

                System.Threading.Thread.Sleep(300);
                driver.FindElement(By.XPath("(//a[@id='catRef']/span)[144]"));
                Assert.Equal("Jóga", driver.FindElement(By.XPath("(//a[@id='catRef']/span)[144]")).Text);
                Assert.Equal(12, driver.FindElements(By.XPath("//img[@id='catImg']")).Count);

               
            }
        }
        [Fact]
        public void CategoriesDropdown()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();

                System.Threading.Thread.Sleep(300);

                driver.FindElement(By.XPath("//a[contains(text(),'Kategóriák')]")).Click();
                System.Threading.Thread.Sleep(30);
                Assert.Equal(12, driver.FindElements(By.XPath("//img[@id='catImg']")).Count);
            }
        }
        [Fact]
        public void CheckUrl()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");
                driver.FindElement(By.CssSelector(".btnSubmit")).Click();

                driver.Navigate().GoToUrl("http://localhost:4200/categories");
                driver.Manage().Window.Maximize();

                System.Threading.Thread.Sleep(300);

                driver.FindElement(By.XPath("(//a[@id='catRef']/span)[12]")).Click();
                System.Threading.Thread.Sleep(30);
                string url = driver.Url;
                Assert.Equal("http://localhost:4200/trainings/category/1", url);
            }
        }
    }
}
