namespace Soindus.Interfaces.Modelos.SOcomData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegCom")]
    public partial class RegCom
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Com_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string User_ID { get; set; }

        public DateTime? LastUpdate { get; set; }

        [StringLength(50)]
        public string StrValue { get; set; }
    }
}
