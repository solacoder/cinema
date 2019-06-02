using CinemaApp.Core.Models;
using CinemaApp.Core.Repositories;
using CinemaApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Persistence.Repositories
{
    public class ActorRepository : Repository<Actor>, IActorRepository
    {
        public ActorRepository(CinemaContext context)
            : base(context)
        { }

        public CinemaContext CinemaContext
        {
            get { return Context as CinemaContext; }
        }

        public bool IsExists(Actor obj)
        {
            Actor actor = null;
            try
            {
                actor = CinemaContext.Actors.First<Actor>(m => m.FirstName == obj.FirstName && m.LastName == obj.LastName );
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return actor != null ? true : false;
        }
    }
}
