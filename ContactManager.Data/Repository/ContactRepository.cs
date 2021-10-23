using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactManager.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ContactManager.Data.Repository
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAllContact();
        Contact GetContactById(int id);

        void CreateContact(Contact contact);
        void UpdateContact(Contact contact);
        bool DeleteContact(int id);
    }
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public ContactRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;


        }

        public void CreateContact(Contact contact)
        {
            _db.Contacts.Add(contact);
            _db.SaveChanges();
        }

        public bool DeleteContact(int id)
        {
            string connString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteContact",conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters.Add("@Result", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.Parameters["@Id"].Value = id;
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    bool result = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
                    conn.Close();

                    return result;
                }
            }
 
        }

        public IEnumerable<Contact> GetAllContact()
        {
            return _db.Contacts.ToList();
        }

        public Contact GetContactById(int id)
        {
            return _db.Contacts.FirstOrDefault(x=> x.Id == id);
        }

        public void UpdateContact(Contact contact)
        {
            var objFromDb = _db.Contacts.FirstOrDefault(x => x.Id == contact.Id);
            objFromDb.FirstName = contact.FirstName;
            objFromDb.LastName = contact.LastName;
            objFromDb.PhoneNumber = contact.PhoneNumber;
            _db.SaveChanges();

        }
    }
}
