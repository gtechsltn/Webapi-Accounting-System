namespace AccountingWebAPI.Models
{
    public class roles
    {
        public int role_id { get; set; }
        public string role_name { get; set; }

        public roles()
        {

        }

        public roles(int role_id_, string role_name_)
        {
            this.role_id = role_id_;
            this.role_name = role_name_;
        }
    }
}
