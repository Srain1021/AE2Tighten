using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace AE2Tightening.Services
{
    /*
     * 这个类并没有引用Dapper相关的库,但是它包含一个DbProviderFactory对象就是给Dapper准备的.
     * 
     */
    public class ServiceBase
    {
        private static readonly string ConnectionString;
        private static readonly DbProviderFactory DbProviderFactory;

        /// <summary>
        /// Initializes the <see cref="ServiceBase"/> class.
        /// </summary>
        static ServiceBase()
        {
            string ProviderName = ConfigurationManager.ConnectionStrings[0].ProviderName;
            DbProviderFactory = DbProviderFactories.GetFactory(ProviderName);
            ConnectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
        }

        /// <summary>
        /// Invokes the specified action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        protected T Invoke<T>(Func<IDbConnection,T> action)
        {
            try
            {
                using (var connnection = DbProviderFactory.CreateConnection())
                {
                    connnection.ConnectionString = ConnectionString;

                    return action.Invoke(connnection);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}
