
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvUserPassport")]
    public class qvUserPassport
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
