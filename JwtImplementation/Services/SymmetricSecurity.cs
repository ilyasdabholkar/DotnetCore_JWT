namespace JwtImplementation.Services
{
    internal class SymmetricSecurity
    {
        private object jwtSecret;

        public SymmetricSecurity(object jwtSecret)
        {
            this.jwtSecret = jwtSecret;
        }
    }
}