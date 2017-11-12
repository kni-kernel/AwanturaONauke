using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwanturaLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AwanturaTests {

    [TestClass]
    public class StorageServiceTests {

        [TestMethod]
        public void ParseTextFileTest() {

            StorageService ss = new StorageService();
            QuestionsSet qs = null;
            ss.ParseTextFile(ref qs,
                @"C:\Users\Krzysztof\Desktop\tmp\awantura o kase\AwanturaSterownik\data\pytania.tsv",
                QuestionsSet.TextFormatParser);

            Console.WriteLine(qs.Questions);

            Assert.AreEqual(qs.Questions.ContainsKey("Gramatyka i ortografia"), true);
        }
    }
}
