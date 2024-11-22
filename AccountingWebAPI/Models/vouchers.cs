namespace AccountingWebAPI.Models
{
    public class vouchers
    {
        public int voucher_id { get; set; }
        public TimeSpan voucher_date { get; set; }
        public string description { get; set; }
        public decimal total_amount { get; set; }

        public vouchers()
        {

        }

        public vouchers(int voucher_id_, TimeSpan voucher_date_, string description_, decimal total_amount_)
        {
            this.voucher_id = voucher_id_;
            this.voucher_date = voucher_date_;
            this.description = description_;
            this.total_amount = total_amount_;
        }
    }
}
