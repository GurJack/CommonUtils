using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseData
{
    public class BaseModel
    {
        [Column("_id")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
