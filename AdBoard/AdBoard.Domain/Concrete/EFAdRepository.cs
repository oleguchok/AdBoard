using AdBoard.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBoard.Domain.Concrete
{
    class EFAdRepository : IAdRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Entities.Ad> Ads
        {
            get { return context.Ads; }
        }
    }
}
