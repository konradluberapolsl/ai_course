using Accord.MachineLearning.Bayes;
using Accord.Math;
using Accord.Statistics.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyPoints_Bayes
{
    class Bayes
    {
        private DataTable data;
        private string[] headers;
        private string headerToPredict;
        private Codification codeBook;
        private NaiveBayes nativeBayes;

        public void LoadData(string path)
        {
            data = new DataTable();
            StreamReader streamReader = new StreamReader(path);
            headers = streamReader.ReadLine().Split(';');

            foreach (var header in headers)
            {
                data.Columns.Add(header);
            }

            while (!streamReader.EndOfStream)
            {
                string[] rows = streamReader.ReadLine().Split(';');
                DataRow dataRow = data.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dataRow[i] = rows[i];
                }
                data.Rows.Add(dataRow);
                codeBook = new Codification(data, headers);
            }
            headerToPredict = headers[3];
            headers = headers.RemoveAt(headers.Length - 1);
        }
        public void Learn()
        {
            DataTable symbols = codeBook.Apply(data);
            int[][] inputs = symbols.ToJagged<int>(headers);
            int[] outputs = symbols.ToArray<int>(headerToPredict);

            var learner = new NaiveBayesLearning();
            nativeBayes = learner.Learn(inputs, outputs);
        }
        public void Predict(params string[] args)
        {
            int[] instance;
            try
            {
                instance = codeBook.Transform(args);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return;
            }

            int c = nativeBayes.Decide(instance);
            string result = codeBook.Revert(headerToPredict, c);
            System.Console.WriteLine(result);

            double[] probs = nativeBayes.Probabilities(instance);

            foreach (var item in probs)
            {
                System.Console.WriteLine(item);
            }
        }


    }
}
