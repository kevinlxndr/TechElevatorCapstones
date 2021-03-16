using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserDAO userDao;
        private readonly ITransferDAO transferDao;

        public UsersController(IUserDAO _userDAO, ITransferDAO _transferDAO)
        {
                userDao = _userDAO;
            transferDao = _transferDAO;
        }

        [HttpGet("{id}/balance")]
        public decimal GetBalance(int id)
        {
            decimal balance = userDao.GetCurrentBalance(id);
            return balance;
        }

        [HttpGet]
        public IActionResult GetUserInfo(string username = null)
        {
            if(username == null)
            {
                return Ok(userDao.GetUsers());
            }
            else{
                int id = userDao.GetUser(username).UserId;
                return Ok(id);
            }
                
        }

        [HttpGet("{id}/transfers")]
        public List<Transfer> GetTransfers(int id)
        {
            List<Transfer> transfers = transferDao.GetTransfersById(id,false);
            return transfers;
        }

        [HttpGet("{id}/transfers/pending")]
        public List<Transfer> GetPendingTransfers(int id)
        {
            List<Transfer> transfers = transferDao.GetTransfersById(id, true);
            return transfers;
        }

    }
}
