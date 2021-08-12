using System.ComponentModel.DataAnnotations.Schema;

namespace EAuction.Order.Domain.Entities.Base
{
    public abstract class Entity : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }

        public Entity Clone()
        {
            return (Entity)this.MemberwiseClone();
        }
    }
}