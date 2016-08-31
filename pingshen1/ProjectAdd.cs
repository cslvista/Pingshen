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

namespace pingshen1
{
    public partial class ProjectAdd : Form
    {
        SqlConnection conn = new SqlConnection();
        public string SelectClass;//评审类别
        public string SelectClassID;//评审类别ID
        StringBuilder  ZDXB_ID=new StringBuilder();//更新项的ID
        public ProjectAdd()
        {
            InitializeComponent();
        }

        private void ProjectAdd_Load(object sender, EventArgs e)
        {
            label5.Text = "评审类别: " + SelectClass.ToString();
            conn.ConnectionString = common.Database.conn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //标题：保存

            //1.检查输入框
            if (textBox1.Text=="" || textBox2.Text == "")
            {
                MessageBox.Show("项目编号和项目内容不能为空！");
                return;
            }
            string XMBH = textBox1.Text.Trim();
            string XMNR = textBox2.Text.Trim();
            string XMBZ = textBox3.Text.Trim();
            string SX_ZT;
            string SX;
            if (checkBox2.Checked == true)//判断是否全部科室有效
            {
                SX_ZT = "1";
                SX = "对所有部门有效";
            }else
            {
                SX_ZT = "0";
                SX = "对指定部门有效";
            }

            //2.写入评审细表
            try
            {
                string sql = "insert into Y_ZDXB (ZDZB_ID,ZDXB_BH,ZDXB_NAME,ZDXB_SX,ZDXB_BZ) values(@ZDZB_ID,@ZDXB_BH,@ZDXB_NAME,@ZDXB_SX,@ZDXB_BZ)";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.Add("@ZDZB_ID", SqlDbType.NChar);
                comm.Parameters.Add("@ZDXB_BH", SqlDbType.NChar);
                comm.Parameters.Add("@ZDXB_NAME", SqlDbType.NChar);
                comm.Parameters.Add("@ZDXB_SX", SqlDbType.NChar);
                comm.Parameters.Add("@ZDXB_BZ", SqlDbType.NChar);
                comm.Parameters["@ZDZB_ID"].Value = SelectClassID;
                comm.Parameters["@ZDXB_BH"].Value = XMBH;
                comm.Parameters["@ZDXB_NAME"].Value = XMNR;
                comm.Parameters["@ZDXB_SX"].Value = SX_ZT;
                comm.Parameters["@ZDXB_BZ"].Value = XMBZ;
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                conn.Close();
            }

            //3.查询新增项的ID
            ZDXB_ID.Length = 0;
            try
            {
                string sql = String.Format("select ZDXB_ID from Y_ZDXB where ZDXB_BH='{0}' and ZDXB_NAME='{1}'", XMBH,XMNR);
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    readData.Read();
                    ZDXB_ID.Append(readData[0].ToString());
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


            //4.将数据添加到原窗体的表格中
            ProjectStartup f1 = (ProjectStartup)this.Owner;
            f1.ProjectDisplay.Rows.Add(new object[] {XMBH, XMNR, XMBZ, SX, DateTime.Now.ToString(), ZDXB_ID.ToString()});                   
            f1.gridControl1.DataSource = f1.ProjectDisplay;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            checkBox2.Checked = false;
            MessageBox.Show("添加成功!");
            
        }
    }
}
