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
    public class AlimentationWalletController : ControllerBase
    {
        private readonly AlimentationWalletContext _authContext;
        private readonly AppDbContext _authContextapp;
        public AlimentationWalletController(AlimentationWalletContext context, AppDbContext authContextapp)
        {
            _authContext = context;
            _authContextapp = authContextapp;
        }
        [HttpPost("AlimentationWallet")]
        public async Task<IActionResult> AddWithDrawalTerminal(IFormFile file, string bankcode,string affiliation)
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

                        List<MPayTransferTpe> dataModels = new List<MPayTransferTpe>();
                        BankInfo bank = _authContextapp.BankInfos.FirstOrDefault(x => x.BankCode == bankcode);
                        if (bank == null)
                        {
                            return BadRequest(new { message = "Bank Not found" });
                        }
                        // Start reading from the second row
                        for (int row = 2; row <= rowCount; row++)
                        {
                            AlimentationWalletDTO dataModel = new AlimentationWalletDTO();

                            // Assuming your data model has properties corresponding to each column in the Excel file
                            for (int column = 1; column <= columnCount; column++)
                            {
                                string header = headers[column - 1];
                                string value = worksheet.Cells[row, column].Value?.ToString();

                                // Map the value to the corresponding property in your data model
                                // You may need to handle data type conversions based on your requirements

                                switch (header)
                                {
                                    case "MPayTransferTpeIdDebit":
                                        dataModel.MPayTransferTpeIdDebit = value;
                                        break;
                                    case "MPayTransferTpeSeqNumDebit":
                                        dataModel.MPayTransferTpeSeqNumDebit = value;
                                        break;
                                    case "MPayTransferTpeSeqNumCredit":
                                        dataModel.MPayTransferTpeSeqNumCredit = value;
                                        break;
                                    case "MPayTransferTpeDispoCode":
                                        dataModel.MPayTransferTpeDispoCode = value;
                                        break;
                                    case "MPayTransferTpeStatus":
                                        dataModel.MPayTransferTpeStatus = value;
                                        break;
                                    case "MPayTransferTpeIdTrsct":
                                        dataModel.MPayTransferTpeIdTrsct = value;
                                        break;
                                    case "MPayTransferTpeBatchNumDebit":
                                        dataModel.MPayTransferTpeBatchNumDebit = value;
                                        break;
                                    case "MPayTransferTpeBatchNumCredit":
                                        dataModel.MPayTransferTpeBatchNumCredit = value;
                                        break;
                                        // Add cases for other column headers
                                }


                            }
                            // Check if MPayAtmId exists in MPayWithdrawalTerminalId
                            MPayTransferTpe mPayTransferTpe = _authContext.MPayTransferTpes.FirstOrDefault(
                                x => x.MPayTransferTpeIdDebit == dataModel.MPayTransferTpeIdDebit && bankcode == x.PayTransferTpeBankCode);
                            if (mPayTransferTpe == null)
                            {

                                mPayTransferTpe = new MPayTransferTpe();
                                mPayTransferTpe.PayTransferTpeBankCode = bankcode;
                                mPayTransferTpe.MPayTransferTpeAffiliationNum = affiliation;
                                mPayTransferTpe.MPayTransferTpeIdDebit = dataModel.MPayTransferTpeIdDebit;
                                mPayTransferTpe.MPayTransferTpeIdCredit = dataModel.MPayTransferTpeIdDebit;
                                mPayTransferTpe.MPayTransferTpeSeqNumCredit = dataModel.MPayTransferTpeSeqNumCredit;
                                mPayTransferTpe.MPayTransferTpeSeqNumDebit = dataModel.MPayTransferTpeSeqNumDebit;
                                mPayTransferTpe.MPayTransferTpeDispoCode = dataModel.MPayTransferTpeDispoCode;
                                mPayTransferTpe.MPayTransferTpeStatus = dataModel.MPayTransferTpeStatus;
                                mPayTransferTpe.MPayTransferTpeIdTrsct = dataModel.MPayTransferTpeIdTrsct;
                                mPayTransferTpe.MPayTransferTpeBatchNumCredit = dataModel.MPayTransferTpeBatchNumCredit;
                                mPayTransferTpe.MPayTransferTpeBatchNumDebit = dataModel.MPayTransferTpeBatchNumDebit;
                                mPayTransferTpe.MPayTransferTpeStatusDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
                                dataModels.Add(mPayTransferTpe);
                            }
                        }

                        //update  id operation
                        int n=dataModels.Count;
                        int m = (n + 1) / 2;
                        for(int i = 0; i < n; i++)
                        {
                            int j = i + 1;
                            if (j <= m) dataModels[i].MPayTransferTpeIdOp = "01";
                            else dataModels[i].MPayTransferTpeIdOp = "04";
                        }
                        // Now you have the data from the Excel file in the 'dataModels' list
                        // Update your database with the retrieved data using your ORM or database access code

                        // Example code to update the database using Entity Framework Core
                        _authContext.MPayTransferTpes.AddRange(dataModels);
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
