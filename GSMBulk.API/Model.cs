namespace GSMBulk.API
{
    public class Number
    {
        public int Id { get; set; }
        public int Phone { get; set; }
    }

    public class SMS
    {
        public int Id { get; set; }
        public int Phone { get; set; }
        public string Time { get; set; }
        public string From { get; set; }
        public string Sms { get; set; }

    }
    public class ApiRespone
    {
        public int err_code { get; set; }
        public string err_msg { get; set; }
        public object data { get; set; }
    }
}
