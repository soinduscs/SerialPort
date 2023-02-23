using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Soindus.Interfaces.Modelos.SOcomData
{
    public partial class Modelo_SOcomData : DbContext
    {
        public Modelo_SOcomData()
            : base("name=Modelo_SOcomData")
        {
        }

        public virtual DbSet<RegCom> RegCom { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegCom>()
                .Property(e => e.Com_ID)
                .IsRequired();
        }
    }
}
