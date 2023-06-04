using System;
using System.Collections.Generic;

#nullable disable

namespace CompraGamer.API.Models
{
    public partial class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public long? Telefono { get; set; }
        public string Email { get; set; }
        public long Dni { get; set; }
        public long? Cuit { get; set; }

    }

}
