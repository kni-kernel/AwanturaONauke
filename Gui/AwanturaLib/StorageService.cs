using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace AwanturaLib {

    class StorageService {

        public void ParseTextFile<T>(ref T o, String path, Func<String, T> Parser) {

            try {

                using(StreamReader reader = new StreamReader(path)) {
                    
                    String text = reader.ReadToEnd();
                    Console.WriteLine(text);
                    o = Parser(text);
                }
            }
            catch(Exception e) {

                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public void ParseXMLFile<T>(ref T o, String path) {

        }
    }
}
