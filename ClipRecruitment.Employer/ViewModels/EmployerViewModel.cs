using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Employer.ViewModels
{
    public class EmployerViewModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthID { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyContact { get; set; }
        public string MobileNo { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Compare("Email")]
        public string ConfirmEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Location { get; set; }

        public List<string> IndustryList { get; set; }
        public string RegistrationNo { get; set; }
        public List<string> AddressList { get; set; }
    }
}
