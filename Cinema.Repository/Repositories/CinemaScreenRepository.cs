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
    public class CinemaScreenRepository : Repository<CinemaScreen>, ICinemaScreenRepository
    {
        public CinemaScreenRepository(CinemaContext context)
            : base(context)
        { }

        public CinemaContext CinemaContext
        {
            get { return Context as CinemaContext; }
        }

        public bool IsExists(CinemaScreen obj)
        {
            CinemaScreen screen = null;
            try
            {
                screen = CinemaContext.CinemaScreens.First<CinemaScreen>(m => m.Name == obj.Name && m.CinemaId == obj.CinemaId);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return screen != null ? true : false;
        }

        public override IEnumerable<CinemaScreen> GetAll()
        {
            return CinemaContext.CinemaScreens.Include(m => m.Cinema).Include(m => m.CinemaOwner);
        }
    }
}
