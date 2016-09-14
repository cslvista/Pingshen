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
    public partial class DepartmentAdd : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand comm;
        SqlDataReader readData;

        public DepartmentAdd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //标题：保存

            //1.检查输入框是否为空
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("请填写部门名称和部门地点");
                return;
            }
            string BMMC= textBox1.Text.Trim();//评审项目，输入过滤
            string BMDD = textBox2.Text.Trim();//备注，输入过滤
            string BMID = "";
            //2.查询是否有相同的项目
           
            try
            {
                comm.CommandText = String.Format("select top 1 ZDBM_ID from Y_ZDBM where ZDBM_MC='{0}' and ZDBM_DD='{1}'", BMMC, BMDD);
                conn.Open();
                readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    conn.Close();
                    MessageBox.Show("该部门已经存在，无需重复添加！");                   
                    return;
                }
                
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("错误1:"+ex.Message);                
                return;

            }
            finally
            {
                readData.Close();
            }


            //3.写入数据库
            //3.1 写入部门表
            try
            {
                comm.CommandText = "insert into Y_ZDBM (ZDBM_MC,ZDBM_DD,ZDMB_ZT) values(@ZDBM_MC,@ZDBM_DD,'1')";
                comm.Parameters.Add("@ZDBM_MC", SqlDbType.NChar);
                comm.Parameters.Add("@ZDBM_DD", SqlDbType.NChar);
                comm.Parameters["@ZDBM_MC"].Value = BMMC;
                comm.Parameters["@ZDBM_DD"].Value = BMDD;
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("错误2:" + ex.Message);                
                return;
            }
            
            //3.2 查询新增项的ID
            try
            {
                comm.CommandText = String.Format("select ZDBM_ID from Y_ZDBM where ZDBM_MC='{0}' and ZDBM_DD='{1}'", BMMC, BMDD);
                readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    readData.Read();
                    BMID = readData[0].ToString();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("错误3:" + ex.Message);
                return;
            }
            
            //4.显示到主窗体的列表中
            DepartmentStartup f1 = (DepartmentStartup)this.Owner;
            f1.BMDisplay.Rows.Add(new object[] { "存在", BMMC, BMDD, DateTime.Now.ToString(), BMID });
            f1.gridControl2.DataSource = f1.BMDisplay;
            MessageBox.Show("添加成功");
            this.Close();
        }

        private void DepartmentAdd_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = common.Database.conn;
            comm = conn.CreateCommand();
        }
    }
}
