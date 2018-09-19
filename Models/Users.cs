using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MvcApplication40.Models
{
    public class Users
    {
        public int    ID       { get; set; }
        [Required(ErrorMessage="Name is Required")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage="Password is Required")]
        public string Password { get; set; }
        public string RoleName { get; set; }
        public HttpPostedFileBase File { get; set; }
        public byte[] ImageData { get; set; }
    }
}