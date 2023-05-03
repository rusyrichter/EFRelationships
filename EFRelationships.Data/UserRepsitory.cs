namespace EFRelationships.Data
{
    public class UserRepsitory
    {
        public string _connectionString { get; set; }
        public UserRepsitory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddUser(User user, string password)
        {
            using var context = new QuestionAnswerDBContext(_connectionString);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (isValidPassword)
            {
                return user; 
            }

            return null;
        }
        public User GetByEmail(string email)
        {
            using var context = new QuestionAnswerDBContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}