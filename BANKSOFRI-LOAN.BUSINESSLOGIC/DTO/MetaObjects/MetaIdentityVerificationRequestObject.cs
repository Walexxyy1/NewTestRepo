using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO.MetaObjects
{
   public class MetaIdentityVerificationRequestObject
    {
        [Required]
        public IFormFile DocumentFront { get; set; }
        [Required]
        public IFormFile DocumentBack { get; set; }
        [Required]
        public IFormFile Liveness { get; set; }
        [Required]
        public List<Input> inputs { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public string CustomerAccountNubmer { get; set; }
    }
    public class IdentityInput
    {
        public string inputType { get; set; }
        public int group { get; set; }
        public IdentityData data { get; set; }
    }
    public class IdentityData
    {
        public string type { get; set; }
        public string country { get; set; }
        public string page { get; set; }
        public string filename { get; set; }
    }


}
