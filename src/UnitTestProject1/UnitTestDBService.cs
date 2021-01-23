using System;
using AE2Tightening.Frame.Data;
using AE2Tightening.Models;
using AE2Tightening.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestDBService
    {
        [TestMethod]
        public void TestSaveMaterialMetas()
        {
            RFIDDBHelper.MSSQLHandler.SaveMaterialMetas(new MaterialMetasModel
            {
                EngineCode = "001",
                StationCode= "ST5ShockL",
                MaterialCode="002",
                Result = 1,
                ResultId = 0,
                CreateTime = DateTime.Now,
                ModifiedTime = DateTime.Now
            });
        }
    }
}
