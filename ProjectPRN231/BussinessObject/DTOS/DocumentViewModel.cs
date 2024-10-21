using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTOS
{
    public class DocumentViewModel
    {
        public int DocumentId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string FilePath { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public int TypeId { get; set; }
        public bool Status { get; set; }

        public IEnumerable<DocTypeMapper>  Type { get; set; } = new List<DocTypeMapper>();
    }
}
