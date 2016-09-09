using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace DBUsageInspector
{
    class FileService
    {
        private string _rootPath;
        public DirectoryInfo RootDirectory;
        public HashSet<string> Extensions;

        public string RootPath
        {
            get { return _rootPath; }

            set
            {
                _rootPath = value;
                RootDirectory = new DirectoryInfo(_rootPath);
            }
        }

        public FileService(string rootPath, HashSet<string> extensions)
        {
            RootPath = rootPath;
            Extensions = extensions;
        }

        public List<ReferenceObject> GetSqlScriptObjects()
        {
            List<ReferenceObject> returnValue = new List<ReferenceObject>();
            List<FileInfo> files = new List<FileInfo>();

            files = GetFiles(RootDirectory);
            returnValue = GetSqlScriptCreateLines(files);

            return returnValue;
        }

        private List<FileInfo> GetFiles(DirectoryInfo folder)
        {
            List<FileInfo> returnValue = new List<FileInfo>();

            // Iterate all files from the root
            foreach (FileInfo file in folder.GetFiles("*", SearchOption.AllDirectories))
            {
                // Iterate all extensions for service
                foreach (string extension in Extensions)
                {
                    if (file.Extension == extension)
                    {
                        // Add file and return to iterating through files
                        returnValue.Add(file);
                        break;
                    }
                }
            }

            return returnValue;
        }

        private List<ReferenceObject> GetSqlScriptCreateLines(List<FileInfo> files)
        {
            List<ReferenceObject> returnValue = new List<ReferenceObject>();
            Regex regEx = new Regex(@"create\s(table|view|procedure|function|synonym)\s([\[a-z0-9_#\]\.]+)\s?", RegexOptions.IgnoreCase);

            foreach (FileInfo file in files)
            {
                using (StreamReader streamReader = file.OpenText())
                {
                    while (!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine();

                        Match match = regEx.Match(line);

                        if (match.Captures.Count > 0)
                        {
                            string name = match.Groups[2].Value.Replace("[dbo].", "").Replace("dbo.", "").Replace("[", "").Replace("]", "").Replace(".", "");

                            returnValue.Add(new ReferenceObject(name, match.Groups[1].Value.ToUpper(), string.Empty));

                            streamReader.Close();
                            break;
                        }
                    }
                }
            }

            returnValue.Sort();

            return returnValue;
        }

        public IDictionary<ReferenceObject, ReferenceObject> GetReferencesToSqlServerObjects(IList<ReferenceObject> sqlServerObjects)
        {
            IDictionary<ReferenceObject, ReferenceObject> returnValue = new Dictionary<ReferenceObject, ReferenceObject>();

            List<FileInfo> files = GetFiles(RootDirectory);

            foreach (FileInfo originalFile in files)
            {
                string fileContent = originalFile.OpenText().ReadToEnd();
                string cleanContent = ParsingService.Normalize(fileContent, originalFile.Extension);

                foreach (KeyValuePair<ReferenceObject, ReferenceObject> objectPair in ParsingService.GetReferences(originalFile.FullName, "CODE", cleanContent, sqlServerObjects))
                {
                    returnValue.Add(objectPair.Key, objectPair.Value);
                }
            }

            return returnValue;
        }

        public void SaveReferenceObjects(IDictionary<ReferenceObject, ReferenceObject> references)
        {
            FileInfo file = new FileInfo("./references.dbui");

            if (file.Exists)
            {
                file.Delete();
            }

            using (TextWriter textWriter = file.CreateText())
            {
                foreach (KeyValuePair<ReferenceObject, ReferenceObject> reference in references)
                {
                    string json = JsonConvert.SerializeObject(reference);

                    textWriter.WriteLine(json);
                }
            }
        }

        public IDictionary<ReferenceObject, ReferenceObject> LoadReferenceObjects()
        {
            IDictionary<ReferenceObject, ReferenceObject> returnValue = new Dictionary<ReferenceObject, ReferenceObject>();

            FileInfo file = new FileInfo("./references.dbui");

            if (file.Exists)
            {
                using (StreamReader streamReader = file.OpenText())
                {
                    while (!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine();

                        if(line.Trim() != string.Empty)
                        {
                            KeyValuePair<ReferenceObject, ReferenceObject> referenceObject = new KeyValuePair<ReferenceObject, ReferenceObject>();
                            referenceObject = (KeyValuePair<ReferenceObject, ReferenceObject>)JsonConvert.DeserializeObject(line, referenceObject.GetType());

                            returnValue.Add(referenceObject);
                        }
                    }
                }
            }

            return returnValue;
        }
    }
}