using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TenmoServer.DAO;
using TenmoServer.Models;
using System.Collections.Generic;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private readonly IUserDAO userDao;
        private readonly ITransferDAO transferDao;

        public TransferController(IUserDAO _userDAO,ITransferDAO _transferDAO)
        {
            userDao = _userDAO;
            transferDao = _transferDAO;
        }

        [HttpPut]
        public IActionResult TransferMoney(Transfer transfer)
        {
            bool success = userDao.TransferFunds(transfer.FromUserId, transfer.ToUserId, transfer.AmountTransfered);
            if (success)
            {
                return Ok(transferDao.AddTransfer(transfer));
            }
            else
            {
                return NotFound(0);
            }
        }

        [HttpGet("{id}/details")]
        public Transfer GetTransferDetailsId(int id)
        {
            Transfer transfer = transferDao.GetTransferDetails(id);
            return transfer;
        }

        [HttpPost("request")]
        public IActionResult CreateRequest(Transfer transfer)
        {
            int result = transferDao.AddTransfer(transfer);
            if (result > 0)
                return Ok(true);
            else
                return NotFound(false);
        }

        [HttpPut("request/update")]
        public IActionResult UpdateRequest(Transfer transfer)
        {
            bool success = true;
            if (transfer.Status == "Approved")
            {
                success = userDao.TransferFunds(transfer.FromUserId, transfer.ToUserId, transfer.AmountTransfered);
            }
             
            if (success)
            {
                return Ok(transferDao.UpdateTransfer(transfer));
            }
            else
            {
                return NotFound(null);
            }

        }



    }
}
