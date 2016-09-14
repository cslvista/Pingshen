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
    public partial class ClassAlter : Form
    {
        SqlConnection conn = new SqlConnection();

        public StringBuilder ZDZB_ID=new StringBuilder();
        public StringBuilder ZDZB_TITLE = new StringBuilder();
        public StringBuilder ZDZB_BZ = new StringBuilder();
        public StringBuilder ZDZB_ZT = new StringBuilder();
        public StringBuilder ZDZB_DATE = new StringBuilder();
        public int SelectClassI;
        public ClassAlter()
        {
            InitializeComponent();
        }

      
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }
           
            
        private void ClassAlter_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = common.Database.conn;
            label5.Text ="评审类别状态： "+ ZDZB_ZT;
            textBox2.Text = ZDZB_TITLE.ToString();
            textBox1.Text = ZDZB_BZ.ToString();
            label2.Text = "创建日期：  "+ZDZB_DATE.ToString();
            if (ZDZB_ZT.ToString()=="停用")
            {
                comboBox1.Items.Add("");
                comboBox1.Items.Add("启用");
                comboBox1.Items.Add("删除");
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
            }
            else
            {
                comboBox1.Items.Add("");
                comboBox1.Items.Add("停用");
                comboBox1.Items.Add("删除");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //标题：保存

            //1.判断类别名称是否为空
            if (textBox2.Text == "")
            {
                MessageBox.Show("评审类别不得为空");
                return;
            }
            string TITLE = textBox2.Text.Trim();//评审项目，输入过滤
            string BZ = textBox1.Text.Trim();//备注，输入过滤
           
            //2.根据状态变更构建SQL语句
            string sql="";

            if (comboBox1.Text=="删除")
            {
                sql = String.Format("delete from Y_ZDZB where ZDZB_ID='{0}';", ZDZB_ID)
                    + String.Format("delete from Y_ZDZBBMGX where ZDZB_ID='{0}';", ZDZB_ID)
                     +String.Format("delete from Y_ZDXB where ZDZB_ID='{0}';", ZDZB_ID);
            }
            else if (comboBox1.Text == "启用")
            {
                sql = String.Format("update Y_ZDZB set ZDZB_ZT='1',ZDZB_TITLE='{0}',ZDZB_BZ='{1}' where ZDZB_ID='{2}'", TITLE,BZ ,ZDZB_ID);
            }
            else if (comboBox1.Text == "停用")
            {
                sql = String.Format("update Y_ZDZB set ZDZB_ZT='0',ZDZB_TITLE='{0}',ZDZB_BZ='{1}' where ZDZB_ID='{2}';", TITLE, BZ, ZDZB_ID)
                    + String.Format("delete from Y_ZDZBBMGX where ZDZB_ID='{0}';", ZDZB_ID);
            }
            else //直接修改
            {
                sql = String.Format("update Y_ZDZB set ZDZB_TITLE='{0}',ZDZB_BZ='{1}' where ZDZB_ID='{2}'", TITLE, BZ, ZDZB_ID);
            }

            //3.写入数据库
            //3.1 写入Y_ZDZB数据库
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
                return;
            }
           
            //4.写入主窗口列表

            ProjectStartup f1 = (ProjectStartup)this.Owner;
            if (comboBox1.Text == "删除")
            {
                f1.ClassDisplay.Rows.RemoveAt(SelectClassI);
            }
            else if (comboBox1.Text == "启用")
            {
                f1.ClassDisplay.Rows[SelectClassI]["ZDZB_ZT"] = "在用";
                f1.ClassDisplay.Rows[SelectClassI]["ZDZB_TITLE"] =TITLE;
                f1.ClassDisplay.Rows[SelectClassI]["ZDZB_BZ"] = BZ;
            }
            else if (comboBox1.Text == "停用")
            {
                f1.ClassDisplay.Rows[SelectClassI]["ZDZB_ZT"] = "停用";
                f1.ClassDisplay.Rows[SelectClassI]["ZDZB_TITLE"] = TITLE;
                f1.ClassDisplay.Rows[SelectClassI]["ZDZB_BZ"] = BZ;
            }else 
            {
                f1.ClassDisplay.Rows[SelectClassI]["ZDZB_TITLE"] = TITLE;
                f1.ClassDisplay.Rows[SelectClassI]["ZDZB_BZ"] = BZ;
            }
            f1.gridControl2.DataSource = f1.ClassDisplay;
            f1.gridControl1.DataSource = null;
            MessageBox.Show("修改成功！");
            this.Close();

        }
    }
}
