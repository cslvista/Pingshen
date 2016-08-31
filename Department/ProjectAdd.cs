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
using common;

namespace Department
{
    public partial class ProjectAdd : Form
    {
        SqlConnection conn = new SqlConnection();
        public string BMMC ="";
        public string BMDD = "";
        public string BMID = "";
        string rec_combobox;
        List<string> ZDXB_ID_ALL = new List<string>();
        List<string> ZDXB_BH_ALL = new List<string>();
        List<string> ZDXB_NAME_ALL = new List<string>();
        List<string> ZDXB_SX_ALL = new List<string>();
        List<string> ZDXB_BZ_ALL = new List<string>();

        List<string> ZDXB_ID_CZ = new List<string>();

        List<string> ZDZB_ID = new List<string>();
        List<string> ZDZB_TITLE = new List<string>();

        StringBuilder SelectZDXB_ID = new StringBuilder();//细项ID
        StringBuilder SelectZDXB_BH = new StringBuilder();//项目编号
        StringBuilder SelectZDXB_NAME = new StringBuilder();//项目内容      
        StringBuilder SelectZDXB_SX = new StringBuilder();//项目属性
        StringBuilder SelectZDXB_BZ = new StringBuilder();//项目属性
        StringBuilder SelectZDZB_ID = new StringBuilder();//主表ID
        StringBuilder SelectZDZB_TITLE = new StringBuilder();//主表名称
        int SelectZDXB_I;

        StringBuilder sql1 = new StringBuilder();        
        int t1_flag = 0;

        DataTable ProjectDisplay = new DataTable();
        DataTable ProjectADD = new DataTable();

        delegate void UpdateGridControl1();
        public ProjectAdd()
        {
            InitializeComponent();
        }

        private void ProjectAdd_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = common.Database.conn;
            button1.Enabled = false;//全部添加
            button2.Enabled = false;//添加
            button4.Enabled = false;//移除
            button5.Enabled = false;//全部移除
            label3.Text = "部门名称： "+BMMC;
            label4.Text = "部门地点： "+BMDD;
            ProjectDisplay.Columns.Add("ZDXB_BH", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_NAME", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_SX", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_ID", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_BZ", typeof(string));

            ProjectADD.Columns.Add("ZDZB_TITLE", typeof(string));
            ProjectADD.Columns.Add("ZDXB_BH", typeof(string));
            ProjectADD.Columns.Add("ZDXB_NAME", typeof(string));
            ProjectADD.Columns.Add("ZDXB_SX", typeof(string));
            ProjectADD.Columns.Add("ZDZB_ID", typeof(string));
            ProjectADD.Columns.Add("ZDXB_ID", typeof(string));
            ProjectADD.Columns.Add("ZDXB_BZ", typeof(string));


            WriteCombobox();
            gridView1.BestFitColumns();//自动调整列宽
            gridView2.BestFitColumns();//自动调整列宽
            gridView1.OptionsBehavior.AutoExpandAllGroups = false;
            gridView2.OptionsBehavior.AutoExpandAllGroups = true;//分组展开
            

        }
        public void WriteCombobox()
        {
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
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                conn.Close();
            }
            for (int i=0;i< ZDZB_TITLE.Count; i++)
            {
                comboBox1.Items.Add(ZDZB_TITLE[i]);
            }
            //gridControl1.DataSource = ProjectDisplay;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //标题：提交

            //1 读取ProjectADD的数据
            if (ProjectADD.Rows.Count == 0)
            {
                MessageBox.Show("请添加项目！");
                return;
            }
            for (int i=0;i< ProjectADD.Rows.Count;i++)
            {
                sql1.Append(String.Format("insert into Y_ZDZBBMGX (ZDBM_ID,ZDZB_ID,ZDXB_ID) values('{0}','{1}','{2}');", BMID, ProjectADD.Rows[i]["ZDZB_ID"].ToString(), ProjectADD.Rows[i]["ZDXB_ID"].ToString()));
            }
            
            //2 写入数据库
            try
            {
                SqlCommand comm = new SqlCommand(sql1.ToString(), conn);
                conn.Open();
                comm.ExecuteNonQuery();
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
            //3 写入主界面
            DepartmentStartup f1 = (DepartmentStartup)this.Owner;
            f1.gridControl1_Click(null, null);
            MessageBox.Show("添加成功！");
            this.Close();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            SelectZDXB_ID.Length = 0;
            SelectZDXB_BH.Length = 0;
            SelectZDXB_NAME.Length = 0;
            SelectZDXB_SX.Length = 0;
            SelectZDXB_BZ.Length = 0;
            try //单击到状态行的时候，会出错，因此要catch
            {
                SelectZDXB_ID.Append(gridView1.GetFocusedRowCellValue("ZDXB_ID").ToString());
                SelectZDXB_BH.Append(gridView1.GetFocusedRowCellValue("ZDXB_BH").ToString());
                SelectZDXB_NAME.Append(gridView1.GetFocusedRowCellValue("ZDXB_NAME").ToString());
                SelectZDXB_SX.Append(gridView1.GetFocusedRowCellValue("ZDXB_SX").ToString());
                SelectZDXB_BZ.Append(gridView1.GetFocusedRowCellValue("ZDXB_BZ").ToString());
                SelectZDXB_I = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle); ;
                button1.Enabled = true;//全部添加
                button2.Enabled = true;//添加
                button4.Enabled = false;//移除
            }
            catch
            {
                button2.Enabled = false;//添加
                button4.Enabled = false;//移除
                return;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //标题：添加

            //1.写入gridControl2
            button5.Enabled = true; //清空
            button2.Enabled = false;//添加
            ProjectDisplay.Rows.RemoveAt(SelectZDXB_I);
            ProjectADD.Rows.Add(new object[] { SelectZDZB_TITLE, SelectZDXB_BH, SelectZDXB_NAME, SelectZDXB_SX, SelectZDZB_ID, SelectZDXB_ID,SelectZDXB_BZ});
            gridControl2.DataSource = ProjectADD;
        }

        private void gridControl2_Click_1(object sender, EventArgs e)
        {
            SelectZDZB_ID.Length = 0;
            SelectZDZB_TITLE.Length = 0;
            SelectZDXB_ID.Length = 0;
            SelectZDXB_BH.Length = 0;
            SelectZDXB_NAME.Length = 0;
            SelectZDXB_SX.Length = 0;
            SelectZDXB_BZ.Length = 0;
            try //单击到状态行的时候，会出错，因此要catch
            {
                SelectZDZB_ID.Append(gridView2.GetFocusedRowCellValue("ZDZB_ID").ToString());
                SelectZDZB_TITLE.Append(gridView2.GetFocusedRowCellValue("ZDZB_TITLE").ToString());
                SelectZDXB_ID.Append(gridView2.GetFocusedRowCellValue("ZDXB_ID").ToString());
                SelectZDXB_BH.Append(gridView2.GetFocusedRowCellValue("ZDXB_BH").ToString());
                SelectZDXB_NAME.Append(gridView2.GetFocusedRowCellValue("ZDXB_NAME").ToString());
                SelectZDXB_BZ.Append(gridView2.GetFocusedRowCellValue("ZDXB_BZ").ToString());
                SelectZDXB_SX.Append(gridView2.GetFocusedRowCellValue("ZDXB_SX").ToString());
                SelectZDXB_I=gridView2.GetDataSourceRowIndex(gridView2.FocusedRowHandle);
                button2.Enabled = false;//添加
                button4.Enabled = true;//移除
            }
            catch
            {
                button2.Enabled = false;//添加
                button4.Enabled = false;//移除
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProjectADD.Rows.RemoveAt(SelectZDXB_I);
            ProjectDisplay.Rows.Add(new object[] { SelectZDXB_BH, SelectZDXB_NAME, SelectZDXB_SX, SelectZDXB_ID, SelectZDXB_BZ});
            button4.Enabled = false;//移除

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridControl1.DataSource = null; 
                     
            ProjectDisplay.Clear();
            SelectZDZB_TITLE.Length = 0;
            SelectZDZB_TITLE.Append(comboBox1.Text);
            //1.查找评审类别的ID
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
            rec_combobox = comboBox1.Text;

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
            ZDXB_ID_ALL.Clear();
            ZDXB_BH_ALL.Clear();
            ZDXB_NAME_ALL.Clear();
            ZDXB_SX_ALL.Clear();
            ZDXB_BZ_ALL.Clear();
            ZDXB_ID_CZ.Clear();


            //查找该细表中所有的项目
            try
            {
                string sql = String.Format(
            "select ZDXB_ID,ZDXB_BH,ZDXB_NAME,ZDXB_SX,ZDXB_BZ from Y_ZDXB where ZDXB_SX='0' and ZDZB_ID='{0}'", SelectZDZB_ID);
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        ZDXB_ID_ALL.Add(readData[0].ToString());
                        ZDXB_BH_ALL.Add(readData[1].ToString());
                        ZDXB_NAME_ALL.Add(readData[2].ToString());
                        ZDXB_SX_ALL.Add(readData[3].ToString());
                        ZDXB_BZ_ALL.Add(readData[4].ToString());

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
            //查找该部门中已经添加的项目
            try
            {
                string sql = String.Format(
            "select ZDXB_ID from Y_ZDZBBMGX where ZDBM_ID='{0}' and ZDZB_ID='{1}'", BMID,SelectZDZB_ID);
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        ZDXB_ID_CZ.Add(readData[0].ToString());
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
            //从gridcontrol中选择已经加入的数据
            for (int i = 0; i < ProjectADD.Rows.Count; i++)
            {
                if (ProjectADD.Rows[i][0].ToString()== rec_combobox)
                {
                    ZDXB_ID_CZ.Add(ProjectADD.Rows[i][5].ToString());
                }
            }

            //从所有项目中剔除已经添加的项目，并加入到DataTable中 
            int k = 0;

            for (int i = 0; i < ZDXB_ID_ALL.Count; i++)//外层是所有的数据库数据
            {
                if (k!= ZDXB_ID_CZ.Count)
                {
                    for (int j = 0; j < ZDXB_ID_CZ.Count; j++)
                    {
                        if (ZDXB_ID_ALL[i] == ZDXB_ID_CZ[j])
                        {
                            k = k + 1;
                            break;
                        }
                        else if (j == ZDXB_ID_CZ.Count - 1)//如果最后一个都不是，那就添加
                        {
                            ProjectDisplay.Rows.Add(new object[] { ZDXB_BH_ALL[i], ZDXB_NAME_ALL[i], ZDXB_SX_ALL[i], ZDXB_ID_ALL[i], ZDXB_BZ_ALL[i] });
                            
                        }
                    }
                }
                else
                {
                    ProjectDisplay.Rows.Add(new object[] { ZDXB_BH_ALL[i], ZDXB_NAME_ALL[i], ZDXB_SX_ALL[i], ZDXB_ID_ALL[i], ZDXB_BZ_ALL[i] });
                }
                
            }


                this.Invoke(new UpdateGridControl1(delegate ()
            {
                gridControl1.DataSource = ProjectDisplay;
                button1.Enabled = true;//全部添加
                t1_flag = 0;
            }));
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //标题：清空
            ProjectADD.Clear();
            comboBox1_SelectedIndexChanged(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //标题：全部添加
            button5.Enabled = true; //清空
            int Rows = gridView1.RowCount;//过程中会动态变化
            for (int i = 0; i < Rows ; i++)
            {
                DataRow dr = gridView1.GetDataRow(0);
                ProjectADD.Rows.Add(new object[] { SelectZDZB_TITLE, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), SelectZDZB_ID, dr[3].ToString(), dr[4].ToString() });
                ProjectDisplay.Rows.RemoveAt(gridView1.GetDataSourceRowIndex(0));
            }
            gridControl2.DataSource = ProjectADD;
        }
    }
}
