using gnomi.dataService.entities;
using System.Threading.Tasks;

namespace gnomi.repositories
{
    public interface iVerificationRepository<key, t> : iRepository<key, t> where t : iEntity<key>
    {
        Task linkVerification(long humanId, string verificationKey);
    }
}
