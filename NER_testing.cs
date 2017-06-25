using System;

namespace NER
{
    // enter text
    // process text by sentence
    // perform NER on every word of every sentence
    // output all sentences

    public class NER_testing
    {
		public string input_text;
		public string output_text;

        public NER_testing()
        {
        	this.input_text = "";
        	this.output_text = "";
        }

        public void Read_Input()
        {
        	this.input_text = Console.ReadLine();
        }

        public void Write_Output()
        {
        	Console.WriteLine(this.output_text);
        }

		public bool is_Named_Entity(string word_to_be_checked, string[] context_words, NER_Training table)
        {
        	float score = 0;
        	foreach (string word in context_words) 
        	{
        		if (table.factors.Contains(word)) 
        		{
        			score += table.frequencies[table.factors.IndexOf(word)];
        		}
        	}
			if (score > GlobalVars.THRESHOLD_SCORE) 
        	{
        		return true;
        	}
        	else
        	{
        		return false;
        	}
        }

//		public void Process_Text()
//		{
//			string[] x = this.input_text.Split (' ');
//
//			List<string> words;
//
//			foreach (string item in x) {
//				words.Add (item);
//			}
//
//			List<string> tmp_words;
//
//			foreach (string word in words) {
//				words.CopyTo (tmp_words);
//				tmp_words.Remove (word);
//				if (this.is_Named_Entity(word, tmp_words, ) {
//					
//				}
//				this.output_text += word;
//			}
//
//
//		}
    }
}

