using System;
using System.IO;
using System.Linq;

namespace AE2Tightening.Lite
{
    public static class FreeHanlder
    {
        public static IFreeSql SqliteHandler { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbfile">Sqlite数据库文件</param>
        public static void InitialSQLiteService(string dbfile)
        {
            if(string.IsNullOrEmpty(dbfile))
            {
                throw new ArgumentNullException(nameof(dbfile));
            }
            //if (!File.Exists(dbfile))
            //{
            //    throw new FileNotFoundException("数据库文件不存在.", dbfile);
            //}
            SqliteHandler = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.Sqlite, $"Data Source = {dbfile}")
                .UseAutoSyncStructure(true)
                .Build();
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="indexs">The indexs.</param>
        /// <returns></returns>
        internal static string GetString(this string value, params int[] indexs) =>
            string.Join("", indexs.Select(x => value[x]));
    }
}
