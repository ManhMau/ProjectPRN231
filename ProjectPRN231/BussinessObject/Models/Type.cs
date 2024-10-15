using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class Type
    {
        public Type()
        {
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; } = null!;

        public virtual ICollection<Document> Documents { get; set; }
    }
}
