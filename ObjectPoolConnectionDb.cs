using System.Data.SqlClient;

namespace ObjectPoolConnectionDb
{
    public class SqlConnectionPool
    {
        private Queue<SqlConnection> _pool;
        private string _connectionString;
        private int _maxConnections;

        public SqlConnectionPool(string connectionString, int maxConnection)
        {
            _connectionString = connectionString;
            _maxConnections = maxConnection;
            _pool = new Queue<SqlConnection>();
            InitializePool();
        }
        private void InitializePool()
        {
            for (int i = 0; i < _maxConnections; i++)
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open(); //apro la connessione e la metto in coda
                _pool.Enqueue(connection);
            }
        }

        public SqlConnection GetConnectionFromPool()
        {
            if (_pool.Count > 0)
            {
                return _pool.Dequeue(); //prendo la prima connessione in coda
            }
            else
            {
                Console.Write("\nTutte le connessioni sono in uso ->");
                return null;
            }
        }

        public void ReturnConnectionToPool(SqlConnection connection)
        {
            if (_pool.Count < _maxConnections) //se non sono ancora arrivata al max di connessioni permesse
            {
                _pool.Enqueue(connection); //inserisco nella coda
            }
            else //massimo di connessioni nella coda
            {
                connection.Close();
                Console.WriteLine("Il pool di connessioni è pieno. La connessione verrà scartata.");
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=NOME-Server;Database=NomeDatabase;Trusted_Connection=true;TrustServerCertificate=True;";
            int maxConnections = 2;
            SqlConnectionPool connectionPool = new SqlConnectionPool(connectionString, maxConnections);

            SqlConnection connection1 = connectionPool.GetConnectionFromPool();
            StampaConnessione(connection1);

            SqlConnection connection2 = connectionPool.GetConnectionFromPool();
            StampaConnessione(connection2);

            SqlConnection connection3 = connectionPool.GetConnectionFromPool(); //null
            StampaConnessione(connection3);

            connectionPool.ReturnConnectionToPool(connection1);

            connection3 = connectionPool.GetConnectionFromPool();
            StampaConnessione(connection3);

        }


        public static void StampaConnessione(SqlConnection connection)
        {
            if(connection == null)
            {
                Console.WriteLine("Nessuna connessione presente nel pool");
            }
            else
            {
                Console.WriteLine($"\nConnessione rilasciata: {connection.GetHashCode()}");
            }
        }
    }
}
