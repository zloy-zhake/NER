using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace NER
{
    [Serializable()]
    public class NER_Training
    {
        // слова контекста   f1  f2  f3
        // частоты NE        n1  n1  n1

        private string buffer_for_counting;
        public List<string> factors;
        // заполняется с помощью Add_factor()
        public float[] frequencies;
        // инициализируется и расчитывается в Compute_frequencies()

        public NER_Training()
        {
            this.buffer_for_counting = "";
            this.factors = new List<string>();
        }

        public void process_corpus()
        {
            // foreach (предложение)
            //   прочитать из корпуса предложение
            //   проверить есть ли в нем <NE>
            //   если есть
            //      извлечь слово
            //      записать все остальные слова в "буфер"

            System.IO.StreamReader corpus_strem_reader = new  System.IO.StreamReader(@GlobalVars.NER_TAGGED_CORPUS);

            string line_from_corpus = "";
            string pattern = @"['\w]+<NE>"; // слово с возможным ', <NE>
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);

            while ((line_from_corpus = corpus_strem_reader.ReadLine()) != null)
            {
                if (line_from_corpus.Contains("<NE>"))
                {
                    MatchCollection matches = rgx.Matches(line_from_corpus);

                    if (matches.Count > 0)
                    {
						for (int i = matches.Count-1; i >= 0; i--) {
							line_from_corpus = line_from_corpus.Remove(matches[i].Index, matches[i].Length);
						}
                    }

                    this.buffer_for_counting += line_from_corpus + " ";
                }
            }

            this.buffer_for_counting = this.buffer_for_counting.Trim();
            while (this.buffer_for_counting.Contains("  "))
            {
                this.buffer_for_counting = this.buffer_for_counting.Replace("  ", " ");
            }
        }

        private void Add_factor(string new_factor)
        {
            this.factors.Add(new_factor);
        }

        public void Compute_frequencies()
        {
            // Разбиваем буфер в массив
            string[] tmp_array = this.buffer_for_counting.Split(' ');

            // Заполняем factors из массива
            for (int i = 0; i < tmp_array.Length; i++)
            {
                if (!this.factors.Contains(tmp_array[i]))
                    this.Add_factor(tmp_array[i]);
            }

            // инициализировать массив частот нулями
            this.frequencies = new float[this.factors.Count];

            // Посчитать количества слов контекста
            for (int i = 0; i < tmp_array.Length; i++)
            {
                this.frequencies[this.factors.IndexOf(tmp_array[i])]++;
            }

            // считаем частоты

            for (int i = 0; i < this.frequencies.Length; i++)
                this.frequencies[i] /= tmp_array.Length;
        }

        // вывод таблицы на экран
        // факторы   f1  f2  f3
        // частоты   n1  n1  n1
        public void Print_The_Table()
        {
            Console.Write("Factors:");
            foreach (string factor in this.factors)
                Console.Write(factor + ";");

            Console.Write('\n');

            Console.Write("Frequencies:");
            foreach (float freq in this.frequencies)
                Console.Write(freq + ";");

            Console.Write('\n');
        }
    }
}