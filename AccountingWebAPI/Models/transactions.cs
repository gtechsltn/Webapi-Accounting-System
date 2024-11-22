namespace AccountingWebAPI.Models
{
    public class transactions
    {
        public int transaction_id { get; set; }
        public int voucher_id { get; set; }
        public int account_id { get; set; }
        public decimal debit_amount { get; set; }
        public decimal credit_amount { get; set; }

        public transactions()
        {

        }

        public transactions(int transaction_id_, int voucher_id_, int account_id_, decimal debit_amount_, decimal credit_amount_)
        {
            this.transaction_id = transaction_id_;
            this.voucher_id = voucher_id_;
            this.account_id = account_id_;
            this.debit_amount = debit_amount_;
            this.credit_amount = credit_amount_;
        }
    }
}
