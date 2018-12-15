using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SolgerySystem2.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public int GId { get; set; }
        public string GrpName { get; set; }
        [ForeignKey("Usr")]
        public int UsrId { get; set; }
        public virtual User Usr { get; set; }

        public virtual ICollection<Payment> payments { get; set; }

    }
}