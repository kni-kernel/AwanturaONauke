using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ExtendedXmlSerialization;


namespace AwanturaLib {

    public class StorageService {

        public T ParseTextFile<T>(String path, Func<String, T> Parser) where T : class {

            T parsedObject = null;

            try {
                using(StreamReader reader = new StreamReader(path)) {
                    
                    String text = reader.ReadToEnd();
                    parsedObject = Parser(text);
                }
            }
            catch(IOException e) {

                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return parsedObject;
        }

        public String SerializeToXMLFile<T>(T o, String path) where T : class {

            var xsSubmit = new ExtendedXmlSerializer();
            var xml = "";

            using(var sww = new StringWriter()) {
                using(XmlWriter writer = XmlWriter.Create(sww)) {

                    xml = xsSubmit.Serialize(o);
                    Console.WriteLine(xml);
                }
            }

            if(path != null) {
                try {
                    File.WriteAllText(path, xml);
                }
                catch (IOException e) {

                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine(xml);
            return xml;
        }

        public T DeserializeFromXMLFile<T>(String path) where T : class {

            T deserializedObject = null;

            try {
                using (StreamReader reader = new StreamReader(path)) {

                    var xml = reader.ReadToEnd();
                    var deserializer = new ExtendedXmlSerializer();
                    deserializedObject = deserializer.Deserialize<T>(xml);
                }
            }
            catch (IOException e) {

                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return deserializedObject;
        }
    }
}
