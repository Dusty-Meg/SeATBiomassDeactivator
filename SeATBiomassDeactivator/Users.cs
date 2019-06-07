namespace SeATBiomassDeactivator
{
    public class Users
    {
        public long id { get; set; }
        public bool active { get; set; }

        public virtual RefreshTokens Token { get; set; }
    }
}