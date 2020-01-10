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
        [DataRow("cb_ledgreen1")]
        [DataRow("cb_ledgreen2")]
        [DataRow("cb_ledgreen3")]
        [DataRow("cb_ledgreen4")]
        [DataRow("cb_ledgreen5")]
        [DataRow("cb_ledgreen6")]
        [DataRow("cb_ledgreen7")]
        [DataRow("cb_ledgreen8")]
        [DataRow("cb_ledred1")]
        [DataRow("cb_ledred2")]
        [DataRow("cb_ledred3")]
        [DataRow("cb_ledred4")]
        [DataRow("cb_ledred5")]
        [DataRow("cb_ledred6")]
        [DataRow("cb_ledred7")]
        [DataRow("cb_ledred8")]
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

        [DataTestMethod]
        [DataRow("cb_Rbutton1")]
        [DataRow("cb_Lbutton1")]
        [DataRow("cb_Rbuttonlong1")]
        [DataRow("cb_Lbuttonlong1")]
        [DataRow("cb_Rbutton2")]
        [DataRow("cb_Lbutton2")]
        [DataRow("cb_Rbuttonlong2")]
        [DataRow("cb_Lbuttonlong2")]
        [DataRow("cb_Rbutton3")]
        [DataRow("cb_Lbutton3")]
        [DataRow("cb_Rbuttonlong3")]
        [DataRow("cb_Lbuttonlong3")]
        [DataRow("cb_Rbutton4")]
        [DataRow("cb_Lbutton4")]
        [DataRow("cb_Rbuttonlong4")]
        [DataRow("cb_Lbuttonlong4")]
        [DataRow("cb_Rbutton5")]
        [DataRow("cb_Lbutton5")]
        [DataRow("cb_Rbuttonlong5")]
        [DataRow("cb_Lbuttonlong5")]
        [DataRow("cb_Rbutton6")]
        [DataRow("cb_Lbutton6")]
        [DataRow("cb_Rbuttonlong6")]
        [DataRow("cb_Lbuttonlong6")]
        [DataRow("cb_Rbutton7")]
        [DataRow("cb_Lbutton7")]
        [DataRow("cb_Rbuttonlong7")]
        [DataRow("cb_Lbuttonlong7")]
        [DataRow("cb_Rbutton8")]
        [DataRow("cb_Lbutton8")]
        [DataRow("cb_Rbuttonlong8")]
        [DataRow("cb_Lbuttonlong8")]
        public void TestButtonContents(string name)
        {
            var cb = MainWindow.FindFirstDescendant(name).AsComboBox();
            Assert.IsNotNull(cb);
            Assert.AreEqual(cb.Items[0].Properties.Name.Value, "NONE");
            Assert.AreEqual(cb.Items[1].Properties.Name.Value, "UID_RANDOM");
            Assert.AreEqual(cb.Items[2].Properties.Name.Value, "UID_LEFT_INCREMENT");
            Assert.AreEqual(cb.Items[3].Properties.Name.Value, "UID_RIGHT_INCREMENT");
            Assert.AreEqual(cb.Items[4].Properties.Name.Value, "UID_LEFT_DECREMENT");
            Assert.AreEqual(cb.Items[5].Properties.Name.Value, "UID_RIGHT_DECREMENT");
            Assert.AreEqual(cb.Items[6].Properties.Name.Value, "CYCLE_SETTINGS");
            Assert.AreEqual(cb.Items[7].Properties.Name.Value, "STORE_MEM");
            Assert.AreEqual(cb.Items[8].Properties.Name.Value, "RECALL_MEM");
            Assert.AreEqual(cb.Items[9].Properties.Name.Value, "TOGGLE_FIELD");
            Assert.AreEqual(cb.Items[10].Properties.Name.Value, "STORE_LOG");
            Assert.AreEqual(cb.Items[11].Properties.Name.Value, "CLONE");
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
