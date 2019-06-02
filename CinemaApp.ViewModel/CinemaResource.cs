using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.ViewModel
{
    public class CinemaResource
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Location { set; get; }
        public int CinemaOwnerId { set; get; }
    }
}
