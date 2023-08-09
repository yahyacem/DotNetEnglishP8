namespace CalifornianHealthMonolithic.Shared.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;

    [Table("Patient")]
    public partial class Patient : IdentityUser<int>
    {
        [StringLength(50)]
        public string FName { get; set; }
        [StringLength(50)]
        public string LName { get; set; }
        [StringLength(255)]
        public string Address1 { get; set; }
        [StringLength(255)]
        public string Address2 { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(10)]
        public string Postcode { get; set; }
    }
}
