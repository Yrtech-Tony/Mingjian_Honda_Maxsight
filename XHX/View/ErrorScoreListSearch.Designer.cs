namespace XHX.View
{
    partial class ErrorScoreListSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grcLossReg = new DevExpress.XtraGrid.GridControl();
            this.grvLossReg = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcProjectCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLossCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLossName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcInUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboAreaCode = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.chkUseChk = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.grdShop = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnShopCode = new DevExpress.XtraEditors.ButtonEdit();
            this.txtShopName = new DevExpress.XtraEditors.TextEdit();
            this.cboProject = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grcLossReg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLossReg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAreaCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseChk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShop)).BeginInit();
            this.grdShop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnShopCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShopName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProject.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grcLossReg
            // 
            this.grcLossReg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcLossReg.Location = new System.Drawing.Point(5, 52);
            this.grcLossReg.MainView = this.grvLossReg;
            this.grcLossReg.Name = "grcLossReg";
            this.grcLossReg.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboAreaCode,
            this.chkUseChk});
            this.grcLossReg.Size = new System.Drawing.Size(978, 556);
            this.grcLossReg.TabIndex = 17;
            this.grcLossReg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvLossReg});
            // 
            // grvLossReg
            // 
            this.grvLossReg.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcProjectCode,
            this.gridColumn1,
            this.gridColumn2,
            this.gcLossCode,
            this.gcLossName,
            this.gcInUserID});
            this.grvLossReg.GridControl = this.grcLossReg;
            this.grvLossReg.Name = "grvLossReg";
            this.grvLossReg.OptionsView.ColumnAutoWidth = false;
            this.grvLossReg.OptionsView.ShowGroupPanel = false;
            // 
            // gcProjectCode
            // 
            this.gcProjectCode.Caption = "期号";
            this.gcProjectCode.FieldName = "ProjectCode";
            this.gcProjectCode.Name = "gcProjectCode";
            this.gcProjectCode.Visible = true;
            this.gcProjectCode.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "经销商代码";
            this.gridColumn1.FieldName = "ShopCode";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 171;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "体系号";
            this.gridColumn2.FieldName = "SubjectCode";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 194;
            // 
            // gcLossCode
            // 
            this.gcLossCode.AppearanceCell.Options.UseTextOptions = true;
            this.gcLossCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcLossCode.AppearanceHeader.Options.UseTextOptions = true;
            this.gcLossCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcLossCode.Caption = "得分";
            this.gcLossCode.FieldName = "Score";
            this.gcLossCode.Name = "gcLossCode";
            this.gcLossCode.Width = 88;
            // 
            // gcLossName
            // 
            this.gcLossName.AppearanceHeader.Options.UseTextOptions = true;
            this.gcLossName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcLossName.Caption = "满分";
            this.gcLossName.FieldName = "FullScore";
            this.gcLossName.Name = "gcLossName";
            this.gcLossName.Visible = true;
            this.gcLossName.VisibleIndex = 3;
            this.gcLossName.Width = 183;
            // 
            // gcInUserID
            // 
            this.gcInUserID.AppearanceCell.Options.UseTextOptions = true;
            this.gcInUserID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcInUserID.AppearanceHeader.Options.UseTextOptions = true;
            this.gcInUserID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcInUserID.Caption = "错误类型";
            this.gcInUserID.FieldName = "ErrorType";
            this.gcInUserID.Name = "gcInUserID";
            this.gcInUserID.Visible = true;
            this.gcInUserID.VisibleIndex = 4;
            this.gcInUserID.Width = 185;
            // 
            // cboAreaCode
            // 
            this.cboAreaCode.AutoHeight = false;
            this.cboAreaCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboAreaCode.Name = "cboAreaCode";
            // 
            // chkUseChk
            // 
            this.chkUseChk.AutoHeight = false;
            this.chkUseChk.Name = "chkUseChk";
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl2.Location = new System.Drawing.Point(5, 47);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(978, 5);
            this.labelControl2.TabIndex = 19;
            // 
            // grdShop
            // 
            this.grdShop.Controls.Add(this.labelControl1);
            this.grdShop.Controls.Add(this.btnShopCode);
            this.grdShop.Controls.Add(this.txtShopName);
            this.grdShop.Controls.Add(this.cboProject);
            this.grdShop.Controls.Add(this.labelControl5);
            this.grdShop.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdShop.Location = new System.Drawing.Point(5, 5);
            this.grdShop.Margin = new System.Windows.Forms.Padding(0);
            this.grdShop.Name = "grdShop";
            this.grdShop.Size = new System.Drawing.Size(978, 42);
            this.grdShop.TabIndex = 18;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.Location = new System.Drawing.Point(181, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "经销商";
            // 
            // btnShopCode
            // 
            this.btnShopCode.EditValue = "";
            this.btnShopCode.Location = new System.Drawing.Point(223, 15);
            this.btnShopCode.Name = "btnShopCode";
            this.btnShopCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnShopCode.Size = new System.Drawing.Size(100, 21);
            this.btnShopCode.TabIndex = 15;
            this.btnShopCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnShopCode_ButtonClick);
            // 
            // txtShopName
            // 
            this.txtShopName.Enabled = false;
            this.txtShopName.Location = new System.Drawing.Point(329, 15);
            this.txtShopName.Name = "txtShopName";
            this.txtShopName.Size = new System.Drawing.Size(238, 21);
            this.txtShopName.TabIndex = 16;
            // 
            // cboProject
            // 
            this.cboProject.Location = new System.Drawing.Point(48, 15);
            this.cboProject.Name = "cboProject";
            this.cboProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProject.Size = new System.Drawing.Size(100, 21);
            this.cboProject.TabIndex = 13;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(18, 18);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 12;
            this.labelControl5.Text = "项目";
            // 
            // ErrorScoreListSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grcLossReg);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.grdShop);
            this.Name = "ErrorScoreListSearch";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(988, 613);
            ((System.ComponentModel.ISupportInitialize)(this.grcLossReg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLossReg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAreaCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseChk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShop)).EndInit();
            this.grdShop.ResumeLayout(false);
            this.grdShop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnShopCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShopName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProject.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcLossReg;
        private DevExpress.XtraGrid.Views.Grid.GridView grvLossReg;
        private DevExpress.XtraGrid.Columns.GridColumn gcLossCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcLossName;
        private DevExpress.XtraGrid.Columns.GridColumn gcInUserID;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboAreaCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkUseChk;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl grdShop;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ButtonEdit btnShopCode;
        private DevExpress.XtraEditors.TextEdit txtShopName;
        private DevExpress.XtraEditors.ComboBoxEdit cboProject;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraGrid.Columns.GridColumn gcProjectCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}