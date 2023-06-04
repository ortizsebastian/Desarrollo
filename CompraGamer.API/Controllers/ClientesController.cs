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
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClienteId(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // GET: api/Clientes/5
        [HttpGet("{dni}")]
        public async Task<ActionResult<Cliente>> GetClienteDNI(int dni)
        {
            var cliente = await _context.Clientes.FindAsync(dni);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }


        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (ValidarCuit(cliente.Cuit))
                {
                    _context.Clientes.Add(cliente);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
                }
                else
                {
                    return BadRequest("Cuit invalido");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private bool ValidarCuit(long? cuit)
        {
            int digitoVerificador = -1;
            List<int> digitosCuit = GetDigitos(Convert.ToInt32(cuit));
            List<int> digitosValidadores = new() { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            int suma = 0;

            digitoVerificador = digitosCuit.Last();
            digitosCuit.RemoveAt(digitosCuit.Count - 1);
            for (int i = 0; i < digitosValidadores.Count; i++)
            {
                suma += digitosValidadores[i] * digitosCuit[i];
            }

            int resto = suma % 11;
            int resultado = suma / 11 - resto;

            if (resultado == 1) resultado = 9;

            if (resultado == digitoVerificador) return true;

            return false;
        }

        private static List<int> GetDigitos(int numero)
        {
            List<int> digitos = new();

            while (numero > 0)
            {
                var digito = numero % 10;
                numero /= 10;
                digitos.Add(digito);
            }
          
            return digitos;
        }
    }
}
