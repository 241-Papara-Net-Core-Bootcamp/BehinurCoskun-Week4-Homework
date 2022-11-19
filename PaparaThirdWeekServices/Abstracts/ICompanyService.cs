using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Services.Abstracts
{
    public interface ICompanyService
    {
        public Task<IReadOnlyList<Company>> GetAll(); 
        public Task<Company>Get(int id);
        public Task<bool> Add(CompanyDto company);
        public Task<bool> Update(CompanyDto company, int id);
        public Task<bool> Delete(int id);
    }
}
