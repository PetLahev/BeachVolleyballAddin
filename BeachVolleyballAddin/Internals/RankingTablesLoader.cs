using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Xml;

namespace BeachVolleyballAddin.Internals
{
    internal class RankingTablesLoader
    {
        readonly static string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                     Properties.Settings.Default.AppFolderName, "RankingTables");

        internal RankingTablesLoader()
        {
            if (!Directory.Exists(appDataFolder)) Directory.CreateDirectory(appDataFolder);
        }

        internal void SaveRankingTable(string link, List<Data.RankingTable> table)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Data.RankingTable>));
                using (var sww = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        serializer.Serialize(writer, table);
                        var xml = sww.ToString();
                        var xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xml);
                        SaveFile(link, xmlDoc);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal List<Data.RankingTable> LoadRankingTable(string link)
        {
            try
            {
                var fullPath = GetFullFileName(link);
                if (!File.Exists(fullPath)) return null;

                XmlSerializer serializer = new XmlSerializer(typeof(List<Data.RankingTable>));
                using (var swr = new StreamReader(fullPath))
                using (XmlReader reader = XmlReader.Create(swr))
                {
                    return (List<Data.RankingTable>)serializer.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SaveFile(string link, XmlDocument doc)
        {
            var fullPath = GetFullFileName(link);
            if (File.Exists(fullPath)) File.Delete(fullPath);
            doc.Save(fullPath);
        }

        private byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        private string GetFullFileName(string link)
        {
            var fileName = GetFileNameFromLink(link);
            return Path.Combine(appDataFolder, fileName);
        }

        private string GetFileNameFromLink(string link)
        {
            return GetHashString(link) + ".xml";
        }
    }
}
