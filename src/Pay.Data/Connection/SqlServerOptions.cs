namespace Pay.Data.Connection
{
    public class SqlServerOptions
    {
        public SqlServerOptions(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
    }
}