namespace AccountingWebAPI.Models
{
    public class user_roles
    {
        public int user_id { get; set; }
        public int role_id { get; set; }

        public user_roles() { }

        public user_roles(int user_id_, int role_id_)
        {
            this.user_id = user_id_;
            this.role_id = role_id_;
        }
    }
}
