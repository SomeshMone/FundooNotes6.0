using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class CustomerEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_email { get; set; }

        public string customer_phonenumber { get; set;}

        public string customer_address { get; set;}
    }
}
