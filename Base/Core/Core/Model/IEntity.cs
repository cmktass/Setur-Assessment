using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core.Model
{
    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
    public interface IEntity
    {
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
