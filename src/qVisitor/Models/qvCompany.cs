using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace qVisitor.Models
{
    [Table ("qvCompany")]
    public class qvCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CounryId { get; set; }
        [ForeignKey("CounryId")]
        public virtual qvCountry Country { get; set; }

        public virtual ICollection <qvBranch> Branches { get; set; }

        public qvCompany() { }
    }
}
