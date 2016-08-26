namespace pingshen1
{
    partial class ProjectStartup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectStartup));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.项目新增ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.项目修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.项目新增ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.评审类别修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.评审类别新增ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ClassAddT = new System.Windows.Forms.ToolStripButton();
            this.ClassAlterT = new System.Windows.Forms.ToolStripButton();
            this.ProjectAddT = new System.Windows.Forms.ToolStripButton();
            this.ProjectAlterT = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.ContextMenuStrip = this.contextMenuStrip2;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridControl1.Location = new System.Drawing.Point(555, 47);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1021, 832);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.项目新增ToolStripMenuItem,
            this.项目修改ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(145, 56);
            // 
            // 项目新增ToolStripMenuItem
            // 
            this.项目新增ToolStripMenuItem.Name = "项目新增ToolStripMenuItem";
            this.项目新增ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.项目新增ToolStripMenuItem.Text = "项目新增";
            this.项目新增ToolStripMenuItem.Click += new System.EventHandler(this.项目新增ToolStripMenuItem_Click);
            // 
            // 项目修改ToolStripMenuItem
            // 
            this.项目修改ToolStripMenuItem.Name = "项目修改ToolStripMenuItem";
            this.项目修改ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.项目修改ToolStripMenuItem.Text = "项目修改";
            this.项目修改ToolStripMenuItem.Click += new System.EventHandler(this.项目修改ToolStripMenuItem_Click);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.GroupRow.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn10});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsFind.FindDelay = 300;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn3, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "项目编号";
            this.gridColumn1.FieldName = "ZDXB_BH";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 160;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "项目内容";
            this.gridColumn2.FieldName = "ZDXB_NAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 650;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "项目属性";
            this.gridColumn3.FieldName = "ZDXB_SX";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "创建日期";
            this.gridColumn4.FieldName = "ZDXB_DATE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 274;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "细表ID";
            this.gridColumn10.FieldName = "ZDXB_ID";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridControl2
            // 
            this.gridControl2.ContextMenuStrip = this.contextMenuStrip1;
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridControl2.Location = new System.Drawing.Point(3, 47);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(546, 832);
            this.gridControl2.TabIndex = 11;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gridControl2.Click += new System.EventHandler(this.gridControl2_Click);
            this.gridControl2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl2_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.项目新增ToolStripMenuItem1,
            this.评审类别修改ToolStripMenuItem,
            this.评审类别新增ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(175, 82);
            // 
            // 项目新增ToolStripMenuItem1
            // 
            this.项目新增ToolStripMenuItem1.Name = "项目新增ToolStripMenuItem1";
            this.项目新增ToolStripMenuItem1.Size = new System.Drawing.Size(174, 26);
            this.项目新增ToolStripMenuItem1.Text = "项目新增";
            this.项目新增ToolStripMenuItem1.Click += new System.EventHandler(this.项目新增ToolStripMenuItem_Click);
            // 
            // 评审类别修改ToolStripMenuItem
            // 
            this.评审类别修改ToolStripMenuItem.Name = "评审类别修改ToolStripMenuItem";
            this.评审类别修改ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.评审类别修改ToolStripMenuItem.Text = "评审类别修改";
            this.评审类别修改ToolStripMenuItem.Click += new System.EventHandler(this.评审类别修改ToolStripMenuItem_Click);
            // 
            // 评审类别新增ToolStripMenuItem
            // 
            this.评审类别新增ToolStripMenuItem.Name = "评审类别新增ToolStripMenuItem";
            this.评审类别新增ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.评审类别新增ToolStripMenuItem.Text = "评审类别新增";
            this.评审类别新增ToolStripMenuItem.Click += new System.EventHandler(this.评审类别新增ToolStripMenuItem_Click);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.GroupRow.Options.UseFont = true;
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.GroupCount = 1;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn5, DevExpress.Data.ColumnSortOrder.Descending)});
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "状态";
            this.gridColumn5.FieldName = "ZDZB_ZT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "评审类别";
            this.gridColumn6.FieldName = "ZDZB_TITLE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "备注";
            this.gridColumn7.FieldName = "ZDZB_BZ";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "创建日期";
            this.gridColumn8.FieldName = "ZDZB_DATE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "主表ID";
            this.gridColumn9.FieldName = "ZDZB_ID";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.gridControl2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1579, 881);
            this.tableLayoutPanel1.TabIndex = 14;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClassAddT,
            this.ClassAlterT,
            this.ProjectAddT,
            this.ProjectAlterT});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1579, 30);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ClassAddT
            // 
            this.ClassAddT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ClassAddT.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClassAddT.Image = ((System.Drawing.Image)(resources.GetObject("ClassAddT.Image")));
            this.ClassAddT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClassAddT.Name = "ClassAddT";
            this.ClassAddT.Size = new System.Drawing.Size(121, 27);
            this.ClassAddT.Text = "评审类别新增 ";
            this.ClassAddT.Click += new System.EventHandler(this.ClassAddT_Click);
            // 
            // ClassAlterT
            // 
            this.ClassAlterT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ClassAlterT.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClassAlterT.Image = ((System.Drawing.Image)(resources.GetObject("ClassAlterT.Image")));
            this.ClassAlterT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClassAlterT.Name = "ClassAlterT";
            this.ClassAlterT.Size = new System.Drawing.Size(121, 27);
            this.ClassAlterT.Text = "评审类别修改 ";
            this.ClassAlterT.Click += new System.EventHandler(this.ClassAlterT_Click);
            // 
            // ProjectAddT
            // 
            this.ProjectAddT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ProjectAddT.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProjectAddT.Image = ((System.Drawing.Image)(resources.GetObject("ProjectAddT.Image")));
            this.ProjectAddT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProjectAddT.Name = "ProjectAddT";
            this.ProjectAddT.Size = new System.Drawing.Size(82, 27);
            this.ProjectAddT.Text = "项目新增";
            this.ProjectAddT.Click += new System.EventHandler(this.ProjectAddT_Click);
            // 
            // ProjectAlterT
            // 
            this.ProjectAlterT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ProjectAlterT.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProjectAlterT.Image = ((System.Drawing.Image)(resources.GetObject("ProjectAlterT.Image")));
            this.ProjectAlterT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProjectAlterT.Name = "ProjectAlterT";
            this.ProjectAlterT.Size = new System.Drawing.Size(82, 27);
            this.ProjectAlterT.Text = "项目修改";
            this.ProjectAlterT.Click += new System.EventHandler(this.ProjectAlterT_Click);
            // 
            // ProjectStartup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1579, 881);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ProjectStartup";
            this.Text = "项目查看及修改";
            this.Load += new System.EventHandler(this.ProjectStartup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        public DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 评审类别新增ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 评审类别修改ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 项目新增ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 项目修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 项目新增ToolStripMenuItem1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ClassAddT;
        private System.Windows.Forms.ToolStripButton ClassAlterT;
        private System.Windows.Forms.ToolStripButton ProjectAddT;
        private System.Windows.Forms.ToolStripButton ProjectAlterT;
    }
}