using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineResturnatManagement.Server.Data;
using OnlineResturnatManagement.Server.Migrations;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
using System.Reflection;

namespace OnlineResturnatManagement.Server.Services.Service
{
    public class SettingSrevice : ISettingSrevice
    {
        public ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SettingSrevice(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<SoftwareSettings> GetSoftwareSettingsConfig()
        {
            var softwareSettings = await _context.SoftwareSettings.FirstOrDefaultAsync();
                
            return softwareSettings;
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

        public async Task<SoftwareSettings> UpdateSoftwareConfig(SoftwareSettings requestsSettings)
        {

            var findExistSettings = await _context.SoftwareSettings.FirstOrDefaultAsync();
            //var listOfPrinter = await _context.Printers.ToListAsync();
            if(findExistSettings != null)
            {
                _context.SoftwareSettings.Update(requestsSettings);
                await _context.SaveChangesAsync();
            }
            else
            {
                await _context.SoftwareSettings.AddAsync(requestsSettings);
                await _context.SaveChangesAsync();

            }
            return requestsSettings; 
            

           
        }

        public async Task<IEnumerable<Printer>> GetPrinters()
        {
            return await _context.Printers.ToListAsync();
        }
    }
}
