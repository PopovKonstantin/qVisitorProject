using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace qVisitor.Models
{
    [Table("qvCountry")]
    public class qvCountry
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection <qvCity> Cities { get; set; }
        public virtual ICollection <qvCompany> Companies { get; set; }

        public qvCountry()
        {
        }
    }
}
