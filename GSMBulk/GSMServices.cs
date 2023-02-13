using GSMBulk;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace GsmBulk
{
    public class GSMServices
    {

        public DataGridView dgv;
        public TextBox textBox;
        private int _IdGSM;
        public int IdGSM
        {
            get { return _IdGSM++; }
            set { _IdGSM = value; }

        }
        public ListGSM list_gsm_event;
        private void SerialPort_DataReceived2(object sender, SerialDataReceivedEventArgs e, GSM Gsm)
        {
            string _data = "";
            string data_utf8 = "";
            SerialPort sp = Gsm.serialPort;
            byte[] buffer = new byte[sp.ReadBufferSize];
            int bytesRead = 0;
            try
            {
                bytesRead = sp.Read(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
            }
            _data += Encoding.ASCII.GetString(buffer, 0, bytesRead);
            data_utf8 = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            if (data_utf8 == "AT\r\r\nOK\r\n")
            {
                Gsm.Status = "OK";
            }
            else
            {
                //Gsm.Respone = data_utf8;
            }

            Tkiet.Comment(data_utf8, textBox);
            #region GuiThanhCong
            if (data_utf8.Contains("+CMGS:"))
            {
                Gsm.Respone = "Gửi SMS thành công";
                return;
            }
            #endregion
            #region CNUM
            if (data_utf8.Contains("CNUM"))
            {
                if (data_utf8.Contains("ERROR") == false)
                {

                    Regex n = new Regex(@"\d{9,11}");
                    Match m = n.Match(data_utf8);
                    var number = m.Groups[0].ToString();
                    if (number.Length >= 9)
                    {
                        var numb = Tkiet.ConvertNumberToInt(number).ToString();
                        if (numb.Length == 9)
                        {
                            Gsm.Number = numb;
                        }
                        Gsm.Status = "NUMBER READY";
                    }

                }

                return;
            }
            #endregion

            #region Nhận SMS
            if (data_utf8.Contains("+CMGL:")|| data_utf8.Contains("+CMGR:"))
            {
                Gsm.SMSReader.FullSMS = _data;
                ReadSMS(Gsm);
                Gsm.SMSReader.FullSMS = string.Empty;
                return;
            }
          
            if (data_utf8.Contains("+CPMS:"))
            {
                Regex r = new Regex(@"(\d+),(\d+),(\d+),(\d+),(\d+),(\d+)");
                Match m = r.Match(data_utf8);
                int storaged_1 = int.Parse(m.Groups[1].ToString());
                int max_storaged_1 = int.Parse(m.Groups[2].ToString());

                int storaged_2 = int.Parse(m.Groups[3].ToString());
                int max_storaged_2 = int.Parse(m.Groups[4].ToString());

                int storaged_3 = int.Parse(m.Groups[5].ToString());
                int max_storaged_3 = int.Parse(m.Groups[6].ToString());
                Gsm.Respone = $"Hộp thư đến : ({storaged_1}/{max_storaged_1}),({storaged_2}/{max_storaged_2}),({storaged_3}/{max_storaged_3})";

                return;
            }


            #endregion

          

            #region USSD
            if (_data.StartsWith("AT+CUSD"))
            {
                Gsm.ReadGSM.isRead = true;
            }
            if (Gsm.ReadGSM.isRead)
            {
                Gsm.ReadGSM.Content += data_utf8;
                if (Gsm.ReadGSM.Content.EndsWith("OK\r\n"))
                {
                    Gsm.ReadGSM.isRead = false;
                    ReadUSSD(Gsm);
                    Gsm.ReadGSM.Content = string.Empty;
                    return;
                }
            }
            #endregion

            #region CPIN
            if (data_utf8.Contains("+CPIN:"))
            {
                if (data_utf8.Contains("NOT"))
                {
                    Gsm.Status = "NOT READY";
                }
                else
                {
                    Gsm.Status = "READY";
                    string ussd = "*101#";
                    COMMAND(Gsm, $"AT+CUSD=1,\"{ussd}\"", 1);

                }
                return;
            }
            #endregion

            #region SMS đến

            if (data_utf8.Contains("+CMTI:"))
            {   //\r\n+CMTI: \"SM\",21\r\n
                Gsm.Respone = "Tin nhắn đến :" + data_utf8;
                if (Gsm.ReadALLSMS)
                {
                    return;
                }
                Regex sms_comming = new Regex("[\"](\\w+)[\"].(\\d+)");
                var match = sms_comming.Match(data_utf8);
                if (match.Success)
                {
                    Gsm.ReadALLSMS = true;
                    Thread t = new Thread(() =>
                    {
                        Thread.Sleep(6000);
                        if (Gsm.ReadALLSMS)
                        {
                            var content = match.Groups[1].ToString();//SM MT 
                            var index_sms = match.Groups[2].ToString();//SM MT 

                            Gsm.serialPort.Write("AT+CPMS=\"" + content + "\"\r\n");
                            Gsm.serialPort.Write("AT+CMGF=1\r\n");
                            Gsm.serialPort.Write("AT+CMGL=\"ALL\"\r\n");
                            Tkiet.Comment("Nhận sms " + Gsm.ComName, textBox);
                            Gsm.List_SMS.Clear();
                            Gsm.ReadALLSMS = false;
                        }
                    
                    });
                    t.Start();

                }
                return;
            }
            #endregion




        }

        private void ReadSMS(GSM Gsm)
        {
            try
            {
                List<string> list_data = Gsm.SMSReader.FullSMS.Split(new string[] { "\r\n" }, StringSplitOptions.None).Where(c => !string.IsNullOrEmpty(c)).ToList();

                for (int i = 0; i < list_data.Count; i++)
                {
                    int val = i;
                    string content_sms = list_data[val];
                    if (content_sms.StartsWith("+CMGL"))
                    {
                        var SplitHeaderSM = content_sms.Replace("\"", string.Empty).Split(',');
                        var index = SplitHeaderSM[0].Replace("+CMGL: ", "");
                        var rec = SplitHeaderSM[1];
                        var from = SplitHeaderSM[2].Replace("\u0011", " ");
                        var ss = SplitHeaderSM[3];

                        string time_date = SplitHeaderSM[4];
                        var time = DateTime.Parse(time_date);
                        bool isnew = false;
                        if (rec.Contains("UNREAD"))
                        {
                            isnew = true;

                        }
                        var sms = new SMS
                        {
                            Index = int.Parse(index),
                            New = isnew,
                            From = from,
                            Time = Tkiet.Date_DateTimeToUnitUnixTimestamp(time),
                        };
                        sms.Message = list_data[val + 1];
                        if (Tkiet.OnlyHexInString(sms.Message) && sms.Message.Length > 10)//Check if string is Hex
                        {
                            sms.Message = Tkiet.FromHex(sms.Message);
                        }

                        Gsm.List_SMS.List.Add(sms);
                        if (sms.New)
                        {
                            Tkiet.AddList($"SMS_{DateTime.UtcNow.AddHours(7).Day}.txt", $"{Gsm.ComName}|{Gsm.Number}|{sms.From}|{sms.Time}|{sms.Message}");

                        }
                    }

                    if (content_sms.StartsWith("+CMGR"))
                    {
                        var SplitHeaderSM = content_sms.Replace("\"", string.Empty).Split(',');
                        var rec = SplitHeaderSM[0].Replace("+CMGR: ", "");

                        var from = SplitHeaderSM[1].Replace("\u0011", " ");
                        var ss = SplitHeaderSM[2];

                        string time_date = SplitHeaderSM[3];
                        var time = DateTime.Parse(time_date);
                        bool isnew = false;
                        if (rec.Contains("UNREAD"))
                        {
                            isnew = true;

                        }
                        var sms = new SMS
                        {
                            Index = 0,
                            New = isnew,
                            From = from,
                            Time = Tkiet.Date_DateTimeToUnitUnixTimestamp(time),
                        };
                        sms.Message = list_data[val + 1];
                        if (Tkiet.OnlyHexInString(sms.Message) && sms.Message.Length > 10)//Check if string is Hex
                        {
                            sms.Message = Tkiet.FromHex(sms.Message);
                        }

                        Gsm.List_SMS.List.Add(sms);

                    }
                }

            }
            catch (Exception er)
            {

            }




        }
        private void ReadUSSD(GSM Gsm)
        {
            List<string> list_data = Gsm.ReadGSM.Content.Split(new string[] { "\r\n" }, StringSplitOptions.None).Where(c => !string.IsNullOrEmpty(c)).ToList();

            for (int i = 0; i < list_data.Count; i++)
            {
                int val = i;
                var content = list_data[val];
                if (content.Contains("+CUSD:"))
                {

                    content = content.Replace("\n", "");
                    if (content.Contains("1.Goi"))
                    {
                        content.Replace("1.Goi", " |1.Goi");
                    }
                    Regex r = new Regex(@"\d{9,11}");
                    Match m = r.Match(content);
                    string number = m.Groups[0].ToString();
                    if (number.Length >= 9)
                    {

                        var numb = Tkiet.ConvertNumberToInt(number).ToString();
                        if (numb.Length == 9)
                        {
                            Gsm.Number = numb;
                        }
                    }

                    Regex r_c = new Regex("\"(.*?)\"");
                    Match m_c = r_c.Match(content);

                    foreach (Match gr in r_c.Matches(content))
                    {
                        var respone = gr.Groups[0].ToString();
                        if (respone.Contains("CUSD") == false)
                        {
                            if (respone.StartsWith("\"") && respone.EndsWith("\""))
                            {
                                Gsm.Respone = respone.Substring(1, respone.Length - 2);
                            }
                            else
                            {
                                Gsm.Respone = respone;
                            }

                        }
                    }


                }
            }

        }

      
        public void GetSMSAndStopTime(int Number)
        {
            try
            {
                var num = Number.ToString();
                var Gsm = list_gsm_event.List.FirstOrDefault(c => c.Number.EndsWith(num));
                if (!Gsm.StopTime)
                {
                    Gsm.StopTime = true;

                }

            }
            catch (Exception)
            {

            }

        }
        #region EVENTCHANGED


        private void Gsm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            GSM Gsm = (GSM)sender;
            switch (e.PropertyName)
            {
                case "Number":
                    Tkiet.ChangeTextDgv(4, Gsm.Number, Gsm.Id, dgv);
                    break;
                case "Status":
                    Tkiet.ChangeTextDgv(3, Gsm.Status, Gsm.Id, dgv);
                    break;
                case "Respone":
                    Tkiet.ChangeTextDgv(8, Gsm.Respone, Gsm.Id, dgv);
                    break;

                default:
                    break;
            }

        }



        #endregion
        public void ActionPort(string actionType)
        {
            for (int i = 0; i < list_gsm_event.List.Count; i++)
            {
                int val = i;
                Task.Run(() =>
                {
                    var Gsm = list_gsm_event.List[val];
                    if (Gsm == null)
                    {
                        return;
                    }
                    if (Gsm.Avaiable)
                    {
                        var serialPort = Gsm.serialPort;
                        if (!serialPort.IsOpen)
                        {
                            try
                            {
                                serialPort.Open();
                            }
                            catch (System.Exception) { }

                        }
                        if (serialPort.IsOpen)
                        {
                            Gsm.Status = "Running...";
                            Gsm.Respone = "";
                            switch (actionType)
                            {
                                case "SMS":
                                    SMS(Gsm);
                                    break;
                                case "USSD":
                                    COMMAND(Gsm, $"AT+CUSD=1,\"{Static.text_Ussd}\"");
                                    break;
                                case "CMRG":
                                    Gsm.serialPort.Write($"AT+CMGR=19\r\n");
                                    break;
                                case "DELSMS":
                                    COMMAND(Gsm, "AT+CMGD=1,4");
                                    break;
                                case "SENDSMS":
                                    //if (Static.text_Number.Length == 11)
                                    //{
                                    //    Static.text_Number = "+" + Static.text_Number;
                                    //}
                                    //
                                    //string SMS = "AT+CMGS=\"+" + Phone + "\"" + (char)(13);
                                    string SendSMS = "AT+CMGS=\"+" + Static.text_Number + "\"" + (char)(13);

                                    Gsm.serialPort.Write(SendSMS);
                                    Gsm.serialPort.Write(Static.text_Sms + (char)(26));
                                    break;
                                case "CALL":
                                    string call = "ATD" + Static.text_CallNum + ";";
                                    COMMAND(Gsm, call);
                                    break;
                                default:
                                    Test(Gsm, actionType);
                                    break;
                            }
                            Gsm.Status = "";
                        }
                    }

                });

            }
        }

        #region METHOD 
        private void COMMAND(GSM Gsm, string Command, int sleep = 0)
        {
            Thread t = new Thread(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(sleep));
                Gsm.serialPort.Write(Command + "\r");
            });
            t.Start();
        }

        private void SMS(GSM Gsm)
        {
            Gsm.List_SMS.Clear();
            //serialPort.DataReceived += (sender, e) => SerialPort_DataReceived_GetSMS(sender, e, Gsm);
            Gsm.serialPort.Write("AT+CPMS=\"MT\"\r\n");
            Gsm.serialPort.Write("AT+CMGF=1\r\n");
            Thread.Sleep(1000);
            Gsm.serialPort.Write("AT+CMGL=\"ALL\"\r\n");
            //Gsm.serialPort.Write("AT+CPMS=\"" + content + "\"\r\n");
            //Gsm.serialPort.Write("AT+CMGF=1\r\n");
            //Gsm.serialPort.Write("AT+CMGL=\"REC UNREAD\"\r\n");
        }



        private void Test(GSM Gsm, string test)
        {
            Gsm.serialPort.Write(test + "\r");
        }
        #endregion
        public void GetPort()
        {
            IdGSM = 0;
            list_gsm_event = new ListGSM(textBox, dgv);
            var ListPortName = SerialPort.GetPortNames();
            var list_sort = ListPortName.ToList();
            var list_sorted = new List<int>();
            for (int i = 0; i < list_sort.Count; i++)
            {
                string item = list_sort[i];
                item = item.Replace("COM", "");
                list_sorted.Add(int.Parse(item));
            }

            list_sorted.Sort();
            for (int i = 0; i < list_sorted.Count; i++)
            {
                GSM Gsm = new GSM
                {
                    Avaiable = false,
                    ComName = "COM" + list_sorted[i],

                };
                Gsm.PropertyChanged += Gsm_PropertyChanged;

                SerialPort serialPort;
                serialPort = new SerialPort("COM" + list_sorted[i], 115200, Parity.None, 8, StopBits.One);
                serialPort.Handshake = Handshake.None;
                // serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.ReadTimeout = 3000;
                serialPort.WriteTimeout = 3000;

                //var serialPort = new SerialPort
                //{
                //    BaudRate = 115200,
                //    DataBits = 8,
                //    Handshake = Handshake.XOnXOff,
                //    StopBits = StopBits.One,//One new SerialPort("COM10", 115200, Parity.None, 8, StopBits.One);
                //    Parity = Parity.None,
                //    ReadTimeout = 20000,
                //    WriteTimeout = 20000,
                //    WriteBufferSize = 1024,
                //    DtrEnable = true,
                //    RtsEnable = true,
                //    PortName = "COM" + list_sorted[i]
                //};

                serialPort.PortName = Gsm.ComName;

                Gsm.serialPort = serialPort;

                if (!serialPort.IsOpen)
                {
                    try
                    {
                        serialPort.Open();
                    }
                    catch (System.Exception) { }

                }
                if (serialPort.IsOpen)
                {
                    serialPort.DataReceived += (sender, e) => SerialPort_DataReceived2(sender, e, Gsm);

                    serialPort.Write("AT\r");
                    Gsm.Id = IdGSM;
                    list_gsm_event.List.Add(Gsm);
                    //serialPort.Write("CPIN?\r");

                    COMMAND(Gsm, "AT+CNUM", 1);

                }

            }
        }


    }
    public class ListGSM
    {
        public TextBox textBox;
        public DataGridView dgv;
        private readonly ObservableCollection<GSM> list;
        public ListGSM(TextBox _textBox, DataGridView _dgv)
        {
            textBox = _textBox;
            dgv = _dgv;
            list = new ObservableCollection<GSM>();
            list.CollectionChanged += ListChanged;

        }

        public ObservableCollection<GSM> List { get { return list; } }

        public void Clear() { list.Clear(); }
        //Hanh
        private void ListChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action.ToString() == "Add")
            {
                var Gsm_new = (GSM)args.NewItems[0];
                Gsm_new.List_SMS = new ListSMS(textBox, dgv, Gsm_new.Id);
                dgv.BeginInvoke((Action)delegate ()
                {
                    dgv.Rows.Add(Gsm_new.Avaiable, Gsm_new.Id, Gsm_new.ComName, Gsm_new.Number, "", "", "", "", "", "Send");

                });
            }

            // list changed
        }
    }
    public class ListSMS
    {
        public TextBox textBox;
        public DataGridView dgv;
        public int val;

        private readonly ObservableCollection<SMS> list;
        public ListSMS(TextBox _textBox, DataGridView _dgv, int _val)
        {
            textBox = _textBox;
            dgv = _dgv;
            val = _val;
            list = new ObservableCollection<SMS>();
            list.CollectionChanged += ListChanged;

        }

        public ObservableCollection<SMS> List { get { return list; } }

        public void Clear() { list.Clear(); }
        //Hanh
        private void ListChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action.ToString() == "Add")
            {
                var SMS_New = (SMS)args.NewItems[0];

                Tkiet.ChangeTextSMSDgv(SMS_New, val, dgv);
            }

            // list changed
        }
    }
    public class SMSReader
    {
        public bool isRead { get; set; }//Đang ghi âm hay không
        public string FullSMS { get; set; }
    }
    public class ReadGSM
    {
        public bool isRead { get; set; }//Đang ghi âm hay không
        public string Content { get; set; }
    }

    public class GSM
    {
        public int Id { get; set; } //Id của đơn hàng đó
        public string ComName { get { return _ComName; } set { SetPropertyField("ComName", ref _ComName, value); } }
        public string Status { get { return _Status; } set { SetPropertyField("Status", ref _Status, value); } }
        public string Number { get { return _Number; } set { SetPropertyField("Number", ref _Number, value); } }
        public bool StopTime { get { return _StopTime; } set { SetPropertyField("StopTime", ref _StopTime, value); } }
        public bool Avaiable { get { return _Avaiable; } set { SetPropertyField("Avaiable", ref _Avaiable, value); } }
        public string Respone { get { return _Respone; } set { SetPropertyField("Respone", ref _Respone, value); } }
        public ListSMS List_SMS { get; set; }
        public bool ReadALLSMS { get; set; }
        public SerialPort serialPort { get; set; }
        public SMSReader SMSReader { get; set; } = new SMSReader();
        public ReadGSM ReadGSM { get; set; } = new ReadGSM();

        public string data { get; set; } = "";


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void SetPropertyField<T>(string propertyName, ref T field, T newValue)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _ComName = "";
        private string _Status = "";
        private string _Number = "";
        private bool _StopTime;
        private bool _Avaiable;
        private string _Respone = "";


    }
    public class SMS
    {
        public bool New { get { return _New; } set { SetPropertyField("New", ref _New, value); } }
        public int Index { get { return _Index; } set { SetPropertyField("Index", ref _Index, value); } }
        public string From { get { return _From; } set { SetPropertyField("From", ref _From, value); } }
        public int Time { get { return _Time; } set { SetPropertyField("Time", ref _Time, value); } }
        public string OTP { get { return _OTP; } set { SetPropertyField("OTP", ref _OTP, value); } }
        public string Message { get { return _Message; } set { SetPropertyField("Message", ref _Message, value); } }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void SetPropertyField<T>(string propertyName, ref T field, T newValue)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool _New;
        private int _Index;
        private string _From;
        private int _Time;
        private string _OTP;
        private string _Message;
    }

    public class BoxSMS
    {
        public int Index { get; set; }
        public string New { get; set; }
        public string Time { get; set; }
        public string From { get; set; }
        public string Otp { get; set; }
        public string Sms { get; set; }

    }
}
