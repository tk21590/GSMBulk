
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace GsmBulk
{
    public class Tkiet
    {


    
        public static string GetOTP(int categoryid, string sms)
        {

            string otp = "";
            Regex regex;
            Match match;
            switch (categoryid)
            {
                case 16: //KOK
                    regex = new Regex(@"\d{4}");
                    match = regex.Match(sms);
                    if (match.Success)
                    {
                        otp = match.ToString();
                    }
                    break;
                case 11: //Khác
                    regex = new Regex(@"\d");
                    foreach (var item in regex.Matches(sms))
                    {
                        otp += item.ToString();
                    }
                    break;
                default:
                    regex = new Regex(@"\d{4,8}");
                    match = regex.Match(sms);
                    if (match.Success)
                    {
                        otp = match.ToString();
                    }
                    break;
            }
            return otp;

        }
        public static string ConvertPhone(string Phone, string DauSo)
        {
            if (Phone.Length == 9)
            {
                Phone = DauSo + Phone;
            }
            if (DauSo == "84")
            {
                if (Phone.Length == 10 && Phone.IndexOf("0") == 0)
                {
                    Phone = DauSo + Phone.Substring(1, Phone.Length - 1);
                }
            }
            if (DauSo == "0")
            {
                if (Phone.Length == 11 && Phone.IndexOf("84") == 0)
                {
                    Phone = DauSo + Phone.Substring(2, Phone.Length - 2);
                }
            }
            return Phone;
        }
        public static bool isMatchNumber(string Number, int NumberMatch)
        {
            return Number.EndsWith(NumberMatch.ToString());
        }
        public static bool PhanLoaiDichVu(string cateGoryName, string smsFrom)
        {
            if (smsFrom.ToLower() == cateGoryName.ToLower())//Nếu tên dịch vụ trùng với sms đến
            {
                return true;
            }
            else
            {
                switch (cateGoryName)
                {
                    case "shopee":
                    default:
                        return false;
                }
            }

        }
        public static int ConvertNumberToInt(string number)
        {
            int new_num = 0;
            switch (number.Length)
            {
                case 9:
                    new_num = int.Parse(number.Substring(0));
                    break;

                case 10:
                    if (number.StartsWith("0"))
                    {
                        new_num = int.Parse(number.Substring(1));
                    }
                    break;
                case 11:
                    if (number.StartsWith("84"))
                    {
                        new_num = int.Parse(number.Substring(2));
                    }
                    break;
                default:
                    break;
            }

            return new_num;

        }
        public static DateTime Date_GetTimeNow()//thời gian hiện tại trả về datetime
        {
            return DateTime.UtcNow.AddHours(7);
        }
        public static int Date_GetTimeNowInt()//thời gian hiện tại trả về double
        {
            return Date_DateTimeToUnitUnixTimestamp(Date_GetTimeNow());
        }
        public static DateTime Date_UnixTimestampToDateTime(int unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
        }

        public static int Date_DateTimeToUnitUnixTimestamp(DateTime input)
        {
            return (int)input.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }



        public static void CheckThreadLive(List<Thread> threads, int sencond)
        {

            while (threads.Count > 0)
            {
                foreach (var Threadchild in threads)
                {
                    if (!Threadchild.IsAlive)
                    {
                        threads.Remove(Threadchild);

                        break;
                    }
                }
                Thread.Sleep(sencond * 5);
            }
        }


        public static string SHA512_ComputeHash(string text, string secretKey)
        {
            var hash = new StringBuilder(); ;
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);
            using (var hmac = new HMACSHA512(secretkeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
        //public static string Ifnonmatch(string t)
        //{
        //    string ifnonematch = "55b03-" + Tkiet.GetMD5("55b03" + Tkiet.GetMD5(t) + "55b03");
        //    return ifnonematch;
        //}
        //string ifnonematch = "55b03-" + Tkiet.GetMD5("55b03" + Tkiet.GetMD5(t) + "55b03");
        public static string ConvertMoney(string money)
        {
            if (money.Length > 5 && !money.Contains("l") && !money.Contains("u"))
            {
                long price = long.Parse(money);
                if (price >= 100000)
                {
                    price = price / 100000;
                }
                string value = price.ToString("#,###");
                return value;
            }
            return "0";

        }




        public static object locker = new object();
        public static void AddList(string path, string NoiDung)
        {
            lock (locker)
            {
                FileStream fs = new FileStream(path, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine(NoiDung);
                sw.Flush();
                sw.Close();
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static List<string> ReadList(string path)
        {
            List<string> ReadAll = new List<string>();
            var m = File.ReadAllLines(path);
            foreach (var item in m)
            {
                ReadAll.Add(item);
            }
            return ReadAll;
        }

        public static string RandomToken(int length)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomNumberString(int length)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomUpcase(int length)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return random.Next(min, max);
        }



        public static string Sha256Hash(string Input)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Input));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        //public static string GetMD5(string chuoi)
        //{
        //    string str_md5 = "";
        //    byte[] mang = System.Text.Encoding.UTF8.GetBytes(chuoi);

        //    MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
        //    mang = my_md5.ComputeHash(mang);

        //    foreach (byte b in mang)
        //    {
        //        str_md5 += b.ToString("x2");
        //    }

        //    return str_md5;
        //}
        public static void RunLabel(string command, Label label)
        {
            label.BeginInvoke((Action)delegate ()
            {
                label.Text = command;

            });
        }
        public static int loopComment = 0;
        public static void Comment(string comment, TextBox textBox)
        {

            if (textBox == null)
            {
                return;
            }

            textBox.BeginInvoke((Action)delegate ()
            {
                textBox.Text += comment + "  -------------------" + DateTime.Now.ToString("HH:ss") + "   \r\n";
                if (loopComment > 100)
                {
                    textBox.Clear();
                    loopComment = 0;
                }
            });
            loopComment++;


        }

        public static bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b[0-9a-fA-F]+\b\Z");
        }
        public static string FromHex(string hex)
        {
            short[] raw = new short[hex.Length / 4];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToInt16(hex.Substring(i * 4, 4), 16);
            }
            string s = "";
            //wtf encoding utf32 ride ahmagh kos sher pas mide
            foreach (var item in raw)
            {
                s += char.ConvertFromUtf32(item).ToString();
            }
            return s;
        }
        public static object lockerdgv = new object();


        public static void ChangeTextDgv(int index, string comment, int val, DataGridView dgv)
        {
            int id = val;
            if (dgv == null)
            {
                return;
            }
            lock (lockerdgv)
            {
                dgv.BeginInvoke((Action)delegate ()
                {
                    dgv.Rows[id].Cells[index].Value = comment;

                });
            }

        }
        public static void ChangeTextSMSDgv(SMS sms, int val, DataGridView dgv)
        {
            if (dgv == null || val < 0)
            {
                return;
            }

            lock (lockerdgv)
            {
                dgv.BeginInvoke((Action)delegate ()
                {
                    dgv.Rows[val].Cells[5].Value = Date_UnixTimestampToDateTime(sms.Time).ToString("dd/MM/yyyy HH:mm:ss");
                    dgv.Rows[val].Cells[6].Value = sms.From;
                    dgv.Rows[val].Cells[7].Value = sms.OTP;
                    dgv.Rows[val].Cells[8].Value = sms.Message;

                });
            }

        }
        public static string RandomNameVietNamese(bool MidName, bool Nam, bool Nu, bool Number, bool isSpace, int SoDauTien, int SoSauCung)
        {
            Random rn = new Random(Guid.NewGuid().GetHashCode());
            string[] firstName = { "Le", "Nguyen", "Ly", "Tran", "Trinh", "Hoai", "Hoang", "Phan", "Bui", "Do", "Ho", "Ngo", "Duong", "Huynh", "Pham", "Vo", "Vu", "Hoai", "Pham", "Dai", "La", "Doan", "Truong", "Dinh" };
            string[] midName = { "van", "le", "truong", "to", "tuan", "huu", "an", "anh", "gia", "hoai", "khanh", "nhat", "minh", "bao", "cong", "duc", "dinh", "duy", "hieu", "khai", "manh", "vy" };
            string[] lastNameNam = { "huan", "phat", "thien", "huu", "tien", "sieu", "ma", "tieu", "cuong", "loi", "canh", "tue", "vu", "dat", "thanh", "cong", "kiet", "thai", "hoai", "khanh", "nhat", "minh", "bao", "cong", "duc", "dinh", "khai", "manh", "bang", "chau", "chien", "chinh", "dai", "dao", "giang", "hiep", "hao", "huy", "phong", "nam", "nhan", "nguyen", "son", "sinh", "sang", "toan", "tri", "tu", "tuan", "tho", "thach", "thang", "vuong", "vinh", "kha" };
            string[] lastNameNu = { "huyen", "hien", "an", "tram", "tran", "thao", "thu", "thuong", "thuy", "van", "mong", "hong", "yen", "phi", "ai", "xuan", "my", "anh", "diep", "nguyet", "que", "quynh", "tu", "an", "van", "binh", "cuc", "chi", "di", "dan", "doan", "diep", "duyen", "linh", "lien", "ly", "hien", "hoa", "lan", "huong", "hue", "lam", "tuyet", "mi", "nhi", "ngoc", "nhu", "nuong", "phuong", "uyen", "suong", "trang", "truc", "trinh" };
            string[] NameRandom = { "huan", "phat", "thien", "huu", "tien", "sieu", "ma", "tieu", "cuong", "loi", "canh", "tue", "vu", "dat", "thanh", "cong", "kiet", "thai", "hoai", "khanh", "nhat", "minh", "bao", "cong", "duc", "dinh", "khai", "manh", "bang", "chau", "chien", "chinh", "dai", "dao", "giang", "hiep", "hao", "huy", "phong", "nam", "nhan", "nguyen", "son", "sinh", "sang", "toan", "tri", "tu", "tuan", "tho", "thach", "thang", "vuong", "vinh", "kha",
                    "huyen", "hien", "an", "tram", "tran", "thao", "thu", "thuong", "thuy", "van", "mong", "hong", "yen", "phi", "ai", "xuan", "my", "anh", "diep", "nguyet", "que", "quynh", "tu", "an", "van", "binh", "cuc", "chi", "di", "dan", "doan", "diep", "duyen", "linh", "lien", "ly", "hien", "hoa", "lan", "huong", "hue", "lam", "tuyet", "mi", "nhi", "ngoc", "nhu", "nuong", "phuong", "uyen", "suong", "trang", "truc", "trinh"
            };
            string Name = "";

            string born = rn.Next(SoDauTien, SoSauCung).ToString();

            Name += firstName[rn.Next(firstName.Length)];

            if (isSpace)
            {
                Name += " ";
            }
            if (MidName)
            {
                Name += midName[rn.Next(midName.Length)];
                if (isSpace)
                {
                    Name += " ";
                }
            }

            if (Nam && !Nu)
            {
                Name += lastNameNam[rn.Next(lastNameNam.Length)];
            }
            else if (!Nam && Nu)
            {
                Name += lastNameNu[rn.Next(lastNameNu.Length)];
            }
            else
            {
                Name += NameRandom[rn.Next(NameRandom.Length)];
            }


            if (Number)
            {
                Name += born;
            }
            return Name;
        }

    }
}
