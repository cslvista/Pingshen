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
        List<string> BMBC=new List<string>();//部门名称
        List<string> BMZT = new List<string>();//部门状态
        List<string> BMDD = new List<string>();//部门地点

        public DepartmentStartup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void DepartmentStartup_Load(object sender, EventArgs e)
        {
            //从Y_ZDBM（部门表）中读取部门列表
           conn.ConnectionString = "a";
            Control.CheckForIllegalCrossThreadCalls = false;
            Thread t1 = new Thread(WriteList);//将结果显示到listView上
            t1.Start();                    
        }

        public void WriteList()
        {
            string sql = "select ZDBM_MC,ZDMB_ZT,ZDBM_DD from Y_ZDBM";
            SqlCommand comm = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        BMBC.Add (readData[0].ToString());
                        BMZT.Add(readData[1].ToString());
                        BMDD.Add (readData[2].ToString());
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

            // 添加到listview当中
            ListViewGroup CZBM = new ListViewGroup();  //创建存在部门分组 
            CZBM.Header = "存在部门";  //设置组的标题。 
            CZBM.Name = "CZBM";   //设置组的名称。 
            CZBM.HeaderAlignment = HorizontalAlignment.Left;   //设置组标题文本的对齐方式。（默认为Left） 

            ListViewGroup CXBM = new ListViewGroup();  //创建撤销部门分组 
            CXBM.Header = "撤销部门";
            CXBM.Name = "CXBM";
            CXBM.HeaderAlignment = HorizontalAlignment.Left;

            listView1.Groups.Add(CZBM);    //把男生分组添加到listview中  
            listView1.Groups.Add(CXBM);   //把男生分组添加到listview中  
            listView1.ShowGroups = true;  //记得要设置ShowGroups属性为true（默认是false），否则显示不出分组  

            ListViewItem lvCZ = new ListViewItem();
            ListViewItem lvCX = new ListViewItem();

            //for (int i = 0; i <= BMBC.Length; i++)
            //{

            //    if (BMZT[i] == "1") //存在的部门
            //    {
            //        lvCZ.Text = "item" + i;
            //        lvCZ.ForeColor = Color.Blue;  //设置行颜色  
            //        lvCZ.SubItems.Add("第3列,第" + i + "行");

            //    }
            //    else if (BMZT[i] == "0") //不存在的部门
            //    {
            //        lvCX.Text = "item" + i;
            //        lvCX.ForeColor = Color.Red;  //设置行颜色
            //    }
            //    listView1.Items.Add(lvCZ);
            //    listView1.Items.Add(lvCX);
            //}
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            项目删除 frm = new 项目删除();
            frm.label1.Text = "";
            frm.label2.Text = "";
            frm.label3.Text = "";
            frm.label4.Text = "";
            frm.ShowDialog(this);
            
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
