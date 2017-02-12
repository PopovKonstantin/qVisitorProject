using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvCity")]
    public class qvCity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryID { get; set; }
        [ForeignKey("CountryID")]
        public virtual qvCountry Country { get; set; }

        public virtual ICollection <qvObject> Objects { get; set; }

        public qvCity() { }
}
}
