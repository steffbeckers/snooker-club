namespace SC.API.ViewModels
{
    public class AuthenticatedVM
    {
        public UserVM User { get; set; }
        public string Token { get; set; }
        public bool RememberMe { get; set; }
    }
}
