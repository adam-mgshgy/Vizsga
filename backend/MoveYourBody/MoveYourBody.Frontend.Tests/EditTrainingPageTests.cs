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

                Assert.Equal("Edz� J�zsef", driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Text);

                driver.FindElement(By.XPath("//ul[@id='userDropdown']/li/a")).Click();
                By.LinkText("Edz�seim").WaitForExists(driver, 5);
                driver.FindElement(By.LinkText("Edz�seim")).Click();
                By.ClassName("container").WaitForExists(driver, 25);
                By.Id("modify").WaitForExists(driver, 25);
                Assert.Equal("Szerkeszt�s", driver.FindElement(By.Id("modify")).Text);
                driver.FindElement(By.XPath("//button[@id='modify']")).Click();
                By.ClassName("container").WaitForExists(driver, 25);

                Assert.Equal("Edz�s szerkeszt�se", driver.FindElement(By.CssSelector("h1")).Text);
                Assert.Equal("Box J�zsival", driver.FindElement(By.Id("name")).GetAttribute("value"));

                Assert.Equal("Box", driver.FindElement(By.Id("category")).GetAttribute("value"));

                Assert.Equal("Szeretettel v�r J�zsi Edz� a legn�pszer�bb box edz�s�n! Kezd�ket �s halad�kat is sz�vesen fogadunk. Nem kell m�s, csak egy t�r�lk�z�, v�z, �s hatalmas lelkesed�s!", driver.FindElement(By.Id("description")).GetAttribute("value"));

                Assert.Equal("A saj�t telefonsz�mom szeretn�m haszn�lni (+36701234567)", driver.FindElement(By.CssSelector(".mb-3 > .form-check-label:nth-child(2)")).Text);

                Assert.Equal("Csoportos", driver.FindElement(By.CssSelector(".checks:nth-child(1) > .form-check-label")).Text);
                Assert.Equal("Saj�t tests�lyos", driver.FindElement(By.CssSelector(".checks:nth-child(2) > .form-check-label")).Text);
                Assert.Equal("Aerobic", driver.FindElement(By.CssSelector(".checks:nth-child(8) > .form-check-label")).Text);
                Assert.Equal("K�redz�s", driver.FindElement(By.CssSelector(".checks:nth-child(10) > .form-check-label")).Text);

                //logout
                driver.FindElement(By.LinkText("Edz� J�zsef")).Click();
                driver.FindElement(By.LinkText("Kijelentkez�s")).Click();
            }
        }
    }
}
