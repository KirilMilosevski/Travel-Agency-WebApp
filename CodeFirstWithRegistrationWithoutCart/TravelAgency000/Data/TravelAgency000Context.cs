using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgency000.Models;

namespace TravelAgency000.Data
{
    public class TravelAgency000Context : DbContext
    {
        public TravelAgency000Context (DbContextOptions<TravelAgency000Context> options)
            : base(options)
        {
        }

        public DbSet<TravelAgency000.Models.Destination> Destination { get; set; }



    }
}
