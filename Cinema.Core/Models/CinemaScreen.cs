namespace CinemaApp.Core.Models
{
    public class CinemaScreen
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public long CinemaOwnerId { set; get; }
        public CinemaOwner CinemaOwner { set; get; }
        public long CinemaId { set; get; }
        public Cinema Cinema { set; get; }
        public int NoOfSeats { set; get; }
    }
}