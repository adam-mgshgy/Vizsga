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
                By.XPath("//ul[@id='userDropdown']/li/a").WaitForExists(driver, 25);

                Assert.Equal("Edzõ József", driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Text);

                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Click();
                By.LinkText("Edzéseim").WaitForExists(driver, 5);
                driver.FindElement(By.LinkText("Edzéseim")).Click();
                By.ClassName("container").WaitForExists(driver, 25);
                By.Id("modify").WaitForExists(driver, 25);
                Assert.Equal("Szerkesztés", driver.FindElement(By.Id("modify")).Text);
                driver.FindElement(By.XPath("//button[@id='modify']")).Click();
                By.ClassName("container").WaitForExists(driver, 25);

                Assert.Equal("Edzés szerkesztése", driver.FindElement(By.CssSelector("h1")).Text);
                Assert.Equal("Box Józsival", driver.FindElement(By.Id("name")).GetAttribute("value"));

                Assert.Equal("Box", driver.FindElement(By.Id("category")).GetAttribute("value"));

                Assert.Equal("Szeretettel vár Józsi Edzõ a legnépszerûbb box edzésén! Kezdõket és haladókat is szívesen fogadunk. Nem kell más, csak egy törölközõ, víz, és hatalmas lelkesedés!", driver.FindElement(By.Id("description")).GetAttribute("value"));

                Assert.Equal("A saját telefonszámom szeretném használni (+36701234567)", driver.FindElement(By.CssSelector(".mb-3 > .form-check-label:nth-child(2)")).Text);

                Assert.Equal("Csoportos", driver.FindElement(By.CssSelector(".checks:nth-child(1) > .form-check-label")).Text);
                Assert.Equal("Saját testsúlyos", driver.FindElement(By.CssSelector(".checks:nth-child(2) > .form-check-label")).Text);
                Assert.Equal("Aerobic", driver.FindElement(By.CssSelector(".checks:nth-child(8) > .form-check-label")).Text);
                Assert.Equal("Köredzés", driver.FindElement(By.CssSelector(".checks:nth-child(10) > .form-check-label")).Text);

                //logout
                driver.FindElement(By.LinkText("Edzõ József")).Click();
                driver.FindElement(By.LinkText("Kijelentkezés")).Click();
            }
        }
    }
}
