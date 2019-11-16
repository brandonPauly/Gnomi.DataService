using gnomi.common.utility.reflection;
using gnomi.dataService.entities;
using gnomi.repositories.utility;
using System;
using System.Threading.Tasks;

namespace gnomi.repositories
{
    public class verificationRepository<key, t> : baseRepository<key, t>, iVerificationRepository<key, t> where t : iEntity<key>
    {
        public verificationRepository(iDataConnectionFactory factory, iInstanceAnalyzer instanceAnalyzer, iFieldSkipHelper skipHelper)
        : base(factory, instanceAnalyzer, skipHelper) { }

        public async Task linkVerification(long humanId, string verificationKey)
        {
            var statement = $"insert into verification (verificationCode, humanId, initiationDate) values (@verificationCode, @humanId, @initiationDate);";

            using (var sqlClient = _sqlClient())
            {
                sqlClient.Open();

                var command = sqlClient.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = statement;

                command.Parameters.AddWithValue("@verificationCode", verificationKey);
                command.Parameters.AddWithValue("@humanId", humanId);
                command.Parameters.AddWithValue("@initiationDate", DateTime.Now);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
