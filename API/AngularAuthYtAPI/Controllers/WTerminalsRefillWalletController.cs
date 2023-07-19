using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models.Dto;
using AngularAuthYtAPI.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace AngularAuthYtAPI.Controllers
{
    //[Authorize(Roles = "ADMIN, ADMIN BANK")]
    [Route("api/[controller]")]
    [ApiController]
    public class WTerminalsRefillWalletController : ControllerBase
    {
        private readonly CashinContext _authContext;
        private readonly AppDbContext _authContextapp;
        public WTerminalsRefillWalletController(CashinContext context, AppDbContext authContextapp)
        {
            _authContext = context;
            _authContextapp = authContextapp;
        }


        [HttpPost("WTerminalsRefillWallet")]
        public async Task<IActionResult> W_Terminals_Refill_Wallet(IFormFile file, string bankcode)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the LicenseContext

            if (file != null && file.Length > 0)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                {
                    using (var package = new ExcelPackage(file.OpenReadStream()))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first sheet

                        int rowCount = worksheet.Dimension.Rows;
                        int columnCount = worksheet.Dimension.Columns;

                        // Assuming your Excel file has headers in the first row
                        List<string> headers = new List<string>();
                        for (int column = 1; column <= columnCount; column++)
                        {
                            headers.Add(worksheet.Cells[1, column].Value.ToString());
                        }

                        List<WTerminalsRefillWallet> dataModels = new List<WTerminalsRefillWallet>();
                        BankInfo bank = _authContextapp.BankInfos.FirstOrDefault(x => x.BankCode == bankcode);
                        if(bank == null)
                        {
                            return BadRequest(new { message = "Bank Not found" });
                        }
                        // Start reading from the second row
                        for (int row = 2; row <= rowCount; row++)
                        {
                            WTerminalsRefillWalletDTO dataModel = new WTerminalsRefillWalletDTO();

                            // Assuming your data model has properties corresponding to each column in the Excel file
                            for (int column = 1; column <= columnCount; column++)
                            {
                                string header = headers[column - 1];
                                string value = worksheet.Cells[row, column].Value?.ToString();

                                // Map the value to the corresponding property in your data model
                                // You may need to handle data type conversions based on your requirements

                                switch (header)
                                {
                                    case "WpTerminalDistributorId":
                                        dataModel.WpTerminalDistributorId = value;
                                        break;
                                    case "WpTerminalDistributorAffiliation":
                                        dataModel.WpTerminalDistributorAffiliation = value;
                                        break;
                                    case "WpTerminalDistributorBatchNum":
                                        dataModel.WpTerminalDistributorBatchNum = value;
                                        break;
                                    case "WpTerminalDistributorSeqNum":
                                        dataModel.WpTerminalDistributorSeqNum= value;
                                        break;
                                    case "WpTerminalDistributorDispoCode":
                                        dataModel.WpTerminalDistributorDispoCode = int.Parse(value);
                                        break;
                                    case "WpTerminalDistributorStatus1":
                                        dataModel.WpTerminalDistributorStatus1 = int.Parse(value);
                                        break;
                                    case "WpTerminalDistributorStatus2":
                                        dataModel.WpTerminalDistributorStatus2 = int.Parse(value);
                                        break;
                                    case "WpTerminalDistributorPwd":
                                        dataModel.WpTerminalDistributorPwd = value;
                                        break;
                                        // Add cases for other column headers
                                }


                            }
                            // Check if MPayAtmId exists in MPayWithdrawalTerminalId
                            WTerminalsRefillWallet wTerminalsRefillWallet = _authContext.WTerminalsRefillWallets.FirstOrDefault(x => x.WpTerminalDistributorId == dataModel.WpTerminalDistributorId && bankcode == x.WpTerminalBankCode);
                            if (wTerminalsRefillWallet == null)
                            {

                                wTerminalsRefillWallet = new WTerminalsRefillWallet();
                                wTerminalsRefillWallet.WpTerminalBankCode = bankcode;
                                wTerminalsRefillWallet.WpTerminalDistributorAffiliation = dataModel.WpTerminalDistributorAffiliation;
                                wTerminalsRefillWallet.WpTerminalDistributorId = dataModel.WpTerminalDistributorId;
                                wTerminalsRefillWallet.WpTerminalDistributorBatchNum = dataModel.WpTerminalDistributorBatchNum;
                                wTerminalsRefillWallet.WpTerminalDistributorSeqNum = dataModel.WpTerminalDistributorSeqNum;
                                wTerminalsRefillWallet.WpTerminalDistributorDispoCode = dataModel.WpTerminalDistributorDispoCode;
                                wTerminalsRefillWallet.WpTerminalDistributorStatus1 = dataModel.WpTerminalDistributorStatus1;
                                wTerminalsRefillWallet.WpTerminalDistributorStatus2 = dataModel.WpTerminalDistributorStatus2;
                                wTerminalsRefillWallet.WpTerminalDistributorPwd = dataModel.WpTerminalDistributorPwd;
                                wTerminalsRefillWallet.WpTerminalDistributorCreationDate = DateTime.Now.ToString("yyyyMMddHHmmss/fff");
                                dataModels.Add(wTerminalsRefillWallet);
                            }
                        }

                        // Now you have the data from the Excel file in the 'dataModels' list
                        // Update your database with the retrieved data using your ORM or database access code

                        // Example code to update the database using Entity Framework Core
                        _authContext.WTerminalsRefillWallets.AddRange(dataModels);
                        await _authContext.SaveChangesAsync();

                        return Ok(new { message = "Excel file data added to the database successfully" });
                    }
                }
                else
                {
                    return BadRequest(new { message = "Invalid file type. Only Excel files are allowed." });
                }
            }

            return BadRequest(new { message = "No file found." });
        }
    }
}

