namespace JwtImplementation.Interfaces
{
    public interface ITokenGenerator
    {
        public string GenerateToken(int id, string email,string role);

    }
}
