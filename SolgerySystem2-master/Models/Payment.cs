using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SolgerySystem2.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public string narration { get; set; }

        [ForeignKey("Grp")]
        public int GrpId { get; set; }
        public virtual Group Grp { get; set; }

        public int amount { get; set; }

        [ForeignKey("FromUsr")]
        public int? FromUsrId { get; set; }
        public virtual User FromUsr { get; set; }

        [ForeignKey("ToUsr")]
        public int? ToUsrId { get; set; }
        public virtual User ToUsr { get; set; }

        
        public int paid { get; set; }
    }
}