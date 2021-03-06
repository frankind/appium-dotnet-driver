﻿using NUnit.Framework;
using System;
using Appium.Integration.Tests.Helpers;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Integration.Tests.iOS
{
	[TestFixture ()]
	public class IosWebviewTest
	{
		private AppiumDriver<IWebElement> driver;

		[TestFixtureSetUp]
		public void BeforeAll(){
			DesiredCapabilities capabilities = Caps.getIos82Caps (Apps.get("iosWebviewApp")); 
			if (Env.isSauce ()) {
				capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME")); 
				capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
				capabilities.SetCapability("name", "ios - webview");
				capabilities.SetCapability("tags", new string[]{"sample"});
			}
			Uri serverUri = Env.isSauce () ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;
            driver = new IOSDriver<IWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);	
			driver.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);
		}

		[TestFixtureTearDown]
		public void AfterAll(){
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

		[Test ()]
		public void GetPageTestCase ()
		{
			driver.FindElementByXPath("//UIATextField[@value='Enter URL']")
				.SendKeys("www.google.com");
			driver.FindElementByName ("Go").Click ();
			driver.FindElementByClassName ("UIAWebView").Click (); // dismissing keyboard
			driver.Context = "WEBVIEW";
			Thread.Sleep (3000);
			var el = driver.FindElementByName ("q");
			el.SendKeys ("sauce labs");
			el.SendKeys(Keys.Return);
			Thread.Sleep (1000);
			Assert.IsTrue (driver.Title.Contains("sauce labs"));
		}
	}
}
