using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace qVisitor.Models
{
    [Table ("qvDepartment")]
    public class qvDepartment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual qvBranch Branch { get; set; }

        public virtual ICollection <qvHotEntrance> HotEntrances { get; set; }

        public qvDepartment() { }
}
}
