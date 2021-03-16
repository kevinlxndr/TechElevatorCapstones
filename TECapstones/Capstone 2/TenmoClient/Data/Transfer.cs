using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    public class Transfer
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public decimal AmountTransfered { get; set; }
        public int TransferId { get; set; }
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
