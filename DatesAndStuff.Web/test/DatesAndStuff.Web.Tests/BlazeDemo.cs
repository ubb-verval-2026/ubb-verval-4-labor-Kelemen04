using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Text;

namespace DatesAndStuff.Web.Tests
{
    [TestFixture]
    public class BlazeDemo
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
                driver.Dispose();
            }
            catch (Exception)
            {
                // Ignore errors
            }
            Assert.That(verificationErrors.ToString(), Is.EqualTo(""));
        }

        [Test]
        public void MexicoCityDublin_MinThreeFlights_Check()
        {
            driver.Navigate().GoToUrl("https://blazedemo.com");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var from = wait.Until(ExpectedConditions.ElementExists(By.XPath("//select[@name='fromPort']")));
            from.SendKeys("Mexico City");

            var to = wait.Until(ExpectedConditions.ElementExists(By.XPath("//select[@name='toPort']")));
            to.SendKeys("Dublin");

            var submitButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='submit']")));
            submitButton.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table[@class='table']/tbody/tr")));

            var rows = driver.FindElements(By.XPath("//table[@class='table']/tbody/tr"));

            rows.Count.Should().BeGreaterThanOrEqualTo(3, "we need atleast 3 flights from Mexico City to Dublin");
        }
    }
}