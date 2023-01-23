using Microsoft.EntityFrameworkCore;
using restApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.Context
{
    public class ConectionSql : DbContext
    {
        public ConectionSql(DbContextOptions<ConectionSql> options) : base(options){

        }
        public DbSet<History> History { get; set; }
    }
}
