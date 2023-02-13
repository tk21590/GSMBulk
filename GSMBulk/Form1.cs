using GsmBulk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSMBulk
{
    public partial class GSMBULK : Form
    {
        public GSMBULK()
        {
            InitializeComponent();
        }
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer
        {
            Interval = 1000
        };
        public GSMServices _gsmServices = new GSMServices();
        private void Form1_Load(object sender, EventArgs e)
        {
            _gsmServices.dgv = dataGridView1;
            _gsmServices.textBox = textBox1;
            textBox1.ScrollBars = ScrollBars.Both;
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            timer.Tick += Timer_Tick; 
            timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();
            if (_gsmServices.list_gsm_event != null)
            {
                _gsmServices.list_gsm_event.List.Clear();

                _gsmServices.list_gsm_event.Clear();
            }

            _gsmServices.GetPort();
        }

        private void btn_SMS_Click(object sender, EventArgs e)
        {
            _gsmServices.ActionPort("SMS");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _gsmServices.ActionPort("DELSMS");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Static.text_Number = Tkiet.ConvertPhone(Static.text_Number, "84");

            if (Static.text_Sms.Length < 5)
            {
                MessageBox.Show("Tin nhắn quá ngắn !");
                return;
            }
            _gsmServices.ActionPort("SENDSMS");

        }

        private void txt_Number_TextChanged(object sender, EventArgs e)
        {
            Static.text_Number = txt_Number.Text;

        }

        private void groupBox1_TextChanged(object sender, EventArgs e)
        {
            Static.text_Sms = txt_SMS.Text;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            _gsmServices.ActionPort("CMRG");
        }
        private void btn_Ussd_Click(object sender, EventArgs e)
        {
            _gsmServices.ActionPort("USSD");

        }

        private void txt_Ussd_TextChanged(object sender, EventArgs e)
        {
            Static.text_Ussd = txt_Ussd.Text;

        }
        bool checkboxCellAll = false;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            var val = e.RowIndex;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {

            }

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
               e.RowIndex >= 0)
            {
                var check = senderGrid[e.ColumnIndex, e.RowIndex] as DataGridViewCheckBoxCell;
                bool isChecked = (bool)check.EditedFormattedValue;
                if (val < _gsmServices.list_gsm_event.List.Count)
                {
                    var Gsm = _gsmServices.list_gsm_event.List[val];
                    Gsm.Avaiable = isChecked;

                    //Tkiet.Comment($"GSM :ID>{Gsm.Id}<-Name>{Gsm.ComName}", textBox1, true);
                }
                int count = _gsmServices.list_gsm_event.List.Count(c => c.Avaiable);
                Tkiet.RunLabel($"COM đang kết nối :{count}", lb_COMConnect);
            }

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewColumn &&
               e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                checkboxCellAll = !checkboxCellAll;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = checkboxCellAll;
                }
                foreach (var item in _gsmServices.list_gsm_event.List)
                {
                    if (item != null)
                    {
                        item.Avaiable = checkboxCellAll;

                    }
                }

            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            var val = e.RowIndex;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn &&
            e.RowIndex >= 0 && e.ColumnIndex == 8)
            {
                var Gsm = _gsmServices.list_gsm_event.List[val];
                CallBox(Gsm);

            }
        }
        public void CallBox(GSM Gsm)
        {
            var list_sms = Gsm.List_SMS.List;
            var boxSMs = new List<BoxSMS>();
            foreach (var item in list_sms)
            {
                string newvalue = "";
                if (item.New)
                {
                    newvalue = "UNREAD";
                }
                else
                {
                    newvalue = "READ";
                }
                boxSMs.Add(new BoxSMS
                {
                    Index = item.Index,
                    From = item.From,
                    New = newvalue,
                    Time = Tkiet.Date_UnixTimestampToDateTime(item.Time).ToString("HH:mm:ss dd/MM/yyyy"),
                    Sms = item.Message,
                    Otp = item.OTP
                });
            }
            int height = list_sms.Count() * 25;
            if (height > 600)
            {
                height = 600;
            }
            Form formSMS = new Form
            {
                Height = 100 + height,
                Width = 800

            };

            formSMS.Text = Gsm.ComName + " SMS";
            DataGridView gridSMS = new DataGridView();
            formSMS.Controls.Add(gridSMS);
            gridSMS.Anchor = AnchorStyles.Left;
            gridSMS.Anchor = AnchorStyles.Top;
            gridSMS.Anchor = AnchorStyles.Bottom;
            gridSMS.Anchor = AnchorStyles.Right;
            gridSMS.Dock = DockStyle.Fill;
            gridSMS.DataSource = boxSMs;
            for (int i = 0; i <= 4; i++)
            {
                gridSMS.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            }
            gridSMS.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            formSMS.ShowDialog();
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        contextMenuStrip1.Show(dataGridView1, new Point(e.X, e.Y));//places the menu at the pointer position
                    }
                    break;
            }

        }

        private void Selected3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = true;


                }
                foreach (var item in _gsmServices.list_gsm_event.List)
                {
                    if (item != null)
                    {
                        item.Avaiable = true;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void SelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    var number = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    if (string.IsNullOrEmpty(number))//Không có số
                    {
                        dataGridView1.Rows[i].Cells[0].Value = true;

                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[0].Value = false;

                    }
                }
                foreach (var item in _gsmServices.list_gsm_event.List)
                {
                    if (item != null)
                    {
                        if (string.IsNullOrEmpty(item.Number))
                        {
                            item.Avaiable = true;

                        }
                        else
                        {
                            item.Avaiable = false;

                        }

                    }
                }
            }
            catch (Exception)
            {

            }


        }
        private void Selected2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    var number = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    if (string.IsNullOrEmpty(number))//Không có số
                    {
                        dataGridView1.Rows[i].Cells[0].Value = false;

                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[0].Value = true;

                    }
                }
                foreach (var item in _gsmServices.list_gsm_event.List)
                {
                    if (item != null)
                    {
                        if (string.IsNullOrEmpty(item.Number))
                        {
                            item.Avaiable = false;

                        }
                        else
                        {
                            item.Avaiable = true;

                        }

                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void Selected4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = false;


                }
                foreach (var item in _gsmServices.list_gsm_event.List)
                {
                    if (item != null)
                    {
                        item.Avaiable = false;
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        private void txt_SMS_TextChanged(object sender, EventArgs e)
        {
            Static.text_Sms = txt_SMS.Text;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.TextLength;
            textBox1.ScrollToCaret();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
