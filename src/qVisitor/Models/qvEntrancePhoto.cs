using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvEntrancePhoto")]
    public class qvEntrancePhoto
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }

        public int EntranceId { get; set; }
        [ForeignKey("EntranceId")]
        public qvEntrance Entrance { get; set; }
    }
}

