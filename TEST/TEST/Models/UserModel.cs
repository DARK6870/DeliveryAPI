namespace TEST.Models
{
    public class UserModel
    {
        public int users_id { get; set; }
        public string email { get; set; }
        public string u_firstname { get; set; }
        public string u_lastname { get; set; }
        public string user_password { get; set; }
        public int role_id { get; set; }
        public string rolename { get; set; }
    }
    public class UserData
    {
        public int users_id { get; set; }
        public string email { get; set; }
        public string u_firstname { get; set; }
        public string u_lastname { get; set; }
        public string user_password { get; set; }
    }
}
