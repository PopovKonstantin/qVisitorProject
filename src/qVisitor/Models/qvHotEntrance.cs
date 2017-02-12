using qVisitor.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvHotEntrance")]
    public class qvHotEntrance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string DocumentNumber { get; set; }
        public string Organization { get; set; }
        public string Attendant { get; set; }
        

        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual qvDepartment Department { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection <qvHotEntranceDoc> HotEntranceDocs { get; set; }
        public virtual ICollection <qvHotEntrancePhoto> HotEntrancePhotoes { get; set; }

        public string Comment { get; set; }

    }
}
