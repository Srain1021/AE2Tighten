using System;
using Dapper;

namespace AE2Tightening.Services
{
    public class TestService : ServiceBase
    {
        public DateTime? GetDateTime()
        {
            return this.Invoke<DateTime?>((c) =>
            {
                try
                {
                    return c.ExecuteScalar<DateTime?>("SELECT GETDATE();", commandTimeout: 5000);
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }
    }
}
