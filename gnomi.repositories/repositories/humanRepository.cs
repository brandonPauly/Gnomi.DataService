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

            using (_sqlClient)
            {
                _sqlClient.Open();

                var command = _sqlClient.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = statement;

                command.Parameters.AddWithValue("@email", human.email);
                command.Parameters.AddWithValue("@password", human.password);
                command.Parameters.AddWithValue("@signUpDate", DateTime.Now);

                human.humanId = (long)command.ExecuteScalar();
            }

            return human;
        }
    }
}
