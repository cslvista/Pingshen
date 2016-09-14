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
    public partial class ProjectAlter : Form
    {
        SqlConnection conn = new SqlConnection();
        public StringBuilder SelectClass = new StringBuilder();//单击评审类别
        public StringBuilder SelectBH = new StringBuilder();//单击项目编号
        public StringBuilder SelectNAME = new StringBuilder();//单击项目内容
        public StringBuilder SelectSX = new StringBuilder();//单击项目属性
        public StringBuilder SelectBZ = new StringBuilder();//单击项目备注
        public StringBuilder SelectDate = new StringBuilder();//单击项目日期
        public StringBuilder SelectID = new StringBuilder();//单击项目ID
        StringBuilder sql = new StringBuilder();
        public int SelectProjectI;//获取所在行
        public ProjectAlter()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = SelectBH.ToString();
            textBox2.Text = SelectNAME.ToString();
            textBox3.Text = SelectBZ.ToString();

            if (checkBox1.Checked == true)
            {
                comboBox1.Text = SelectSX.ToString();
                comboBox1.Enabled = false;
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
            }
            else
            {
                comboBox1.Enabled = true;
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //标题：保存

            if (textBox1.Text == ""  && textBox2.Text == "")
            {
                MessageBox.Show("项目编号和项目内容不得为空");
                return;
            }
            string XMBH = textBox1.Text.Trim();//项目编号，输入过滤
            string XMNR= textBox2.Text.Trim();//项目内容，输入过滤
            string XMBZ = textBox3.Text.Trim();//项目备注，输入过滤

            //2.根据状态变更构建SQL语句
            sql.Length = 0;

            if (checkBox1.Checked == false && comboBox1.Text== SelectSX.ToString())//纯更新
            {
                sql.Append( String.Format("update Y_ZDXB set ZDXB_BH='{0}',ZDXB_NAME='{1}',ZDXB_BZ='{2}' where ZDXB_ID='{3}'", XMBH, XMNR, XMBZ,SelectID));
                
            }
            else if (checkBox1.Checked == true)//删除
            {
                if (SelectSX.ToString()== "对所有部门有效")
                {
                    sql.Append(String.Format("delete from Y_ZDXB where ZDXB_ID='{0}'", SelectID));
                }
                else
                {
                    sql.Append(String.Format("delete from Y_ZDXB where ZDXB_ID='{0}';", SelectID)
                        + String.Format("delete from Y_ZDZBBMGX where ZDXB_ID='{0}'", SelectID));
                }
            }
            else if (comboBox1.Text != SelectSX.ToString())//变更状态+更新
            {
                if (SelectSX.ToString() == "对所有部门有效")
                {
                    sql.Append(String.Format("update Y_ZDXB set ZDXB_BH='{0}',ZDXB_NAME='{1}',ZDXB_BZ='{2}',ZDXB_SX='0' where ZDXB_ID='{3}'", XMBH, XMNR, XMBZ,SelectID));                   
                }
                else
                {
                    sql.Append(String.Format("update Y_ZDXB set ZDXB_BH='{0}',ZDXB_NAME='{1}',ZDXB_BZ='{2}',ZDXB_SX='1' where ZDXB_ID='{3}';", XMBH, XMNR, XMBZ,SelectID)
                             + String.Format("delete from Y_ZDZBBMGX where ZDXB_ID='{0}'", SelectID));
                }
            }

                

            //3.保存到数据库            
            try
            {
                SqlCommand comm = new SqlCommand(sql.ToString(), conn);
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
            

          //4.写入到窗口的菜单中
            ProjectStartup f1 = (ProjectStartup)this.Owner;
            if (checkBox1.Checked == false && comboBox1.Text == SelectSX.ToString())//纯更新
            {
                f1.ProjectDisplay.Rows[SelectProjectI]["ZDXB_BH"] = XMBH;
                f1.ProjectDisplay.Rows[SelectProjectI]["ZDXB_NAME"] = XMNR;
                f1.ProjectDisplay.Rows[SelectProjectI]["ZDXB_BZ"] = XMBZ;
            }
            else if (checkBox1.Checked == true)//删除
            {
                f1.ProjectDisplay.Rows.RemoveAt(SelectProjectI);
            }
            else if (comboBox1.Text != SelectSX.ToString())//变更状态+更新
            {
                f1.ProjectDisplay.Rows[SelectProjectI]["ZDXB_BH"] = XMBH;
                f1.ProjectDisplay.Rows[SelectProjectI]["ZDXB_NAME"] = XMNR;
                f1.ProjectDisplay.Rows[SelectProjectI]["ZDXB_BZ"] = XMBZ;
                if (SelectSX.ToString() == "对所有部门有效")
                {
                    f1.ProjectDisplay.Rows[SelectProjectI]["ZDXB_SX"] = "对指定部门有效";
                }
                else
                {
                    f1.ProjectDisplay.Rows[SelectProjectI]["ZDXB_SX"] = "对所有部门有效";
                }               
            }

            f1.gridControl1.DataSource = f1.ProjectDisplay;
            MessageBox.Show("修改成功！");
            this.Close();
        }

        private void ProjectAlter_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = common.Database.conn;
            label5.Text = "评审类别: " +SelectClass.ToString();
            textBox1.Text = SelectBH.ToString();
            textBox2.Text = SelectNAME.ToString();
            textBox3.Text = SelectBZ.ToString();
            if (SelectSX.ToString() == "对所有部门有效")
            {
                comboBox1.Items.Add("对所有部门有效");
                comboBox1.Items.Add("变更为对指定部门有效");
                comboBox1.Text = "对所有部门有效";
            }
            else
            {
                comboBox1.Items.Add("对指定部门有效");
                comboBox1.Items.Add("变更为对所有部门有效");
                comboBox1.Text = "对指定部门有效";
            }
        }


    }
}
