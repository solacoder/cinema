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
    public class ProducerRepository : Repository<Producer>, IProducerRepository
    {
        public ProducerRepository(CinemaContext context)
            : base(context)
        { }

        public CinemaContext CinemaContext
        {
            get { return Context as CinemaContext; }
        }

        public bool IsExists(Producer obj)
        {
            Producer producer = null;
            try
            {
                producer = CinemaContext.Producers.First<Producer>(m => m.FirstName == obj.FirstName);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return producer != null ? true : false;
        }
    }
}
