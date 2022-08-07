using Microsoft.EntityFrameworkCore;
using OnlineResturnatManagement.Server.Data;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Shared.DTO;

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

        public async Task<CompanyProfile> GetCompanyProfile()
        {
            var result = await _context.CompanyProfiles.FirstOrDefaultAsync();
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<CompanyProfile> SaveCompanyProfile(CompanyProfile companyInfo)
        {
            var findCompanyInfo = await _context.CompanyProfiles.FirstOrDefaultAsync();
            if(findCompanyInfo == null)
            {
                await _context.CompanyProfiles.AddAsync(companyInfo);
                 await _context.SaveChangesAsync();
            }
            else
            {
                findCompanyInfo.Name = companyInfo.Name;
                findCompanyInfo.Address = companyInfo.Address;
                findCompanyInfo.OwnerInfo = companyInfo.OwnerInfo;
                findCompanyInfo.LogoUrl = companyInfo.LogoUrl;
                findCompanyInfo.VatRegNo = companyInfo.VatRegNo;
                _context.CompanyProfiles.Update(findCompanyInfo);
                await _context.SaveChangesAsync();
            }
            return companyInfo;
        }
    }
}
