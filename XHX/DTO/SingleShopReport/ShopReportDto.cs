using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHX.DTO.SingleShopReport
{
    public class ShopReportDto
    {
        public string ProjectCode { get; set; }
        public string ShopCode { get; set; }
        public string ShopName { get; set; }
        public string AreaName { get; set; }
        public string SalesContant { get; set; }
        public string ShopScore { get; set; }
        public string SmallAreaScore { get; set; }
        public string BigAreaScore { get; set; }
        public string AllScore { get; set; }
        public string OrderNO_Area { get; set; }
        public string OrderNO_All { get; set; }
        public string MustLoss { get; set; }
        public string Province { get; set; }
        public string City { get; set; }

        public List<ShopCharterScoreInfoDto> ShopCharterScoreInfoDtoList { get; set; }
        public List<ShopSubjectScoreInfoDto> ShopSubjectScoreInfoDtoList { get; set; }
        public List<BDCORRepScoreInfoDto> BDCORRepScoreInfoDtoList { get; set; }
        public List<ShopSubjectScoreInfo_BDCOrRepDto> BDCShopSubjectScoreInfoList { get; set; }

        public List<SaleContantScoreInfoDto> SaleContantScoreInfoList { get; set; }
        public List<SaleContantScoreInfo_AreaDto> SaleContantScoreInfo_AreaList { get; set; }
        public List<SaleContantCharterScoreInfoDto> SaleContantCharterScoreInfoDtoList { get; set; }
        public List<SaleAreaCharterScoreDto> SaleAreaCharterScoreDtoList { get; set; }
        public List<SaleContantSubjectScoreDto> SaleContantSubjectScoreDtoList { get; set; }
      
    }
    public class ShopCharterScoreInfoDto
    {
        public string CharterCode { get; set; }
        public string ShopScore { get; set; }
        public string SmallScore { get; set; }
        public string BigScore { get; set; }
        public string AllScore { get; set; }
    }
    public class ShopSubjectScoreInfoDto
    {
        public string SubjectCode { get; set; }
        public string CheckPoint { get; set; }
        public string Score { get; set; }
        public string ScoreYOrN { get; set; }
        public string LossDesc { get; set; }
        public string Remark { get; set; }
    }
    public class BDCORRepScoreInfoDto
    {
        public string Score { get; set; }
        public string SaleName { get; set; }
        public string SmallAreaScore { get; set; }
        public string BigAreaScore { get; set; }
        public string AllScore { get; set; }
        public string SalesType { get; set; }
    }
    public class ShopSubjectScoreInfo_BDCOrRepDto
    {
        public string SubjectCode { get; set; }
        public string CheckPoint { get; set; }
        public string Score { get; set; }
        public string LossDesc { get; set; }
        public string Remark { get; set; }

    }

    public class SaleContantScoreInfoDto
    {
        public string SaleName { get; set; }
        public string Score { get; set; }
    }
    public class SaleContantScoreInfo_AreaDto
    {
        public string SmallAreaScore { get; set; }
        public string BigAreaScore { get; set; }
        public string AllScore { get; set; }
    }
    public class SaleContantCharterScoreInfoDto
    {
        public string CharterCode { get; set; }
        public string SaleName { get; set; }
        public string Score { get; set; }
    }
    public class SaleAreaCharterScoreDto
    {
        public string CharterCode { get; set; }
        public string SmallCharterScore { get; set; }
        public string BigCharterScore { get; set; }
        public string AllCharterScore { get; set; }
    }
    public class SaleContantSubjectScoreDto
    {
        public string SubjectCode { get; set; }
        public string SaleName { get; set; }
        public string Score { get; set; }
        public string Remark { get; set; }

    }
}
