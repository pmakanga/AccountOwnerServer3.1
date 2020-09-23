using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOwnerServerThreeDotOne
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDto>();

            CreateMap<Account, AccountDto>();

            CreateMap<AccountForCreationDto, Account>();

            CreateMap<AccountForUpdateDto, Account>();

            CreateMap<OwnerForCreationDto, Owner>();

            CreateMap<OwnerForUpdateDto, Owner>();
        }
    }
}
