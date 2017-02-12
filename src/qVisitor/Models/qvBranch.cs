using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvBranch")]
    public class qvBranch
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public qvCity City { get; set; }

        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual qvCompany Company { get; set; }

        public string HighBranchId { get; set; }
        public virtual ICollection <qvDepartment> Departments { get; set; }
    }
}
