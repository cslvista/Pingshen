using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace pingshen
{
    class Database
    {
        public static string  conn = "server=192.168.8.12;database=ERPOA;uid=sa;pwd=hnfy";
        
    }

    class Liebiao
    {        public void BMLB (ListView listView1) //部门列表
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Database.conn;
            String[] BMBC;//部门名称
            String[] BMZT;//部门状态
            String[] BMDD;//部门地点

            string sql = "select ZDBM_MC,ZDMB_ZT,ZDBM_DD from Y_ZDBM";
            SqlCommand comm = new SqlCommand(sql, conn);
            int i = 0;
            try
            {
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        //BMBC[i] = readData[0].ToString();
                        //BMZT[i] = readData[1].ToString();
                        //BMDD[i] = readData[2].ToString();
                        i = i + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

            //for (i = 0; i <= BMBC.Length; i++)
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
    }
}
