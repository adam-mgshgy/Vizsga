using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace MoveYourBody.Frontend.Tests
{
    public class EditTrainingPageTests
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

                Assert.Equal("Edzõ József", driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Text);

                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Click();
                driver.FindElement(By.LinkText("Edzéseim")).WaitForDisplayed(5);
                driver.FindElement(By.LinkText("Edzéseim")).Click();
                driver.FindElement(By.ClassName("container")).WaitForDisplayed(20);

                driver.FindElement(By.Id("modify")).WaitForDisplayed(5);
                Assert.Equal("Szerkesztés", driver.FindElement(By.Id("modify")).Text);

                driver.FindElement(By.Id("name")).WaitForDisplayed(5);
                Assert.Equal("Edzés 1", driver.FindElement(By.Id("name")).Text);

                driver.FindElement(By.Id("category")).WaitForDisplayed(5);
                Assert.Equal("Box", driver.FindElement(By.Id("category")).Selected.ToString());

                driver.FindElement(By.Id("description")).WaitForDisplayed(5);
                Assert.Equal("Rövid leírás az edzésrõl még sokkal hosszabb leírás fúha nagyon hosszú ki se fér, lássuk meddig megy a szöveg olvassuk tovább", driver.FindElement(By.Id("description")).Text);

                driver.FindElement(By.XPath(".mb-3 > .form-check-label:nth-child(2)")).WaitForDisplayed(5);
                Assert.Equal("A saját telefonszámom szeretném használni (+36701234567)", driver.FindElement(By.XPath(".mb-3 > .form-check-label:nth-child(2)")).Text);

                driver.FindElement(By.Id("Csoportos")).WaitForDisplayed(5);
                Assert.Equal("Csoportos", driver.FindElement(By.Id("Csoportos")).Text);

                //logout
                driver.FindElement(By.LinkText("Edzõ József")).Click();
                driver.FindElement(By.LinkText("Kijelentkezés")).Click();
            }
        }
    }
}
