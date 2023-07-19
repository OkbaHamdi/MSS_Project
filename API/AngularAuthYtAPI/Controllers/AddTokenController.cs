using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models.Dto;
using AngularAuthYtAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace AngularAuthYtAPI.Controllers
{
    //[Authorize(Roles = "ADMIN, ADMIN BANK")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddTokenController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public AddTokenController(AppDbContext context)
        {
            _authContext = context;
        }

        [HttpPost("AddToken")]
        public async Task<IActionResult> AddToken(AddTokenDTO model)
        {
            // From BankInfo
            BankInfo bank = _authContext.BankInfos.FirstOrDefault(x => x.BankCode == model.BankCode);
            if (bank == null)
            {
                return BadRequest(new { Message = "Bank not found" });
            }

            // Read the JSON file
            string jsonFilePath = @"C:\Users\hokba\Downloads\data.json";
            string json = await System.IO.File.ReadAllTextAsync(jsonFilePath);

            // Parse the JSON into a JObject
            JObject jsonObj = JObject.Parse(json);

            // Access the "AppSettings" section
            JObject appSettings = (JObject)jsonObj["AppSettings"];

            // Access the "TokenAgregateur" field
            JObject tokenAgregateur = (JObject)appSettings["TokenAgregateur"];

            // Check if the bank code is already present
            if (tokenAgregateur[model.BankCode] != null)
            {
                return Ok("Bank code already present");
            }

            // Add or update the bank code entry with the provided token and empty values
            tokenAgregateur[model.BankCode] = JObject.FromObject(new
            {
                Token = model.Token,
                Mean = "",
                client_id = "",
                client_secret = "",
                merchant_id = "",
                merchant_secret = ""
            });

            // Save the modified JObject back to the JSON file
            await System.IO.File.WriteAllTextAsync(jsonFilePath, jsonObj.ToString());

            return Ok("Success");
        }
    }
}
