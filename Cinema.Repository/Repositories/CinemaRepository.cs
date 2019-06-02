using CinemaApp.Core.Repositories;
using CinemaApp.Core.Models;
using CinemaApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Persistence.Repositories
{
    public class CinemaRepository : Repository<CinemaApp.Core.Models.Cinema>, ICinemaRepository
    {

        public CinemaRepository(CinemaContext context)
            : base(context)
        { }

        public CinemaContext CinemaContext
        {
            get { return Context as CinemaContext; }
        }

        public bool IsExists(CinemaApp.Core.Models.Cinema obj)
        {
            CinemaApp.Core.Models.Cinema cinema = null;
            try
            {
                cinema = CinemaContext.Cinemas.First<CinemaApp.Core.Models.Cinema> (m => m.Name == obj.Name && m.Location == obj.Location);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return cinema != null ? true : false;
        }

        public override IEnumerable<Core.Models.Cinema> GetAll()
        {
            return CinemaContext.Cinemas.Include(m => m.CinemaOwner);
        }
    }
}
