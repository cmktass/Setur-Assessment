using Core.CoreDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Entities
{
    public class Contact : Aggregate<Guid>
    {
        public string FirstName { get; }
        public string LastName { get;  }
        public string Company { get;  }

        public List<ContactInfo> ContactInfos { get; }
    }
}
