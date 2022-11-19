using AutoMapper;
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Services.Concretes
{
    public class CompanyServices : ICompanyService
    {
        private readonly IDapperRepository<Company> _companyRepository;
        private readonly IMapper _mapper;

        public CompanyServices(IDapperRepository<Company> companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<Company>> GetAll()
        {
            return await _companyRepository.GetAll();
        }
        public async Task<Company> Get(int id)
        {
            return await _companyRepository.GetById(id);
        }
        public async Task<bool> Add(CompanyDto company)
        {
            var newCompany = _mapper.Map<Company>(company);
            newCompany.CreatedBy = "BehinurTheQueen";
            newCompany.CreatedDate = DateTime.Now;
            newCompany.IsDeleted = false;

            return await _companyRepository.Add(newCompany);
        }
        public async Task<bool> Update(CompanyDto company, int id)
        {
            var _company = await _companyRepository.GetById(id);
            if (_company == null)
                return false;

            var updatedCompany = _mapper.Map<Company>(company);
            updatedCompany.Id = _company.Id;
            updatedCompany.IsDeleted = _company.IsDeleted;
            updatedCompany.CreatedDate = _company.CreatedDate;
            updatedCompany.LastUpdateBy = "BehinurTheQueen";
            updatedCompany.LastUpdateAt = DateTime.Now;

            return await _companyRepository.Update(updatedCompany, id);
        }
        public async Task<bool> Delete(int id)
        {
            return await _companyRepository.DeleteById(id); ;
        }
    }
}
