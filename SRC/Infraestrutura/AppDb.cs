using MySqlConnector;

namespace Infrastructure
{
    public class AppDb : IDisposable
    {
        internal MySqlConnection Connection;

        public AppDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);            
        }
        public void Dispose() => Connection.Dispose();
        
    }
}