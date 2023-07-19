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
    //[Authorize(Roles = "ADMIN, ADMIN BANK")]
    [Route("api/[controller]")]
    [ApiController]
    public class MobileServiceController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public MobileServiceController(AppDbContext context)
        {
            _authContext = context;
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
            var Bank_Infos =await  _authContext.BankInfos
                .FirstOrDefaultAsync(x => x.BankCode == model.BankCode );
            

            if (Bank_Infos != null)
            {
                return BadRequest(new { Message = "Bank exist" });

            }
            Bank_Infos = new BankInfo();
            Bank_Infos.BankCode = Bank.BankCode;
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
                 => _authContext.BankInfos.AllAsync(x => x.BankCode != Bankcode);
            private Task<bool> CheckServiceProviderNotExistAsync(string? ServiceProvider)
                 => _authContext.BankInfos.AllAsync(x => x.ServiceProvider != ServiceProvider);
    }

}

