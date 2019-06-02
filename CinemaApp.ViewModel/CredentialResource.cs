using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaApp.ViewModel
{
    public class CredentialResource
    {
        [Required]
        public string UserName { set; get; }

        [StringLength(50, MinimumLength = 7)]
        public string Password { set; get; }
    }
}
