using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.ViewModel
{
    public class CinemaScreenResource
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public int CinemaOwnerId { set; get; }
        public int CinemaId { set; get; }
        public int NoOfSeats { set; get; }
    }
}
