using ESIConnectionLibrary.Public_classes;
using ESIConnectionLibrary.PublicModels;
using Microsoft.EntityFrameworkCore;

namespace SeATBiomassDeactivator
{
    public class Program2
    {
        public void Main(string connectionString)
        {
            LatestCharacterEndpoints esiLibrary = new LatestCharacterEndpoints("Seat Biomassed Character Script. Slack: Dusty Meg");

            using (LocalDbContext dbContext = new LocalDbContext(connectionString))
            {
                bool updateDb = false;

                foreach (Users user in dbContext.Users.Include(x => x.Token))
                {
                    if (user.Token?.deleted_at == null || !user.active)
                    {
                        continue;
                    }

                    if (!int.TryParse(user.id.ToString(), out int userId))
                    {
                        continue;
                    }

                    V4CharactersPublicInfo characterPublic = esiLibrary.PublicInfo(userId);

                    if (characterPublic.CorporationId != 1000001)
                    {
                        continue;
                    }

                    user.active = false;
                    updateDb = true;
                }

                if (updateDb)
                {
                    dbContext.SaveChanges();
                }
            }
        }
    }
}