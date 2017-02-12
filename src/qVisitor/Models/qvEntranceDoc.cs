using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvEntranceDoc")]
    public class qvEntranceDoc
    {
        public int Id { get; set; }
        public byte[] Scan { get; set; }

        public int EntranceId { get; set; }
        [ForeignKey("EntranceId")]
        public qvEntrance Entrance { get; set; }
    }
}
