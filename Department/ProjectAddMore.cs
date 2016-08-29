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
    public partial class ProjectAddMore : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlConnection conn1 = new SqlConnection();

        //List集合用于接收数据库传递的数据
        List<string> ZDZB_ID = new List<string>();//评审类别ID
        List<string> ZDZB_TITLE = new List<string>();//评审类别名称

        List<string> ZDXB_ID = new List<string>();//项目ID
        List<string> ZDXB_BH = new List<string>();//项目编号
        List<string> ZDXB_NAME = new List<string>();//项目内容
        List<string> ZDXB_BZ = new List<string>();//项目备注

        List<string> ZDBM_ID = new List<string>();//部门名称
        List<string> ZDBM_MC = new List<string>();//部门状态
        List<string> ZDBM_DD = new List<string>();//部门地点

        StringBuilder SelectZDZB_ID = new StringBuilder();//combobox1选择的评审类别ID
        StringBuilder SelectZDZB_TITLE = new StringBuilder();//combobox1选择的评审类别名称

        StringBuilder SelectZDXB_ID = new StringBuilder();//单击GridControl1选中的项目ID
        StringBuilder SelectZDXB_BH = new StringBuilder();//单击GridControl1选中的项目编号
        StringBuilder SelectZDXB_NAME = new StringBuilder();//单击GridControl1选中的项目内容 
        StringBuilder SelectZDXB_BZ = new StringBuilder();//单击GridControl1选中的项目备注

        StringBuilder SelectZDBM_ID2 = new StringBuilder();//单击GridControl2选中的部门ID
        StringBuilder SelectZDBM_MC2 = new StringBuilder();//单击GridControl2选中的部门名称   
        StringBuilder SelectZDBM_DD2 = new StringBuilder();//单击GridControl2选中的部门地点

        StringBuilder SelectZDBM_ID3 = new StringBuilder();//单击GridControl2选中的部门ID
        StringBuilder SelectZDBM_MC3 = new StringBuilder();//单击GridControl2选中的部门名称   
        StringBuilder SelectZDBM_DD3 = new StringBuilder();//单击GridControl2选中的部门地点

        int SelectGridControl1_I;//单击GridControl1选中的行所对应的DataTable中的行
        int SelectGridControl2_I;//单击GridControl2选中的行所对应的DataTable中的行
        int SelectGridControl3_I;//单击GridControl3选中的行所对应的DataTable中的行

        string rec_combobox1;//记录combobox1的数据，用于多线程
        int t1_flag = 0;//用于combobox1变更时多线程状态的判断

      
        DataTable ProjectDisplay = new DataTable();//评审项目
        DataTable BMDisplay = new DataTable();//部门列表
        DataTable BMAdd = new DataTable();//添加的部门列表

        delegate void UpdateUI();//用于多线程更新主界面UI

        public ProjectAddMore()
        {
            InitializeComponent();
        }


        private void ProjectAddMore_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = common.Database.conn;
            conn1.ConnectionString = common.Database.conn;

            ProjectDisplay.Columns.Add("ZDXB_BH", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_NAME", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_ID", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_BZ", typeof(string));

            BMDisplay.Columns.Add("ZDBM_MC", typeof(string));
            BMDisplay.Columns.Add("ZDBM_DD", typeof(string));
            BMDisplay.Columns.Add("ZDBM_ID", typeof(string));
            
            BMAdd.Columns.Add("ZDBM_MC", typeof(string));
            BMAdd.Columns.Add("ZDBM_DD", typeof(string));
            BMAdd.Columns.Add("ZDBM_ID", typeof(string));

            gridControl3.DataSource = BMAdd;

            Thread t1 = new Thread(WriteCombobox);//将评审类别显示到combobox上
            t1.IsBackground = true;           
            t1.Start();

            Thread t2 = new Thread(WriteGridControl2);//将部门信息显示到GridControl2上          
            t2.IsBackground = true;
            t2.Start();


        }
        public void WriteCombobox()
        {
            //标题：将评审类别显示到combobox上
            string sql = "select ZDZB_ID,ZDZB_TITLE from Y_ZDZB where ZDZB_ZT='1'";
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
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("WriteCombobox.ERROR:"+ex.Message);
                return;
            }
            finally
            {
                conn.Close();
            }
            this.Invoke(new UpdateUI(delegate ()
            {
                for (int i = 0; i < ZDZB_TITLE.Count; i++)
                {
                    comboBox1.Items.Add(ZDZB_TITLE[i]);
                }
            }));

            
            
        }
        public void WriteGridControl2()
        {
            //标题：将部门信息显示到GridControl2上
            ZDBM_ID.Clear();
            ZDBM_MC.Clear();
            ZDBM_DD.Clear();
            string sql = "select ZDBM_ID,ZDBM_MC,ZDBM_DD from Y_ZDBM where ZDMB_ZT='1'";
            SqlCommand comm = new SqlCommand(sql, conn1);
            try
            {
                conn1.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        ZDBM_ID.Add(readData[0].ToString());
                        ZDBM_MC.Add(readData[1].ToString());
                        ZDBM_DD.Add(readData[2].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("WriteGridControl2.ERROR:" + ex.Message);
                return;
            }
            finally
            {
                conn1.Close();
            }
            for (int i = 0; i < ZDBM_ID.Count; i++)
            {               
               BMDisplay.Rows.Add(new object[] {ZDBM_MC[i], ZDBM_DD[i],  ZDBM_ID[i] });
            }
            this.Invoke(new UpdateUI(delegate ()
            {
                gridControl2.DataSource = BMDisplay;
            }));


        }

        private void button3_Click(object sender, EventArgs e)
        {
            //标题：添加
            //1.判断评审项目是否被选中
            if (gridView1.RowCount!=1)
            {
                MessageBox.Show("请先选择评审项目!");
                return;
            }

            //2.在数据库中查找是否已经添加
            try
            {
                string sql = String.Format("select top 1 ZDBM_ID from Y_ZDZBBMGX where ZDBM_ID='{0}' and ZDXB_ID='{1}'", SelectZDBM_ID2, ProjectDisplay.Rows[0][2].ToString());
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    MessageBox.Show("该项目已经存在，无需重复添加！");
                    return;
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

            //3.删除BMDisplay对应的行，并在BMAdd中增加行
            BMDisplay.Rows.RemoveAt(SelectGridControl2_I);
            BMAdd.Rows.Add(new object[] { SelectZDBM_MC2, SelectZDBM_DD2,SelectZDBM_ID2 });            
            button3.Enabled = false;
            button4.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //1.初始化
            gridControl1.DataSource = null;
            ProjectDisplay.Clear();

            //1.查找评审类别的ID
            SelectZDZB_TITLE.Length = 0;
            SelectZDZB_TITLE.Append(comboBox1.Text);            
            for (int i = 0; i < ZDZB_TITLE.Count; i++)
            {
                if (comboBox1.Text == ZDZB_TITLE[i])
                {
                    SelectZDZB_ID.Length = 0;
                    SelectZDZB_ID.Append(ZDZB_ID[i]);
                    break;
                }
            }
            //2.记录combobox数据
            rec_combobox1 = comboBox1.Text;

            if (t1_flag == 0)
            {
                Thread t1 = new Thread(WriteGridControl1);//将评审细表显示到表格上
                t1.IsBackground = true;
                t1.Start();
            }
        }

        public void WriteGridControl1()
        {
            //标题：将评审细表显示到表格上

            t1_flag = 1;//线程在运行标志
                        
            ZDXB_ID.Clear();
            ZDXB_BH.Clear();
            ZDXB_NAME.Clear();
            ZDXB_BZ.Clear();
            //查找该细表中所有的项目
            try
            {
                string sql = String.Format(
             "select ZDXB_ID,ZDXB_BH,ZDXB_NAME,ZDXB_BZ from Y_ZDXB where ZDXB_SX='0' and ZDZB_ID='{0}'", SelectZDZB_ID);
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
                        ZDXB_BZ.Add(readData[3].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("WriteGridControl1.ERROR"+ex.Message);
                t1_flag = 0;
                return;
            }
            finally
            {
                conn.Close();                
            }
            for (int i=0;i< ZDXB_ID.Count;i++)
            {
                ProjectDisplay.Rows.Add(new object[] { ZDXB_BH[i], ZDXB_NAME[i],  ZDXB_ID[i], ZDXB_BZ[i] });
            }
            this.Invoke(new UpdateUI(delegate ()
            {
                gridControl1.DataSource = ProjectDisplay;
            }));
            t1_flag = 0;//线程结束
        }

        private void 选中ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ProjectDisplay.Clear();
            ProjectDisplay.Rows.Add(new object[] { SelectZDXB_BH, SelectZDXB_NAME, SelectZDXB_ID,SelectZDXB_BZ});
            
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            SelectZDXB_ID.Length = 0;
            SelectZDXB_BH.Length = 0;
            SelectZDXB_NAME.Length = 0;
            SelectZDXB_BZ.Length = 0;
            try //单击到状态行的时候，会出错，因此要catch
            {
                SelectZDXB_ID.Append(gridView1.GetFocusedRowCellValue("ZDXB_ID").ToString());
                SelectZDXB_BH.Append(gridView1.GetFocusedRowCellValue("ZDXB_BH").ToString());
                SelectZDXB_NAME.Append(gridView1.GetFocusedRowCellValue("ZDXB_NAME").ToString());
                SelectZDXB_BZ.Append(gridView1.GetFocusedRowCellValue("ZDXB_BZ").ToString());
                SelectGridControl1_I = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle); 
            }
            catch
            {
                return;
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                gridControl1_Click(null, null);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //标题：移除

            //1.删除BMAdd对应的行，并在BMDisplay中增加行
            BMAdd.Rows.RemoveAt(SelectGridControl3_I);
            BMDisplay.Rows.Add(new object[] { SelectZDBM_MC3, SelectZDBM_DD3, SelectZDBM_ID3 });
            button4.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //标题：提交

            if (gridView1.RowCount != 1)
            {
                MessageBox.Show("请选中项目！");
                return;
            }

            if (gridView3.RowCount == 0)
            {
                MessageBox.Show("请添加部门！");
                return;
            }
            StringBuilder sql = new StringBuilder();

            //1.添加到"部门与评审项目的关系表" 
            for (int i = 0; i < BMAdd.Rows.Count; i++)
            {
                //1.1先判断是否已经添加
                try
                {
                    sql.Length = 0;
                    sql.Append( String.Format("select top 1 ZDBM_ID from Y_ZDZBBMGX where ZDBM_ID='{0}' and ZDXB_ID='{1}'", BMAdd.Rows[i][2], SelectZDXB_ID));
                    conn.Open();
                    SqlCommand comm = new SqlCommand(sql.ToString(), conn);
                    SqlDataReader readData = comm.ExecuteReader();
                    if (readData.HasRows)
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("查询错误："+ex.Message);
                    return;
                }
                finally
                {
                    conn.Close();
                }
                //1.2 没有添加时就可以添加
                try
                {
                    sql.Length = 0;
                    sql.Append(String.Format("insert into Y_ZDZBBMGX (ZDBM_ID,ZDZB_ID,ZDXB_ID) values('{0}','{1}','{2}');", BMAdd.Rows[i][2], SelectZDZB_ID, SelectZDXB_ID));

                    SqlCommand comm = new SqlCommand(sql.ToString(), conn);
                    conn.Open();
                    comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("写入错误："+ex.Message);
                    return;
                }
                finally
                {
                    conn.Close();
                }
            }
                      
            //2.清空界面            
            //重新添加项目列表
            ProjectDisplay.Clear();
            for (int i = 0; i < ZDXB_ID.Count; i++)
            {
                ProjectDisplay.Rows.Add(new object[] { ZDXB_BH[i], ZDXB_NAME[i], ZDXB_ID[i] });
            }

            MessageBox.Show("添加成功！");
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            SelectZDBM_ID2.Length = 0;
            SelectZDBM_MC2.Length = 0;
            SelectZDBM_DD2.Length = 0;
            try //单击到状态行的时候，会出错，因此要catch
            {
                SelectZDBM_ID2.Append(gridView2.GetFocusedRowCellValue("ZDBM_ID").ToString());
                SelectZDBM_MC2.Append(gridView2.GetFocusedRowCellValue("ZDBM_MC").ToString());
                SelectZDBM_DD2.Append(gridView2.GetFocusedRowCellValue("ZDBM_DD").ToString());
                SelectGridControl2_I = gridView2.GetDataSourceRowIndex(gridView2.FocusedRowHandle);
                button3.Enabled = true;
            }
            catch
            {
                button3.Enabled = false;
                return;
                
            }
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
            SelectZDBM_ID3.Length = 0;
            SelectZDBM_MC3.Length = 0;
            SelectZDBM_DD3.Length = 0;
            try //单击到状态行的时候，会出错，因此要catch
            {
                SelectZDBM_ID3.Append(gridView3.GetFocusedRowCellValue("ZDBM_ID").ToString());
                SelectZDBM_MC3.Append(gridView3.GetFocusedRowCellValue("ZDBM_MC").ToString());
                SelectZDBM_DD3.Append(gridView3.GetFocusedRowCellValue("ZDBM_DD").ToString());
                SelectGridControl3_I = gridView3.GetDataSourceRowIndex(gridView3.FocusedRowHandle);
                button4.Enabled = true;
            }
            catch
            {
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //标题：全部删除
            BMAdd.Clear();
            BMDisplay.Clear();
            for (int i = 0; i < ZDBM_ID.Count; i++)
            {
                BMDisplay.Rows.Add(new object[] { ZDBM_MC[i], ZDBM_DD[i], ZDBM_ID[i] });
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //标题：全部添加
            int Rows = gridView2.RowCount;//过程中会动态变化
            for (int i = 0; i < Rows; i++)
            {
                DataRow dr = gridView2.GetDataRow(0);
                BMAdd.Rows.Add(new object[] {dr[0].ToString(), dr[1].ToString(), dr[2].ToString()});
                BMDisplay.Rows.RemoveAt(gridView2.GetDataSourceRowIndex(0));
            }
            gridControl3.DataSource = BMAdd;
        }
    }
}
