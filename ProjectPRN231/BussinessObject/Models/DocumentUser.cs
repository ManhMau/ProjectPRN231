using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class DocumentUser
    {
        public int Id { get; set; }
        public int? DocumentId { get; set; }
        public int? UserId { get; set; }

        public virtual Document? Document { get; set; }
        public virtual User? User { get; set; }
    }
}
