using CinemaApp.Core.Models;
using CinemaApp.Core.Repositories;
using CinemaApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Persistence.Repositories
{
    public class CinemaOwnerRepository : Repository<CinemaOwner>, ICinemaOwnerRepository
    {
        public CinemaOwnerRepository(CinemaContext context)
            : base(context)
        { }

        public CinemaContext CinemaContext
        {
            get { return Context as CinemaContext; }
        }

        public bool IsExists(CinemaOwner obj)
        {
            CinemaOwner cinemaOwner = null;
            try
            {
                cinemaOwner = CinemaContext.CinemaOwners.First<CinemaOwner>(m => m.Name == obj.Name);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return cinemaOwner != null ? true : false;
        }

        public override IEnumerable<CinemaOwner> GetAll()
        {
            return CinemaContext.CinemaOwners.Include(m => m.Cinemas);
        }
    }
}
