using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models.DataModels
{
    public class MultiFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public int MultiFileFormId { get; set; }
        
        public virtual MultiFileForm MultiFileForm { get; set; }
    }
}
