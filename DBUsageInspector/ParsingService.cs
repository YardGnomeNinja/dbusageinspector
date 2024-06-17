using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DBUsageInspector
{
    public static class ParsingService
    {
        public static IDictionary<ReferenceObject, ReferenceObject> GetReferences(string referencerName, string referencerType, string referencerSchema, string referencerContent, IList<ReferenceObject> sqlServerObjects)
        {
            IDictionary<ReferenceObject, ReferenceObject> returnValue = new Dictionary<ReferenceObject, ReferenceObject>();

            Dictionary<string, string> relationshipTypes = new Dictionary<string, string>();
            relationshipTypes.Add("FROM", "SELECTS_FROM");
            relationshipTypes.Add("JOIN", "SELECTS_FROM");
            relationshipTypes.Add("INTO", "INSERTS_INTO");
            relationshipTypes.Add("UPDATE", "UPDATES");
            relationshipTypes.Add("DELETE FROM", "DELETES_FROM");
            relationshipTypes.Add("EXECUTE", "EXECUTES");
            relationshipTypes.Add("CALL", "CALLS");
            relationshipTypes.Add("REFERENCES", "REFERENCES");

            foreach (ReferenceObject item in sqlServerObjects)
            {
                if (referencerContent.Contains(item.Name)) // "Is there any reason to look closer?" check
                {
                    Regex itemName = new Regex(@"(FROM|JOIN|INTO|UPDATE|DELETE FROM)?\s?\(?\s?\[?\s?(\w+\.)?[^\w]" + item.Name + @"[^\w]\s?\]?\s?\)?""?");

                    MatchCollection references = itemName.Matches(referencerContent);

                    if (references.Count > 0)
                    {
                        if (item.Type == "TABLE" || item.Type == "VIEW")
                        {
                            // Iterate references and determine the type
                            foreach (Match reference in references)
                            {
                                if (reference.Groups.Count > 1 && reference.Groups[1].ToString() != string.Empty) // Object was preceeded by a recognized SQL statement
                                {
                                    string sqlStatement = reference.Groups[1].ToString();

                                    returnValue.Add(new ReferenceObject(referencerName, referencerType, relationshipTypes[sqlStatement], referencerSchema), new ReferenceObject(item.Name, item.Type, string.Empty, item.Schema));
                                }
                            }
                        }
                        else if (item.Type == "PROCEDURE")
                        {
                            returnValue.Add(new ReferenceObject(referencerName, referencerType, relationshipTypes["EXECUTE"], referencerSchema), new ReferenceObject(item.Name, item.Type, string.Empty, item.Schema));
                        }
                        else if (item.Type == "FUNCTION")
                        {
                            returnValue.Add(new ReferenceObject(referencerName, referencerType, relationshipTypes["CALL"], referencerSchema), new ReferenceObject(item.Name, item.Type, string.Empty, item.Schema));
                        }
                        else
                        {
                            // Default any referenced object not defined above
                            returnValue.Add(new ReferenceObject(referencerName, referencerType, relationshipTypes["REFERENCES"], referencerSchema), new ReferenceObject(item.Name, item.Type, string.Empty, item.Schema));
                        }
                    }
                }
            }

            return returnValue;
        }

        public static string Normalize(string input, string extension)
        {
            string returnValue = string.Empty;

            // Default to ".cs" unless otherwise specified below
            Regex comment = new Regex(@"//.*$", RegexOptions.IgnoreCase);
            Regex blockComment = new Regex(@"/\*.*\*/", RegexOptions.IgnoreCase);
            Regex blockCommentOpen = new Regex(@"/\*.*", RegexOptions.IgnoreCase);
            Regex blockCommentClose = new Regex(@"^.*\*/", RegexOptions.IgnoreCase);

            if (extension == ".vb")
            {
                comment = new Regex(@"'.*$", RegexOptions.IgnoreCase);
            }
            else if (extension == ".sql")
            {
                comment = new Regex(@"--.*$", RegexOptions.IgnoreCase);
                blockComment = new Regex(@"/\*.*\*/", RegexOptions.IgnoreCase);
                blockCommentOpen = new Regex(@"/\*.*", RegexOptions.IgnoreCase);
                blockCommentClose = new Regex(@"^.*\*/", RegexOptions.IgnoreCase);
            }

            bool isFirstLine = true;
            bool currentlyInBlockComment = false;

            //First pass: Remove comments
            using (StringReader reader = new StringReader(input))
            {
                string tempLine = string.Empty;

                do
                {
                    tempLine = reader.ReadLine();

                    if (!string.IsNullOrWhiteSpace(tempLine))
                    {
                        if (extension != ".sql" || extension == ".sql" && !isFirstLine) // skip the first line if ".sql" to avoid false positives on self references due to the object name in the definition
                        {
                            if (extension != ".vb") // VB has no block comment
                            {
                                tempLine = blockComment.Replace(tempLine, "");

                                if (currentlyInBlockComment)
                                {
                                    if (blockCommentClose.IsMatch(tempLine))
                                    {
                                        tempLine = blockCommentClose.Replace(tempLine, "");
                                        currentlyInBlockComment = false;
                                    }
                                    else
                                    {
                                        tempLine = string.Empty;
                                    }
                                }

                                if (blockCommentOpen.IsMatch(tempLine))
                                {
                                    tempLine = blockCommentOpen.Replace(tempLine, "");
                                    currentlyInBlockComment = true;
                                }
                            }

                            tempLine = comment.Replace(tempLine, "");

                            returnValue += tempLine + "\n";
                        }

                        isFirstLine = false;
                    }
                } while (tempLine != null);
            }

            // Remove these characters to create a single line string with as minimal
            Regex newLine = new Regex(@"\n+");
            Regex tab = new Regex(@"\t+");
            Regex singleQuote = new Regex(@"'");
            Regex doubleQuote = new Regex("\"");
            Regex ampersand = new Regex("&");
            Regex plus = new Regex(@"\+");
            Regex leftBracket = new Regex(@"\[");
            Regex rightBracket = new Regex(@"\]");
            Regex dbo = new Regex(@"dbo\.");
            Regex space = new Regex(@"\s+");

            returnValue = newLine.Replace(returnValue, " ");
            returnValue = tab.Replace(returnValue, " ");
            returnValue = singleQuote.Replace(returnValue, "");
            returnValue = doubleQuote.Replace(returnValue, "");
            returnValue = ampersand.Replace(returnValue, "");
            returnValue = plus.Replace(returnValue, "");
            returnValue = leftBracket.Replace(returnValue, "");
            returnValue = rightBracket.Replace(returnValue, "");
            returnValue = dbo.Replace(returnValue, "");
            returnValue = space.Replace(returnValue, " ");

            return returnValue;
        }
    }
}