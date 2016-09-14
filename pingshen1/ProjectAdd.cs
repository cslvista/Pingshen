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
        SqlCommand comm;
        SqlDataReader readData;

        public string SelectClass;//评审类别
        public string SelectClassID;//评审类别ID
        StringBuilder  ZDXB_ID=new StringBuilder();//更新项的ID

        public List<string> ZDBM_ID = new List<string>();//部门ID
        public List<string> ZDBM_MC = new List<string>();//部门名称
        public List<string> ZDBM_DD = new List<string>();//部门地点

        public ProjectAdd()
        {
            InitializeComponent();
        }

        private void ProjectAdd_Load(object sender, EventArgs e)
        {
            label5.Text = "评审类别: " + SelectClass.ToString();
            textBox4.ReadOnly = true;
            comboBox1.Text = "对指定部门有效";
            conn.ConnectionString = common.Database.conn;
            comm = conn.CreateCommand();
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
            if ( comboBox1.Text== "对所有部门有效")//判断是否全部部门有效
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
                comm.CommandText = "insert into Y_ZDXB (ZDZB_ID,ZDXB_BH,ZDXB_NAME,ZDXB_SX,ZDXB_BZ) values(@ZDZB_ID,@ZDXB_BH,@ZDXB_NAME,@ZDXB_SX,@ZDXB_BZ)";
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
                conn.Close();
                MessageBox.Show("错误A1:"+ex.Message);
                return;
            }          

            //3.查询新增项的ID
            ZDXB_ID.Length = 0;
            try
            {
                comm.CommandText= String.Format("select ZDXB_ID from Y_ZDXB where ZDXB_BH='{0}' and ZDXB_NAME='{1}'", XMBH,XMNR);
                readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    readData.Read();
                    ZDXB_ID.Append(readData[0].ToString());
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("错误A2:" + ex.Message);
                return;
            }
            finally
            {
                readData.Close();
            }

            //4.写入部门关系表
            if (comboBox1.Text=="对指定部门有效" && textBox4.Text != "")
            {
                StringBuilder sql = new StringBuilder();
                sql.Length = 0;
                for (int i = 0; i < ZDBM_ID.Count; i++)
                {
                    sql.Append(String.Format("insert into Y_ZDZBBMGX (ZDBM_ID,ZDZB_ID,ZDXB_ID) values('{0}','{1}','{2}');", ZDBM_ID[i], SelectClassID, ZDXB_ID.ToString()));
                }
                try
                {
                    comm.CommandText = sql.ToString();
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("错误A3:" + ex.Message);
                    return;
                }
                finally
                {
                    readData.Close();
                }
            }
            


            //4.将数据添加到原窗体的表格中
            ProjectStartup f1 = (ProjectStartup)this.Owner;
            f1.ProjectDisplay.Rows.Add(new object[] {XMBH, XMNR, XMBZ, SX, DateTime.Now.ToString(), ZDXB_ID.ToString()});                   
            f1.gridControl1.DataSource = f1.ProjectDisplay;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            ZDBM_ID.Clear();
            ZDBM_MC.Clear();
            ZDBM_DD.Clear();
            MessageBox.Show("添加成功!");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BMADD frm = new BMADD();
            frm.XMBH = textBox1.Text.Trim();
            frm.XMNR = textBox2.Text.Trim();
            frm.BMAdd.Columns.Add("ZDBM_MC", typeof(string));
            frm.BMAdd.Columns.Add("ZDBM_DD", typeof(string));
            frm.BMAdd.Columns.Add("ZDBM_ID", typeof(string));

            for (int i=0;i< ZDBM_ID.Count; i++)
            {
                frm.BMAdd.Rows.Add(new object[] { ZDBM_MC[i], ZDBM_DD[i], ZDBM_ID[i] });
                frm.ZDBM_MC.Add( ZDBM_MC[i]);
                frm.ZDBM_DD.Add( ZDBM_DD[i]);
                frm.ZDBM_ID.Add( ZDBM_ID[i]);
            }
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "对所有部门有效")
            {
                button2.Enabled = false;
                textBox4.Text = "";
                ZDBM_ID.Clear();
                ZDBM_MC.Clear();
                ZDBM_DD.Clear();
            }
            else
            {
                button2.Enabled = true;
            }
        }
    }
}
