namespace AccountingWebAPI.Models
{
    public class auths
    {
        public auths() {
            rolelist = new List<user_roles>();
        }  
        public int user_id { get; set; }
        public string username { get; set; }

        public List<user_roles> rolelist { get; set; }
    }
}
