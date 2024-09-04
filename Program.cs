using Dapper;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddSingleton<ClienteRepository>(sp => new ClienteRepository(connectionString));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();


app.MapGet("/api/clientes/{id}", async (int id, ClienteRepository repo) =>
{
    var cliente = await repo.ObtenerClientePorId(id);
    return cliente != null ? Results.Ok(cliente) : Results.NotFound();
});


app.MapPost("/api/clientes", async (Cliente cliente, ClienteRepository repo) =>
{
    await repo.CrearCliente(cliente);
    return Results.Created($"/api/clientes/{cliente.Id}", cliente);
});


app.MapPut("/api/clientes/{id}", async (int id, Cliente cliente, ClienteRepository repo) =>
{
    var clienteExistente = await repo.ObtenerClientePorId(id);
    if (clienteExistente == null) return Results.NotFound();

    cliente.Id = id;
    await repo.ActualizarCliente(cliente);
    return Results.NoContent();
});


app.MapDelete("/api/clientes/{id}", async (int id, ClienteRepository repo) =>
{
    var clienteExistente = await repo.ObtenerClientePorId(id);
    if (clienteExistente == null) return Results.NotFound();

    await repo.EliminarCliente(id);
    return Results.NoContent();
});

app.Run();
