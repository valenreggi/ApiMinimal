public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }

    public Cliente(int id, string nombre, string correo, string telefono, string direccion)
    {
        Id = id;
        Nombre = nombre;
        Correo = correo;
        Telefono = telefono;
        Direccion = direccion;
    }
}
