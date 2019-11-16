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

        public async Task<bool> isHumanNew(string email)
        {
            var statement = $"select humanId from human where email = @email;";
            object id;

            using (var sqlClient = _sqlClient())
            {
                sqlClient.Open();

                var command = sqlClient.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = statement;

                command.Parameters.AddWithValue("@email", email);

                id = await command.ExecuteScalarAsync();
            }

            return id == null;
        }
    }
}
