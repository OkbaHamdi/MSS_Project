using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models;
using AngularAuthYtAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAuthYtAPI.Controllers
{
    //[Authorize(Roles = "ADMIN, ADMIN BANK")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddBinFileController : ControllerBase
    {
        private readonly AppDbContext _authContext;

        public AddBinFileController(AppDbContext context)
        {
            _authContext = context;
        }
        

        [HttpPost("AddBinFile")]
        public async Task<IActionResult> AddBinFile(IFormFile file, string bankcode,bool stock)
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

                        List<PayBin> dataModels = new List<PayBin>();

                        // Start reading from the second row
                        for (int row = 2; row <= rowCount; row++)
                        {
                            AddBinDTO dataModel = new AddBinDTO();

                            // Assuming your data model has properties corresponding to each column in the Excel file
                            for (int column = 1; column <= columnCount; column++)
                            {
                                string header = headers[column - 1];
                                string value = worksheet.Cells[row, column].Value?.ToString();

                                // Map the value to the corresponding property in your data model
                                // You may need to handle data type conversions based on your requirements

                                dataModel.BinID = value;
                                dataModel.BankCode=bankcode;
                                dataModel.Stock = stock;
                            }
                            BankInfo bank = _authContext.BankInfos.FirstOrDefault(x => x.BankCode == dataModel.BankCode);
                            if (bank != null)
                            {
                                // Check if BinId exists in PayBin
                                PayBin paybin = _authContext.PayBins.FirstOrDefault(x => x.BinId == dataModel.BinID);
                                if (paybin == null)
                                {
                                    paybin = new PayBin();
                                    paybin.BankCode = dataModel.BankCode;
                                    paybin.BankName = bank.Organisation;
                                    paybin.BinId = dataModel.BinID;
                                    paybin.CodeApp = bank.CodeApp;
                                    if (dataModel.Stock == true)
                                        paybin.Privilege = "P2";
                                    else
                                        paybin.Privilege = "P0";
                                    dataModels.Add(paybin);
                                }
                            }
                        }

                        // Now you have the data from the Excel file in the 'dataModels' list
                        // Update your database with the retrieved data using your ORM or database access code

                        // Example code to update the database using Entity Framework Core
                        _authContext.PayBins.AddRange(dataModels);
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
