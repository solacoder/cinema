using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.ViewModel
{
    public class ShowTimeResource
    {
        public long Id { set; get; }
        public string Week { set; get; }
        public long CinemaOwnerId { set; get; }
        public CinemaOwnerResource CinemaOwner { set; get; }
        public int CinemaId { set; get; }
        public CinemaResource Cinema { set; get; }
        public int CinemaScreenId { set; get; }
        public CinemaScreenResource CinemaScreen { set; get; }
        public long MovieCategoryId { set; get; }
        public string MovieCategoryName { set; get; }
        public int MovieId { set; get; }
        public string MovieName { set; get; }
        public DateTime ShowDate { set; get; }
        public DateTime Time { set; get; }
    }
}
