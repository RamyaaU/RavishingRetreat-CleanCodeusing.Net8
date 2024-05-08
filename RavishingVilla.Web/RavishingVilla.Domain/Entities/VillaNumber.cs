using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavishingVilla.Domain.Entities
{
    public class VillaNumber
    {
        //database will not generate it as an identity column
        //this is making this field as primary key without having the default
        //identity column
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Villa Number")]
        public int Villa_Number { get; set; }

        [ForeignKey("Villa")]
        public int Villa_Id { get; set; }

        [ValidateNever]
        public Villa Villa { get; set; }

        public string? SpecialDetails { get; set; }

    }
}
