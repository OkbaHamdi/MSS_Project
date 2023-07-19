using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models.Dto;
using AngularAuthYtAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
// mochkla fl update
namespace AngularAuthYtAPI.Controllers
{
    //[Authorize(Roles = "ADMIN, ADMIN BANK")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddSMSController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public AddSMSController(AppDbContext context)
        {
            _authContext = context;
        }
        [HttpPost("AddSMS")]
        public async Task<IActionResult> AddSms(AddSMSDTO model)
        {
            BankInfo Bank = _authContext.BankInfos.Where(x => x.BankCode == model.BankCode).FirstOrDefault();
            if (Bank == null)
            {
                return BadRequest(new { Message = "Bank Not found" });
            }
            
          
           
            Bank.SMSid = model.SMSid;
            Bank.SMSpwd = model.SMSpwd;
            _authContext.Update(Bank);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                //Status = 200,
                Message = "Update Id Successfuly!"
            });
        }
    }
}
