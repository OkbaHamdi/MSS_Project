using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models.Dto;
using AngularAuthYtAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AngularAuthYtAPI.Controllers
{
    //[Authorize(Roles = "ADMIN, ADMIN BANK")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddBinController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public AddBinController(AppDbContext context)
        {
            _authContext = context;
        }

        [HttpPost("AddBin")]
        public async Task<IActionResult> Addbin(AddBinDTO model)
        {
            // from  BankInfo
             BankInfo Bank = _authContext.BankInfos.Where(x => x.BankCode == model.BankCode).FirstOrDefault();
            if (Bank == null)
            {
                return BadRequest(new { Message = "Bank Not found" });
            }
            // check BinId exit or no in PayBin
            PayBin paybin = _authContext.PayBins.Where(x => x.BinId == model.BinID).FirstOrDefault();

            if (paybin != null)
            {
                return BadRequest(new { Message = "Code Bin exists" });

            }
            paybin = new PayBin();
            paybin.BankCode = model.BankCode;
            paybin.BankName = Bank.Organisation;
            paybin.BinId = model.BinID;
            paybin.CodeApp=Bank.CodeApp;
            if (model.Stock == true) paybin.Privilege = "P2";
            else paybin.Privilege ="P0";
            await _authContext.AddAsync(paybin);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                //Status = 200,
                Message = "BIN Added Successfuly!"
            });
        }
    }

}