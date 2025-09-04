using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseData
{
    /// <summary>
    /// Базовая модель для всех сущностей в системе.
    /// Предоставляет уникальный идентификатор для каждой сущности.
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Уникальный идентификатор сущности.
        /// Автоматически генерируется при создании объекта.
        /// </summary>
        [Column("_id")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
