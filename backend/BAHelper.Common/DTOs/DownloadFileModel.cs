using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs
{
    public class DownloadFileModel
    {
        public MemoryStream MemoryStream { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public DownloadFileModel(MemoryStream memoryStream, string mimeType, string fileName) 
        {
            MemoryStream= memoryStream;
            MimeType= mimeType;
            FileName= fileName;
        }
    }
}
