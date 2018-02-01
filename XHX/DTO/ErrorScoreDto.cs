using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHX.DTO
{
    public class ErrorScoreDto
    {
        public string ProjectCode { get; set; }
        public string ShopCode { get; set; }
        public string SubjectCode { get; set; }
        public string Score { get; set; }
        public string FullScore { get; set; }
        public string ErrorType { get; set; }

    }
}
