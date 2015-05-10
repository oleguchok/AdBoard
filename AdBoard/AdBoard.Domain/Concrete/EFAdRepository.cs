using AdBoard.Domain.Abstract;
using AdBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBoard.Domain.Concrete
{
    public class EFAdRepository : IAdRepository
    {
        EFDbContext context = new EFDbContext();

        public virtual IEnumerable<Entities.Ad> Ads
        {
            get { return context.Ads; }
        }

        public IEnumerable<Entities.Image> Images
        {
            get { return context.Images; }
        }

        public void SaveAd(Ad ad)
        {
            if (ad.Id == 0)
                context.Ads.Add(ad);
            else
            {
                Ad dbEntry = context.Ads.Find(ad.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = ad.Name;
                    dbEntry.Description = ad.Description;
                    dbEntry.Price = ad.Price;
                    dbEntry.Category = ad.Category;
                    dbEntry.Date = ad.Date;
                }
            }
            context.SaveChanges();
        }
        
        public Ad DeleteAd(int adId)
        {
            Ad dbEntry = context.Ads.Find(adId);
            if (dbEntry != null)
            {
                context.Ads.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
