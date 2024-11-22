namespace AccountingWebAPI.Models
{
    public class users
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password_hash { get; set; }

        public users()
        {

        }

        public users(int user_id_, string username_, string password_hash_)
        {
            this.user_id = user_id_;
            this.username = username_;
            this.password_hash = password_hash_;
        }
    }
}
