namespace CalifornianHealthMonolithic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Consultant")]
    public partial class Consultant
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string FName { get; set; }

        [StringLength(100)]
        public string LName { get; set; }

        [StringLength(50)]
        public string Speciality { get; set; }
    }
}
