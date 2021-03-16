using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        int AddTransfer(Transfer transfer);
        Transfer UpdateTransfer(Transfer transfer);
        List<Transfer> GetTransfersById(int id, bool pending);
        Transfer GetTransferDetails(int id);

    }


}
