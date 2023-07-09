using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models;
using AngularAuthYtAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthYtAPI.Controllers
{
    [Authorize(Roles = "ADMIN, ADMIN BANK")]
    [Route("api/[controller]")]
    [ApiController]
    public class MobileServiceController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public MobileServiceController(AppDbContext context)
        {
            _authContext = context;
        }
        public class AuthenticateRequest
        {
            public MobileService Bank { get; set; }
            public User User { get; set; }
        }

        [HttpPost("AddBank")]
        public async Task<IActionResult> AddBank(AddBankDTO model)
        {
            //MobileService Bank = request.Bank;
            MobileService Bank = _authContext.MobileServices.Where(x=>x.BankCode== model.BankCode).FirstOrDefault();
            if (Bank == null) 
            {
                return BadRequest(new { Message = "Bank Not found" });
            }
            //User user = request.User;
        
            /*if(user.Role!="ADMIN BANK")
            {
                return BadRequest(new { Message = "User is not an Admin Bank" });
            }*/
            // check BankCode
            //if (await CheckBankCodeNotExistAsync(Bank.BankCode))
            //    return BadRequest(new { Message = "Bank Code doesn't exist" });
            
            //check Service Provider
           //if (await CheckServiceProviderNotExistAsync(Bank.ServiceProvider))
           //     return BadRequest(new { Message = "Service Provider doesn't exist" });

            //check bank exist in MobileService
            var Bank_Infos =await  _authContext.BankInfos
                .FirstOrDefaultAsync(x => x.Bankcode == model.BankCode );
            

            if (Bank_Infos != null)
            {
                return BadRequest(new { Message = "Bank exist" });

            }
            Bank_Infos = new BankInfo();
            Bank_Infos.Bankcode = Bank.BankCode;
            Bank_Infos.CodeApp = model.CodeApp;
            Bank_Infos.Organisation = Bank.BankName;
            Bank_Infos.ServiceProvider=Bank.ServiceProvider;

            await _authContext.AddAsync(Bank_Infos);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                //Status = 200,
                Message = "Bank Added Successfuly!"
            });
        }
            private Task<bool> CheckBankCodeNotExistAsync(string? Bankcode)
                 => _authContext.BankInfos.AllAsync(x => x.Bankcode != Bankcode);
            private Task<bool> CheckServiceProviderNotExistAsync(string? ServiceProvider)
                 => _authContext.BankInfos.AllAsync(x => x.ServiceProvider != ServiceProvider);
    }

}

