﻿using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;
using System.Drawing;
using System.Threading;

namespace Appium.Integration.Tests.Android
{
    [TestFixture()]
    class AndroidGestureTest
    {
        private AndroidDriver<AndroidElement> driver;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            DesiredCapabilities capabilities = Env.isSauce() ?
                Caps.getAndroid18Caps(Apps.get("androidApiDemos")) :
                Caps.getAndroid19Caps(Apps.get("androidApiDemos"));
            if (Env.isSauce())
            {
                capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.SetCapability("name", "android - complex");
                capabilities.SetCapability("tags", new string[] { "sample" });
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);
            driver.CloseApp();
        }

        [SetUp]
        public void SetUp()
        {
            if (driver != null)
            {
                driver.LaunchApp();
            }
        }

        [TearDown]
        public void TearDowwn()
        {
            if (driver != null)
            {
                driver.CloseApp();
            }
        }

        [TestFixtureTearDown]
        public void AfterAll()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test()]
        public void scrollAndSwipeTest()
        {
            driver.FindElementByName("Graphics").Click();
            driver.ScrollTo("FingerPaint", "android:id/list");
            driver.FindElementByName("FingerPaint").Click();
            AndroidElement element = driver.FindElementById("android:id/content");
            Point point = element.Coordinates.LocationInDom;
            Size size = element.Size;
            driver.Swipe
            (
                point.X + 5, 
                point.Y + 5, 
                point.X + size.Width - 5,
                point.Y + size.Height - 5, 
                200
            ); 
            
            driver.Swipe
            (
                point.X + size.Width - 5,
                point.Y + 5,
                point.X + 5,
                point.Y + size.Height - 5,
                2000
            );
        }

        [Test()]
        public void pinchAndZoomTest()
        {
            driver.FindElementByName("Graphics").Click();
            driver.ScrollTo("OpenGL ES", "android:id/list").Click();
            //driver.FindElementByName("OpenGL ES").Click();
            driver.ScrollTo("Touch Rotate", "android:id/list").Click();
            //driver.FindElementByName("TouchRotate").Click();

            AndroidElement element = driver.FindElementById("android:id/content");
            driver.Pinch(element);
            driver.Zoom(element);

            Thread.Sleep(2000);
        }
    }
}
