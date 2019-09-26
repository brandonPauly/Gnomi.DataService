using gnomi.common.utility.reflection;
using gnomi.dataService.entities;
using gnomi.repositories.utility;
using System;

namespace gnomi.repositories
{
    public class humanRepository<key, t> : baseRepository<key, t>,  iHumanRepository<key, t> where t : iEntity<key>
    {
        public humanRepository(iDataConnectionFactory factory, iInstanceAnalyzer instanceAnalyzer, iFieldSkipHelper skipHelper)
        : base(factory, instanceAnalyzer, skipHelper) { }

        public human<long> addNewHuman(human<long> human)
        {
            var statement = $"insert into human (email, password, signUpDate) output inserted.humanId values (@email, @password, @signUpDate);";

            using (var sqlClient = _sqlClient())
            {
                sqlClient.Open();

                var command = sqlClient.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = statement;

                command.Parameters.AddWithValue("@email", human.email);
                command.Parameters.AddWithValue("@password", human.password);
                command.Parameters.AddWithValue("@signUpDate", DateTime.Now);

                human.humanId = (long)command.ExecuteScalar();
            }

            return human;
        }

        public void linkVerification(long humanId, string verificationCode)
        {
            var statement = $"insert into verificationKey (verificationCode, humanId) values (@verificationCode, @humanId);";

            using (var sqlClient = _sqlClient())
            {
                sqlClient.Open();

                var command = sqlClient.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = statement;

                command.Parameters.AddWithValue("@verificationCode", verificationCode);
                command.Parameters.AddWithValue("@humanId", humanId);

                command.ExecuteNonQuery();
            }
        }
    }
}
