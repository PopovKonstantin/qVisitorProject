
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace qVisitor.Models
{
    [Table ("qvEntrance")]
    public class qvEntrance
    {
        public int Id { get; set; }

        public int CheckPointId { get; set; }
        [ForeignKey("CheckPointId")]
        public qvCheckPoint CheckPoint { get; set; }

        public DateTime ActionDate { get; set; }

        public int EntranceTypeId { get; set; }
        [ForeignKey("EntranceTypeId")]
        public qvEntranceType EntranceType { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public qvOrder Order { get; set; }

        public ICollection<qvEntrancePhoto> EntrancePhotoes { get; set; }
        public ICollection<qvEntranceDoc> EntranceDocs { get; set; }

    }
}
