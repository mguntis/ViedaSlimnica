using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ViedaSlimnicaProject.Models;

namespace ViedaSlimnicaProject.Context
{
    public class PalataContext : DbContext
    {
        public DbSet<Palata> Palatas { get; set; }
    }
}