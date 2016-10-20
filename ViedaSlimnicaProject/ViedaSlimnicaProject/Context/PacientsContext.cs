
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ViedaSlimnicaProject.Models;

namespace ViedaSlimnicaProject.Context
{
    public class PacientsContext : DbContext
    {
        public DbSet<Pacients> Pacienti { get; set; }
    }

}