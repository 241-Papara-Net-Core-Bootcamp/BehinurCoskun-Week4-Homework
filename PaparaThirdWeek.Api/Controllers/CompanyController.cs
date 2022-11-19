using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            this._companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet("Companies")]
        public async Task<IActionResult> Get()
        {
            var result = await _companyService.GetAll();
            return Ok(result);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _companyService.Get(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, CompanyDto company)
        {
            if (!await _companyService.Update(company, id))
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(CompanyDto company)
        {
            if (!await _companyService.Add(company))
                return BadRequest();

            return Ok();


            var newCompany = _mapper.Map<Company>(company);
            newCompany.CreatedBy = "BehinurTheQueen";
            newCompany.CreatedDate = DateTime.Now;
            newCompany.IsDeleted = false;

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _companyService.Delete(id))
                return BadRequest();
                
            return Ok();
        }
        
    }
}
