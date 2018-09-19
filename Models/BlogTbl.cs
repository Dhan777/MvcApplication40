using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace MvcApplication40.Models
{
    public class BlogTbl
    {
        public int BlogID { get; set; }
        public string BlogTitle { get; set; }
        [Required(ErrorMessage="Technology is Required")]
        public string Technology { get; set; }
        [AllowHtml]
        [Required(ErrorMessage="Content Is Required")]
        public string Content { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UploadedDate { get; set; }
        public string Author { get; set; }
        [Required(ErrorMessage="File is Required")]
        public HttpPostedFileBase File { get; set; }
        public byte[] ImageData     {get;set;}
        public byte[] TechnologyImage { get; set; }
        public byte[] UserImage       { get; set; }
        public int TechnologyID { get; set; }
    }
   
}