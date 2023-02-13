using Azure;
using GSMBulk.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GSMBulk.API.Controllers
{
    [ApiController]
    public class GSMController : ControllerBase
    {
        private AppDbContext _appDb;
        private ApiRespone _respone;
        public GSMController(AppDbContext appDb)
        {
            _appDb = appDb;
            _respone = new ApiRespone();
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {

            return Ok("API ! GsmBulk");
        }
        [HttpGet]
        [Route("phone/get")]
        public async Task<IActionResult> GetListPhone()
        {
            try
            {
                var listNum = await _appDb.NumberDb.ToListAsync();
                if (listNum.Count > 0)
                {
                    _respone.data = listNum;
                    _respone.err_code = 0;
                    _respone.err_msg = "success";

                }
                else
                {
                    _respone.err_code = 1;
                    _respone.err_msg = "failed";

                }
            }
            catch (Exception err)
            {
                _respone.err_code = 2;
                _respone.err_msg = err.Message;
            }

            return Ok(_respone);
        }
        [HttpGet]
        [Route("phone/id/{id}")]
        public async Task<IActionResult> GetPhone(int id)
        {
            var phone = _appDb.NumberDb.Where(c => c.Id == id);
            if (phone != null)
            {
                _respone.data = phone;
                _respone.err_code = 0;
                _respone.err_msg = "success";

            }
            else
            {
                _respone.err_code = 1;
                _respone.err_msg = "phone not found";
            }

            return Ok(_respone);
        }
        [HttpPost]
        [Route("phone/add")]
        public async Task<IActionResult> AddPhone([FromBody] List<int> list_num)
        {
            List<Number> list_model = new List<Number>();
            foreach (var num in list_num)
            {
                if (num.ToString().Length == 9)
                {
                    Number number = new Number
                    {
                        Phone = num
                    };
                    list_model.Add(number);
                }

            }
            await _appDb.NumberDb.AddRangeAsync(list_model);
            await _appDb.SaveChangesAsync();
            _respone.err_code = 0;
            _respone.err_msg = $"add {list_num.Count}/{list_model.Count} number success";
            return Ok(_respone);
        }

        [HttpGet]
        [Route("/phone/delete")]
        public async Task<IActionResult> DeleteNum()
        {
            var listnum = await _appDb.NumberDb.ToListAsync();
            _appDb.NumberDb.RemoveRange(listnum);
            await _appDb.SaveChangesAsync();
            _respone.err_code = 0;
            _respone.err_msg = $"delete {listnum.Count} number success";
            return Ok(_respone);
        }

        [HttpGet]
        [Route("sms/delete")]
        public async Task<IActionResult> DeleteSMS()
        {
            var listsms = await _appDb.SMSDb.ToListAsync();
            _appDb.SMSDb.RemoveRange(listsms);
            await _appDb.SaveChangesAsync();
            _respone.err_code = 0;
            _respone.err_msg = $"delete {listsms.Count} sms success";
            return Ok(_respone);
        }
        [HttpGet]
        [Route("sms/get/{phone}")]
        public async Task<IActionResult> GetSms(int phone)
        {
            var listSMS = await _appDb.SMSDb.Where(c => c.Phone == phone).ToListAsync();

            if (listSMS.Count > 0)
            {
                _respone.data = listSMS;
                _respone.err_code = 0;
                _respone.err_msg = "success";

            }
            else
            {
                _respone.err_code = 1;
                _respone.err_msg = "failed";

            }
            return Ok(_respone);
        }
        [HttpPost]
        [Route("sms/add")]
        public async Task<IActionResult> AddSMS([FromBody] SMS sms)
        {
            await _appDb.SMSDb.AddAsync(sms);
            await _appDb.SaveChangesAsync();
            _respone.err_code = 0;
            _respone.err_msg = $"add sms success";
            return Ok(_respone);
        }

    }
}
