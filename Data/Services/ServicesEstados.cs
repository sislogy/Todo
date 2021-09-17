using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Data.Context;
using Todo.Data.Models;

namespace Todo.Data.Services
{
    public class ServicesEstados
    {
        private readonly TodoContext _context;

        public ServicesEstados(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<Estado>> GetEstadoAsync()
        {
            return await _context.Estado.ToListAsync();
        }

        public async Task<Estado> GetEstadoAsync(long id)
        {
            var estado = await _context.Estado.FindAsync(id);

            if (estado == null)
            {
                return null;
            }

            return estado;
        }

        public async Task<bool> PutEstadoAsync(long id, Estado estado)
        {
            if (id != estado.IdEstado)
            {
                return false;
            }

            _context.Entry(estado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<Estado> PostEstadoAsync(Estado estado)
        {
            _context.Estado.Add(estado);
            await _context.SaveChangesAsync();

            return estado;
        }

        public async Task<bool> DeleteEstadoAsync(long id)
        {
            var estado = await _context.Estado.FindAsync(id);
            if (estado == null)
            {
                return false;
            }

            _context.Estado.Remove(estado);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool EstadoExists(long id)
        {
            return _context.Estado.Any(e => e.IdEstado == id);
        }
    }
}
