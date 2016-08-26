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


namespace Department
{
    public partial class DepartmentStartup : Form
    {
        SqlConnection conn = new SqlConnection();
        List<string> ZDBM_ID = new List<string>();//部门名称
        List<string> ZDBM_MC = new List<string>();//部门状态
        List<string> ZDBM_DD = new List<string>();//部门地点
        List<string> ZDMB_ZT = new List<string>();//部门状态
        List<string> ZDMB_DATE = new List<string>();//创建日期

        int t1_flag = 0;//线程标记
        int t2_flag = 0;//线程标记
        public StringBuilder SelectBMID = new StringBuilder();//部门ID
        public StringBuilder SelectBMMC = new StringBuilder();//部门名称
        public StringBuilder SelectBMDD = new StringBuilder();//部门地点
        public StringBuilder SelectBMZT = new StringBuilder();//部门状态
        public StringBuilder SelectBMDATE = new StringBuilder();//部门日期
        public int SelectBMI;//在datatable中的行数

        public StringBuilder SelectProjectZB_TITLE = new StringBuilder();//评审类别
        public StringBuilder SelectProjectXB_BH = new StringBuilder();//项目编号
        public StringBuilder SelectProjectXB_NAME = new StringBuilder();//项目内容      
        public StringBuilder SelectProjectXB_SX= new StringBuilder();//项目属性
        public StringBuilder SelectProjectZB_ID = new StringBuilder();//类别ID
        public StringBuilder SelectProjectXB_ID = new StringBuilder();//细项ID
        public int SelectProjectI;//在datatable中的行数

        List<string> ZDZB_ID = new List<string>();
        List<string> ZDZB_TITLE = new List<string>();
        List<string> ZDZB_ID_ALL = new List<string>();
        List<string> ZDZB_TITLE_ALL = new List<string>();

        List<string> ZDXB_ID = new List<string>();
        List<string> ZDXB_BH = new List<string>();
        List<string> ZDXB_NAME = new List<string>();
        List<string> ZDXB_SX = new List<string>();

        int xWidth = SystemInformation.PrimaryMonitorSize.Width;//获取显示器屏幕宽度 
        int yHeight = SystemInformation.PrimaryMonitorSize.Height;//获取显示器屏幕高度

        public DataTable BMDisplay = new DataTable();
        public DataTable ProjectDisplay = new DataTable();

        public delegate void UpdateUI();
        public DepartmentStartup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DepartmentAdd frm = new DepartmentAdd();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProjectAdd frm = new ProjectAdd();
            frm.BMMC = SelectBMMC.ToString();
            frm.BMID = SelectBMID.ToString();
            frm.BMDD = SelectBMDD.ToString();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }


        private void DepartmentStartup_Load(object sender, EventArgs e)
        {            
            conn.ConnectionString = common.Database.conn;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;


            BMDisplay.Columns.Add("ZDMB_ZT", typeof(string));
            BMDisplay.Columns.Add("ZDBM_MC", typeof(string));
            BMDisplay.Columns.Add("ZDBM_DD", typeof(string));
            BMDisplay.Columns.Add("ZDMB_DATE", typeof(string));
            BMDisplay.Columns.Add("ZDBM_ID", typeof(string));

            ProjectDisplay.Columns.Add("ZDXB_BH", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_NAME", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_SX", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_ID", typeof(string));

            WriteList();
            gridView2.OptionsBehavior.AutoExpandAllGroups = true;//分组展开
            gridView1.OptionsBehavior.AutoExpandAllGroups = true;//分组展开
            //gridView1.BestFitColumns();//自动调整列宽
            gridView2.BestFitColumns();//自动调整列宽

        }

        public void WriteList()
        {
            string sql = "select ZDBM_ID,ZDBM_MC,ZDBM_DD,ZDMB_ZT,ZDMB_DATE from Y_ZDBM";
            SqlCommand comm = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        ZDBM_ID.Add (readData[0].ToString());
                        ZDBM_MC.Add(readData[1].ToString());
                        ZDBM_DD.Add (readData[2].ToString());
                        ZDMB_ZT.Add(readData[3].ToString());
                        ZDMB_DATE.Add(readData[4].ToString());
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
            for (int i = 0; i < ZDBM_ID.Count; i++)
            {
                if (ZDMB_ZT[i] == "1")
                    BMDisplay.Rows.Add(new object[] { "存在", ZDBM_MC[i], ZDBM_DD[i], ZDMB_DATE[i], ZDBM_ID[i]});
                else
                {
                    BMDisplay.Rows.Add(new object[] { "撤销", ZDBM_MC[i], ZDBM_DD[i], ZDMB_DATE[i], ZDBM_ID[i]});
                }
            }
            gridControl2.DataSource = BMDisplay;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProjectDelete frm = new ProjectDelete();
            frm.BMMC =SelectBMMC.ToString();
            frm.BMID =SelectBMID.ToString();
            frm.BMDD =SelectBMDD.ToString();
            frm.PSLB =SelectProjectZB_TITLE.ToString();
            frm.XMBH =SelectProjectXB_BH.ToString();
            frm.XMID =SelectProjectXB_ID.ToString();
            frm.XMNR =SelectProjectXB_NAME.ToString();
            frm.XMSX =SelectProjectXB_SX.ToString();
            frm.SelectProjectI = SelectProjectI;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);

        }

        public void gridControl2_Click(object sender, EventArgs e)
        {
            //1.获取表格中数据
            
            gridControl1.DataSource = null;
            ZDZB_ID.Clear();
            ZDZB_TITLE.Clear();
            ZDZB_ID_ALL.Clear();
            ZDZB_TITLE_ALL.Clear();
            SelectBMID.Length = 0;
            SelectBMMC.Length = 0;
            SelectBMDD.Length = 0;
            SelectBMZT.Length = 0;
            SelectBMDATE.Length = 0;
            try //单击到状态行的时候，会出错，因此要catch
            {
                SelectBMID.Append(gridView2.GetFocusedRowCellValue("ZDBM_ID").ToString());
                SelectBMMC.Append(gridView2.GetFocusedRowCellValue("ZDBM_MC").ToString());
                SelectBMDD.Append(gridView2.GetFocusedRowCellValue("ZDBM_DD").ToString());
                SelectBMZT.Append(gridView2.GetFocusedRowCellValue("ZDMB_ZT").ToString());
                SelectBMDATE.Append(gridView2.GetFocusedRowCellValue("ZDMB_DATE").ToString());
                SelectBMI = gridView2.GetDataSourceRowIndex(gridView2.FocusedRowHandle);

            }
            catch
            {
                button4.Enabled = false;//部门修改
                button2.Enabled = false;//项目新增
                button3.Enabled = false;//项目删除
                部门修改ToolStripMenuItem.Enabled = false;
                项目新增ToolStripMenuItem1.Enabled = false;
                ProjectDisplay.Clear();
                comboBox1.Items.Clear();
                return;
            }

            if (SelectBMZT.ToString() == "存在")
            {
                部门新增ToolStripMenuItem.Enabled = true;
                部门修改ToolStripMenuItem.Enabled = true;
                项目新增ToolStripMenuItem1.Enabled = true;
                button4.Enabled = true;//部门修改
                button2.Enabled = true;//项目新增
                button3.Enabled = false;//项目删除

            }
            else
            {
                button4.Enabled = true;//部门修改
                button2.Enabled = false;//项目新增
                button3.Enabled = false;//项目删除

                部门新增ToolStripMenuItem.Enabled = true;
                部门修改ToolStripMenuItem.Enabled = true;
                项目新增ToolStripMenuItem1.Enabled = false;
               
            }

            //2.启动多线程
            if (t2_flag == 0)
            {
                Thread t1 = new Thread(WriteCombobox);//将评审类别显示到combobox上
                t1.IsBackground = true;
                t1.Start();
            }

        }
        public void WriteCombobox()
        {
            t2_flag = 1;           
            
            //1.读取所有的部门列表
            try
            {
            string sql = "select ZDZB_ID,ZDZB_TITLE from Y_ZDZB where ZDZB_ZT='1'";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader readData = comm.ExecuteReader();
            if (readData.HasRows)
            {
               while (readData.Read())
               {
                    ZDZB_ID_ALL.Add(readData[0].ToString());
                    ZDZB_TITLE_ALL.Add(readData[1].ToString());                       
                }
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                t2_flag = 0;
                return;
            }
            finally
            {
                conn.Close();
            }
            //2.判断这个部门是否和这个评审主表关联
            for (int i=0;i< ZDZB_ID_ALL.Count; i++)
            {
                //从评审细表中看是否有全部科室地点的项目
                try
                {
                    string sql = String.Format("select top 1 ZDXB_BH from Y_ZDXB where ZDXB_SX='1' and ZDZB_ID='{0}'", ZDZB_ID_ALL[i]);
                    SqlCommand comm = new SqlCommand(sql, conn);
                    conn.Open();
                    SqlDataReader readData = comm.ExecuteReader();
                    if (readData.HasRows)
                    {
                        ZDZB_ID.Add(ZDZB_ID_ALL[i]);
                        ZDZB_TITLE.Add(ZDZB_TITLE_ALL[i]);
                        conn.Close();
                        continue;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    conn.Close();
                    t2_flag = 0;
                    return;
                }


                //从Y_ZDZBBMGX中看出是否有部门科室地点的项目
                try
                {
                    string sql = String.Format("select top 1 ZDBM_ID from Y_ZDZBBMGX where ZDBM_ID='{0}' and ZDZB_ID='{1}'", SelectBMID, ZDZB_ID_ALL[i]);
                    SqlCommand comm = new SqlCommand(sql, conn);
                    conn.Open();
                    SqlDataReader readData = comm.ExecuteReader();
                    if (readData.HasRows)
                    {
                        ZDZB_ID.Add(ZDZB_ID_ALL[i]);
                        ZDZB_TITLE.Add(ZDZB_TITLE_ALL[i]);
                    }

                }
                catch (Exception ex)
                {                    
                    MessageBox.Show(ex.Message);
                    t2_flag = 0;
                    return;
                }
                finally
                {
                    conn.Close();
                }
            }

            this.Invoke(new UpdateUI(delegate ()
            {
                comboBox1.Items.Clear();
                for (int i=0;i< ZDZB_TITLE.Count; i++)
                {
                    comboBox1.Items.Add(ZDZB_TITLE[i].ToString());

                }
                
            }));
            t2_flag = 0;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            DepartmentAlter frm = new DepartmentAlter();
            frm.SelectBMID = SelectBMID;
            frm.SelectBMMC = SelectBMMC;
            frm.SelectBMDD = SelectBMDD;
            frm.SelectBMZT = SelectBMZT;
            frm.SelectBMDATE = SelectBMDATE;
            frm.SelectBMI = SelectBMI;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }

        public void gridControl1_Click(object sender, EventArgs e)
        {
            //标题：单击项目列表

            //1.获取表格中数据
            button2.Enabled = true;//项目新增可用
            SelectProjectXB_BH.Length = 0;
            SelectProjectXB_NAME.Length = 0;
            SelectProjectXB_SX.Length = 0;
            SelectProjectXB_ID.Length = 0;

            try //单击到状态行的时候，会出错，因此要catch
            {
                SelectProjectXB_BH.Append(gridView1.GetFocusedRowCellValue("ZDXB_BH").ToString());
                SelectProjectXB_NAME.Append(gridView1.GetFocusedRowCellValue("ZDXB_NAME").ToString());
                SelectProjectXB_SX.Append(gridView1.GetFocusedRowCellValue("ZDXB_SX").ToString());
                SelectProjectXB_ID.Append(gridView1.GetFocusedRowCellValue("ZDXB_ID").ToString());
                SelectProjectI= gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);                              

            }
            catch
            {
                button3.Enabled = false;//项目删除不可用
                项目删除ToolStripMenuItem.Enabled = false;
                return;
            }

            if (SelectProjectXB_SX.ToString() =="对所有部门有效")
            {
                项目删除ToolStripMenuItem.Enabled = false;
                button3.Enabled = false;//项目删除
            }
            else
            {
                项目删除ToolStripMenuItem.Enabled = true;
                button3.Enabled = true;//项目删除
            }
        }






       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            gridControl1.DataSource = null;
            ProjectDisplay.Clear();
            SelectProjectZB_TITLE.Length = 0;
            SelectProjectZB_TITLE.Append(comboBox1.Text);
            //1.查找评审类别的ID
            for (int i = 0; i < ZDZB_TITLE.Count; i++)
            {
                if (comboBox1.Text == ZDZB_TITLE[i])
                {
                    SelectProjectZB_ID.Length = 0;
                    SelectProjectZB_ID.Append(ZDZB_ID[i]);
                }
            }

            if (t1_flag == 0)
            {
                Thread t1 = new Thread(WriteDataGrid);//将评审细表显示到表格上
                t1.IsBackground = true;
                t1.Start();
            }
        }

        public void WriteDataGrid()
        {
            //将评审细表显示到表格上

            t1_flag = 1;//线程在运行标志          
                       

            //清空list集合
            ZDXB_ID.Clear();
            ZDXB_BH.Clear();
            ZDXB_NAME.Clear();
            ZDXB_SX.Clear();

            //2 从数据库中读取内容
            //2.1 先读取部分部门有效的数据            
            try
            {
                string sql = String.Format(
               "select a.ZDXB_ID,b.ZDXB_BH,b.ZDXB_NAME,b.ZDXB_SX from Y_ZDZBBMGX a inner join Y_ZDXB b on a.ZDXB_ID = b.ZDXB_ID where a.ZDBM_ID = '{0}' and a.ZDZB_ID='{1}'", SelectBMID, SelectProjectZB_ID);
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        ZDXB_ID.Add(readData[0].ToString());
                        ZDXB_BH.Add(readData[1].ToString());
                        ZDXB_NAME.Add(readData[2].ToString());
                        ZDXB_SX.Add(readData[3].ToString());
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
            //2.2 读取所有部门有效的数据
            try
            {
                string sql = String.Format(
               "select ZDXB_ID,ZDXB_BH,ZDXB_NAME,ZDXB_SX from Y_ZDXB where ZDZB_ID='{0}' and ZDXB_SX='1';", SelectProjectZB_ID);
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        ZDXB_ID.Add(readData[0].ToString());
                        ZDXB_BH.Add(readData[1].ToString());
                        ZDXB_NAME.Add(readData[2].ToString());
                        ZDXB_SX.Add(readData[3].ToString());
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

            for (int i = 0; i < ZDXB_ID.Count; i++)
            {
                if (ZDXB_SX[i] == "1")
                {
                    ProjectDisplay.Rows.Add(new object[] { ZDXB_BH[i], ZDXB_NAME[i], "对所有部门有效", ZDXB_ID[i] });
                }else
                {
                    ProjectDisplay.Rows.Add(new object[] { ZDXB_BH[i], ZDXB_NAME[i], "对部分部门有效", ZDXB_ID[i] });
                }

            }

            this.Invoke(new UpdateUI(delegate ()
            {                             
                gridControl1.DataSource = ProjectDisplay;
            }));
            t1_flag = 0;//线程结束标识
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                gridControl2_Click(null, null);
            }

        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                gridControl1_Click(null, null);
            }
        }

        private void 项目新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.PerformClick();
        }

        private void 项目删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.PerformClick();
        }


        private void 部门新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void 部门修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4.PerformClick();
        }

        private void 项目新增ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button2.PerformClick();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProjectAddMore frm = new ProjectAddMore();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void 部门新增ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void 部门修改ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DepartmentAdd frm = new DepartmentAdd();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }

        private void 部门修改ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DepartmentAlter frm = new DepartmentAlter();
            frm.SelectBMID = SelectBMID;
            frm.SelectBMMC = SelectBMMC;
            frm.SelectBMDD = SelectBMDD;
            frm.SelectBMZT = SelectBMZT;
            frm.SelectBMDATE = SelectBMDATE;
            frm.SelectBMI = SelectBMI;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }

        private void 项目删除ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ProjectDelete frm = new ProjectDelete();
            frm.BMMC = SelectBMMC.ToString();
            frm.BMID = SelectBMID.ToString();
            frm.BMDD = SelectBMDD.ToString();
            frm.PSLB = SelectProjectZB_TITLE.ToString();
            frm.XMBH = SelectProjectXB_BH.ToString();
            frm.XMID = SelectProjectXB_ID.ToString();
            frm.XMNR = SelectProjectXB_NAME.ToString();
            frm.XMSX = SelectProjectXB_SX.ToString();
            frm.SelectProjectI = SelectProjectI;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }

        private void 单部门ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectAdd frm = new ProjectAdd();
            frm.BMMC = SelectBMMC.ToString();
            frm.BMID = SelectBMID.ToString();
            frm.BMDD = SelectBMDD.ToString();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }

        private void 多部门ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectAddMore frm = new ProjectAddMore();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }
    }
}
