using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;
using System.IO;
using OpenQA.Selenium.Firefox;

namespace SeleniumTests
{
    internal class Program
    {
        static void LogsWriter(IWebDriver _tempDriver, string _title, StreamWriter _logWriter)
        {
            _logWriter.WriteLine("[" + DateTime.Now + "]" + _tempDriver.ToString() + "\n[" + DateTime.Now + "]" + _title);
            Console.WriteLine("[" + DateTime.Now + "]" + _tempDriver.ToString() + "\n[" + DateTime.Now + "]" + _title);
        }
        static void LogsWriter(string _temtElement, StreamWriter _logWriter)
        {
            _logWriter.WriteLineAsync("[" + DateTime.Now + "]" + _temtElement);
            Console.WriteLine("[" + DateTime.Now + "]" + _temtElement);
        }
        static void LogsWriter(WebElement _tempElement, StreamWriter _logWriter)
        {
            _logWriter.WriteLineAsync("[" + DateTime.Now + "]" + _tempElement.GetAttribute("text").ToString());
            Console.WriteLine("[" + DateTime.Now + "]" + _tempElement.GetAttribute("text").ToString());
        }
        static void Main(string[] args)
        {
            IWebElement element;
            string path = "log.txt";
            StreamWriter logWriter;
            IWebDriver cDriver = new ChromeDriver();
            //IWebDriver fDriver = new FirefoxDriver();
            IWebDriver[] tempDrivers = { cDriver/*, fDriver */};
            try
            {
                using (logWriter = new StreamWriter(path, false)) { }
                logWriter = new StreamWriter(path, true);
                foreach (var tempDriver in tempDrivers)
                {
                    tempDriver.Manage().Window.Maximize();
                    tempDriver.Navigate().GoToUrl(@"https://habr.com");
                    WebElement parentElement = (WebElement)tempDriver.FindElement(By.CssSelector("#app > div.tm-layout__wrapper div.tm-base-layout__header div.tm-main-menu nav"));
                    var _elements = parentElement.FindElements(By.TagName("a"));
                    LogsWriter(tempDriver, "\t\tНазвания пунктов главного меню:\t", logWriter);
                    for (int i = 0; i < _elements.Count; i++)
                    {
                        element = _elements[i];
                        element.Click();
                        LogsWriter(element.GetAttribute("text").ToString().Trim(), logWriter);
                        Thread.Sleep(750);
                    }
                    tempDriver.Navigate().GoToUrl(@"https://habr.com");
                    Thread.Sleep(2000);
                    parentElement = (WebElement)tempDriver.FindElement(By.XPath("//div[@class='tm-articles-list']"));
                    _elements = parentElement.FindElements(By.TagName("h2"));
                    WebElement chaildElement;
                    LogsWriter("\t\tСписок заголовков статей:", logWriter);
                    for (int i = 0; i < _elements.Count; i++)
                    {
                        chaildElement = (WebElement)_elements[i].FindElement(By.TagName("a"));
                        LogsWriter(chaildElement, logWriter);
                        Thread.Sleep(250);
                    }
                    element = tempDriver.FindElement(By.XPath("//a[@class='tm-header-user-menu__item tm-header-user-menu__search']"));
                    element.Click();
                    Thread.Sleep(1000);
                    element = tempDriver.FindElement(By.XPath("//input[@name='q']"));
                    element.Click();
                    element.SendKeys("selenium");
                    element.SendKeys(Keys.Enter);
                    element = tempDriver.FindElement(By.XPath("//button[@class='tm-navigation-dropdown__button tm-navigation-dropdown__button']"));
                    element.Click();
                    Thread.Sleep(500);
                    element = tempDriver.FindElement(By.XPath("//ul[@class='tm-navigation-dropdown__options tm-navigation-dropdown__options_absolute']/li[2]"));
                    element.Click();
                    Thread.Sleep(750);
                    parentElement = (WebElement)tempDriver.FindElement(By.XPath("//div[@class='tm-article-snippet__hubs']"));
                    _elements = parentElement.FindElements(By.TagName("a"));
                    Thread.Sleep(750);
                    LogsWriter("\t\tСписок хабов:", logWriter);
                    for (int i = 0; i < _elements.Count(); i++)
                    {
                        element = _elements[i];
                        LogsWriter(element.GetAttribute("text").ToString(), logWriter);
                    }
                    Thread.Sleep(750);
                    parentElement = (WebElement)tempDriver.FindElement(By.XPath("//div[@class='tm-articles-list']"));
                    _elements = parentElement.FindElements(By.XPath("//h2[@class='tm-article-snippet__title tm-article-snippet__title_h2']"));
                    var tempElements = parentElement.FindElements(By.XPath("//div[@class='tm-article-body tm-article-snippet__lead']"));
                    chaildElement = (WebElement)_elements[2].FindElement(By.TagName("a"));
                    Thread.Sleep(750);
                    LogsWriter("\t\tНазвание третьей статьи:", logWriter);
                    LogsWriter(chaildElement.GetAttribute("text").ToString(), logWriter);
                    chaildElement = (WebElement)tempElements[2].FindElement(By.TagName("a"));
                    LogsWriter("\t\tТекст кнопки:", logWriter);
                    LogsWriter(chaildElement.GetAttribute("text").ToString(), logWriter);
                    chaildElement.Click();
                    Thread.Sleep(750);
                    LogsWriter("\t\tНазвание статьи:", logWriter);
                    Thread.Sleep(300);
                    element = tempDriver.FindElement(By.XPath("//h1[@class='tm-article-snippet__title tm-article-snippet__title_h1']/span"));
                    LogsWriter(element.GetAttribute("textContent").ToString(), logWriter);
                    LogsWriter("\t\tКомментарии: ", logWriter);
                    tempDriver.FindElement(By.TagName("body")).SendKeys(Keys.Control + Keys.End);
                    Thread.Sleep(3000);
                    parentElement = (WebElement)tempDriver.FindElement(By.XPath("//h2[@class='tm-comments-wrapper__title']"));
                    if (parentElement.FindElements(By.TagName("span")).Count != 0)
                    {
                        element = parentElement.FindElement(By.XPath("//h2[@class='tm-comments-wrapper__title']/span"));
                        LogsWriter(element.GetAttribute("textContent").ToString().Trim(), logWriter);
                    }
                    else
                    {
                        LogsWriter("Комментариев нет!\n", logWriter);
                    }

                    tempDriver.Quit();
                }
            }
            catch (Exception e)
            {
                logWriter = new StreamWriter(path, true);
                LogsWriter("Ошибка выполнения!!!", logWriter);
                throw;
            }
            logWriter.Close();
            Console.ReadKey();
        }
    }
}
