using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class Document
    {
        public Document()
        {
            DocumentUsers = new HashSet<DocumentUser>();
        }

        public int DocumentId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string FilePath { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public int TypeId { get; set; }
        public bool Status { get; set; }

        public virtual Type Type { get; set; } = null!;
        public virtual ICollection<DocumentUser> DocumentUsers { get; set; }
    }

}
