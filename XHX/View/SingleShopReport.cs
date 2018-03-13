using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XHX.DTO.SingleShopReport;
using XHX.DTO;
using XHX.Common;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Threading;

namespace XHX.View
{
    public partial class SingleShopReport : BaseForm
    {
        public static localhost.Service service = new localhost.Service();
        //LocalService service = new LocalService();
        MSExcelUtil msExcelUtil = new MSExcelUtil();
        List<ShopDto> shopList = new List<ShopDto>();
        List<ShopDto> shopLeft = new List<ShopDto>();
        public List<ShopDto> ShopList
        {
            get { return shopList; }
            set { shopList = value; }
        }
        GridCheckMarksSelection selection;
        internal GridCheckMarksSelection Selection
        {
            get
            {
                return selection;
            }
        }
        public SingleShopReport()
        {
            InitializeComponent();
            XHX.Common.BindComBox.BindProject(cboProjects);
            tbnFilePath.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            btnModule.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            CommonHandler.SetRowNumberIndicator(gridView1);
            SearchAllShopByProjectCode(CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString());
            
            selection = new GridCheckMarksSelection(gridView1);
            selection.CheckMarkColumn.VisibleIndex = 0;
        }

        public override List<BaseForm.ButtonType> CreateButton()
        {
            List<XHX.BaseForm.ButtonType> list = new List<XHX.BaseForm.ButtonType>();
            return list;
        }

        private List<ShopDto> SearchAllShopByProjectCode(string projectCode)
        {
            DataSet ds = service.SearchShopByProjectCode(projectCode);
            List<ShopDto> shopDtoList = new List<ShopDto>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ShopDto shopDto = new ShopDto();
                    shopDto.ShopCode = Convert.ToString(ds.Tables[0].Rows[i]["ShopCode"]);
                    shopDto.ShopName = Convert.ToString(ds.Tables[0].Rows[i]["ShopName"]);
                    shopDtoList.Add(shopDto);
                }
            }
            grcShop.DataSource = shopDtoList;
            return shopDtoList;
        }

        private ShopReportDto GetShopReportDto(string projectCode, string shopCode)
        {
            DataSet[] dataSetList = service.GetShopReportDto(projectCode, shopCode);
            ShopReportDto shopReportDto = new ShopReportDto();
            List<ShopSubjectScoreInfoDto> shopSubjectScoreInfoDtoList = new List<ShopSubjectScoreInfoDto>();


            shopReportDto.ShopSubjectScoreInfoDtoList = shopSubjectScoreInfoDtoList;

            #region 封面信息
            DataSet ds = dataSetList[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    shopReportDto.ProjectCode = Convert.ToString(ds.Tables[0].Rows[i]["ProjectCode"]);
                    shopReportDto.ShopCode = Convert.ToString(ds.Tables[0].Rows[i]["ShopCode"]);
                    shopReportDto.ShopName = Convert.ToString(ds.Tables[0].Rows[i]["ShopName"]);
                    shopReportDto.AreaName = Convert.ToString(ds.Tables[0].Rows[i]["AreaName"]);
                    shopReportDto.Province = Convert.ToString(ds.Tables[0].Rows[i]["Province"]);
                    shopReportDto.City = Convert.ToString(ds.Tables[0].Rows[i]["City"]);
                }
            }
            #endregion
            #region 指标点得分
            ds = dataSetList[1];
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ShopSubjectScoreInfoDto subjectScore = new ShopSubjectScoreInfoDto();
                    //subjectScore.FullScore = Convert.ToDecimal(ds.Tables[0].Rows[i]["FullScore"]);
                    subjectScore.Score = Convert.ToString(ds.Tables[0].Rows[i]["Score"]);
                    subjectScore.ScoreYOrN = Convert.ToString(ds.Tables[0].Rows[i]["ScoreYOrN"]);
                    subjectScore.LossDesc = Convert.ToString(ds.Tables[0].Rows[i]["LossDesc"]);
                    // subjectScore.PicName = Convert.ToString(ds.Tables[0].Rows[i]["PicName"]);
                    subjectScore.SubjectCode = Convert.ToString(ds.Tables[0].Rows[i]["SubjectCode"]);
                    shopSubjectScoreInfoDtoList.Add(subjectScore);
                }
            }
            #endregion
            return shopReportDto;
        }

        private void WriteDataToExcel(ShopReportDto shopReportDto)
        {

            Workbook workbook = msExcelUtil.OpenExcelByMSExcel(tbnFilePath.Text + @"\" + "单店报告模板.xlsx");

            #region 经销商基本信息
            {
                Worksheet worksheet_FengMian = workbook.Worksheets["广汽本田客服领域特约店得分"] as Worksheet;
                #region 经销商基本信息
                // msExcelUtil.SetCellValue(worksheet_FengMian, "D1", shopReportDto.ShopName);
                msExcelUtil.SetCellValue(worksheet_FengMian, "D6", shopReportDto.Province);
                msExcelUtil.SetCellValue(worksheet_FengMian, "D7", shopReportDto.ShopCode);
                msExcelUtil.SetCellValue(worksheet_FengMian, "H6", shopReportDto.AreaName);
                msExcelUtil.SetCellValue(worksheet_FengMian, "H7", shopReportDto.ShopName);

                #endregion

                #region 体系信息

                Worksheet worksheet_Subject = workbook.Worksheets["考核项目达成明细"] as Worksheet;
                for (int i = 5; i < 300; i++)
                {
                    for (int j = 0; j < shopReportDto.ShopSubjectScoreInfoDtoList.Count; j++)
                    {
                        if (msExcelUtil.GetCellValue(worksheet_Subject, "M", i).ToString() == shopReportDto.ShopSubjectScoreInfoDtoList[j].SubjectCode
                            || msExcelUtil.GetCellValue(worksheet_Subject, "M", i).ToString() == "*" + shopReportDto.ShopSubjectScoreInfoDtoList[j].SubjectCode)
                        {
                            msExcelUtil.SetCellValue(worksheet_Subject, "O", i, shopReportDto.ShopSubjectScoreInfoDtoList[j].ScoreYOrN);
                            msExcelUtil.SetCellValue(worksheet_Subject, "P", i, shopReportDto.ShopSubjectScoreInfoDtoList[j].LossDesc);
                            if (shopReportDto.ShopSubjectScoreInfoDtoList[j].LossDesc.Length >= 42)
                                msExcelUtil.SetCellHeight(worksheet_Subject, "P", i, 36);
                            if (shopReportDto.ShopSubjectScoreInfoDtoList[j].LossDesc.Length >= 63)
                                msExcelUtil.SetCellHeight(worksheet_Subject, "P", i, 54);
                            if (shopReportDto.ShopSubjectScoreInfoDtoList[j].LossDesc.Length >= 84)
                                msExcelUtil.SetCellHeight(worksheet_Subject, "P", i, 72);
                            if (shopReportDto.ShopSubjectScoreInfoDtoList[j].LossDesc.Length >= 105)
                                msExcelUtil.SetCellHeight(worksheet_Subject, "P", i, 90);

                        }
                    }
                }
                #endregion


            }
            #endregion

            workbook.Close(true, Path.Combine(tbnFilePath.Text, shopReportDto.AreaName + "_" + shopReportDto.ShopCode + "_" + shopReportDto.ShopName + "_2018年第1期售后明检项目_单店报告.xlsx"), Type.Missing);
        }

        private void GenerateReport()
        {
            string projectCode = CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString();
            _shopDtoList = new List<ShopDto>();
            //_shopDtoList = SearchAllShopByProjectCode(projectCode);
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellValue(i, "CheckMarkSelection") != null && gridView1.GetRowCellValue(i, "CheckMarkSelection").ToString() == "True")
                {
                    _shopDtoList.Add(gridView1.GetRow(i) as ShopDto);
                }
            }
            _shopDtoListCount = _shopDtoList.Count;
            this.Enabled = false;
            _bw = new BackgroundWorker();
            _bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            _bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            _bw.WorkerReportsProgress = true;
            _bw.RunWorkerAsync(new object[] { projectCode });
        }

        BackgroundWorker _bw;
        List<ShopDto> _shopDtoList;
        int _shopDtoListCount = 0;
        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbrProgress.Value = (e.ProgressPercentage) * 100 / _shopDtoListCount;
            System.Windows.Forms.Application.DoEvents();
        }
        void WriteErrorLog(string errMessage)
        {
            string path = tbnFilePath.Text + "\\" + "Error.txt";

            // Delete the file if it exists.
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream fs = File.Create(path))
            {
                AddText(fs, errMessage + "\r\n");
            }

        }
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] shopNames;
            int currentShopDtoIndex = 0;
            foreach (ShopDto shopDto in _shopDtoList)
            {
                try
                {
                    object[] arguments = e.Argument as object[];
                    ShopReportDto shopReportDto = GetShopReportDto(arguments[0] as string, shopDto.ShopCode);
                    WriteDataToExcel(shopReportDto);
                    _bw.ReportProgress(currentShopDtoIndex++);
                }
                catch (Exception ex)
                {
                    shopLeft.Add(shopDto);
                    WriteErrorLog(shopDto.ShopCode + shopDto.ShopName + ex.Message.ToString());
                    continue;
                }

            }
        }
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            this.Enabled = true;
            List<ShopDto> gridSource = grcShop.DataSource as List<ShopDto>;

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.SetRowCellValue(i, "CheckMarkSelection", false);
                foreach (ShopDto shop in shopLeft)
                {
                    if (shop.ShopCode == gridSource[i].ShopCode)
                    {
                        gridView1.SetRowCellValue(i, "CheckMarkSelection", true);
                    }
                    //else
                    //{
                    //    gridView1.SetRowCellValue(i, "CheckMarkSelection", false);
                    //}
                }
            }
            //if (shopLeft.Count > 0)
            //{
            //    string str = string.Empty;
            //    foreach (ShopDto shop in shopLeft)
            //    {
            //        str += shop.ShopCode + ":" + shop.ShopName + ";";
            //    }
            //    CommonHandler.ShowMessage(MessageType.Information, "报告生成完毕未生成报告经销商如下:" + str);
            //}
            //else
            //{
            CommonHandler.ShowMessage(MessageType.Information, "报告生成完毕");
            //}

        }

        private void tbnFilePath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbnFilePath.Text = fbd.SelectedPath;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbnFilePath.Text))
            {
                CommonHandler.ShowMessage(MessageType.Information, "请选择报告生成路径");
                return;
            }
            GenerateReport();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SearchAllShopByProjectCode(CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString());
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //ShopNotInScore shop = new ShopNotInScore(CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString());
            //shop.ShowDialog();

        }

        private void btnModule_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofp = new OpenFileDialog();
            ofp.Filter = "Excel(*.xlsx)|";
            ofp.FilterIndex = 2;
            if (ofp.ShowDialog() == DialogResult.OK)
            {
                btnModule.Text = ofp.FileName;
            }
        }
        public static void log(string message)
        {
            string fileName = "D:" + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            //File.Create(fileName);
            if (!Directory.Exists("D:" + @"\" + DateTime.Now.ToString("yyyyMMdd")))
            {
                Directory.CreateDirectory("D:" + @"\" + DateTime.Now.ToString("yyyyMMdd"));
            }
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                byte[] by = WriteStringToByte(message, fs);
                fs.Flush();
            }
        }
        public static byte[] WriteStringToByte(string str, FileStream fs)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(str);
            fs.Write(info, 0, info.Length);
            return info;
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {

            if (tbnFilePath.Text == "")
            {
                CommonHandler.ShowMessage(MessageType.Information, "请选择\"数据路径\"");
                tbnFilePath.Focus();
                return;
            }
            _shopDtoList = new List<ShopDto>();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellValue(i, "CheckMarkSelection") != null && gridView1.GetRowCellValue(i, "CheckMarkSelection").ToString() == "True")
                {
                    _shopDtoList.Add(gridView1.GetRow(i) as ShopDto);
                }
            }

            foreach (ShopDto shop in _shopDtoList)
            {
                if (!Directory.Exists(tbnFilePath.Text + @"\" + CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName))
                {
                    Directory.CreateDirectory(tbnFilePath.Text + @"\" + CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName);
                }
                DataSet ds = service.SearchLossPicByShopCode(CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString(), shop.ShopCode);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (!Directory.Exists(tbnFilePath.Text + @"\" + CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName + @"\" + ds.Tables[0].Rows[i]["SubjectCode"].ToString()))
                        {
                            Directory.CreateDirectory(tbnFilePath.Text + @"\" + CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName + @"\" + ds.Tables[0].Rows[i]["SubjectCode"].ToString());
                        }
                        string[] picName = ds.Tables[0].Rows[i]["PicName"].ToString().Split(';');
                        //string lossDesc = ds.Tables[0].Rows[i]["LossDesc"].ToString();
                        if (picName.Length == 1)
                        {
                            byte[] image = service.SearchPicStream(CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName, ds.Tables[0].Rows[i]["SubjectCode"].ToString(), picName[0].Replace(".jpg", ""));
                            if (image != null)
                            {

                                MemoryStream buf = new MemoryStream(image);
                                Image picimage = Image.FromStream(buf, true);
                                picimage.Save(tbnFilePath.Text + @"\" + CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName + @"\" + ds.Tables[0].Rows[i]["SubjectCode"].ToString() + @"\" + picName[0].Replace(".jpg", "") + ".jpg");
                            }
                        }
                        else
                        {
                            for (int j = 0; j < picName.Length; j++)
                            {
                                byte[] image = service.SearchPicStream(CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName, ds.Tables[0].Rows[i]["SubjectCode"].ToString(), picName[j].Replace(".jpg", ""));
                                if (image != null)
                                {
                                    MemoryStream buf = new MemoryStream(image);
                                    Image picimage = Image.FromStream(buf, true);
                                    picimage.Save(tbnFilePath.Text + @"\" + CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName + @"\" + ds.Tables[0].Rows[i]["SubjectCode"].ToString() + @"\" + picName[j].Replace(".jpg", "") + ".jpg");
                                }
                            }
                        }

                    }
                }

            }
            CommonHandler.ShowMessage(MessageType.Information, "生成完毕");
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (tbnFilePath.Text == "")
            {
                CommonHandler.ShowMessage(MessageType.Information, "请选择\"数据路径\"");
                tbnFilePath.Focus();
                return;
            }
            _shopDtoList = new List<ShopDto>();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellValue(i, "CheckMarkSelection") != null && gridView1.GetRowCellValue(i, "CheckMarkSelection").ToString() == "True")
                {
                    _shopDtoList.Add(gridView1.GetRow(i) as ShopDto);
                }
            }

            foreach (ShopDto shop in _shopDtoList)
            {
                if (!Directory.Exists(tbnFilePath.Text + @"\" + CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName))
                {
                    Directory.CreateDirectory(tbnFilePath.Text + @"\" + CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName);
                }
                DataSet ds = service.SearchSubjectFile(CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString(), txtSubjectCode.Text);
                // DataSet ds = service.SearchLossPicByShopCode(CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString(), shop.ShopCode);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (!Directory.Exists(tbnFilePath.Text + @"\" + CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName + @"\" + ds.Tables[0].Rows[i]["SubjectCode"].ToString()))
                        {
                            Directory.CreateDirectory(tbnFilePath.Text + @"\" + CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName + @"\" + ds.Tables[0].Rows[i]["SubjectCode"].ToString());
                        }
                        string fileName = ds.Tables[0].Rows[i]["FileName"].ToString();
                        //string lossDesc = ds.Tables[0].Rows[i]["LossDesc"].ToString();
                        //if (picName.Length == 1)
                        //{
                        byte[] image = service.SearchPicStream(CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName, ds.Tables[0].Rows[i]["SubjectCode"].ToString(), fileName.Replace(".jpg", ""));
                        if (image != null)
                        {

                            MemoryStream buf = new MemoryStream(image);
                            Image picimage = Image.FromStream(buf, true);
                            picimage.Save(tbnFilePath.Text + @"\" + CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + shop.ShopName + @"\" + ds.Tables[0].Rows[i]["SubjectCode"].ToString() + @"\" + fileName.Replace(".jpg", "") + ".jpg");
                        }

                    }
                }

            }
            CommonHandler.ShowMessage(MessageType.Information, "生成完毕");
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            List<ShopDto> gridSource = grcShop.DataSource as List<ShopDto>;
            List<string> excelList = new List<string>();
            Workbook workbook = msExcelUtil.OpenExcelByMSExcel(btnModule.Text);
            Worksheet worksheet = workbook.Worksheets["Sheet1"] as Worksheet;
            for (int i = 1; i < 700; i++)
            {
                string shopCode = msExcelUtil.GetCellValue(worksheet, "A", i).ToString();
                if (!string.IsNullOrEmpty(shopCode))
                {
                    excelList.Add(shopCode);
                }
            }

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.SetRowCellValue(i, "CheckMarkSelection", false);
                foreach (string shop in excelList)
                {
                    if (shop == gridSource[i].ShopCode)
                    {
                        gridView1.SetRowCellValue(i, "CheckMarkSelection", true);
                    }
                }
            }
            CommonHandler.ShowMessage(MessageType.Information, "设置完毕");
        }

    }
}
