﻿using Neo4j.Driver.V1;
using System.Collections.Generic;

namespace DBUsageInspector
{
    internal class Neo4jService
    {
        public string Password;
        public string Url;
        public string Username;

        public Neo4jService()
        {
        }

        public Neo4jService(string url, string username, string password)
        {
            Url = url;
            Username = username;
            Password = password;
        }

        public void CreateObjects(IEnumerable<ReferenceObject> sqlObjects)
        {
            using (var driver = GraphDatabase.Driver(Url, AuthTokens.Basic(Username, Password)))
            {
                using (var session = driver.Session())
                {
                    foreach (ReferenceObject sqlObject in sqlObjects)
                    {
                        session.Run("CREATE (a:" + sqlObject.Type.ToUpper() + " {name:'" + sqlObject.Name.Replace("\\", "\\\\") + "', schema:'" + sqlObject.Schema.Replace("\\", "\\\\") + "'})");
                    }
                }
            }
        }

        public void CreateRelationships(IDictionary<ReferenceObject, ReferenceObject> references)
        {
            string query = string.Empty;

            using (var driver = GraphDatabase.Driver(Url, AuthTokens.Basic(Username, Password)))
            {
                using (var session = driver.Session())
                {
                    foreach (KeyValuePair<ReferenceObject, ReferenceObject> reference in references)
                    {
                        string relationship = (reference.Key.Relationship == string.Empty ? "REFERENCES" : reference.Key.Relationship); // If no relationship is defined, default to "REFERENCES"
                        query = "MATCH (a:" + reference.Key.Type + "), (b:" + reference.Value.Type + ") WHERE a.name =~ '" + reference.Key.Name.Replace("\\", "\\\\\\\\") + "' AND b.name =~ '" + reference.Value.Name.Replace("\\", "\\\\\\\\") + "' CREATE (a)-[:" + relationship + "]->(b)";
                        session.Run(query);
                    }
                }
            }
        }
    }
}