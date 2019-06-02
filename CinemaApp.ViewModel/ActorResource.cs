using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.ViewModel
{
    public class ActorResource
    {
        public long Id { set; get; }
        [Required]
        public string FirstName { set; get; }
        [Required]
        public string LastName { set; get; }
    }
}
