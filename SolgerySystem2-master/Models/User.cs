using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SolgerySystem2.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string uname { get; set; }
        public string password { get; set; }
        public virtual ICollection<Group> groups { get; set; }

        [InverseProperty("FromUsr")]
        public virtual ICollection<Payment> fromPayments { get; set; }
        [InverseProperty("ToUsr")]
        public virtual ICollection<Payment> toPayments { get; set; }




    }
}