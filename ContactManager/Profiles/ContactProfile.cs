using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactManager.Dtos;
using ContactManager.Model;

namespace ContactManager.Profiles
{
    public class ContactProfile :Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactReadDto>();
            CreateMap<ContactCreateDto, Contact>();
            CreateMap<ContactUpdateDto, Contact>();
        }

    }
}
