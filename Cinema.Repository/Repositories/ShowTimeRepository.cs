using CinemaApp.Core.Models;
using CinemaApp.Core.Repositories;
using CinemaApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Persistence.Repositories
{
    public class ShowTimeRepository : Repository<ShowTime>, IShowTimeRepository
    {
        public ShowTimeRepository(CinemaContext context)
            : base(context)
        { }

        public CinemaContext CinemaContext
        {
            get { return Context as CinemaContext; }
        }

        public bool IsExists(ShowTime obj)
        {
            ShowTime show = null;
            try
            {
                show = CinemaContext.ShowTimes.First<ShowTime>(m => m.Week == obj.Week && m.CinemaScreenId == obj.CinemaScreenId && m.Time == obj.Time);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return show != null ? true : false;
        }

        public override IEnumerable<ShowTime> GetAll()
        {
            return CinemaContext.ShowTimes
                                .Include(m => m.Cinema)
                                .ThenInclude(m => m.CinemaOwner)
                                .Include(m => m.CinemaScreen)
                                .Include(m => m.Movie)
                                .ThenInclude(m => m.MovieCategory);
        }
    }
}
