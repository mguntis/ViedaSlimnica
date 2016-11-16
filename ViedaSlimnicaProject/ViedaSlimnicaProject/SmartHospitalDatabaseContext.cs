using System.Data.Entity;
using ViedaSlimnicaProject.Models;

namespace ViedaSlimnicaProject
{
    public class SmartHospitalDatabaseContext : DbContext 
    {
        public DbSet<Pacients> Pacienti { get; set; }
        public DbSet<Palata> Palatas { get; set; }
        public DbSet<Profils> Accounts { get; set; }
        public SmartHospitalDatabaseContext() : base("SmartHospitalDatabaseContext")
        {
            
        }
    }

}