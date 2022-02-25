using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace MoveYourBody.Frontend.Tests
{
    public class CategoriesPageTest
    {
        [Fact]
        public void NumberOfCategories()
        {
            using (var driver = new ChromeDriver())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("http://localhost:4200/login");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Name("email")).SendKeys("jozsiedzo@email.com");
                driver.FindElement(By.Name("password")).SendKeys("jozsi");

                System.Threading.Thread.Sleep(5000);

                driver.Navigate().GoToUrl("http://localhost:4200/categories");
                driver.Manage().Window.Maximize();

                System.Threading.Thread.Sleep(5000);
                

                //driver.FindElement(By.XPath("//span[12]/img")).WaitForDisplayed(5);
  

               // Assert.Equal("J�ga", driver.FindElement(By.XPath("span:nth-child(12)>#catImg")).Text);
                //driver.FindElement(By.CssSelector(".btnSubmit")).Click();
                //driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).WaitForDisplayed(500);


                //Assert.Equal("Edz� J�zsef", driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Text);
                //driver.FindElement(By.LinkText("Edz� J�zsef")).Click();
                //driver.FindElement(By.LinkText("Kijelentkez�s")).Click();

            }
        }
    }
}
