using System.Xml;
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
            XmlSerializer serializer = new XmlSerializer(typeof(List<Type>));

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    var deserialized = reader.Peek() == -1 ? new List<Type>() : serializer.Deserialize(reader);

                    return deserialized == null ? new List<Type>() : (List<Type>)deserialized;
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
                List<Type> objects = ReadObjectsFromXml<Type>(filePath);

                foreach (Type obj in data)
                {
                    objects.Add(obj);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(List<Type>));

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, objects);
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