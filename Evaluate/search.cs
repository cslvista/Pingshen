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

namespace Evaluate
{
    public partial class search : Form
    {
        SqlConnection conn = new SqlConnection();

        List<string> ZDBM_MC = new List<string>();
        List<string> ZDBM_DD = new List<string>();
        List<string> PS_DATE = new List<string>();
        List<string> PX_PF = new List<string>();
        List<string> PX_BZ = new List<string>();
        List<string> ZDXB_BH = new List<string>();
        List<string> ZDXB_NAME = new List<string>();

        List<string> ZDZB_ID = new List<string>();
        List<string> ZDZB_TITLE = new List<string>();

        StringBuilder sql = new StringBuilder();
        public DataTable PingshenDisplay = new DataTable();
        public search()
        {
            InitializeComponent();
        }

        private void search_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = common.Database.conn;
            dateTimePicker1.Value = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);
            PingshenDisplay.Columns.Add("ZDBM_MC", typeof(string));
            PingshenDisplay.Columns.Add("ZDBM_DD", typeof(string));
            PingshenDisplay.Columns.Add("PS_DATE", typeof(string));
            PingshenDisplay.Columns.Add("PX_PF", typeof(string));
            PingshenDisplay.Columns.Add("PX_BZ", typeof(string));
            PingshenDisplay.Columns.Add("ZDXB_BH", typeof(string));
            PingshenDisplay.Columns.Add("ZDXB_NAME", typeof(string));
            //读取评审类别列表
            WritePSLB();//读取评审类别
            WriteBM();//读取部门列表
        }

        public void WritePSLB()
        {
            //读取评审类别
            try
            {
                string sql = "select ZDZB_TITLE,ZDZB_ID from Y_ZDZB where ZDZB_ZT='1'";
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        comboBox3.Items.Add(readData[0].ToString());
                        ZDZB_TITLE.Add(readData[0].ToString());
                        ZDZB_ID.Add(readData[1].ToString());

                    }
                }

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
        }

        public void WriteBM()
        {
            //读取部门列表
            try
            {
                listBox1.Items.Add("");
                string sql = "select distinct(ZDBM_MC) from Y_ZDBM where ZDMB_ZT='1'";
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        listBox1.Items.Add(readData[0].ToString());
                    }
                }

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
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //1.查找
            Startup f1 = (Startup)this.Owner;
            f1.gridControl2.DataSource = null;
            PingshenDisplay.Clear();
            sql.Length = 0;
            if (comboBox3.Text== "")
            {
                MessageBox.Show("评审类别不得为空！");
                return;
            }
            ZDBM_MC.Clear();
            ZDBM_DD.Clear();
            PS_DATE.Clear();
            PX_PF.Clear();
            PX_BZ.Clear();
            ZDXB_BH.Clear();
            ZDXB_NAME.Clear();
                      

            sql.Append("select b.ZDBM_MC,c.ZDBM_DD,b.PS_DATE,a.PX_PF,a.PX_BZ,a.ZDXB_BH,a.ZDXB_NAME from Y_PSMX a inner join  Y_PSZB b on a.PS_ID= b.PS_ID inner join  Y_ZDBM c on c.ZDBM_ID=a.ZDBM_ID where");

            //评审类别
            for (int i=0;i< ZDZB_TITLE.Count; i++)
            {
                if(ZDZB_TITLE[i]== comboBox3.Text)
                {
                    sql.Append(String.Format(" b.ZDZB_ID='{0}'", ZDZB_ID[i]));
                }
            }
            
            if (listBox1.Text != "")//部门名称               
            {
                sql.Append(String.Format("and c.ZDBM_MC='{0}'", listBox1.Text));
            }


            if (comboBox1.Text != "")//部门地点
            {
                sql.Append(String.Format("and c.ZDBM_DD='{0}'", comboBox1.Text));
            }

            string PF = "";
            if (comboBox2.Text != "")//评审结果
            {
                if (comboBox2.Text == "达标")
                {
                    PF = "1";
                }
                else if (comboBox2.Text == "不达标")
                {
                    PF = "0";
                }
                else if (comboBox2.Text == "部分达标")
                {
                    PF = "2";
                }
                sql.Append(String.Format("and a.PX_PF='{0}'", PF));
            }

            //时间
            string time1 = String.Format("{0}-{1}-{2} 00:00:01.001",dateTimePicker1.Value.Year.ToString(), dateTimePicker1.Value.Month.ToString(), dateTimePicker1.Value.Day.ToString());
            string time2 = String.Format("{0}-{1}-{2} 23:59:59.100", dateTimePicker2.Value.Year.ToString(), dateTimePicker2.Value.Month.ToString(), dateTimePicker2.Value.Day.ToString());
            sql.Append(String.Format("and b.PS_DATE between '{0}' and '{1}'", time1, time2));


            SqlCommand comm = new SqlCommand(sql.ToString(), conn);
            try
            {
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        ZDBM_MC.Add(readData[0].ToString());
                        ZDBM_DD.Add(readData[1].ToString());
                        PS_DATE.Add(readData[2].ToString());
                        PX_PF.Add(readData[3].ToString());
                        PX_BZ.Add(readData[4].ToString());
                        ZDXB_BH.Add(readData[5].ToString());
                        ZDXB_NAME.Add(readData[6].ToString());
                    }
                }

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
            for (int i = 0; i < ZDBM_MC.Count; i++)
            {
                if (PX_PF[i] == "0")
                {
                    PingshenDisplay.Rows.Add(new object[] { ZDBM_MC[i], ZDBM_DD[i], PS_DATE[i], "不达标", PX_BZ[i], ZDXB_BH[i], ZDXB_NAME[i] });
                }
                else if (PX_PF[i] == "1")
                {
                    PingshenDisplay.Rows.Add(new object[] { ZDBM_MC[i], ZDBM_DD[i], PS_DATE[i], "达标", PX_BZ[i], ZDXB_BH[i], ZDXB_NAME[i] });
                }
                else if (PX_PF[i] == "2")
                {
                    PingshenDisplay.Rows.Add(new object[] { ZDBM_MC[i], ZDBM_DD[i], PS_DATE[i], "部分达标", PX_BZ[i], ZDXB_BH[i], ZDXB_NAME[i] });
                }
            }

            //2.写入主界面
            f1.PingshenDisplay = PingshenDisplay;
            f1.gridControl2.DataSource=PingshenDisplay;

    }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("");
            try
            {
                string sql = String.Format("select ZDBM_DD from Y_ZDBM where ZDBM_MC='{0}'", listBox1.Text);
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader readData = comm.ExecuteReader();
                if (readData.HasRows)
                {
                    while (readData.Read())
                    {
                        comboBox1.Items.Add(readData[0].ToString());
                    }
                }

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
        }
    }
}
