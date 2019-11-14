using gnomi.common.utility.reflection;
using gnomi.dataService.entities;
using gnomi.repositories.utility;
using System;
using System.Threading.Tasks;

namespace gnomi.repositories
{
    public class humanRepository<key, t> : baseRepository<key, t>,  iHumanRepository<key, t> where t : iEntity<key>
    {
        public humanRepository(iDataConnectionFactory factory, iInstanceAnalyzer instanceAnalyzer, iFieldSkipHelper skipHelper)
        : base(factory, instanceAnalyzer, skipHelper) { }

        public async Task<human<long>> addNewHuman(human<long> human)
        {
            var statement = $"insert into human (email, password, signUpDate, isVerified) output inserted.humanId values (@email, @password, @signUpDate, 0);";

            using (var sqlClient = _sqlClient())
            {
                sqlClient.Open();

                var command = sqlClient.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = statement;

                command.Parameters.AddWithValue("@email", human.email);
                command.Parameters.AddWithValue("@password", human.password);
                command.Parameters.AddWithValue("@signUpDate", DateTime.Now);

                human.humanId = (long) await command.ExecuteScalarAsync();
            }

            return human;
        }

        public async Task linkVerification(long humanId, string verificationCode)
        {
            var statement = $"insert into verificationKey (verificationCode, humanId, initiationDate) values (@verificationCode, @humanId, @initiationDate);";

            using (var sqlClient = _sqlClient())
            {
                sqlClient.Open();

                var command = sqlClient.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = statement;

                command.Parameters.AddWithValue("@verificationCode", verificationCode);
                command.Parameters.AddWithValue("@humanId", humanId);
                command.Parameters.AddWithValue("@initiationDate", DateTime.Now);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
