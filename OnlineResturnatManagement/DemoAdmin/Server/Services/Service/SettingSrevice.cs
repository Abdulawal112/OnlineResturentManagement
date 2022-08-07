using Microsoft.EntityFrameworkCore;
using OnlineResturnatManagement.Server.Data;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;

namespace OnlineResturnatManagement.Server.Services.Service
{
    public class SettingSrevice : ISettingSrevice
    {
        public ApplicationDbContext _context;
        public SettingSrevice(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ActiveModule>> GetActiveModules()
        {
            return await _context.ActiveModules.Where(x => x.Status == true).ToListAsync();
        }
    }
}
