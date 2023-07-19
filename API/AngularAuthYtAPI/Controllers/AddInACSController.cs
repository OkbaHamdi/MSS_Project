using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models;
using AngularAuthYtAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace AngularAuthYtAPI.Controllers
{
    //[Authorize(Roles = "ADMIN, ADMIN BANK")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddInACSController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public AddInACSController(AppDbContext context)
        {
            _authContext = context;
        }

        [HttpPost("ConfigACS")]
        public async Task<IActionResult> ConfigACS(ACSDTO request)
        {
            string bankcode = request.BankCode;
            // From BankInfo
            BankInfo bank = _authContext.BankInfos.FirstOrDefault(x => x.BankCode == bankcode);
            if (bank == null)
            {
                return BadRequest(new { Message = "Bank not found" });
            }

            // Load the XML file
            string xmlFilePath = @"C:\Users\hokba\Downloads\data.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            // Access the appSettings node
            XmlNode appSettingsNode = xmlDoc.SelectSingleNode("appSettings");

            // Access the specific add node with key "listTreatedfiles"
            XmlNode addNode = appSettingsNode.SelectSingleNode("add[@key='listTreatedfiles']");

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

            return Ok("Success");
        }
    }
}
