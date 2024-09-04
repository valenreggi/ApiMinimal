document.getElementById('clienteForm').addEventListener('submit', function (event) {
    event.preventDefault();

    const clienteId = document.getElementById('clienteId').value; // Obtiene el ID del cliente

    // Realiza la solicitud GET a la API
    fetch(`http://localhost:5178/api/clientes/${clienteId}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Cliente no encontrado');
            }
            return response.json();
        })
        .then(cliente => {
            
            document.getElementById('clienteInfo').innerHTML = `
                <h2>Datos del Cliente</h2>
                <p><strong>Nombre:</strong> ${cliente.nombre}</p>
                <p><strong>Correo:</strong> ${cliente.correo}</p>
                <p><strong>Teléfono:</strong> ${cliente.telefono}</p>
                <p><strong>Dirección:</strong> ${cliente.direccion}</p>
            `;
        })
        .catch(error => {

            document.getElementById('clienteInfo').innerHTML = `<p>${error.message}</p>`;
        });
});