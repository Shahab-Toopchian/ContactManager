using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactManager.Data.Repository;
using ContactManager.Model;

namespace ContactManager.Service
{
    public interface IContactService
    {
        IEnumerable<Contact> GetAllContact();
        Contact GetContactById(int id);

        void CreateContact(Contact contact);
        void UpdateContact(Contact contact);
        bool DeleteContact(int id);
    }
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public void CreateContact(Contact contact)
        {
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }
            else
            {
                _contactRepository.CreateContact(contact);    
            }
        }

        public bool DeleteContact(int id)
        {
            bool result;

            try
            {
                result = _contactRepository.DeleteContact(id);
            }
            catch (Exception ex)
            {
                result = false;
                throw new ArgumentNullException(nameof(ex));
            }

            return result;
        }

        public IEnumerable<Contact> GetAllContact()
        {
            return _contactRepository.GetAllContact();
        }

        public Contact GetContactById(int id)
        {
            return _contactRepository.GetContactById(id);
        }

        public void UpdateContact(Contact contact)
        {
            _contactRepository.UpdateContact(contact);
        }
    }
}
