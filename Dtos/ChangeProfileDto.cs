using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Dtos
{
    public class ChangeProfileDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IFormFile Image { get; set; }

        //[DataType(DataType.Password)]
        //[Compare("NewPassword", ErrorMessage ="The new password ad confirmation password do not match")]
        //public string ConfirmPassword { get; set; }

    }
}
