namespace WP.BusinessLogic.Models
{
    public class Token
    {
        public Guid UserId { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
