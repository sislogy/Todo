using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data.Context;
using Todo.Data.Models;

namespace Todo.Data.Services
{

    public class ServicesCards
    {
        private readonly TodoContext _context;
        public ServicesCards(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<Card>> GetCardAsync()
        {
            return await _context.Card.Where(y => y.IdEstado != (int)Data.Negocio.Estados.Card.Eliminado).OrderByDescending(x => x.IdCard).ToListAsync();
        }
        public async Task<Card> GetCardAsync(long id)
        {
            var card = await _context.Card.FindAsync(id);

            if (card == null)
            {
                return null;
            }

            return card;
        }
        public async Task<bool> PutCardAsync(long id, Card card)
        {
            if (id != card.IdCard)
            {
                return false;
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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
        public async Task<Card> PostCardAsync(Card card)
        {
            _context.Card.Add(card);
            await _context.SaveChangesAsync();

            return card;
        }
        public async Task<bool> DeleteCardAsync(long id)
        {
            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                return false;
            }

            _context.Card.Remove(card);
            await _context.SaveChangesAsync();

            return true;
        }
        private bool CardExists(long id)
        {
            return _context.Card.Any(e => e.IdCard == id);
        }
    }
}
