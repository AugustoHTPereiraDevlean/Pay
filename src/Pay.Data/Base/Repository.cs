using System.Data;
using System.Data.SqlClient;
using Pay.Data.Connection;

namespace Pay.Data.Base
{
    public class Repository : IDisposable
    {
        private readonly SqlServerOptions _options;

        public Repository(SqlServerOptions options)
        {
            _options = options;
        }

        public void Dispose()
        {
            CloseConnection();
            GC.SuppressFinalize(this);
        }

        public IDbConnection Connection => new SqlConnection(_options.ConnectionString);

        public void CloseConnection()
        {
            if (Connection.State.Equals(ConnectionState.Open))
            {
                Connection.Close();
            }

            Connection.Dispose();
        }
    }
}