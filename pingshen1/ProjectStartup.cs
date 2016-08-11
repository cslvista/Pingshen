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
        
        //属于Y_ZDZB（评审主表）
        List<string> ZDZB_ID = new List<string>();
        List<string> ZDZB_TITLE = new List<string>();
        List<string> ZDZB_BZ = new List<string>();
        List<string> ZDZB_ZT = new List<string>();
        List<string> ZDZB_DATE = new List<string>();
        
        //属于Y_ZDXB（评审细表）
        List<string> ZDXB_ID = new List<string>();
        List<string> ZDZB_ID1 = new List<string>();
        List<string> ZDXB_BH = new List<string>();
        List<string> ZDXB_NAME = new List<string>();
        List<string> ZDXB_SX = new List<string>();
        List<string> ZDXB_DATE = new List<string>();

        private int t2_flag=0;//用于线程结束的判断
        DataTable ClassDisplay = new DataTable();
        DataTable ProjectDisplay = new DataTable();
        private string SelectClass;
        public ProjectStartup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ProjectStartup_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = Database.conn;
            
            //绑定评审列别列名
            ClassDisplay.Columns.Add("ZDZB_ZT", typeof(string));
            ClassDisplay.Columns.Add("ZDZB_TITLE", typeof(string));
            ClassDisplay.Columns.Add("ZDZB_BZ", typeof(string));
            ClassDisplay.Columns.Add("ZDZB_DATE", typeof(string));

            //绑定项目列名
            ProjectDisplay.Columns.Add("ZDXB_BH", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_NAME", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_SX", typeof(string));
            ProjectDisplay.Columns.Add("ZDXB_DATE", typeof(string));

            Control.CheckForIllegalCrossThreadCalls = false;
            Thread t1 = new Thread(WriteList);//将结果显示到第一个gridControl上
            t1.Start();

            
           
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
            for (int i=0;i< ZDZB_ID.Count; i++)
            {
                if (ZDZB_ZT[i]=="1")
                ClassDisplay.Rows.Add(new object[] {"在用", ZDZB_TITLE[i], ZDZB_BZ [i], ZDZB_DATE[i] });
                else
                {
                ClassDisplay.Rows.Add(new object[] { "停用", ZDZB_TITLE[i], ZDZB_BZ[i], ZDZB_DATE[i] });
                }
            }
            
            gridControl2.DataSource = ClassDisplay;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProjectAlter frm = new ProjectAlter();
            frm.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClassAlter frm = new ClassAlter();
            frm.ZDZB_ID = ZDZB_ID;
            frm.ZDZB_TITLE = ZDZB_TITLE;
            frm.ZDZB_BZ = ZDZB_BZ;
            frm.ZDZB_ZT = ZDZB_ZT;
            frm.ZDZB_DATE = ZDZB_DATE;
            frm.ShowDialog(this);
        }
        private void gridControl2_Click(object sender, EventArgs e)
        {
            // string colValue = gridView1.GetFocusedRowCellValue(gridView1.FocusedColumn);
            // MessageBox.Show(colValue);
            SelectClass = "DB2BD394-524C-4874-B2DE-BC6AB983455C";
            //if (t2_flag == 0)
            //{
            //    Thread t2 = new Thread(WriteDataGrid);//将评审细表显示到表格上
            //    t2.Start();
            //}
            WriteDataGrid();
        }
        public void WriteDataGrid()
        {
            //将评审细表显示到表格上
            t2_flag = 1;//线程在运行标志
            string sql = String.Format("select ZDXB_ID,ZDZB_ID,ZDXB_BH,ZDXB_NAME,ZDXB_SX,ZDXB_DATE from Y_ZDXB where ZDZB_ID='{0}'",SelectClass);
            SqlCommand comm = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        
                        ZDXB_ID.Add(readData[0].ToString());
                        ZDZB_ID1.Add(readData[1].ToString());
                        ZDXB_BH.Add(readData[2].ToString());
                        ZDXB_NAME.Add(readData[3].ToString());
                        ZDXB_SX.Add(readData[4].ToString());
                        ZDXB_DATE.Add(readData[5].ToString());
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
            for (int i = 0; i <ZDXB_ID.Count; i++)
            {
                ProjectDisplay.Rows.Add(new object[] { ZDXB_BH[i], ZDXB_NAME[i], ZDXB_SX[i], ZDXB_DATE[i] });
            }

            gridControl1.DataSource = ProjectDisplay;
            t2_flag = 0;//线程结束标志
        }

  
    }
}
