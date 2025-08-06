using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseData
{
    public class BaseModel
    {
        [Column("_id")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
