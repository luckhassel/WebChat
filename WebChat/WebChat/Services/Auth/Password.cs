namespace WebChat.Services.Auth
{
    public class Password : IPassword
    {
        public string HashPwd(string pwd)
        {
            return BCrypt.Net.BCrypt.HashPassword(pwd);
        }

        public bool VerifyPwd(string pwd, string pwdStored)
        {
            return BCrypt.Net.BCrypt.Verify(pwd, pwdStored);
        }
    }
}
