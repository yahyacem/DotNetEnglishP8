namespace CalifornianHealthMonolithic.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ConsultantNameModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
