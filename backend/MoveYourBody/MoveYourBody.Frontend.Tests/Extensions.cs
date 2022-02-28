using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoveYourBody.Frontend.Tests
{
    public static class Extensions
    {
        public static void WaitForDisplayed(this IWebElement element, int seconds)
        {

            int i = 0;
            while (i < seconds*4 && !element.Displayed)
            {
                System.Threading.Thread.Sleep(250);
                i++;
            }
        }

        public static void WaitForExists(this By by, ChromeDriver driver,int seconds)
        {

            int i = 0;
            while (i < seconds * 4 && driver.FindElements(by).Count != 0)
            {
                System.Threading.Thread.Sleep(250);
                i++;
            }
        }
    }
}
