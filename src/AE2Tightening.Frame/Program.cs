using Serilog;
using System;
using System.Threading;
using System.Windows.Forms;

namespace AE2Tightening.Frame
{
    static class Program
    {
        static ILogger log;
        static System.Threading.Mutex mutex = null;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool ret;
            mutex = new Mutex(true, Application.ProductName, out ret);
            if (ret)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //这个Ioc容器是GodSharp.AtlasCopco.OpenProtocol; 内部在使用,注释掉拧紧机客户端会报错
                //Ioc.Configure(x => x.UseAutofacDependencyInjection(), x =>
                //{
                //    x.AddCommunciationMessages();
                //    x.AddAtlasCopcoOpenProtocol();
                //});
                //Ioc.AddIoc();
                //Ioc.Adapter.GetService<IMessageFactory>().Initialize();
                string output = "{Timestamp:HH:mm:ss.fff}: [{Level:u3}]{Message:lj}{NewLine}{Exception}";
                log = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .MinimumLevel.Information()
                    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(f => f.Level < Serilog.Events.LogEventLevel.Error)
                        .WriteTo.File("Log\\info\\.txt", rollingInterval: RollingInterval.Day, outputTemplate: output))
                    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(f => f.Level >= Serilog.Events.LogEventLevel.Error)
                        .WriteTo.File("Log\\err\\.txt", rollingInterval: RollingInterval.Day, outputTemplate: output))
                    .CreateLogger();
                Log.Logger = log;
                log.Information("***************系统启动*****************");
                Application.Run(new AppController());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("已有一个本程序正在运行中，请不要同时运行多个本程序,点击确认后退出。", "车间管理系统",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                Application.Exit();
            }
           
        }
    }
}
