using OpenQA.Selenium;
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

    }
}
