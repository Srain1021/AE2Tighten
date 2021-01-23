using System;
using AE2Devices;
using AE2Tightening.Frame.Data;
using AE2Tightening.Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestLocalDB
    {
        [TestMethod]
        public void TestTighteningInsert()
        {
            LocalSQLService LocalSQLHandler = new LocalSQLService();
            string code = "L15B84104476";
            var data = new TightenData();
            data.EngineCode = code;
            data.Torque = 195.5;
            data.Angle = 360;
            data.BoltNo = 1;
            data.Result = 1;
            //data.TightenTime = DateTime.Now;
            var model = TightenMapper.LocalMap(data);
            model.StationName = "td";
            int result = LocalSQLHandler.TighteningService.Insert(model);
            Assert.IsTrue(result > 0);
        }
        [TestMethod]
        public void TestGetEngineType()
        {
            LocalSQLService LocalSQLHandler = new LocalSQLService();
            LEngineTypeModel model = LocalSQLHandler.LEngineType.Get("L15B86012031");
            Assert.IsNotNull(model);
        }
    }
}
