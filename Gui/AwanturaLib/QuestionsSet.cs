using System;
using System.Collections.Generic;


namespace AwanturaLib {

    class QuestionsSet {

        private Dictionary<String, List<Question>> m_questions;

        public QuestionsSet(Dictionary<String, List<Question>> questions) {

            m_questions = questions;
        }

        public static QuestionsSet ParseTextFormat(String text) {

            Dictionary<String, List<Question>> questions = new Dictionary<String, List<Question>>();
            String[] lines = text.Split(new String[]{"\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach(String line in lines) {

                String[] fields = line.Split(new String[]{"  "}, StringSplitOptions.RemoveEmptyEntries);
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
