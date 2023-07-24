//normalement CV
using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models;
using AngularAuthYtAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace AngularAuthYtAPI.Controllers
{

    [Authorize(Roles = "ADMIN, ADMIN BANK")]
    [Route("api/[controller]")]
    [ApiController]
    public class JSON_XMLController : ControllerBase
    {
        private readonly MPaymentContext _mContext;
        private readonly IConfiguration _configuration; 
        public JSON_XMLController(MPaymentContext context, IConfiguration configuration)
        {
            _mContext = context;
            _configuration = configuration;
        }

        [HttpPost("ConfigACS")]
        public async Task<IActionResult> ConfigACS(ACSDTO request)
        {
            string bankcode = request.BankCode;
            // From BankInfo
            Bankinfo bank = _mContext.Bankinfos.FirstOrDefault(x => x.BankCode == bankcode);
            if (bank == null)
            {
                return BadRequest(new { Message = "Bank not found" });
            }

            // Load the XML file
            string xmlFilePath = _configuration["XmlFilePath"];
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            // Access the specific add node with key "listTreatedfiles"
            XmlNode addNode = xmlDoc.SelectSingleNode("appSettings").SelectSingleNode("add[@key='listTreatedfiles']");

            // Check if the bank code is already present in the value
            string value = addNode.Attributes["value"].Value;
            string bankCodeToCheck = $"Cards{bank.Organisation}_:{bankcode}";
            if (value.Contains(bankCodeToCheck))
            {
                return Ok("Bank code already present");
            }

            // Update the value with the new token and bank code
            addNode.Attributes["value"].Value += $",{bankCodeToCheck}";

            // Save the modified XML back to the file
            xmlDoc.Save(xmlFilePath);

            return Ok("Updating XML with Success");
        }
        [HttpPost("AddTokenJSON")]
        public async Task<IActionResult> AddTokenJSON(AddTokenDTO model)
        {
            // From BankInfo
            Bankinfo bank = _mContext.Bankinfos.FirstOrDefault(x => x.BankCode == model.BankCode);
            if (bank == null)
            {
                return BadRequest(new { Message = "Bank not found" });
            }

            // Read the JSON file
            string jsonFilePath = _configuration["JSONFilePath"]; ;
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
                // Update the token value for the existing bank code
                tokenAgregateur[model.BankCode]["Token"] = model.Token;

                // Save the modified JObject back to the JSON file
                await System.IO.File.WriteAllTextAsync(jsonFilePath, jsonObj.ToString());

                return Ok("Token Updated Successfully");
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
            return Ok("Token added Successfully");

            return Ok("JSON Updated Successfully");
        }
    }
}
