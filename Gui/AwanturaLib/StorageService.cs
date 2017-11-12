using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


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
                    PrintWriter output = new PrintWriter(
                        new BufferedWriter(
                            new FileWriter(path, true)));
                    output.println(xml);
                    output.close();
                }
                catch (IOException e) {

                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }

            return xml;
        }

        public T DeserializeFromXML<T>(String path) {

            using(var stream = System.IO.File.OpenRead(path)) {

                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stream) as T;
            }
        }
    }
}
