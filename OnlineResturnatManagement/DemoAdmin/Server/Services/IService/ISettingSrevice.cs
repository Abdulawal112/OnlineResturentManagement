﻿using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Server.Services.IService
{
    public interface ISettingSrevice
    {
        public Task<List<ActiveModule>> GetActiveModules();
        Task<CompanyProfile> SaveCompanyProfile(CompanyProfile companyInfo);
        Task<CompanyProfile> GetCompanyProfile();
        Task<SoftwareSettings> GetSoftwareSettingsConfig();
        Task<SoftwareSettings> UpdateSoftwareConfig(SoftwareSettings requestsSettings);
        Task<IEnumerable<Printer>> GetPrinters();
    }
}
