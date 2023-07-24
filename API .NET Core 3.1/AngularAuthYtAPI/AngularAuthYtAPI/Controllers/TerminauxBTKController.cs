//normelement CV
using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models.Dto;
using AngularAuthYtAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace AngularAuthYtAPI.Controllers
{
    [Authorize(Roles = "ADMIN, ADMIN BANK")]
    [Route("api/[controller]")]
    [ApiController]
    public class TerminauxBTKController : ControllerBase
    {
        private readonly MPaymentContext _mContext;
        public TerminauxBTKController( MPaymentContext context)
        {
            _mContext = context;
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
                        Bankinfo bank = _mContext.Bankinfos.FirstOrDefault(x => x.BankCode == bankcode);
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

                                //adding with name
                               /* switch (header)
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
                                    case "MPayAtmDispoCode":
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
                                */
                                //adding with rang of column
                                switch (column)
                                {
                                    case 1:
                                        dataModel.MPayAtmId = value;
                                        break;
                                    case 2:
                                        dataModel.MPayAtmAffiliation = value;
                                        break;
                                    case 3:
                                        dataModel.MPayAtmBatchNum = value;
                                        break;
                                    case 4:
                                        dataModel.MPayAtmSeqNum = value;
                                        break;
                                    case 5:
                                        dataModel.MPayAtmDispoCode = int.Parse(value);
                                        break;
                                    case 6:
                                        dataModel.MPayAtmStatus1 = int.Parse(value);
                                        break;
                                    case 7:
                                        dataModel.MPayAtmStatus2 = int.Parse(value);
                                        break;
                                    case 8:
                                        dataModel.MPayAtmCreationDate = value;
                                        break;
                                    case 9:
                                        dataModel.MPayAtmPwd = value;
                                        break;
                                        // Add cases for other column headers
                                }

                            }
                            // Check if MPayAtmId exists in MPayWithdrawalTerminalId
                            MPayWithdrawalTerminalId mPayWithdrawalTerminalId = _mContext.MPayWithdrawalTerminalIds.FirstOrDefault(
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
                        //add range of models -> update range
                        _mContext.MPayWithdrawalTerminalIds.AddRange(dataModels);
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
                        Bankinfo bank = _mContext.Bankinfos.FirstOrDefault(x => x.BankCode == bankcode);
                        if (bank == null)
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

                                // add with name
                                /*switch (header)
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
                                        dataModel.WpTerminalDistributorSeqNum = value;
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
                                */
                                // add with rang column
                                switch (column)
                                {
                                    case 1:
                                        dataModel.WpTerminalDistributorId = value;
                                        break;
                                    case 2:
                                        dataModel.WpTerminalDistributorAffiliation = value;
                                        break;
                                    case 3:
                                        dataModel.WpTerminalDistributorBatchNum = value;
                                        break;
                                    case 4:
                                        dataModel.WpTerminalDistributorSeqNum = value;
                                        break;
                                    case 5:
                                        dataModel.WpTerminalDistributorDispoCode = int.Parse(value);
                                        break;
                                    case 6:
                                        dataModel.WpTerminalDistributorStatus1 = int.Parse(value);
                                        break;
                                    case 7:
                                        dataModel.WpTerminalDistributorStatus2 = int.Parse(value);
                                        break;
                                    case 8:
                                        dataModel.WpTerminalDistributorPwd = value;
                                        break;
                                        // Add cases for other column headers
                                }

                            }
                            // Check if MPayAtmId exists in MPayWithdrawalTerminalId
                            WTerminalsRefillWallet wTerminalsRefillWallet = _mContext.WTerminalsRefillWallets.FirstOrDefault(x => x.WpTerminalDistributorId == dataModel.WpTerminalDistributorId && bankcode == x.WpTerminalBankCode);
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
                                wTerminalsRefillWallet.WpTerminalDistributorCreationDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                                dataModels.Add(wTerminalsRefillWallet);
                            }
                        }

                        _mContext.WTerminalsRefillWallets.AddRange(dataModels);
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

        [HttpPost("AlimentationWallet")]
        public async Task<IActionResult> AddWithDrawalTerminal(IFormFile file, string bankcode, string affiliation)
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
                        Bankinfo bank = _mContext.Bankinfos.FirstOrDefault(x => x.BankCode == bankcode);
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

                                //add with name column
                                /*switch (header)
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
                                */
                                //add with rang column
                                switch (column)
                                {
                                    case 1:
                                        dataModel.MPayTransferTpeIdDebit = value;
                                        break;
                                    case 2:
                                        dataModel.MPayTransferTpeSeqNumDebit = value;
                                        break;
                                    case 3:
                                        dataModel.MPayTransferTpeSeqNumCredit = value;
                                        break;
                                    case 4:
                                        dataModel.MPayTransferTpeDispoCode = value;
                                        break;
                                    case 5:
                                        dataModel.MPayTransferTpeStatus = value;
                                        break;
                                    case 6:
                                        dataModel.MPayTransferTpeIdTrsct = value;
                                        break;
                                    case 7:
                                        dataModel.MPayTransferTpeBatchNumDebit = value;
                                        break;
                                    case 8:
                                        dataModel.MPayTransferTpeBatchNumCredit = value;
                                        break;
                                        // Add cases for other column headers
                                }
                            }
                            // Check if MPayAtmId exists in MPayWithdrawalTerminalId
                            MPayTransferTpe mPayTransferTpe = _mContext.MPayTransferTpes.FirstOrDefault(
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
                        int n = dataModels.Count;
                        int m = (n + 1) / 2;
                        for (int i = 0; i < n; i++)
                        {
                            int j = i + 1;
                            if (j <= m) dataModels[i].MPayTransferTpeIdOp = "01";
                            else dataModels[i].MPayTransferTpeIdOp = "04";
                        }
                        _mContext.MPayTransferTpes.AddRange(dataModels);
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
