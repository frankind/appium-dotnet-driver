﻿using NUnit.Framework;
using System;
using Appium.Integration.Tests.Helpers;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Integration.Tests.iOS
{
	[TestFixture ()]
	public class IosSimpleTest
	{
		private IOSDriver<IOSElement> driver;
		private Random rnd = new Random();

		[TestFixtureSetUp]
		public void BeforeAll(){
			DesiredCapabilities capabilities = Caps.getIos82Caps (Apps.get("iosTestApp")); 
			if (Env.isSauce ()) {
				capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME")); 
				capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
				capabilities.SetCapability("name", "ios - simple");
				capabilities.SetCapability("tags", new string[]{"sample"});
			}
			Uri serverUri = Env.isSauce () ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;
            driver = new IOSDriver<IOSElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);	
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

		private int Populate() {
			IList<string> fields = new List<string> ();
			fields.Add ("IntegerA");
			fields.Add ("IntegerB");
			int sum = 0;
			for (int i = 0; i < fields.Count; i++) {
				IWebElement el = driver.FindElementByName (fields[i]);
				int x = rnd.Next (1, 10);
				el.SendKeys("" + x);
				sum += x;
			}
			return sum;
		}

		[Test ()]
		public void ComputeSumTestCase ()
		{
			// fill form with random data
			int sumIn = Populate ();

			// compute and check the sum
			driver.FindElementByAccessibilityId ("ComputeSumButton").Click ();
			Thread.Sleep (1000);

			IWebElement sumEl = driver.FindElementByIosUIAutomation (".elements().withName(\"Answer\")");
			int sumOut = Convert.ToInt32 (sumEl.Text);
			Assert.AreEqual (sumIn, sumOut);
		}

	}
}

