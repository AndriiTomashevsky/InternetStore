using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace InternetStore.UITests
{
    [TestClass]
    public class GoogleSearchTest
    {
        [TestMethod]
        public void TestSearch()
        {
            IWebDriver driver = new ChromeDriver(@"c:\СhromeDriver\");

            driver.Navigate().GoToUrl("https://www.google.com.ua/");

            IWebElement element = driver.FindElement(By.Name("q"));

            element.SendKeys("Hello Selenium WebDriver!");

            element.Submit();
        }
    }
}
