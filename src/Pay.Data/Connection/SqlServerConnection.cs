namespace Pay.Data.Connection
{
    public class SqlServerConnection
    {
        private readonly SqlServerOptions _options;

        public SqlServerConnection(SqlServerOptions options)
        {
            _options = options;
        }
    }
}