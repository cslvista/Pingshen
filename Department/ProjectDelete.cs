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
    public partial class ProjectDelete : Form
    {
        SqlConnection conn = new SqlConnection();
        public string BMMC = "";//部门名称
        public string BMID = "";//部门ID
        public string BMDD = "";//部门地点
        public string PSLB = "";//评审类别
        public string XMBH = "";//项目编号
        public string XMID = "";//项目ID
        public string XMNR = "";//项目内容
        public string XMSX = "";//项目属性
        public int SelectProjectI;

        //public string  
        public ProjectDelete()
        {
            InitializeComponent();
        }

        private void ProjectDelete_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = common.Database.conn;
            label1.Text = "部门名称： "+BMMC;
            label6.Text = "部门地点： "+BMDD;
            label2.Text = "评审类别： "+PSLB;
            label3.Text = "项目编号： "+XMBH;
            label4.Text = "项目内容： "+XMNR;
             label5.Text = "项目属性： 对部分部门有效"  ;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //1.根据
            string sql = String.Format("delete from Y_ZDZBBMGX where ZDBM_ID='{0}' and ZDXB_ID='{1}'", BMID, XMID);
            try
            {
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
            //2.写入主界面
            DepartmentStartup f1 = (DepartmentStartup)this.Owner;
            f1.ProjectDisplay.Rows.RemoveAt(SelectProjectI);
            f1.gridControl1.DataSource = f1.ProjectDisplay;
            MessageBox.Show("删除成功！");
            this.Close();
        }
    }
}
