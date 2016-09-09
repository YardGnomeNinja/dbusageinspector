using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DBUsageInspector
{
    internal class SqlServerService
    {
        public string ConnectionString;

        public SqlServerService()
        {
        }

        public SqlServerService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IEnumerable<ReferenceObject> GetObjects()
        {
            List<ReferenceObject> returnValue = new List<ReferenceObject>();

            string query = "SELECT DISTINCT " +
                            "   Name, " +
                            "   CASE type_desc " +
                            "        WHEN 'SQL_INLINE_TABLE_VALUED_FUNCTION' THEN 'FUNCTION' " +
                            "        WHEN 'SQL_SCALAR_FUNCTION' THEN 'FUNCTION' " +
                            "        WHEN 'SQL_STORED_PROCEDURE' THEN 'PROCEDURE' " +
                            "        WHEN 'SQL_TABLE_VALUED_FUNCTION' THEN 'FUNCTION' " +
                            "        WHEN 'SQL_TRIGGER' THEN 'TRIGGER' " +
                            "        WHEN 'USER_TABLE' THEN 'TABLE' " +
                            "    ELSE " +
                            "        type_desc " +
                            "    END AS [Type] " +
                            "FROM " +
                            "    sys.objects " +
                            "WHERE " +
                            "    type_desc <> 'PRIMARY_KEY_CONSTRAINT' " +
                            "    AND type_desc <> 'FOREIGN_KEY_CONSTRAINT' " +
                            "    AND type_desc <> 'DEFAULT_CONSTRAINT' " +
                            "    AND type_desc <> 'UNIQUE_CONSTRAINT' " +
                            "    AND type_desc <> 'INTERNAL_TABLE' " +
                            "    AND type_desc <> 'SERVICE_QUEUE' " +
                            "    AND type_desc <> 'SYSTEM_TABLE' " +
                            "ORDER BY " +
                            "    [Type] ASC";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        returnValue.Add(new ReferenceObject(dataReader["Name"].ToString(), dataReader["Type"].ToString(), string.Empty));
                    }
                }
            }

            return returnValue;
        }

        public IDictionary<ReferenceObject, ReferenceObject> GetReferences(IList<ReferenceObject> sqlServerObjects)
        {
            IDictionary<ReferenceObject, ReferenceObject> returnValue = new Dictionary<ReferenceObject, ReferenceObject>();

            string query = "SELECT " +
                            "   sys.objects.name AS [ObjectName], " +
                            "   CASE sys.objects.type_desc " +
                            "       WHEN 'SQL_INLINE_TABLE_VALUED_FUNCTION' THEN 'FUNCTION' " +
                            "       WHEN 'SQL_SCALAR_FUNCTION' THEN 'FUNCTION' " +
                            "       WHEN 'SQL_STORED_PROCEDURE' THEN 'PROCEDURE' " +
                            "       WHEN 'SQL_TABLE_VALUED_FUNCTION' THEN 'FUNCTION' " +
                            "       WHEN 'SQL_TRIGGER' THEN 'TRIGGER' " +
                            "       WHEN 'USER_TABLE' THEN 'TABLE' " +
                            "   ELSE " +
                            "       sys.objects.type_desc " +
                            "   END AS [ObjectType], " +
                            "   sys.sql_modules.definition AS [ObjectDefinition] " +
                            "FROM " +
                            "   sys.objects " +
                            "   INNER JOIN sys.sql_modules ON sys.sql_modules.object_id = sys.objects.object_id ";

            List<Tuple<string, string, string>> sqlObjects = new List<Tuple<string, string, string>>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 3600;
                    connection.Open();

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        string objectName = dataReader["ObjectName"].ToString();
                        string objectType = dataReader["ObjectType"].ToString();
                        string objectDefinition = dataReader["ObjectDefinition"].ToString();

                        string cleanContent = ParsingService.Normalize(objectDefinition, ".sql");

                        sqlObjects.Add(new Tuple<string, string, string>(objectName, objectType, cleanContent));
                    }
                }
            }

            foreach (Tuple<string, string, string> sqlObject in sqlObjects)
            {
                foreach (KeyValuePair<ReferenceObject, ReferenceObject> objectPair in ParsingService.GetReferences(sqlObject.Item1, sqlObject.Item2, sqlObject.Item3, sqlServerObjects))
                {
                    returnValue.Add(objectPair.Key, objectPair.Value);
                }
            }

            return returnValue;
        }
    }
}