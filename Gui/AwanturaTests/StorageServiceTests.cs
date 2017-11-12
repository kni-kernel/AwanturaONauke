using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AwanturaLib;

namespace AwanturaTests {

    [TestClass]
    public class StorageServiceTests {

        [TestMethod]
        public void ParseTextFileTest() {

            StorageService ss = new StorageService();

            QuestionsSet qs =  ss.ParseTextFile(
                @"..\..\TestsData\pytania.tsv",
                QuestionsSet.TextFormatParser);

            Assert.AreEqual(qs.Questions.ContainsKey("Gramatyka i ortografia"), true);
        }

        [TestMethod]
        public void SerializeAndDeserializeXMLTest() {

            StorageService ss = new StorageService();

            Dictionary<String, List<Question>> questions =
                new Dictionary<String, List<Question>>();
            questions["Algebra liniowa"] = new List<Question>();

            Question question = new Question();
            question.Content = "Czy zdam egzamin w pierwszym terminie?";
            question.Tip1 = "1";
            question.Tip2 = "2";
            question.Tip3 = "3";
            question.Tip4 = "4";
            question.Answear = "1";
            question.Category = new Category();
            question.FileName = "ziomek.png";
            question.Used = true;

            questions["Algebra liniowa"].Add(question);

            QuestionsSet qs = new QuestionsSet(questions);
            
            String testFilePath = @"..\..\TestsData\XML_TEST.xml";
            ss.SerializeToXMLFile(qs, testFilePath);
            QuestionsSet qsCheck = ss.DeserializeFromXMLFile<QuestionsSet>(testFilePath);

            Assert.AreEqual(qs, qsCheck);
        }
    }
}
