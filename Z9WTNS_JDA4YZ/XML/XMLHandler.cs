using System.Xml.Serialization;
using Z9WTNS_JDA4YZ.DataClasses;

namespace Z9WTNS_JDA4YZ.Xml
{
    internal static class XmlHandler
    {
        internal static bool InitializeXmlData(string filePath)
        {
            try
            {
                string? directoryPath = Path.GetDirectoryName(filePath);

                if (directoryPath == null) return false;

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                }

                return true;
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception.Message);

                return false;
            }
        }

        internal static List<Type> ReadObjectsFromXml<Type>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Type));

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    var deserialized = reader.Peek() == -1 ? new List<Type>() : (List<Type>)serializer.Deserialize(reader)!;

                    return deserialized == null ? new List<Type>() : deserialized;
                }
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception.Message);
                return new List<Type>();
            }
        }

        internal static bool AppendObjectsToXml<Type>(string filePath, List<Type> data)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Type>));

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    serializer.Serialize(writer, data);
                    return true;
                }
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }
    }
}