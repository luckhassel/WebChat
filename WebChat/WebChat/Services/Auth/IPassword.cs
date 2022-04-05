namespace WebChat.Services.Auth
{
    public interface IPassword
    {
        public string HashPwd(string pwd);
        public bool VerifyPwd(string pwd, string pwdStored);
    }
}
