using System.Collections.Generic;
using ViedaSlimnicaProject.Models;

namespace ViedaSlimnicaProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SmartHospitalDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SmartHospitalDatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var firstRoom = new Palata()
            {
                GultasNr = 1,
                Nodala = "abcd nodaļa",
                PalatasID = 1,
                PalatasIetilpiba = 4,
                Stavs = 3
            };
            var secondRoom = new Palata()
            {
                GultasNr = 2,
                Nodala = "abcd nodaļa",
                PalatasID = 2,
                PalatasIetilpiba = 2,
                Stavs = 1
            };
            var thirdRoom = new Palata()
            {
                GultasNr = 3,
                Nodala = "abcd nodaļa",
                PalatasID = 3,
                PalatasIetilpiba = 3,
                Stavs = 2
            };
            var fourthRoom = new Palata()
            {
                GultasNr = 4,
                Nodala = "abcd nodaļa",
                PalatasID = 4,
                PalatasIetilpiba = 1,
                Stavs = 2
            };

            var palatas = new List<Palata>()
            {
                firstRoom,
                secondRoom,
                thirdRoom,
                fourthRoom
            };

            context.Palatas.AddOrUpdate(palatas.ToArray());
            context.SaveChanges();
            var firstPatient = new Pacients()
            {
                Epasts = "es@es.lv",
                IerasanasDatums = DateTime.Now - TimeSpan.FromDays(2),
                Palata = firstRoom,
                //Nodala = "abcd nod",
                PacientaID = 1,
                PersKods = "113456-89112",
                TNumurs = "22345678",
                Uzvards = "ESSE",
                Vards = "esjau"
            };

            var secondPatient = new Pacients()
            {
                Epasts = "es@es.lv",
                IerasanasDatums = DateTime.Now - TimeSpan.FromDays(12),
                Palata = firstRoom,
               // Nodala = "abcd nod",
                PacientaID = 2,
                PersKods = "123456-89112",
                TNumurs = "12345678",
                Uzvards = "ESSE",
                Vards = "esesmues"
            };
            var thirdPatient = new Pacients()
            {
                Epasts = "es@es.lv",
                IerasanasDatums = DateTime.Now - TimeSpan.FromDays(3),
                Palata = thirdRoom,
               // Nodala = "abcd nod",
                PacientaID = 3,
                PersKods = "323456-89112",
                TNumurs = "32345678",
                Uzvards = "ESSE",
                Vards = "esmusese"
            };
            var fourthPatient = new Pacients()
            {
                Epasts = "es@es.lv",
                IerasanasDatums = DateTime.Now - TimeSpan.FromDays(14),
                Palata = fourthRoom,
              //  Nodala = "abcd nod",
                PacientaID = 4,
                PersKods = "423456-89112",
                TNumurs = "42345678",
                Uzvards = "ESSE",
                Vards = "esmusaka"
            };

            var patients = new List<Pacients>()
                {
                    firstPatient,
                    secondPatient,
                    thirdPatient,
                    fourthPatient
                };

            context.Pacienti.AddOrUpdate(patients.ToArray());
            context.SaveChanges();


        }
    }
}
