//Normalement CV
using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models;
using AngularAuthYtAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAuthYtAPI.Controllers
{
    //[Authorize(Roles = "ADMIN, ADMIN BANK")]
    [Route("api/[controller]")]
    [ApiController]
    public class BankinfoController : ControllerBase
    {
        private readonly MPaymentContext _mContext;
        public BankinfoController(MPaymentContext context)
        {
            _mContext = context;
        }

        [HttpPost("AddBank")]
        public async Task<IActionResult> AddBank(AddBankDTO model)
        {
            //MobileService Bank = request.Bank;
            MobileService Bank = _mContext.MobileServices.Where(x=>x.BankCode== model.BankCode).FirstOrDefault();
            if (Bank == null) 
            {
                return BadRequest(new { Message = "Bank Not found" });
            }
            var Bank_Infos =await  _mContext.Bankinfos
                .FirstOrDefaultAsync(x => x.BankCode == model.BankCode );
            

            if (Bank_Infos != null)
            {
                return BadRequest(new { Message = "Bank exist" });

            }
            Bank_Infos = new Bankinfo();
            Bank_Infos.BankCode = Bank.BankCode;
            Bank_Infos.CodeApp = model.CodeApp;
            Bank_Infos.Organisation = Bank.BankName;
            Bank_Infos.ServiceProvider=Bank.ServiceProvider;
            await _mContext.AddAsync(Bank_Infos);
            await _mContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "Bank Added Successfuly!"
            });
        }
        [HttpPost("AddSMS")]
        public async Task<IActionResult> AddSms(AddSMSDTO model)
        {
            Bankinfo Bank = _mContext.Bankinfos.Where(x => x.BankCode == model.BankCode).FirstOrDefault();
            if (Bank == null)
            {
                return BadRequest(new { Message = "Bank Not found" });
            }



            Bank.Smsid = model.SMSid;
            Bank.Smspwd = model.SMSpwd;
            _mContext.Update(Bank);
            await _mContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "Update Id Successfuly!"
            });
        }

    }

}

