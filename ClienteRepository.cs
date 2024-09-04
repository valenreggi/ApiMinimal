using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

public class ClienteRepository
{
    private readonly string _connectionString;

    public ClienteRepository(string connectionString)
    {
        
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString), "La cadena de conexión no puede ser nula.");
    }

    public IDbConnection Connection => new SqlConnection(_connectionString);

    
    public async Task<Cliente?> ObtenerClientePorId(int id)
    {
        using (var dbConnection = Connection)
        {
            string query = "SELECT * FROM Clientes WHERE Id = @Id";
            return await dbConnection.QueryFirstOrDefaultAsync<Cliente>(query, new { Id = id });
        }
    }

    public async Task CrearCliente(Cliente cliente)
    {
        using (var dbConnection = Connection)
        {
            string query = "INSERT INTO Clientes (Nombre, Correo, Telefono, Direccion) VALUES (@Nombre, @Correo, @Telefono, @Direccion);" +
                           "SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await dbConnection.QuerySingleAsync<int>(query, cliente);
            cliente.Id = id;
        }
    }

    public async Task ActualizarCliente(Cliente cliente)
    {
        using (var dbConnection = Connection)
        {
            string query = "UPDATE Clientes SET Nombre = @Nombre, Correo = @Correo, Telefono = @Telefono, Direccion = @Direccion WHERE Id = @Id";
            await dbConnection.ExecuteAsync(query, cliente);
        }
    }

    public async Task EliminarCliente(int id)
    {
        using (var dbConnection = Connection)
        {
            string query = "DELETE FROM Clientes WHERE Id = @Id";
            await dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }
}
