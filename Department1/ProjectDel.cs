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

namespace Department
{
    public partial class ProjectDel : Form
    {
        public string ID;//项目编号
        public string lebie;//项目类别
        SqlConnection conn = new SqlConnection();
        public ProjectDel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //在部门查看与修改的数据表中删除这一行
            string sql = "delete from";
            SqlCommand comm = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                comm.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            //在数据库中删除这一行
        }

        private void 项目删除_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = Database.conn;
        }
    }
}
