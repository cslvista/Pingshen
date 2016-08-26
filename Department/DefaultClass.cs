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
using System.Threading;

namespace Department
{
    public partial class DefaultClass : Form
    {
        SqlConnection conn = new SqlConnection();

        List<string> ZDZB_ID = new List<string>();
        List<string> ZDZB_TITLE = new List<string>();

        delegate void UpdateUI();//用于多线程更新主界面UI
        public DefaultClass()
        {
            InitializeComponent();
        }

        private void DefaultClass_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = common.Database.conn;

            Thread t1 = new Thread(WriteCombobox);//将评审类别显示到combobox上
            t1.IsBackground = true;
            t1.Start();
        }

        public void WriteCombobox()
        {
            //标题：将评审类别显示到combobox上
            string sql = "select ZDZB_ID,ZDZB_TITLE from Y_ZDZB where ZDZB_ZT='1'";
            SqlCommand comm = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        ZDZB_ID.Add(readData[0].ToString());
                        ZDZB_TITLE.Add(readData[1].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("WriteCombobox.ERROR:" + ex.Message);
                return;
            }
            finally
            {
                conn.Close();
            }
            this.Invoke(new UpdateUI(delegate ()
            {
                for (int i = 0; i < ZDZB_TITLE.Count; i++)
                {
                    comboBox1.Items.Add(ZDZB_TITLE[i]);
                }
            }));

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
