using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

namespace ChameleonTest
{
    [TestClass]
    public class ChameleonUnitTest
    {
        static Application ChameleonApplication = null;
        static FlaUI.Core.AutomationElements.Window MainWindow = null;

        [ClassInitialize]
        public static void ChameleonUnitTestInit(TestContext context)
        {
            ChameleonApplication = Application.Launch("..\\..\\..\\..\\ChameleonMiniGUI\\bin\\Debug\\ChameleonMiniGUI.exe");

            using (var automation = new UIA3Automation())
            {
                MainWindow = ChameleonApplication.GetMainWindow(automation);
                Assert.IsNotNull(MainWindow);

                while (MainWindow.Title == "SplashScreen")
                {
                    Thread.Sleep(1000);
                    MainWindow = ChameleonApplication.GetMainWindow(automation);
                }
            }
        }

        [TestMethod]
        public void TestWindowTitle()
        {
            Assert.IsNotNull(MainWindow);
            StringAssert.StartsWith(MainWindow.Title, "Chameleon Mini GUI");
            StringAssert.Contains(MainWindow.Title, "Iceman Edition");
        }

        [TestMethod]
        public void TestTabs()
        {
            var tab = MainWindow.FindFirstDescendant("tabControl1").AsTab();
            Assert.IsNotNull(tab);
            Assert.AreEqual(tab.SelectedTabItemIndex, 0);
            Assert.AreEqual(tab.TabItems[0].Properties.Name.Value, "Operation");
            Assert.AreEqual(tab.TabItems[1].Properties.Name.Value, "Dump Management");
            Assert.AreEqual(tab.TabItems[2].Properties.Name.Value, "Utils");
            Assert.AreEqual(tab.TabItems[3].Properties.Name.Value, "Serial");
            Assert.AreEqual(tab.TabItems[4].Properties.Name.Value, "Settings");
        }

        [TestMethod]
        public void TestModeContents()
        {
        }

        [ClassCleanup]
        public static void ChameleonUnitTestClean()
        {
            Console.WriteLine("ChameleonUnitTest TestCleanup");
            if (ChameleonApplication != null) {
                ChameleonApplication.Close();
                ChameleonApplication = null;
            }
        }
    }
}
