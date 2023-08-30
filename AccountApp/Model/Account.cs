using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Model
{
    [Serializable]
    internal class Account
    {
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public double Balance { get; set; }
    }
}
