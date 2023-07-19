using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models.Dto;
using AngularAuthYtAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Data;

namespace AngularAuthYtAPI.Controllers
{
    //[Authorize(Roles = "ADMIN, ADMIN BANK")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddWithdrawalTerminalController : ControllerBase
    {
        private readonly CashoutContext _authContext;
        private readonly AppDbContext _authContextapp;
        public AddWithdrawalTerminalController( CashoutContext context, AppDbContext authContextapp)
        {
            _authContext = context;
            _authContextapp = authContextapp;
        }


        [HttpPost("AddWithdrawalTerminal")]
        public async Task<IActionResult> AddWithDrawalTerminal(IFormFile file, string bankcode)
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

                        List<MPayWithdrawalTerminalId> dataModels = new List<MPayWithdrawalTerminalId>();
                        BankInfo bank = _authContextapp.BankInfos.FirstOrDefault(x => x.BankCode == bankcode);
                        if (bank == null)
                        {
                            return BadRequest(new { message = "Bank Not found" });
                        }
                        // Start reading from the second row
                        for (int row = 2; row <= rowCount; row++)
                        {
                            AddWithdrawalTerminalDTO dataModel = new AddWithdrawalTerminalDTO();

                            // Assuming your data model has properties corresponding to each column in the Excel file
                            for (int column = 1; column <= columnCount; column++)
                            {
                                string header = headers[column - 1];
                                string value = worksheet.Cells[row, column].Value?.ToString();

                                // Map the value to the corresponding property in your data model
                                // You may need to handle data type conversions based on your requirements

                                switch (header)
                                {
                                    case "MPayAtmId":
                                        dataModel.MPayAtmId = value;
                                        break;
                                    case "MPayAtmAffiliation":
                                        dataModel.MPayAtmAffiliation = value;
                                        break;
                                    case "MPayAtmBatchNum":
                                        dataModel.MPayAtmBatchNum = value;
                                        break;
                                    case "MPayAtmSeqNum":
                                        dataModel.MPayAtmSeqNum = value;
                                        break;
                                    case " MPayAtmDispoCode":
                                        dataModel.MPayAtmDispoCode = int.Parse(value);
                                        break;
                                    case "MPayAtmStatus1":
                                        dataModel.MPayAtmStatus1 = int.Parse(value);
                                        break;
                                    case "MPayAtmStatus2":
                                        dataModel.MPayAtmStatus2 = int.Parse(value);
                                        break;
                                    case "MPayAtmCreationDate":
                                        dataModel.MPayAtmCreationDate = value;
                                        break;
                                    case "MPayAtmPwd":
                                        dataModel.MPayAtmPwd = value;
                                        break;
                                        // Add cases for other column headers
                                }


                            }
                            // Check if MPayAtmId exists in MPayWithdrawalTerminalId
                            MPayWithdrawalTerminalId mPayWithdrawalTerminalId = _authContext.MPayWithdrawalTerminalIds.FirstOrDefault(
                                x => x.MPayAtmId == dataModel.MPayAtmId&&bankcode==x.MPayAtmBankCode);
                            if (mPayWithdrawalTerminalId == null)
                            {

                                mPayWithdrawalTerminalId = new MPayWithdrawalTerminalId();
                                mPayWithdrawalTerminalId.MPayAtmBankCode = bankcode;
                                mPayWithdrawalTerminalId.MPayAtmBankName = bank.Organisation;

                                mPayWithdrawalTerminalId.MPayAtmAffiliation = dataModel.MPayAtmAffiliation;
                                mPayWithdrawalTerminalId.MPayAtmBatchNum = dataModel.MPayAtmBatchNum;
                                mPayWithdrawalTerminalId.MPayAtmSeqNum = dataModel.MPayAtmSeqNum;
                                mPayWithdrawalTerminalId.MPayAtmPwd = dataModel.MPayAtmPwd;
                                mPayWithdrawalTerminalId.MPayAtmStatus1 = dataModel.MPayAtmStatus1;
                                mPayWithdrawalTerminalId.MPayAtmStatus2 = dataModel.MPayAtmStatus2;
                                mPayWithdrawalTerminalId.MPayAtmId = dataModel.MPayAtmId;
                                mPayWithdrawalTerminalId.MPayAtmDispoCode = dataModel.MPayAtmDispoCode;
                                mPayWithdrawalTerminalId.MPayAtmCreationDate = dataModel.MPayAtmCreationDate;
                                mPayWithdrawalTerminalId.MPayAtmStatusDate2 = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                                dataModels.Add(mPayWithdrawalTerminalId);
                            }
                        }

                        // Now you have the data from the Excel file in the 'dataModels' list
                        // Update your database with the retrieved data using your ORM or database access code

                        // Example code to update the database using Entity Framework Core
                        _authContext.MPayWithdrawalTerminalIds.AddRange(dataModels);
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
