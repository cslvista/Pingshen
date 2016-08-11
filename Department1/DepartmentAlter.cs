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
    public partial class DepartmentAlter : Form
    {
        SqlConnection conn = new SqlConnection();
        public DepartmentAlter()
        {
            InitializeComponent();
        }

        private void DepartmentAlter_Load(object sender, EventArgs e)
        {
            comboBox2.Text = "存在"; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //新增部门按钮
            comboBox2.Text = "存在";
            comboBox1.Text = null;
            checkBox1.Checked = false;
            checkBox1.Visible = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "存在")
            {
                checkBox1.Text = "撤销该部门";
                checkBox1.Visible = true;
            }
            else
            {
                checkBox1.Text = "重新启用";
                checkBox1.Visible = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //部门名称，每次点击时要显示部门地点和部门备注
            textBox2.Text = null;
            textBox2.Text = null;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //保存按钮

            //写入数据库Y_ZDBM（部门表）
            conn.ConnectionString = Database.conn;
            String sql = "update";
            ;

            if (comboBox2.Text=="存在" && checkBox1.Checked==true)
            {
                //撤销存在部门
                //sql = String.Format("delete from Y_ZDBM  where ZDBM_MC='{0}'",);
            }
            else if (comboBox2.Text == "存在" && checkBox1.Checked==false)
            {
                sql = String.Format("");
            }
            else if (comboBox2.Text == "不存在" && checkBox1.Checked == true)
            {
                sql = String.Format("");
            }
            else if (comboBox2.Text == "不存在" && checkBox1.Checked == false)
            {
                sql = String.Format("");
            }
            else if (comboBox2.Text == "不存在" && checkBox1.Visible== false)
            {
                sql = String.Format("");
            }           
            SqlCommand comm = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                comm.ExecuteNonQuery();
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
            //查找评审细表Y_ZDXB中适用于所有科室的项目，
            //并加入Y_ZDZBBMGX（部门与评审项目的关系表）


            //从部门列表中显示或者删除

        }
    }
}
