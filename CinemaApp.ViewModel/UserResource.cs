using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaApp.ViewModel
{
    public class UserResource
    {
        [Required]
        public string FirstName { set; get; }

        [Required]
        public string UserName { set; get; }

        [Required]
        public string LastName { set; get; }

        [Required]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")]
        public string Email { set; get; }

        [Required]
        [StringLength(60, MinimumLength = 7)]
        public string Password { set; get; }
    }
}
