namespace LD_4_Interneto_tech.Dto
{
    public class LoginResDto
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public List<int?> FavoritePropertyIds { get; set; }
        public string Token { get; set; }
    }
}