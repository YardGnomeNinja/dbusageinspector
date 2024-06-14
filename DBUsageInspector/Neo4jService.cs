using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DBUsageInspector
{
    internal class Neo4jService
    {
        public string Password;
        public string Url;
        public string Username;

        FileService _fileService;

        public Neo4jService(FileService fileService)
        {
            _fileService = fileService;
        }

        public Neo4jService(string url, string username, string password)
        {
            Url = url;
            Username = username;
            Password = password;
        }

        public async Task<bool> CreateObjects(IEnumerable<ReferenceObject> sqlObjects, TextWriter logWriter)
        {
            using (var driver = Neo4j.Driver.GraphDatabase.Driver(Url, Neo4j.Driver.AuthTokens.Basic(Username, Password)))
            {
                using (var session = driver.AsyncSession())
                {
                    foreach (ReferenceObject sqlObject in sqlObjects)
                    {
                        //session.ExecuteWriteAsync("CREATE (a:" + sqlObject.Type.ToUpper() + " {name:'" + sqlObject.Name.Replace("\\", "\\\\") + "', schema:'" + sqlObject.Schema.Replace("\\", "\\\\") + "'})");
                        await session.ExecuteWriteAsync(
                            tx =>
                            {
                                var command = "CREATE (a:" + sqlObject.Type.ToUpper() +
                                    " {name:'" + sqlObject.Name.Replace("\\", "\\\\") +
                                    "', schema:'" + sqlObject.Schema.Replace("\\", "\\\\") + "'})";

                                logWriter.WriteLine(command);

                                var result = tx.RunAsync(command);

                                return result;
                            });
                    }
                }
            }

            return true;
        }

        public async Task<bool> CreateRelationships(IDictionary<ReferenceObject, ReferenceObject> references, TextWriter logWriter)
        {
            string query = string.Empty;

            using (var driver = Neo4j.Driver.GraphDatabase.Driver(Url, Neo4j.Driver.AuthTokens.Basic(Username, Password)))
            {
                using (var session = driver.AsyncSession())
                {
                    foreach (KeyValuePair<ReferenceObject, ReferenceObject> reference in references)
                    {
                        //string relationship = (reference.Key.Relationship == string.Empty ? "REFERENCES" : reference.Key.Relationship); // If no relationship is defined, default to "REFERENCES"
                        //query = "MATCH (a:" + reference.Key.Type + "), (b:" + reference.Value.Type + ") WHERE a.name =~ '" + reference.Key.Name.Replace("\\", "\\\\\\\\") + "' AND b.name =~ '" + reference.Value.Name.Replace("\\", "\\\\\\\\") + "' CREATE (a)-[:" + relationship + "]->(b)";
                        //session.Run(query);
                        string relationship = (reference.Key.Relationship == string.Empty ? "REFERENCES" : reference.Key.Relationship); // If no relationship is defined, default to "REFERENCES"

                        await session.ExecuteWriteAsync(
                            tx =>
                            {
                                var command = "MATCH (a:" + reference.Key.Type +
                                    "), (b:" + reference.Value.Type + ") WHERE a.name =~ '" + reference.Key.Name.Replace("\\", "\\\\\\\\") +
                                    "' AND b.name =~ '" + reference.Value.Name.Replace("\\", "\\\\\\\\") + "' CREATE (a)-[:" + relationship + "]->(b)";

                                logWriter.WriteLine(command);

                                var result = tx.RunAsync(command);

                                return result;
                            });
                    }

                }
            }

            return true;
        }
    }
}