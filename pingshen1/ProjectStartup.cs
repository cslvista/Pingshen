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



namespace pingshen1
{
    public partial class ProjectStartup : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand comm;
        SqlDataReader readData;

        //属于Y_ZDZB（评审主表）
        List<string> ZDZB_ID = new List<string>();
        List<string> ZDZB_TITLE = new List<string>();
        List<string> ZDZB_BZ = new List<string>();
        List<string> ZDZB_ZT = new List<string>();//状态：可用，停用
        List<string> ZDZB_DATE = new List<string>();
        
        //属于Y_ZDXB（评审细表）
        List<string> ZDXB_ID = new List<string>();
        List<string> ZDXB_BH = new List<string>();
        List<string> ZDXB_NAME = new List<string>();
        List<string> ZDXB_SX = new List<string>();
        List<string> ZDXB_BZ = new List<string>();
        List<string> ZDXB_DATE = new List<string>();

        private int t2_flag=0;//用于线程结束的判断
        public DataTable ClassDisplay = new DataTable();
        public DataTable ProjectDisplay = new DataTable();

        public StringBuilder SelectClassID=new StringBuilder();//单击评审类别ID
        public StringBuilder SelectClass= new StringBuilder();//单击评审类别
        public StringBuilder SelectClassBZ = new StringBuilder();//单击评审类别
        public StringBuilder SelectClassZT = new StringBuilder();//单击评审类别
        public StringBuilder SelectClassDATE = new StringBuilder();//单击评审类别
        public int  SelectClassI=0;//单击评审类别的行数


        public StringBuilder SelectBH = new StringBuilder();//单击项目编号
        public StringBuilder SelectNAME = new StringBuilder();//单击项目内容
        public StringBuilder SelectSX = new StringBuilder();//单击项目属性
        public StringBuilder SelectBZ = new StringBuilder();//单击项目备注
        public StringBuilder SelectDate = new StringBuilder();//单击项目日期
        public StringBuilder SelectID = new StringBuilder();//单击项目日期
        public int SelectProjectI=0;//单击项目的行数

        public delegate void UpdateUI();

        int xWidth = SystemInformation.PrimaryMonitorSize.Width;//获取显示器屏幕宽度 
        int yHeight = SystemInformation.PrimaryMonitorSize.Height;//获取显示器屏幕高度 
        public ProjectStartup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //项目新增
            ProjectAdd frm = new ProjectAdd();
            frm.SelectClass = SelectClass.ToString();
            frm.SelectClassID = SelectClassID.ToString();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }

        private void ProjectStartup_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = common.Database.conn;
            comm = conn.CreateCommand();
            ClassAlterT.Enabled = false;
            ProjectAlterT.Enabled = false;
            ProjectAddT.Enabled = false;
            //绑定评审列别列名
            //ClassDisplay.Columns.Add("ZDZB_ZT", typeof(string));
            //ClassDisplay.Columns.Add("ZDZB_TITLE", typeof(string));
            //ClassDisplay.Columns.Add("ZDZB_BZ", typeof(string));
            //ClassDisplay.Columns.Add("ZDZB_DATE", typeof(string));
            //ClassDisplay.Columns.Add("ZDZB_ID", typeof(string));

            //绑定项目列名
            ProjectDisplay.Columns.Add("ZDXB_BH", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_NAME", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_BZ", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_SX", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_DATE", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_ID", typeof(string));


            项目新增ToolStripMenuItem1.Enabled = false;
            项目修改ToolStripMenuItem.Enabled = false;
            项目新增ToolStripMenuItem1.Enabled = false;
            评审类别修改ToolStripMenuItem.Enabled = false;

            //WriteList();
            gridView2.OptionsBehavior.AutoExpandAllGroups = true;//分组展开
            gridView1.OptionsBehavior.AutoExpandAllGroups = true;//分组展开
            gridView1.BestFitColumns();//自动调整列宽
            gridView2.BestFitColumns();//自动调整列宽

            
        }

        public void WriteList()
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
            for (int i=0; i< ZDZB_ID.Count; i++)
            {
                if (ZDZB_ZT[i]=="1")
                ClassDisplay.Rows.Add(new object[] {"在用", ZDZB_TITLE[i], ZDZB_BZ [i], ZDZB_DATE[i], ZDZB_ID[i]});
                else
                {
                ClassDisplay.Rows.Add(new object[] { "停用", ZDZB_TITLE[i], ZDZB_BZ[i], ZDZB_DATE[i], ZDZB_ID[i]});
                }
            }
            
            gridControl2.DataSource = ClassDisplay;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //评审项目修改
            ProjectAlter frm = new ProjectAlter();
            frm.SelectClass = SelectClass;
            frm.SelectBH = SelectBH;
            frm.SelectNAME = SelectNAME;
            frm.SelectSX = SelectSX;
            frm.SelectDate = SelectDate;
            frm.SelectID = SelectID;
            frm.SelectProjectI= gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClassAdd frm = new ClassAdd();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }
        private void gridControl2_Click(object sender, EventArgs e)
        {

            gridControl1.DataSource = null;
            ProjectDisplay.Clear();
            SelectClassID.Length=0;
            SelectClass.Length = 0;
            SelectClassBZ.Length = 0;
            SelectClassZT.Length = 0;
            SelectClassDATE.Length = 0;
            try //单击到状态行的时候，会出错，因此要catch
            {
                SelectClassID.Append(gridView2.GetFocusedRowCellValue("ZDZB_ID").ToString());
                SelectClass.Append(gridView2.GetFocusedRowCellValue("ZDZB_TITLE").ToString());
                SelectClassBZ.Append(gridView2.GetFocusedRowCellValue("ZDZB_BZ").ToString());
                SelectClassZT.Append(gridView2.GetFocusedRowCellValue("ZDZB_ZT").ToString());
                SelectClassDATE.Append(gridView2.GetFocusedRowCellValue("ZDZB_DATE").ToString());
                SelectClassI = gridView2.GetDataSourceRowIndex(gridView2.FocusedRowHandle);
            }
            catch
            {
                ClassAlterT.Enabled = false;
                ProjectAlterT.Enabled = false;
                ProjectAddT.Enabled = false;
                评审类别新增ToolStripMenuItem.Enabled = true;
                评审类别修改ToolStripMenuItem.Enabled = false;
                项目新增ToolStripMenuItem1.Enabled = false;
                项目新增ToolStripMenuItem1.Enabled = false;
                ProjectDisplay.Clear();
                return;
            }

            if (SelectClassZT.ToString()=="停用")
            {

                ClassAlterT.Enabled = true;
                ProjectAlterT.Enabled = false;
                ProjectAddT.Enabled = false;
                项目新增ToolStripMenuItem1.Enabled = false;
                项目修改ToolStripMenuItem.Enabled = false;
                项目新增ToolStripMenuItem1.Enabled = false;
                评审类别修改ToolStripMenuItem.Enabled = true;
            }
            else
            {
                ClassAlterT.Enabled = true;
                ProjectAlterT.Enabled = false;
                ProjectAddT.Enabled = true;
                项目新增ToolStripMenuItem1.Enabled = true;
                项目修改ToolStripMenuItem.Enabled = false;
                评审类别修改ToolStripMenuItem.Enabled = true;
            }
            
            if (t2_flag == 0)
            {
                Thread t2 = new Thread(WriteDataGrid);//将评审细表显示到表格上
                t2.IsBackground = true;
                t2.Start();
            }
            
        }
        public void WriteDataGrid()
        {
            //将评审细表显示到表格上
           
            t2_flag = 1;//线程在运行标志
            //清空list集合
            ZDXB_ID.Clear();
            ZDXB_BH.Clear();
            ZDXB_NAME.Clear();
            ZDXB_BZ.Clear();
            ZDXB_SX.Clear();
            ZDXB_DATE.Clear();
            
            try
            {
                comm.CommandText = String.Format("select ZDXB_ID,ZDXB_BH,ZDXB_NAME,ZDXB_BZ,ZDXB_SX,ZDXB_DATE from Y_ZDXB where ZDZB_ID='{0}'", SelectClassID);
                conn.Open();
                readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {                        
                        ZDXB_ID.Add(readData[0].ToString());
                        ZDXB_BH.Add(readData[1].ToString());
                        ZDXB_NAME.Add(readData[2].ToString());
                        ZDXB_BZ.Add(readData[3].ToString());
                        ZDXB_SX.Add(readData[4].ToString());
                        ZDXB_DATE.Add(readData[5].ToString());
                    }
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
                t2_flag = 0;
                return;
            }

            //将数据库内容加入到DataTable中

            for (int i = 0; i < ZDXB_ID.Count; i++)
            {
                if (ZDXB_SX[i] == "1")
                {
                    ProjectDisplay.Rows.Add(new object[] { ZDXB_BH[i], ZDXB_NAME[i], ZDXB_BZ[i], "对所有部门有效", ZDXB_DATE[i], ZDXB_ID[i] });
                }
                else
                {
                    ProjectDisplay.Rows.Add(new object[] { ZDXB_BH[i], ZDXB_NAME[i], ZDXB_BZ[i], "对指定部门有效", ZDXB_DATE[i], ZDXB_ID[i] });
                }

            }
            this.Invoke(new UpdateUI(delegate () 
            {                
                gridControl1.DataSource = ProjectDisplay;
            } ));
            t2_flag = 0;//线程结束标志
            
            
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            //标题：表格2被单击
            int count = gridView1.RowCount;//判断有没有行
            if (count >= 1)
            {
                SelectBH.Length = 0;
                SelectNAME.Length = 0;
                SelectSX.Length = 0;
                SelectBZ.Length = 0;
                SelectDate.Length = 0;
                SelectID.Length = 0;
                try //单击到分组的时候会出错
                {
                    SelectBH.Append(gridView1.GetFocusedRowCellValue("ZDXB_BH").ToString());
                    SelectNAME.Append(gridView1.GetFocusedRowCellValue("ZDXB_NAME").ToString());
                    SelectBZ.Append(gridView1.GetFocusedRowCellValue("ZDXB_BZ").ToString());
                    SelectSX.Append(gridView1.GetFocusedRowCellValue("ZDXB_SX").ToString());
                    SelectDate.Append(gridView1.GetFocusedRowCellValue("ZDXB_DATE").ToString());
                    SelectID.Append(gridView1.GetFocusedRowCellValue("ZDXB_ID").ToString());
                }
                catch
                {
                    ProjectAlterT.Enabled = false;
                    ProjectAddT.Enabled = true;
                    项目新增ToolStripMenuItem.Enabled = true;
                    项目修改ToolStripMenuItem.Enabled = false;
                    return;
                }

                if (SelectSX.Length>0)
                {
                    ProjectAlterT.Enabled = true;
                    ProjectAddT.Enabled =true;
                    项目新增ToolStripMenuItem.Enabled = true;
                    项目修改ToolStripMenuItem.Enabled = true ;
                }

                }
                else if (SelectSX.Length == 0)
                {
                      ProjectAlterT.Enabled = false;
                      ProjectAddT.Enabled = false;
                      项目新增ToolStripMenuItem.Enabled = false;
                      项目修改ToolStripMenuItem.Enabled = false;
                }

            }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            gridControl1_Click(null, null);
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                gridControl2_Click(null, null);
            }
        }




        private void button5_Click(object sender, EventArgs e)
        {
            //评审类别修改
            ClassAlter frm = new ClassAlter();
            frm.ZDZB_ID = SelectClassID;
            frm.ZDZB_TITLE = SelectClass;
            frm.ZDZB_BZ = SelectClassBZ;
            frm.ZDZB_ZT = SelectClassZT;
            frm.ZDZB_DATE = SelectClassDATE;
            frm.SelectClassI = SelectClassI;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }


        private void 评审类别新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassAddT_Click(null, null);
        }

        private void 评审类别修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassAlterT_Click(null, null);
        }


        private void 项目新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectAddT_Click(null, null);
        }

        private void 项目新增ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProjectAddT_Click(null, null);
        }

        private void 项目修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectAlterT_Click(null, null);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ProjectAddT_Click(object sender, EventArgs e)
        {
            //项目新增
            ProjectAdd frm = new ProjectAdd();
            frm.SelectClass = SelectClass.ToString();
            frm.SelectClassID = SelectClassID.ToString();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }

        private void ClassAddT_Click(object sender, EventArgs e)
        {
            ClassAdd frm = new ClassAdd();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }

        private void ClassAlterT_Click(object sender, EventArgs e)
        {
            //评审类别修改
            ClassAlter frm = new ClassAlter();
            frm.ZDZB_ID = SelectClassID;
            frm.ZDZB_TITLE = SelectClass;
            frm.ZDZB_BZ = SelectClassBZ;
            frm.ZDZB_ZT = SelectClassZT;
            frm.ZDZB_DATE = SelectClassDATE;
            frm.SelectClassI = SelectClassI;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }

        private void ProjectAlterT_Click(object sender, EventArgs e)
        {
            //评审项目修改
            ProjectAlter frm = new ProjectAlter();
            frm.SelectClass = SelectClass;
            frm.SelectBH = SelectBH;
            frm.SelectNAME = SelectNAME;
            frm.SelectSX = SelectSX;
            frm.SelectBZ = SelectBZ;
            frm.SelectDate = SelectDate;
            frm.SelectID = SelectID;
            frm.SelectProjectI = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }
    }
}


