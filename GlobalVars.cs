namespace NER
{
    static class GlobalVars
    {
        // корпус в нижнем регистре, очищенный от знаков препинания
        public static string NER_TAGGED_CORPUS = "ner_tagged_corpus";
        // файл для хранения частот
        public static string NER_FREQUENCIES_FILE = "ner_frequencies";
		public static float THRESHOLD_SCORE = 0.23f;
    }
}

