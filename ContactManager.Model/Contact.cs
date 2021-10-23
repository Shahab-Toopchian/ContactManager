using System;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Model
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int PhoneNumber { get; set; }
    }
}
