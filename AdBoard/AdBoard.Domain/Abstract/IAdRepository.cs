using AdBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBoard.Domain.Abstract
{
    public interface IAdRepository
    {
        IEnumerable<Ad> Ads { get; }
        IEnumerable<Image> Images { get; }
    }
}
