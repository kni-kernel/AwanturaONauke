using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace AwanturaLib {

    public class StorageService {

        public void ParseTextFile<T>(ref T o, String path, Func<String, T> Parser) {

            try {
                using(StreamReader reader = new StreamReader(path)) {
                    
                    String text = reader.ReadToEnd();
                    o = Parser(text);
                }
            }
            catch(IOException e) {

                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public String  SerializeToXML<T>(T o, String path) {

            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            var xml = "";
            
            using(var sww = new StringWriter()) {
                using(XmlWriter writer = XmlWriter.Create(sww)) {

                    xsSubmit.Serialize(writer, o);
                    xml = sww.ToString();
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

            return xml;
        }

        public T DeserializeFromXML<T>(String path) where T : class {

            using(var stream = System.IO.File.OpenRead(path)) {

                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stream) as T;
            }
        }
    }
}
