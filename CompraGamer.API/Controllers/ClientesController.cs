using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompraGamer.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.RegularExpressions;

namespace CompraGamer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientesController : ControllerBase
    {
        private readonly testContext _context;

        public ClientesController(testContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id:int}/GetClienteId")]
        public async Task<ActionResult<Cliente>> GetClienteId(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            return cliente;
        }

        // GET: api/Clientes/5
        [HttpGet("{dni:int}/GetClienteDNI")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClienteDNI(long dni)
        {
            var cliente = await _context.Clientes.Where(c => c.Dni == dni).ToListAsync();
            if (cliente == null) return null;

            return cliente;
        }


        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {

                if (!ValidarCuit(cliente.Cuit)) return BadRequest("Cuit inválido.");
                if (!ValidarEmail(cliente.Email)) return BadRequest("Email inválido.");
                if (!ValidarDNI(cliente.Dni)) return BadRequest("DNI inválido.");
                if (!ValidarTelefono(cliente.Telefono.ToString())) return BadRequest("Telefono inválido.");
                if (!ValidarFechaNacimiento(cliente.FechaNacimiento)) return BadRequest("Fecha de Nacimiento inválida.");
                
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetClienteId", new { id = cliente.Id }, cliente);            
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        public static bool ValidarFechaNacimiento(DateTime fecha)
        {
            if (fecha.Date <= DateTime.Now.Date) return true;
            return false;
        }

        public static bool ValidarTelefono(string telefono)
        {
            string patron = @"^\d{10}$";
            return Regex.IsMatch(telefono, patron);
        }

        public static bool ValidarDNI(long? dni)
        {
            if (dni == null) return false; 

            string patron = @"^\d{7,8}$";
            return Regex.IsMatch(dni.ToString(), patron);
        }

        public static bool ValidarEmail(string email)
        {
            string patron = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, patron);
        }

        private static bool ValidarCuit(long? cuit)
        {
            if (cuit == null) return false;
            int[] cuitArray = GetDigitos(cuit);
            
            if (cuitArray.Length != 11) return false;

            int[] baseArray = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };

            int aux = 0;
            for (int i = 0; i < 10; i++) aux += cuitArray[i] * baseArray[i];

            int result = 11 - (aux % 11);
            if (result >= 0 && result <= 9) return true;

            return false;
        }

        private static int[] GetDigitos(long? cuit)
        {
            string numeroStr = cuit.ToString();
            int[] digitos = new int[numeroStr.Length];
            for (int i = 0; i < numeroStr.Length; i++) digitos[i] = int.Parse(numeroStr[i].ToString());

            return digitos;
        }
    }
}
