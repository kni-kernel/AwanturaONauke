using System;
using System.Collections.Generic;


namespace AwanturaLib {

    [Serializable]
    public class QuestionsSet {
        private static QuestionsSet current;
        public static QuestionsSet Current
        {
            get
            {
                if (current != null)
                    return current;

                return current = new StorageService().DeserializeFromXMLFile<QuestionsSet>("pytania.xml");
            }
        }

        public Dictionary<String, List<Question>> Questions { get; set; }

        public QuestionsSet() { }

        public QuestionsSet(Dictionary<String, List<Question>> questions) {

            Questions = questions;
        }

        public static QuestionsSet TextFormatParser(String text) {

            Dictionary<String, List<Question>> questions = new Dictionary<String, List<Question>>();
            String[] lines = text.Split(new String[]{"\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach(String line in lines) {

                String[] fields = line.Split(new String[]{"\t"}, StringSplitOptions.RemoveEmptyEntries);
                if(fields.Length < 6) continue;

                Question question = new Question();
                question.Content = fields[1];
                question.Tip1 = fields[2];
                question.Tip2 = fields[3];
                question.Tip3 = fields[4];
                question.Tip4 = fields[5];

                if(!questions.ContainsKey(fields[0]))
                    questions[fields[0]] = new List<Question>();
                questions[fields[0]].Add(question);
            }

            return new QuestionsSet(questions);
        }
    }
}
