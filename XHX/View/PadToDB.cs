using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using XHX.Common;
using System.Data.Common;
using XHX.DTO;
using DbAccess;
using System.Threading;

namespace XHX.View
{
    public partial class PadToDB : BaseForm
    {
        public static localhost.Service service = new localhost.Service();
        UploadFileToAliyun aliyun = new UploadFileToAliyun();
        string ProjectCode_Golbal = "";
        string ShopCode_Golbal = "";

        public PadToDB()
        {
            InitializeComponent();
            XHX.Common.BindComBox.BindProject(cboProjects);
            XHX.Common.BindComBox.BindSubjectExamType(cboExamType);
           
        }

        public override List<XHX.BaseForm.ButtonType> CreateButton()
        {
            List<XHX.BaseForm.ButtonType> list = new List<XHX.BaseForm.ButtonType>();
            return list;
        }

        private void btnShopCode_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Shop_Popup pop = new Shop_Popup("", "", false);
            pop.ShowDialog();
            ShopDto dto = pop.Shopdto;
            if (dto != null)
            {
                btnShopCode.Text = dto.ShopCode;
                txtShopName.Text = dto.ShopName;
            }
            ProjectCode_Golbal = CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString();
            ShopCode_Golbal = btnShopCode.Text;

            //卖场改变的时候对应的试卷类型也进行改变

            //List<ShopSubjectExamTypeDto> list = new List<ShopSubjectExamTypeDto>();
            ShopSubjectExamTypeDto shop = new ShopSubjectExamTypeDto();
            DataSet ds = service.SearchShopExamTypeByProjectCodeAndShopCode(ProjectCode_Golbal, ShopCode_Golbal);
            if (ds.Tables[0].Rows.Count > 0)
            {
                shop.ExamTypeCode = ds.Tables[0].Rows[0]["SubjectTypeCodeExam"] == null ? "" : ds.Tables[0].Rows[0]["SubjectTypeCodeExam"].ToString();
            }
            else
            {
                shop.ExamTypeCode = "";
            }
            CommonHandler.SetComboBoxSelectedValue(cboExamType, shop.ExamTypeCode);
        }

        #region UploadData

        private void btnDataPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                btnDataPath.Text = fbd.SelectedPath;
            }
        }

        private void btnUploadData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //if (CommonHandler. == 0)
            //{
            //    CommonHandler.ShowMessage(MessageType.Information, "请选择\"项目\"");
            //    cboProjects.Focus();
            //    return;
            //}
            if (txtShopName.Text == "")
            {
                CommonHandler.ShowMessage(MessageType.Information, "请选择\"经销商\"");
                txtShopName.Focus();
                return;
            }
            if (btnDataPath.Text == "")
            {
                CommonHandler.ShowMessage(MessageType.Information, "请选择\"数据路径\"");
                btnDataPath.Focus();
                return;
            }

            ProjectCode_Golbal = CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString();
            ShopCode_Golbal = btnShopCode.Text;

            DirectoryInfo dataDir = new DirectoryInfo(btnDataPath.Text);
            FileInfo[] filesInfo = dataDir.GetFiles();

            bool isExistDBFile = false;
            foreach (FileInfo fileInfo in filesInfo)
            {
                if (fileInfo.Name == "writeable.db")
                {
                    isExistDBFile = true;
                    SqliteHelper.SetConnectionString("Data Source=" + fileInfo.FullName, "");
                }
            }
            if (!isExistDBFile)
            {
                CommonHandler.ShowMessage(MessageType.Information, "路径中不存在数据库文件'writeable.db'");
                return;
            }
            #endregion
            if (!RecheckStatus())
            {
                #region 上传Answer表数据
                {
                    List<String> dataList = SqliteHelper.Search("SELECT ProjectCode,SubjectCode,ShopCode,Score,Remark,ImageName,InUserID,'0','',AssessmentDate,InDateTime from Answer WHERE  ProjectCode='" + ProjectCode_Golbal + "' AND ShopCode='" + ShopCode_Golbal + "'");
                    List<String> updateStringList = new List<string>();
                    foreach (String data in dataList)
                    {
                        String[] properties = data.Split('$');
                        String updateString = @"update Answer Set Flag=1 WHERE ProjectCode='{0}' " +
                                                   "AND SubjectCode='{1}' " +
                                                   "AND ShopCode='{2}'";
                        updateString = String.Format(updateString, properties[0], properties[1], properties[2]);
                        updateStringList.Add(updateString);

                    }
                    service.SaveAnswerList(dataList.ToArray());
                    SqliteHelper.InsertOrUpdata(updateStringList);
                }
                #endregion

                #region 上传AnswerLog表数据
                {
                    List<String> dataList = SqliteHelper.Search("SELECT ProjectCode,SubjectCode,ShopCode,Score,Desc,InUserID,StatusCode from AnswerLog WHERE  ProjectCode='" + ProjectCode_Golbal + "' AND ShopCode='" + ShopCode_Golbal + "'");
                    List<String> updateStringList = new List<string>();
                    foreach (String data in dataList)
                    {
                        String[] properties = data.Split('$');
                        String updateString = @"update AnswerLog Set Flag=1 WHERE ProjectCode='{0}' " +
                                               "AND SubjectCode='{1}' " +
                                               "AND ShopCode='{2}'" +
                                               "AND StatusCode='{3}'";
                        updateString = String.Format(updateString, properties[0], properties[1], properties[2], properties[6]);
                        updateStringList.Add(updateString);

                    }
                    service.SaveAnswerLogList(dataList.ToArray());
                    SqliteHelper.InsertOrUpdata(updateStringList);
                }
                #endregion

                #region 上传AnswerDtl表数据
                {
                    List<String> dataList = SqliteHelper.Search("SELECT ProjectCode,SubjectCode,ShopCode,SeqNO,InUserID,CheckOptionCode,PicNameList from AnswerDtl WHERE  ProjectCode='" + ProjectCode_Golbal + "' AND ShopCode='" + ShopCode_Golbal + "'");
                    List<String> updateStringList = new List<string>();
                    foreach (String data in dataList)
                    {
                        String[] properties = data.Split('$');
                        String updateString = @"update AnswerDtl Set Flag=1,PicNameList='{4}' WHERE ProjectCode='{0}' " +
                                                   "AND SubjectCode='{1}' " +
                                                   "AND ShopCode='{2}' " +
                                                   "AND SeqNO={3}"; ;
                        updateString = String.Format(updateString, properties[0], properties[1], properties[2], properties[3], properties[6]);
                        updateStringList.Add(updateString);

                    }
                    service.SaveAnswerDtlList(dataList.ToArray());
                    SqliteHelper.InsertOrUpdata(updateStringList);
                }
                #endregion

                #region 上传AnswerDtl2表数据
                {
                    List<String> dataList = SqliteHelper.Search("SELECT ProjectCode,SubjectCode,ShopCode,SeqNO,InUserID,CheckOptionCode from AnswerDtl2 WHERE  ProjectCode='" + ProjectCode_Golbal + "' AND ShopCode='" + ShopCode_Golbal + "'");
                    List<String> updateStringList = new List<string>();
                    foreach (String data in dataList)
                    {
                        String[] properties = data.Split('$');
                        String updateString = @"update AnswerDtl2 Set Flag=1 WHERE ProjectCode='{0}' " +
                                                   "AND SubjectCode='{1}' " +
                                                   "AND ShopCode='{2}' " +
                                                   "AND SeqNO={3}";
                        updateString = String.Format(updateString, properties[0], properties[1], properties[2], properties[3]);
                        updateStringList.Add(updateString);

                    }
                    service.SaveAnswerDtl2StreamList(dataList.ToArray());
                    SqliteHelper.InsertOrUpdata(updateStringList);
                }
                #endregion
                #region 上传AnswerDtl3表数据
                {
                    List<String> dataList = SqliteHelper.Search("SELECT ProjectCode,SubjectCode,ShopCode,SeqNO,LossDesc,PicName from AnswerDtl3 WHERE ProjectCode='" + ProjectCode_Golbal + "' AND ShopCode='" + ShopCode_Golbal + "'");
                    List<String> updateStringList = new List<string>();
                    foreach (String data in dataList)
                    {
                        String[] properties = data.Split('$');
                        String updateString = @"update AnswerDtl3 Set Flag=1 WHERE ProjectCode='{0}' " +
                                                   "AND SubjectCode='{1}' " +
                                                   "AND ShopCode='{2}' " +
                                                   "AND SeqNO={3}";
                        updateString = String.Format(updateString, properties[0], properties[1], properties[2], properties[3]);
                        updateStringList.Add(updateString);

                    }
                    service.SaveAnswerDtl3StringList(dataList.ToArray());
                    SqliteHelper.InsertOrUpdata(updateStringList);
                }
                #endregion
            }
            else
            {

                CommonHandler.ShowMessage(MessageType.Information, "已经提交复审了，分数不会上传只会上传照片信息");
            }
            #region 上传图片文件
            {
                if (chkPic.Checked)
                {
                    //DateTime st = DateTime.Now;
                    DirectoryInfo[] dirInfos = dataDir.GetDirectories();
                    foreach (DirectoryInfo dirInfo in dirInfos)
                    {
                        if (dirInfo.Name == ProjectCode_Golbal + txtShopName.Text)
                        {
                            FileInfo[] fileList = dirInfo.GetFiles("Thumbs.db");
                            if (fileList != null && fileList.Length != 0)
                            {
                                foreach (FileInfo file in fileList)
                                {
                                    if (file.Name == "Thumbs.db")
                                    {
                                        file.Delete();
                                        break;
                                    }
                                }
                            }
                            UploadImgZipFileBySubDirectory(dirInfo.FullName);
                        }
                    }
                }
                CommonHandler.ShowMessage(MessageType.Information, "数据上传完毕。");
                //TimeSpan ts = DateTime.Now - st;
                //CommonHandler.ShowMessage(MessageType.Information,ts.ToString());
            }
            #endregion
        }
        string fail = string.Empty;
        void UploadImgZipFileBySubDirectory(string dirPath)
        {
            DirectoryInfo shopDir = new DirectoryInfo(dirPath);
            double shopDirSize = 0;
            foreach (DirectoryInfo dir in shopDir.GetDirectories())
            {
                foreach (FileInfo fi in dir.GetFiles())
                {
                    shopDirSize += fi.Length;
                }

            }
            DirectoryInfo[] dirInfos = shopDir.GetDirectories();

            for (int i = 0; i < dirInfos.Length; i++)
            {
                try
                {
                    DirectoryInfo subjectDir = dirInfos[i];
                    double subjectDirSize = 0;
                    foreach (FileInfo fi in subjectDir.GetFiles())
                    {
                        subjectDirSize += fi.Length;
                    }

                    // List<String> dataList = SqliteHelper.Search("SELECT ProjectCode,SubjectCode,ShopCode from PictureUploadLog WHERE  ProjectCode='" + ProjectCode_Golbal + "' AND ShopCode='" + ShopCode_Golbal + "' AND SubjectCode='"+dirInfos[i]+"'");
                    //if(dataList.Count!=0)
                    //{
                    //    continue;
                    //}
                    FileInfo[] picInfos = subjectDir.GetFiles();
                    for (int j = 0; j < picInfos.Length; j++)
                    {
                        // string tempFile = Path.Combine(Path.GetTempPath(), subjectDir.Name + picInfos[j].Name + ".zip");
                        //if (ZipHelper.Zip(picInfos[j].FullName, tempFile, ""))
                        //{
                        //FileStream fs = new FileStream(tempFile, FileMode.Open);
                        //byte[] zipFile = new byte[fs.Length];
                        //fs.Read(zipFile, 0, zipFile.Length);
                        //fs.Close();
                        aliyun.PutObject("yrtech", "HONDA" + @"/" + CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + txtShopName.Text + @"/" + subjectDir + @"/" + picInfos[j].Name,
                                    picInfos[j].FullName);
                        //service.UploadImgZipFile1(shopDir.Name, subjectDir.Name, zipFile);
                        try
                        {
                            pbrProgressForUpload.Value += (int)((subjectDirSize / shopDirSize) * 100D);
                        }
                        catch (Exception)
                        {

                        }
                        Application.DoEvents();
                        // File.Delete(subjectDir.FullName);
                        //}
                        //else
                        //{
                        //    CommonHandler.ShowMessage(MessageType.Information, "压缩图片文件夹\"" + subjectDir.FullName + "\"失败。");
                        //}
                    }
                    //Thread.Sleep(500);
                    //List<String> updateStringList = new List<string>();
                    //String updateString = @"insert into [PictureUploadLog](ProjectCode,ShopCode,SubjectCode,UploadChk)VALUES('{0}','{1}','{2}','{3}')";
                    //updateString = String.Format(updateString, ProjectCode_Golbal, ShopCode_Golbal, dirInfos[i],"Y");
                    //updateStringList.Add(updateString);

                    //SqliteHelper.InsertOrUpdata(updateStringList);

                }
                catch (UnauthorizedAccessException exx)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    fail += dirInfos[i].FullName + ";";
                    continue;
                }
            }
            if (string.IsNullOrEmpty(fail))
            {
                // CommonHandler.ShowMessage(MessageType.Information, "数据上传完毕。");
            }
            else
            {
                CommonHandler.ShowMessage(MessageType.Information, "数据上传完毕。" + fail + "未上传成功");
            }
            pbrProgressForUpload.Value = 0;
        }

        #endregion

        #region DownloadData

        private void tbnSQLitePath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbnSQLitePath.Text = fbd.SelectedPath;
            }
        }

        private void btnDownloadData_Click(object sender, EventArgs e)
        {
            if (tbnSQLitePath.Text == "")
            {
                CommonHandler.ShowMessage(MessageType.Information, "请选择\"数据路径\"");
                tbnSQLitePath.Focus();
                return;
            }

            string sqlConnString = GetSqlServerConnectionString("123.57.229.128", "HONDA", "sa", "mxT1@mfb");
            string sqlitePath = Path.Combine(tbnSQLitePath.Text.Trim(), "readonly.db");
            this.Cursor = Cursors.WaitCursor;
            SqlConversionHandler handler = new SqlConversionHandler(delegate(bool done,
                bool success, int percent, string msg)
            {
                Invoke(new MethodInvoker(delegate()
                {
                    pbrProgress.Value = percent;

                    if (done)
                    {
                        this.Cursor = Cursors.Default;

                        if (success)
                        {
                            File.Copy(sqlitePath, Path.Combine(Path.GetDirectoryName(sqlitePath), "writeable.db"), true);
                            CommonHandler.ShowMessage(MessageType.Information, "下载成功");
                            pbrProgress.Value = 0;
                        }
                        else
                        {
                            CommonHandler.ShowMessage(MessageType.Information, "下载失败\r\n" + msg);
                            pbrProgress.Value = 0;
                        }
                    }
                }));
            });
            SqlTableSelectionHandler selectionHandler = new SqlTableSelectionHandler(delegate(List<TableSchema> schema)
            {
                return schema;
            });

            FailedViewDefinitionHandler viewFailureHandler = new FailedViewDefinitionHandler(delegate(ViewSchema vs)
            {
                return null;
            });

            string password = null;
            SqlServerToSQLite.ConvertSqlServerToSQLiteDatabase(sqlConnString, sqlitePath, password, handler,
                selectionHandler, viewFailureHandler, false, false);
        }

        private static string GetSqlServerConnectionString(string address, string db, string user, string pass)
        {
            string res = @"Data Source=" + address.Trim() +
                ";Initial Catalog=" + db.Trim() + ";User ID=" + user.Trim() + ";Password=" + pass.Trim();
            return res;
        }

        #endregion

        #region UpdateData

        private void tbnSQLitePathForUpdate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbnSQLitePathForUpdate.Text = fbd.SelectedPath;
            }
        }

        private void btnDownloadDataForUpdate_Click(object sender, EventArgs e)
        {
            if (tbnSQLitePathForUpdate.Text == "")
            {
                CommonHandler.ShowMessage(MessageType.Information, "请选择\"数据路径\"");
                tbnSQLitePathForUpdate.Focus();
                return;
            }

            string sqlConnString = GetSqlServerConnectionString("123.57.229.128", "GACFCA_SqlLite", "sa", "mxT1@mfb");
            string sqlitePath = Path.Combine(tbnSQLitePathForUpdate.Text.Trim(), "readonly.db");
            this.Cursor = Cursors.WaitCursor;
            SqlConversionHandler handler = new SqlConversionHandler(delegate(bool done,
                bool success, int percent, string msg)
            {
                Invoke(new MethodInvoker(delegate()
                {
                    pbrProgressForUpdate.Value = percent;

                    if (done)
                    {
                        this.Cursor = Cursors.Default;

                        if (success)
                        {
                            CommonHandler.ShowMessage(MessageType.Information, "下载成功");
                            pbrProgressForUpdate.Value = 0;
                        }
                        else
                        {
                            CommonHandler.ShowMessage(MessageType.Information, "下载失败\r\n" + msg);
                            pbrProgressForUpdate.Value = 0;
                        }
                    }
                }));
            });
            SqlTableSelectionHandler selectionHandler = new SqlTableSelectionHandler(delegate(List<TableSchema> schema)
            {
                return schema;
            });

            FailedViewDefinitionHandler viewFailureHandler = new FailedViewDefinitionHandler(delegate(ViewSchema vs)
            {
                return null;
            });

            string password = null;
            SqlServerToSQLite.ConvertSqlServerToSQLiteDatabase(sqlConnString, sqlitePath, password, handler,
                selectionHandler, viewFailureHandler, false, false);
        }

        #endregion
        /// <summary>
        /// 测试大文件上传使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            DateTime dt = DateTime.Now;
            //string tempFile = Path.Combine(Path.GetTempPath(), "201503广汽丰田重庆朔龙龙腾店" + ".zip");
            //if (ZipHelper.Zip(@"C:\Users\ElandEmp\Desktop\XHX_YIQI_Data\XHX_YIQI_Data\201503广汽丰田重庆朔龙龙腾店", tempFile, ""))
            //{
            //    aliyun.PutObjectMultipart("yrtech", "Test" + @"/" + "201503广汽丰田重庆朔龙龙腾店1",
            //                        tempFile);
            //}

            //CommonHandler.ShowMessage(MessageType.Information, "上传完毕");

            DirectoryInfo shopDir = new DirectoryInfo(@"C:\Users\ElandEmp\Desktop\XHX_YIQI_Data\XHX_YIQI_Data\201503广汽丰田重庆朔龙龙腾店");
            double shopDirSize = 0;
            foreach (DirectoryInfo dir in shopDir.GetDirectories())
            {
                foreach (FileInfo fi in dir.GetFiles())
                {
                    shopDirSize += fi.Length;
                }

            }
            DirectoryInfo[] dirInfos = shopDir.GetDirectories();

            for (int i = 0; i < dirInfos.Length; i++)
            {
                try
                {
                    DirectoryInfo subjectDir = dirInfos[i];
                    double subjectDirSize = 0;
                    foreach (FileInfo fi in subjectDir.GetFiles())
                    {
                        subjectDirSize += fi.Length;
                    }
                    FileInfo[] picInfos = subjectDir.GetFiles();
                    for (int j = 0; j < picInfos.Length; j++)
                    {
                        aliyun.PutObject("yrtech", "Test" + @"/" + "11" + @"/" + CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString() + txtShopName.Text + @"/" + subjectDir + @"/" + picInfos[j].Name,
                                    picInfos[j].FullName);
                        try
                        {
                            pbrProgressForUpload.Value += (int)((subjectDirSize / shopDirSize) * 100D);
                        }
                        catch (Exception)
                        {

                        }
                        Application.DoEvents();
                    }


                }
                catch (UnauthorizedAccessException exx)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    fail += dirInfos[i].FullName + ";";
                    continue;
                }
            }
            if (string.IsNullOrEmpty(fail))
            {
                CommonHandler.ShowMessage(MessageType.Information, "数据上传完毕。");
            }
            else
            {
                CommonHandler.ShowMessage(MessageType.Information, "数据上传完毕。" + fail + "未上传成功");
            }
            //pbrProgressForUpload.Value = 0;
            TimeSpan ts = (DateTime.Now - dt);
            MessageBox.Show(ts.Hours.ToString() + " " + ts.Minutes.ToString() + " " + ts.Seconds.ToString());
        }
        public bool RecheckStatus()
        {
            DataSet ds = service.SearchRecheckStatus(ProjectCode_Golbal, ShopCode_Golbal);
            if (ds.Tables[0].Rows.Count > 0 || cboProjects.SelectedIndex != 0)
            {
                //btnSpecialCaseApply.Enabled = false;
                //grcFileAndPic.DragEnter -= new DragEventHandler(grcFileAndPic_DragEnter);
                //grcLoss.DragEnter -= new DragEventHandler(grcLoss_DragEnter);
                //btnAddRowLoss.Enabled = false;
                //btnDeleteLoss.Enabled = false;
                //txtScore.Enabled = false;
                //chkNotinvolved.Enabled = false;
                //txtRemark.Enabled = false;
                return true;
            }
            else
            {
                //btnSpecialCaseApply.Enabled = true; ;
                //grcFileAndPic.DragEnter += new DragEventHandler(grcFileAndPic_DragEnter);
                //grcLoss.DragEnter += new DragEventHandler(grcLoss_DragEnter);
                //btnAddRowLoss.Enabled = true; ;
                //btnDeleteLoss.Enabled = true;
                //txtScore.Enabled = true;
                //chkNotinvolved.Enabled = true;
                //txtRemark.Enabled = true;
                return false;
            }


        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.UserInfoDto.RoleType == "S")
            {
                service.CopyDataToSqlLite(CommonHandler.GetComboBoxSelectedValue(cboProjects).ToString());
                CommonHandler.ShowMessage(MessageType.Information, "更新完毕");
            }
            else
            {
                CommonHandler.ShowMessage(MessageType.Information, "不是管理员权限");
            }
        }

    }
}