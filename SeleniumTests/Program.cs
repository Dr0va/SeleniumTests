using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Timers;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Firefox;

namespace SeleniumTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWebElement element;
            string path = "log.txt";
            IWebDriver cDriver = new ChromeDriver();
            IWebDriver fDriver = new FirefoxDriver();
            IWebDriver[] tempDrivers = { cDriver, fDriver };
            StreamWriter logWriter;
            try
            {
                using (logWriter = new StreamWriter(path, false)) { }
                foreach (var tempDriver in tempDrivers)
                {
                    tempDriver.Manage().Window.Maximize();
                    tempDriver.Navigate().GoToUrl(@"https://habr.com");
                    WebElement parentElement = (WebElement)tempDriver.FindElement(By.CssSelector("#app > div.tm-layout__wrapper div.tm-base-layout__header div.tm-main-menu nav"));
                    var _elements = parentElement.FindElements(By.TagName("a"));
                    using (logWriter = new StreamWriter(path, true))
                    {
                        logWriter.WriteLine("[" + DateTime.Now + "]" + tempDriver.ToString() + "\n[" + DateTime.Now + "]" + "\t\tНазвания пунктов главного меню:\t");
                        Console.WriteLine("[" + DateTime.Now + "]" + tempDriver.ToString() + "\n[" + DateTime.Now + "]" + "\t\tНазвания пунктов главного меню:\t");
                        for (int i = 0; i < _elements.Count; i++)
                        {
                            element = _elements[i];
                            element.Click();
                            logWriter.WriteLineAsync("[" + DateTime.Now + "]" + element.GetAttribute("text").ToString().Trim());
                            Console.WriteLine("[" + DateTime.Now + "]" + element.GetAttribute("text").ToString().Trim());
                            Thread.Sleep(750);
                        }
                    }
                    tempDriver.Navigate().GoToUrl(@"https://habr.com");
                    parentElement = (WebElement)tempDriver.FindElement(By.XPath("//div[@class='tm-articles-list']"));
                    _elements = parentElement.FindElements(By.XPath("//h2[@data-test-id='articleTitle']"));
                    WebElement chaildElement;
                    using (logWriter = new StreamWriter(path, true))
                    {
                        logWriter.WriteLine("[" + DateTime.Now + "]" + "\t\tСписок заголовков статей:");
                        Console.WriteLine("[" + DateTime.Now + "]" + "\t\tСписок заголовков статей:");
                        for (int i = 0; i < _elements.Count; i++)
                        {
                            chaildElement = (WebElement)_elements[i].FindElement(By.TagName("a"));
                            logWriter.WriteLineAsync("[" + DateTime.Now + "]" + chaildElement.GetAttribute("text").ToString());
                            Console.WriteLine("[" + DateTime.Now + "]" + chaildElement.GetAttribute("text").ToString());
                            Thread.Sleep(250);
                        }
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
                    using (logWriter = new StreamWriter(path, true))
                    {
                        logWriter.WriteLine("[" + DateTime.Now + "]" + "\t\tСписок хабов:");
                        Console.WriteLine("[" + DateTime.Now + "]" + "\t\tСписок хабов:");
                        for (int i = 0; i < _elements.Count(); i++)
                        {
                            element = _elements[i];
                            logWriter.WriteLineAsync("[" + DateTime.Now + "]" + element.GetAttribute("text").ToString());
                            Console.WriteLine("[" + DateTime.Now + "]" + element.GetAttribute("text").ToString());
                        }
                    }
                    Thread.Sleep(750);
                    parentElement = (WebElement)tempDriver.FindElement(By.XPath("//div[@class='tm-articles-list']"));
                    _elements = parentElement.FindElements(By.XPath("//h2[@class='tm-article-snippet__title tm-article-snippet__title_h2']"));
                    var tempElements = parentElement.FindElements(By.XPath("//div[@class='tm-article-body tm-article-snippet__lead']"));
                    using (logWriter = new StreamWriter(path, true))
                    {
                        chaildElement = (WebElement)_elements[2].FindElement(By.TagName("a")); 
                        Thread.Sleep(750);
                        logWriter.WriteLine("[" + DateTime.Now + "]" + "\t\tНазвание третьей статьи:");
                        Console.WriteLine("[" + DateTime.Now + "]" + "\t\tНазвание третьей статьи:");
                        logWriter.WriteLine("[" + DateTime.Now + "]" + chaildElement.GetAttribute("text").ToString());
                        Console.WriteLine("[" + DateTime.Now + "]" + chaildElement.GetAttribute("text").ToString());

                        chaildElement = (WebElement)tempElements[2].FindElement(By.TagName("a")); 
                        logWriter.WriteLine("[" + DateTime.Now + "]" + "\t\tТекст кнопки:");
                        Console.WriteLine("[" + DateTime.Now + "]" + "\t\tТекст кнопки:");
                        logWriter.WriteLine("[" + DateTime.Now + "]" + chaildElement.GetAttribute("text").ToString());
                        Console.WriteLine("[" + DateTime.Now + "]" + chaildElement.GetAttribute("text").ToString());
                        chaildElement.Click();
                    }
                    Thread.Sleep(750);
                    using (logWriter = new StreamWriter(path, true))
                    {
                        logWriter.WriteLine("[" + DateTime.Now + "]" + "\t\tНазвание статьи:"); 
                        Console.WriteLine("[" + DateTime.Now + "]" + "\t\tНазвание статьи:");
                        Thread.Sleep(300);
                        element = tempDriver.FindElement(By.XPath("//h1[@class='tm-article-snippet__title tm-article-snippet__title_h1']/span"));
                        logWriter.WriteLine("[" + DateTime.Now + "]" + element.GetAttribute("textContent").ToString());
                        Console.WriteLine("[" + DateTime.Now + "]" + element.GetAttribute("textContent").ToString());

                        logWriter.WriteLine("[" + DateTime.Now + "]" + "\t\tКомментарии: "); 
                        Console.WriteLine("[" + DateTime.Now + "]" + "\t\tКомментарии: ");
                        tempDriver.FindElement(By.TagName("body")).SendKeys(Keys.Control + Keys.End);
                        Thread.Sleep(3000);
                        parentElement = (WebElement)tempDriver.FindElement(By.XPath("//h2[@class='tm-comments-wrapper__title']"));
                        if (parentElement.FindElements(By.TagName("span")).Count != 0)
                        {
                            element = parentElement.FindElement(By.XPath("//h2[@class='tm-comments-wrapper__title']/span"));
                            logWriter.WriteLine("[" + DateTime.Now + "]" + element.GetAttribute("textContent").ToString().Trim());
                            Console.WriteLine("[" + DateTime.Now + "]" + element.GetAttribute("textContent").ToString().Trim());
                        }
                        else
                        {
                            logWriter.WriteLine("[" + DateTime.Now + "]" + "Комментариев нет!\n");
                            Console.WriteLine("[" + DateTime.Now + "]" + "Комментариев нет!\n");
                        }
                    }
                    Console.WriteLine("[" + DateTime.Now + "]" + "GOOD!");
                    tempDriver.Quit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[" + DateTime.Now + "]" + "Exception: " + e.Message + " BAD!");
                throw;
            }
            logWriter.Close();
        }
    }
}
