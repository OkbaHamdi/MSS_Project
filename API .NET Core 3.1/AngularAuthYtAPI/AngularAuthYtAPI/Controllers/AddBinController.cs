// Normalement CV
using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models.Dto;
using AngularAuthYtAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace AngularAuthYtAPI.Controllers
{
    [Authorize(Roles = "ADMIN, ADMIN BANK")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddBinController : ControllerBase
    {
        private readonly MPaymentContext _mContext;
        public AddBinController(MPaymentContext context)
        {
            _mContext = context;
        }

        [HttpPost("Add_one_Bin")]
        public async Task<IActionResult> Add_one_bin(AddBinDTO model)
        {
            
            // from  BankInfo
            Bankinfo Bank = _mContext.Bankinfos.Where(x => x.BankCode == model.BankCode).FirstOrDefault();
            if (Bank == null)
            {
                return BadRequest(new { Message = "Bank Not found" });
            }
            // check BinId exist or not in PayBin
            PayBin paybin = _mContext.PayBins.Where(x => x.BinId == model.BinID).FirstOrDefault();

            if (paybin != null)
            {
                return BadRequest(new { Message = "Code Bin exists" });

            }
            paybin = new PayBin();
            paybin.BankCode = model.BankCode;
            paybin.BankName = Bank.Organisation;
            paybin.BinId = model.BinID;
            paybin.CodeApp=Bank.CodeApp;
            paybin.Privilege = model.Stock ? "P2" : "P1";
            await _mContext.AddAsync(paybin);
            await _mContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "BIN Added Successfuly!"
            });
        }
        [HttpPost("Add_Bin_File")]
        public async Task<IActionResult> Add_Bin_File(IFormFile file, string bankcode, bool stock)
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
                                dataModel.BankCode = bankcode;
                                dataModel.Stock = stock;
                            }
                            Bankinfo bank = _mContext.Bankinfos.FirstOrDefault(x => x.BankCode == dataModel.BankCode);
                            if (bank != null)
                            {
                                // Check if BinId exists in PayBin
                                PayBin paybin = _mContext.PayBins.FirstOrDefault(x => x.BinId == dataModel.BinID);
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
                        _mContext.PayBins.AddRange(dataModels);
                        await _mContext.SaveChangesAsync();

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