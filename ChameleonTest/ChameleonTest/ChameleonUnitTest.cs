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

        [DataTestMethod]
        [DataRow("cb_mode1")]
        [DataRow("cb_mode2")]
        [DataRow("cb_mode3")]
        [DataRow("cb_mode4")]
        [DataRow("cb_mode5")]
        [DataRow("cb_mode6")]
        [DataRow("cb_mode7")]
        [DataRow("cb_mode8")]
        public void TestModeContents(string name)
        {
            var cb = MainWindow.FindFirstDescendant(name).AsComboBox();
            Assert.IsNotNull(cb);
            Assert.AreEqual(cb.Items[0].Properties.Name.Value, "NONE");
            Assert.AreEqual(cb.Items[1].Properties.Name.Value, "MF_CLASSIC_1K");
            Assert.AreEqual(cb.Items[2].Properties.Name.Value, "MF_CLASSIC_1K_7B");
            Assert.AreEqual(cb.Items[3].Properties.Name.Value, "MF_CLASSIC_4K");
            Assert.AreEqual(cb.Items[4].Properties.Name.Value, "MF_CLASSIC_4K_7B");
            Assert.AreEqual(cb.Items[5].Properties.Name.Value, "MF_ULTRALIGHT");
            Assert.AreEqual(cb.Items[6].Properties.Name.Value, "ISO14443A_SNIFF");
            Assert.AreEqual(cb.Items[7].Properties.Name.Value, "ISO14443A_READER");
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
