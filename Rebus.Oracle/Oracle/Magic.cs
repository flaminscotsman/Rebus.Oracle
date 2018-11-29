using System.Collections.Generic;

namespace Rebus.Oracle
{
    static class OracleMagic
    {
        public static List<string> GetTableNames(this OracleDbConnection connection)
        {
            var tableNames = new List<string>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT TABLE_NAME AS NAME FROM USER_TABLES
UNION ALL
SELECT VIEW_NAME AS NAME FROM USER_VIEWS
UNION ALL
SELECT SYNONYM_NAME AS NAME FROM USER_SYNONYMS;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tableNames.Add(reader["NAME"].ToString());
                    }
                }
            }

            return tableNames;
        }
    }
}
