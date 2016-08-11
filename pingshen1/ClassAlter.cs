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

        public List<string> ZDZB_ID = new List<string>();
        public List<string> ZDZB_TITLE = new List<string>();
        public List<string> ZDZB_BZ = new List<string>();
        public List<string> ZDZB_ZT = new List<string>();
        public List<string> ZDZB_DATE = new List<string>();
        public ClassAlter()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ZT;//用来判断是启用还是停用
            if (comboBox2.Text == "在用")
            {
                checkBox1.Text = "停用";
                button2.Enabled = Enabled;//新增按钮可用
                button3.Enabled = Enabled;//修改名称按钮可用
                ZT = 1;
            }
            else   //停用
            {
                checkBox1.Text = "重新启用";
                button2.Enabled = false;//新增按钮不可用
                button3.Enabled = false;//修改名称按钮不可用
                ZT = 0;
            }
            //将数据添加到列表中 
            comboBox1.Items.Clear();
            for (int i = 0; i<ZDZB_ID.Count; i++)
           {
               if (ZT == 1 && ZDZB_ZT[i] == "1" ) //在用的
               {
                   comboBox1.Items.Add(ZDZB_TITLE[i]);
                }                    
               else if (ZT == 0 && ZDZB_ZT[i] == "0") //停用的
                {
                    comboBox1.Items.Add(ZDZB_TITLE[i]);
                }
           }
            
        }
           
            

        private void ClassAlter_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = Database.conn;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox1.Visible = false;

        }
    }
}
