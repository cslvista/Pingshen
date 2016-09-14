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
    public partial class BMADD : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand comm;
        SqlDataReader readData;

        public List<string> ZDBM_ID_ALL = new List<string>();//部门ID
        public List<string> ZDBM_MC_ALL = new List<string>();//部门名称
        public List<string> ZDBM_DD_ALL = new List<string>();//部门地点

        public List<string> ZDBM_ID = new List<string>();//添加的部门ID
        public List<string> ZDBM_MC = new List<string>();//添加的部门名称
        public List<string> ZDBM_DD = new List<string>();//添加的部门地点

        StringBuilder SelectZDBM_ID1 = new StringBuilder();//单击GridControl1选中的部门ID
        StringBuilder SelectZDBM_MC1 = new StringBuilder();//单击GridControl1选中的部门名称   
        StringBuilder SelectZDBM_DD1 = new StringBuilder();//单击GridControl1选中的部门地点

        StringBuilder SelectZDBM_ID2 = new StringBuilder();//单击GridControl2选中的部门ID
        StringBuilder SelectZDBM_MC2 = new StringBuilder();//单击GridControl2选中的部门名称   
        StringBuilder SelectZDBM_DD2 = new StringBuilder();//单击GridControl2选中的部门地点

        int SelectGridControl1_I;//单击GridControl1选中的行所对应的DataTable中的行
        int SelectGridControl2_I;//单击GridControl2选中的行所对应的DataTable中的行

        DataTable BMDisplay = new DataTable();//部门列表
        public DataTable BMAdd = new DataTable();//添加的部门列表

        public string XMBH;//项目编号
        public string XMNR;//项目内容

        delegate void UpdateUI();//用于多线程更新主界面UI
        public BMADD()
        {
            InitializeComponent();
        }

        private void BMAdd_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = common.Database.conn;
            comm = conn.CreateCommand();

            BMDisplay.Columns.Add("ZDBM_MC", typeof(string));
            BMDisplay.Columns.Add("ZDBM_DD", typeof(string));
            BMDisplay.Columns.Add("ZDBM_ID", typeof(string));

            gridControl2.DataSource = BMAdd;

            label1.Text = "项目编号： "+XMBH;
            label2.Text = "项目内容： "+XMNR;
            button3.Enabled = false;
            button4.Enabled = false;

            Thread t1 = new Thread(WriteGridControl1);//将部门列表显示到表格1上
            t1.IsBackground = true;
            t1.Start();
        }

        public void WriteGridControl1()
        {
            //标题：将部门信息显示到GridControl2上
            ZDBM_ID_ALL.Clear();
            ZDBM_MC_ALL.Clear();
            ZDBM_DD_ALL.Clear();

            try
            {
                comm.CommandText= "select ZDBM_ID,ZDBM_MC,ZDBM_DD from Y_ZDBM where ZDMB_ZT='1'";
                conn.Open();
                readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        ZDBM_ID_ALL.Add(readData[0].ToString());
                        ZDBM_MC_ALL.Add(readData[1].ToString());
                        ZDBM_DD_ALL.Add(readData[2].ToString());
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("A1:" + ex.Message);
                return;
            }
            finally
            {
                readData.Close();
            }

            int k = 0;

            for (int i = 0; i < ZDBM_ID_ALL.Count; i++)
            {
                if (k != ZDBM_ID.Count)
                {
                    for (int j = 0; j < ZDBM_ID.Count;j++)
                    {
                        if (ZDBM_ID_ALL[i] == ZDBM_ID[j])
                        {
                            k = k + 1;
                            break;
                        }
                        else if (j == ZDBM_ID.Count - 1)//如果最后一个都不是，那就添加
                        {
                            BMDisplay.Rows.Add(new object[] { ZDBM_MC_ALL[i], ZDBM_DD_ALL[i], ZDBM_ID_ALL[i] });

                        }
                    }
                }
                else
                {
                    BMDisplay.Rows.Add(new object[] { ZDBM_MC_ALL[i], ZDBM_DD_ALL[i], ZDBM_ID_ALL[i] });
                }

                
            }
            this.Invoke(new UpdateUI(delegate ()
            {
                gridControl1.DataSource = BMDisplay;
            }));


        }

        private void button3_Click(object sender, EventArgs e)
        {
            //标题：添加
            BMDisplay.Rows.RemoveAt(SelectGridControl1_I);
            BMAdd.Rows.Add(new object[] { SelectZDBM_MC1, SelectZDBM_DD1, SelectZDBM_ID1});
            gridControl2.DataSource = BMAdd;
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //标题：移除
            BMAdd.Rows.RemoveAt(SelectGridControl2_I);
            BMDisplay.Rows.Add(new object[] { SelectZDBM_MC2, SelectZDBM_DD2, SelectZDBM_ID2 });
            button4.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //标题：全部添加
            int Rows = gridView1.RowCount;//过程中会动态变化
            for (int i = 0; i < Rows; i++)
            {
                DataRow dr = gridView1.GetDataRow(0);
                BMAdd.Rows.Add(new object[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString() });
                BMDisplay.Rows.RemoveAt(gridView1.GetDataSourceRowIndex(0));
            }
            gridControl2.DataSource = BMAdd;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //标题：全部删除
            BMAdd.Clear();
            BMDisplay.Clear();
            for (int i = 0; i < ZDBM_ID_ALL.Count; i++)
            {
                BMDisplay.Rows.Add(new object[] { ZDBM_MC_ALL[i], ZDBM_DD_ALL[i], ZDBM_ID_ALL[i] });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //标题：提交
            ProjectAdd f1 = (ProjectAdd)this.Owner;
            StringBuilder BM_MC=new StringBuilder();
            f1.ZDBM_ID.Clear();
            f1.ZDBM_MC.Clear();
            f1.ZDBM_DD.Clear();
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow dr = gridView2.GetDataRow(i);
                f1.ZDBM_MC.Add(dr[0].ToString());
                f1.ZDBM_DD.Add(dr[1].ToString());
                f1.ZDBM_ID.Add(dr[2].ToString());
                BM_MC.Append((i+1).ToString()+"."+ dr[0].ToString() + "—"+ dr[1].ToString() + "\r\n");
            }
            f1.textBox4.Text = BM_MC.ToString();
            this.Close();
            
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            SelectZDBM_ID1.Length = 0;
            SelectZDBM_MC1.Length = 0;
            SelectZDBM_DD1.Length = 0;
            try //单击到状态行的时候，会出错，因此要catch
            {
                SelectZDBM_ID1.Append(gridView1.GetFocusedRowCellValue("ZDBM_ID").ToString());
                SelectZDBM_MC1.Append(gridView1.GetFocusedRowCellValue("ZDBM_MC").ToString());
                SelectZDBM_DD1.Append(gridView1.GetFocusedRowCellValue("ZDBM_DD").ToString());
                SelectGridControl1_I = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
                button3.Enabled = true;
            }
            catch
            {
                button3.Enabled = false;
                return;
            }
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
                button4.Enabled = true;
            }
            catch
            {
                button4.Enabled = false;
                return;
            }
        }
    }
}
