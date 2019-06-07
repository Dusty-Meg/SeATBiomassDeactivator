using System;

namespace SeATBiomassDeactivator
{
    public class RefreshTokens
    {
        public long character_id { get; set; }
        public DateTime? deleted_at { get; set; }

        public virtual Users User { get; set; }
    }
}