using System.ComponentModel.DataAnnotations;

namespace BankTransaction.Models
{
    public class Transaction
    {
        [Key]
        public int ID    { get; set; }
        public string AccountNumber { get; set; }
        public string ClientName { get; set; }
        public string BankName { get; set; }
        public int Amount {  get; set; }
        public int Loan { get; set; }
      

    }
}
