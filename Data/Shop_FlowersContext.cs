using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop_Flowers.Models;

namespace Shop_Flowers.Data
{
    public class Shop_FlowersContext : DbContext
    {
        public Shop_FlowersContext (DbContextOptions<Shop_FlowersContext> options)
            : base(options)
        {
        }

        public DbSet<Shop_Flowers.Models.DanhmucModel> DanhmucModel { get; set; } = default!;
    }
}
