namespace AccountingWebAPI.Models
{
    public class accounts
    {
        public int account_id { get; set; }
        public string account_number { get; set; }
        public string account_name { get; set; }
        public string account_type { get; set; }
        public decimal balance { get; set; }

        public accounts()
        {

        }
        public accounts(int account_id_, string account_number_, string account_name_, string account_type_, decimal balance_)
        {
            this.account_id = account_id_;
            this.account_number = account_number_;
            this.account_name = account_name_;
            this.account_type = account_type_;
            this.balance = balance_;
        }
    }
}
