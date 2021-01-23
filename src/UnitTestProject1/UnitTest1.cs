using System;
using AE2Tightening.Configura;
using AE2Tightening.Frame;
using AE2Tightening.Frame.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AE2Tightening.Frame.ViewModel;
using AE2Tightening.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        //LogHandler log = new LogHandler("UnitTest1");
        public UnitTest1()
        {

        }
        [TestMethod]
        public void TestConfigJson()
        {
            Configs.ReadJsonConfig("Config.json");
            Assert.IsNotNull(Configs.FileConfigs);
        }


        [TestMethod]
        public void TestPnoCodeCheck()
        {
            Configs.ReadJsonConfig("Config.json");
            MainForm f = new MainForm(Configs.FileConfigs.Screenlist[0]);
            bool state = f.CheckEnginePart("L5AFHF500", "22340862");
            Assert.IsTrue(state);
        }
        [TestMethod]
        public void TestRegexCode()
        {
            AppConfig config=  Configs.ReadJsonConfig("Config.json");
            //Regex codeRegex = new Regex("^[A-Z][A-Z0-9]{4}[0-9]{7}$");
            Regex codeRegex = new Regex(config.BarCodePattern);
            Assert.IsTrue(codeRegex.IsMatch("L15B87643712"));
            Assert.IsTrue(codeRegex.IsMatch("L15B84206301"));
            Assert.IsTrue(codeRegex.IsMatch("L15B84206331")); 
            Assert.IsTrue(codeRegex.IsMatch("L15B84206373"));
            Assert.IsTrue(codeRegex.IsMatch("L15B84206424"));
            Assert.IsTrue(codeRegex.IsMatch("P10A52013733"));
            Assert.IsTrue(codeRegex.IsMatch("L15B84251182\r\n"));
        }
    }
}
