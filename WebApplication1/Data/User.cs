namespace WebApplication1.Data
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string password { get; set; }
        public string passwordSalt { get; set; }
        public int UserTypeId { get; set; }
        public bool IsActive { get; set; }       
        public bool IsDelete { get; set; }    
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public UserType UserTypes { get; set; }
        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; }
       

    }
}
