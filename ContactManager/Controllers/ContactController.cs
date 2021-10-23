using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactManager.Dtos;
using ContactManager.Model;
using ContactManager.Service;

namespace ContactManager.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService,IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContactReadDto>> GetAll()
        {
            var contacts = _contactService.GetAllContact();
            return Ok(_mapper.Map<IEnumerable<ContactReadDto>>(contacts));
        }

        [HttpGet("{id}")]
        public ActionResult<ContactReadDto> GetById(int id)
        {
            var contacts = _contactService.GetContactById(id);
            return Ok(_mapper.Map<ContactReadDto>(contacts));
        }


        [HttpPost]
        public ActionResult<ContactReadDto> Create(ContactCreateDto contactCreateDto)
        {
            var contacts = _mapper.Map<Contact>(contactCreateDto);
            _contactService.CreateContact(contacts);

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, ContactUpdateDto contactUpdateDto)
        {
            var contacts = _contactService.GetContactById(id);
            if (contacts == null)
            {
                return NotFound();
            }
            _mapper.Map(contactUpdateDto, contacts);
            _contactService.UpdateContact(contacts);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
           bool result = _contactService.DeleteContact(id);
            return Ok(result);
        }
    }
}
