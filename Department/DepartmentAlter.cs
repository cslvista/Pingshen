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
        public StringBuilder SelectBMID = new StringBuilder();//部门ID
        public StringBuilder SelectBMMC = new StringBuilder();//部门名称
        public StringBuilder SelectBMDD = new StringBuilder();//部门地点
        public StringBuilder SelectBMZT = new StringBuilder();//部门状态
        public StringBuilder SelectBMDATE = new StringBuilder();//部门日期
        public int SelectBMI;
        public DepartmentAlter()
        {
            InitializeComponent();
        }

        private void DepartmentAlter_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = common.Database.conn;
            label1.Text = "部门状态： "+ SelectBMZT;
            textBox1.Text = SelectBMMC.ToString();
            textBox2.Text = SelectBMDD.ToString();
            if (SelectBMZT.ToString() == "存在")
            {
                comboBox1.Items.Add("");
                comboBox1.Items.Add("撤销");
                comboBox1.Items.Add("删除");             
            }
            else
            {
                comboBox1.Items.Add("");
                comboBox1.Items.Add("启用");
                comboBox1.Items.Add("删除");
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //标题：保存

            //1.文本框检测
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("请填写部门名称和部门地点");
                return;
            }
            string BMMC = textBox1.Text.Trim();//部门名称，输入过滤
            string BMDD = textBox2.Text.Trim();//部门地点，输入过滤

            //2 构建SQL语句
            string sql = "";
            if (comboBox1.Text == "删除")
            {
                sql = String.Format("delete from Y_ZDBM where ZDBM_ID='{0}'", SelectBMID);
            }
            else if (comboBox1.Text == "撤销")
            {
                sql = String.Format("update Y_ZDBM set ZDBM_MC='{0}',ZDBM_DD='{1}',ZDMB_ZT='0' where ZDBM_ID='{2}'", BMMC, BMDD, SelectBMID);
            }
            else if (comboBox1.Text == "启用")
            {
                sql = String.Format("update Y_ZDBM set ZDBM_MC='{0}',ZDBM_DD='{1}',ZDMB_ZT='1' where ZDBM_ID='{2}'", BMMC, BMDD, SelectBMID);
            }
            else //直接修改
            {
                sql = String.Format("update Y_ZDBM set ZDBM_MC='{0}',ZDBM_DD='{1}'  where ZDBM_ID='{2}'", BMMC, BMDD, SelectBMID);
            }

            //3 执行SQL语句
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
            //3.写入主界面
            DepartmentStartup f1 = (DepartmentStartup)this.Owner;
            if (comboBox1.Text == "删除")
            {
                f1.BMDisplay.Rows.RemoveAt(SelectBMI);
            }
            else if (comboBox1.Text == "撤销")
            {
                f1.BMDisplay.Rows[SelectBMI]["ZDMB_ZT"] = "撤销";
                f1.BMDisplay.Rows[SelectBMI]["ZDBM_MC"] = BMMC;
                f1.BMDisplay.Rows[SelectBMI]["ZDBM_DD"] = BMDD;
            }
            else if (comboBox1.Text == "启用")
            {
                f1.BMDisplay.Rows[SelectBMI]["ZDMB_ZT"] = "存在";
                f1.BMDisplay.Rows[SelectBMI]["ZDBM_MC"] = BMMC;
                f1.BMDisplay.Rows[SelectBMI]["ZDBM_DD"] = BMDD;
            }
            else
            {
                f1.BMDisplay.Rows[SelectBMI]["ZDBM_MC"] = BMMC;
                f1.BMDisplay.Rows[SelectBMI]["ZDBM_DD"] = BMDD;
            }
            f1.gridControl2.DataSource = f1.BMDisplay;
            MessageBox.Show("修改成功！");
            this.Close();
        }
    }
}
