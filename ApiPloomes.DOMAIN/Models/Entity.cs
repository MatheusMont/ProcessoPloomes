using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Models
{
    public abstract class Entity
    {
        public virtual Guid Id { get; protected set; }
        public DateTime CreationDate { get; protected set; } = DateTime.UtcNow;
        public DateTime DeletionDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Active { get; set; } = true;
    }
}
