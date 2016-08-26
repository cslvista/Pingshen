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
    public partial class ClassAdd : Form
    {
        SqlConnection conn = new SqlConnection();
        public ClassAdd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //标题：保存
            if (textBox1.Text == "")
            {
                MessageBox.Show("请填写评审类别");
                return;
            }
            string TITLE = textBox1.Text.Trim();//评审项目，输入过滤
            string BZ = textBox2.Text.Trim();//备注，输入过滤
            string ZDZB_ID="";
            //写入数据库
            try
            {
                string sql = String.Format("insert into Y_ZDZB (ZDZB_TITLE,ZDZB_BZ,ZDZB_ZT) values('{0}','{1}','{2}')", TITLE, BZ, "1");
                SqlCommand comm = new SqlCommand(sql, conn);
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
            
            //查询新增项的ID
            try
            {
                string sql1 = String.Format("select ZDZB_ID from Y_ZDZB where ZDZB_TITLE='{0}'",TITLE);
                
                SqlCommand comm = new SqlCommand(sql1, conn);
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    readData.Read();
                    ZDZB_ID = readData[0].ToString();
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
            //显示到主窗体的列表中
            ProjectStartup f1 = (ProjectStartup)this.Owner;
            f1.ClassDisplay.Rows.Add(new object[] {"在用",textBox1.Text, textBox2.Text, DateTime.Now.ToString(), ZDZB_ID});
            f1.gridControl2.DataSource = f1.ClassDisplay;
            MessageBox.Show("添加成功");
            this.Close();

        }

        private void ClassAdd_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = common.Database.conn;
        }
    }
}
