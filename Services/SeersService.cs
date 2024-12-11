using Microsoft.EntityFrameworkCore;
using Система_за_управление_на_гадатели_MVC.Data;
using Система_за_управление_на_гадатели_MVC.Interfaces;
using Система_за_управление_на_гадатели_MVC.Models;

namespace Система_за_управление_на_гадатели_MVC.Services
{
    public class SeersService : ISeersService
    {
        private ApplicationDbContext context;

        public SeersService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Seer>> GetAllSeersAsync()
        {
            var seers = await context.Seers.Include(u => u.ApplicationUser).ToListAsync();

            return seers;
        }
    }
}
