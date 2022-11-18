using AutoMapper;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Services.AutoMappingProfiles
{
    public class AutoMappingProfile:Profile //profile autommapprden geliyor configuration clasın propartilerde eşleme işlemi yapıyor.
    { //profile metotları constructor içinde çalışır.
        public AutoMappingProfile()
        {
            CreateMap<Company,CompanyDto>().ReverseMap(); //reversemap compay ile companyDto arasında cevirme yapıyor.
        }

    }
}
