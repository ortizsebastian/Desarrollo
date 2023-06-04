using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace CompraGamer.API.Models
{
    public partial class UsuarioAcceso
    {
        public short Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
