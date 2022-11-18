using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.Abstracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Services.Concretes
{
    public class CompanyServices : ICompanyService
    {
        private readonly IDapperRepository<Company> _companyRepository;

        public CompanyServices(IDapperRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IReadOnlyList<Company>> GetAll()
        {
            return await _companyRepository.GetAll();
        }
        public async Task<Company> Get(int id)
        {
            return await _companyRepository.GetById(id);
        }

        public async Task<int> Add(Company company)
        {
            return await _companyRepository.Add(company);
        }

        public async Task<int> Update(Company company, int id)
        {
            return await _companyRepository.Update(company, id);
        }

        public async Task<int> Delete(int id)
        {
            return await _companyRepository.DeleteById(id);
        }
    }
}
