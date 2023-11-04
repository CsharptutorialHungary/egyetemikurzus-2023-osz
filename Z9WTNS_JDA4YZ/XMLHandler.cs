using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Z9WTNS_JDA4YZ
{
    internal static class XMLHandler
    {
        internal static bool InitializeXMLData(string filePath)
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
                    File.Create(filePath);
                }

                return true;
            }
            catch(IOException exception)
            {
                Console.WriteLine(exception.Message);

                return false;
            }
        }

        internal static List<Type>? readObjectsFromXML<Type>(string filePath) 
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Type));

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    var deserialized = (List<Type>)serializer.Deserialize(reader)!;

                    return deserialized == null ? new List<Type>() : deserialized;
                }
            }
            catch(IOException exception)
            {
                Console.WriteLine(exception.Message);

                return null;
            }
        }
        internal static bool? writeObjectsToXML<Type>(string filePath, List<Type> data)
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

   