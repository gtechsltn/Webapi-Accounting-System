namespace AccountingWebAPI.Models
{
    public class users
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password_hash { get; set; }
        public string email { get; set; }

        public users()
        {

        }

        public users(int user_id_, string username_, string password_hash_, string email_)
        {
            this.user_id = user_id_;
            this.username = username_;
            this.password_hash = password_hash_;
            this.email = email_;
        }
    }


    public class usersrequest
    {
        public string email { get; set; }
        public string password { get; set; }       
    }

    public class passchangerequest
    {
        public string email { get; set; }
        public string old_password { get; set; }

        public string password { get; set; }

        public string confirm_password { get; set; }
    }


}
