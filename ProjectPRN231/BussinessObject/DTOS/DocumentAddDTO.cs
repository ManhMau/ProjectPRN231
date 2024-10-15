using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTOS
{
    public class DocumentAddDTO
    {
        public int DocumentId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string FilePath { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public int TypeId { get; set; }
        public bool Status { get; set; }
    }
}
