using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using pingshen1;
using Department;

namespace Evaluate
{
    public partial class Startup : Form
    {
        SqlConnection conn = new SqlConnection();
        List<string> ZDZB_ID = new List<string>();
        List<string> ZDZB_TITLE = new List<string>();
        List<string> ZDZB_BZ = new List<string>();
        List<string> ZDZB_ZT = new List<string>();//状态：可用，停用
        List<string> ZDZB_DATE = new List<string>();

        List<string> ZDBM_MC = new List<string>();
        List<string> ZDBM_DD = new List<string>();
        List<string> PS_DATE = new List<string>();
        List<string> PX_PF = new List<string>();
        List<string> PX_BZ = new List<string>();
        List<string> ZDXB_BH = new List<string>();
        List<string> ZDXB_NAME = new List<string>();

        public StringBuilder SelectClassID = new StringBuilder();//单击评审类别ID
        public StringBuilder SelectClass = new StringBuilder();//单击评审类别
        public StringBuilder SelectClassBZ = new StringBuilder();//单击评审备注
        public StringBuilder SelectClassZT = new StringBuilder();//单击评审状态
        public StringBuilder SelectClassDATE = new StringBuilder();//单击评审执日期

        int t1_flag = 0;//线程标记

        int xWidth = SystemInformation.PrimaryMonitorSize.Width;//获取显示器屏幕宽度 
        int yHeight = SystemInformation.PrimaryMonitorSize.Height;//获取显示器屏幕高度

        public DataTable ClassDisplay = new DataTable();
        public DataTable PingshenDisplay = new DataTable();

        delegate void UpdateUI();//用于多线程的界面UI委托
        public Startup()
        {
            InitializeComponent();
        }

        private void Startup_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = common.Database.conn;

            ClassDisplay.Columns.Add("ZDZB_ZT", typeof(string));
            ClassDisplay.Columns.Add("ZDZB_TITLE", typeof(string));
            ClassDisplay.Columns.Add("ZDZB_BZ", typeof(string));
            ClassDisplay.Columns.Add("ZDZB_DATE", typeof(string));
            ClassDisplay.Columns.Add("ZDZB_ID", typeof(string));

            PingshenDisplay.Columns.Add("ZDBM_MC", typeof(string));
            PingshenDisplay.Columns.Add("ZDBM_DD", typeof(string));
            PingshenDisplay.Columns.Add("PS_DATE", typeof(string));
            PingshenDisplay.Columns.Add("PX_PF", typeof(string));
            PingshenDisplay.Columns.Add("PX_BZ", typeof(string));
            PingshenDisplay.Columns.Add("ZDXB_BH", typeof(string));
            PingshenDisplay.Columns.Add("ZDXB_NAME", typeof(string));

            //从Y_ZDZB（评审主表）中读取评鉴项目
            WriteGridControl1();
            gridView1.OptionsBehavior.AutoExpandAllGroups = true;//分组展开
        }

         public void WriteGridControl1()
        {
            string sql = "select ZDZB_ID,ZDZB_TITLE,ZDZB_BZ,ZDZB_ZT,ZDZB_DATE from Y_ZDZB";
            SqlCommand comm = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        ZDZB_ID.Add(readData[0].ToString());
                        ZDZB_TITLE.Add(readData[1].ToString());
                        ZDZB_BZ.Add(readData[2].ToString());
                        ZDZB_ZT.Add(readData[3].ToString());
                        ZDZB_DATE.Add(readData[4].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                conn.Close();
            }
            for (int i = 0; i < ZDZB_ID.Count; i++)
            {
                if (ZDZB_ZT[i] == "1")
                    ClassDisplay.Rows.Add(new object[] { "在用", ZDZB_TITLE[i], ZDZB_BZ[i], ZDZB_DATE[i], ZDZB_ID[i] });
                else
                {
                    ClassDisplay.Rows.Add(new object[] { "停用", ZDZB_TITLE[i], ZDZB_BZ[i], ZDZB_DATE[i], ZDZB_ID[i] });
                }
            }

            gridControl1.DataSource = ClassDisplay;
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 部门ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Department.DepartmentStartup frm = new DepartmentStartup();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show(this);
        }

        private void 评审类别ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pingshen1.ProjectStartup frm = new ProjectStartup();
            frm.ClassDisplay.Columns.Add("ZDZB_ZT", typeof(string));
            frm.ClassDisplay.Columns.Add("ZDZB_TITLE", typeof(string));
            frm.ClassDisplay.Columns.Add("ZDZB_BZ", typeof(string));
            frm.ClassDisplay.Columns.Add("ZDZB_DATE", typeof(string));
            frm.ClassDisplay.Columns.Add("ZDZB_ID", typeof(string));
            frm.ClassDisplay = ClassDisplay;
            frm.gridControl2.DataSource = frm.ClassDisplay;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }

 

        private void gridControl1_Click(object sender, EventArgs e)
        {
            gridControl2.DataSource = null;
            PingshenDisplay.Clear();
            SelectClassID.Length = 0;
            SelectClass.Length = 0;
            SelectClassBZ.Length = 0;
            SelectClassZT.Length = 0;
            SelectClassDATE.Length = 0;
            try //单击到状态行的时候，会出错，因此要catch
            {
                SelectClassID.Append(gridView1.GetFocusedRowCellValue("ZDZB_ID").ToString());
                SelectClass.Append(gridView1.GetFocusedRowCellValue("ZDZB_TITLE").ToString());
                SelectClassBZ.Append(gridView1.GetFocusedRowCellValue("ZDZB_BZ").ToString());
                SelectClassZT.Append(gridView1.GetFocusedRowCellValue("ZDZB_ZT").ToString());
                SelectClassDATE.Append(gridView1.GetFocusedRowCellValue("ZDZB_DATE").ToString());
            }
            catch
            {
                PingshenDisplay.Clear();
                return;
            }
            //2.启动多线程
            if (t1_flag == 0)
            {
                Thread t1 = new Thread(WriteDataGrid);//将评鉴结果显示到表格上
                t1.IsBackground = true;
                t1.Start();
            }
        }

        public void WriteDataGrid()
        {
            //将评鉴结果显示到表格上

            t1_flag = 1;//线程在运行标志
            //清空list集合
            ZDBM_MC.Clear();
            ZDBM_DD.Clear();
            PS_DATE.Clear();
            PX_PF.Clear();
            PX_BZ.Clear();
            ZDXB_BH.Clear();
            ZDXB_NAME.Clear();

            string time1 = String.Format("{0}-{1}-{2} 00:00:01.001", dateTimePicker1.Value.Year.ToString(), dateTimePicker1.Value.Month.ToString(), "1");
            string time2 = String.Format("{0}-{1}-{2} 23:59:59.100", dateTimePicker1.Value.Year.ToString(), dateTimePicker1.Value.Month.ToString(), dateTimePicker1.Value.Day.ToString());

            string sql = String.Format("select b.ZDBM_MC,c.ZDBM_DD,b.PS_DATE,a.PX_PF,a.PX_BZ,a.ZDXB_BH,a.ZDXB_NAME from Y_PSMX a inner join  Y_PSZB b on a.PS_ID= b.PS_ID inner join  Y_ZDBM c on c.ZDBM_ID=a.ZDBM_ID where  b.ZDZB_ID='{0}'", SelectClassID)
                        +String.Format("and b.PS_DATE between '{0}' and '{1}'", time1, time2);
            SqlCommand comm = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        ZDBM_MC.Add(readData[0].ToString());
                        ZDBM_DD.Add(readData[1].ToString());
                        PS_DATE.Add(readData[2].ToString());
                        PX_PF.Add(readData[3].ToString());
                        PX_BZ.Add(readData[4].ToString());
                        ZDXB_BH.Add(readData[5].ToString());
                        ZDXB_NAME.Add(readData[6].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                t1_flag = 0;
                return;
            }
            finally
            {
                conn.Close();
            }
            //将数据库内容加入到DataTable中
            
            for (int i = 0; i < ZDBM_MC.Count; i++)
            {
                if (PX_PF[i]=="0")
                {
                  PingshenDisplay.Rows.Add(new object[] { ZDBM_MC[i], ZDBM_DD[i], PS_DATE[i], "不达标", PX_BZ[i], ZDXB_BH[i], ZDXB_NAME[i] });
                }
                else if (PX_PF[i] == "1")
                {
                  PingshenDisplay.Rows.Add(new object[] { ZDBM_MC[i], ZDBM_DD[i], PS_DATE[i], "达标", PX_BZ[i], ZDXB_BH[i], ZDXB_NAME[i] });
                }
                else if (PX_PF[i] == "2")
                {
                    PingshenDisplay.Rows.Add(new object[] { ZDBM_MC[i], ZDBM_DD[i], PS_DATE[i], "部分达标", PX_BZ[i], ZDXB_BH[i], ZDXB_NAME[i] });
                }


            }
            
            this.Invoke(new UpdateUI(delegate () 
            {
                gridControl2.DataSource = PingshenDisplay;
            }));
            t1_flag = 0;//线程结束标志
        }



        private void button1_Click(object sender, EventArgs e)
        {
            search frm = new search();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show(this);
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void gridView2_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;

            int index = gridView2.GetDataRowHandleByGroupRowHandle(e.RowHandle);

            info.GroupText = "部门: " + gridView2.GetRowCellValue(index, "ZDBM_MC").ToString()
                + "              评审日期:" + gridView2.GetRowCellValue(index, "PS_DATE").ToString();
        }
    }
}
