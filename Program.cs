using System;
using System.Collections.Generic;

namespace NER
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// тренировка модели
			NER_Training ner_training_obj = new NER_Training ();

			ner_training_obj.process_corpus ();
			ner_training_obj.Compute_frequencies ();
			ner_training_obj.Print_The_Table ();

			// использование натренированной модели
			NER_testing ner_testing_obj = new NER_testing ();

			ner_testing_obj.Read_Input();

			string[] x = ner_testing_obj.input_text.Split (' ');

			List<string> words = new List<string>();

			foreach (string item in x) {
				words.Add (item);
			}

			List<string> tmp_words = new List<string>();

			foreach (string word in words)
			{
				tmp_words.Clear ();

				foreach (string item in words)
				{
					tmp_words.Add (item);
				}

				tmp_words.Remove (word);
				if (ner_testing_obj.is_Named_Entity (word, tmp_words.ToArray(), ner_training_obj)) {
					ner_testing_obj.output_text += word + "<NE>" + " ";
				} else {
					ner_testing_obj.output_text += word + " ";
				};
			}

			ner_testing_obj.output_text.Trim ();
			Console.WriteLine (ner_testing_obj.output_text);

			Console.Read ();
		}
	}
}
