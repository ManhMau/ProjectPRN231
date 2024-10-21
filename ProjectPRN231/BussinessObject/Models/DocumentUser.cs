using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class DocumentUser
    {
        public int Id { get; set; }
        public int? DocumentId { get; set; }
        public string? UserId { get; set; } // Ensure this is string as it matches IdentityUser's key type

        public virtual Document? Document { get; set; }
        public virtual User? User { get; set; }
    }

}
