namespace EcommerceApi.BLL.Models
{
    public class JwtOptions
    {
        public string Key { get; set; }
        public int LifeTime { get; set; }
        public string Issuer { get; set; }
    }
}
