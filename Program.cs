using System;

namespace neural_network
{
    // и сделать по нормальному массивы и классы
    class Program
    {
        static readonly Random random = new Random();
        static public int answer;
        static public double Activation(double x)
        {
            return x / (1 + Math.Abs(2 * x)) + 0.5;
        }

        static public double Error(double answer, double guess)
        {
            return Math.Pow((answer - guess), 2);
        }

        public class InputNeuron
        {
            public double output;
            public double weight1;
            public double exDeltaWeight1;
            public double deltaWeight1;
            public double grad1;
            public double weight2;
            public double deltaWeight2;
            public double exDeltaWeight2;
            public double grad2;
        }

        public class HidenNeuron
        {
            public double weight;
            public double exDeltaWeight;
            public double deltaWeight;
            public double input;
            public double output;
            public double grad;
        }

        public class OutputNeuron
        {
            public double delta;
            public double input;
            public double output;
        }

        public static InputNeuron inputNeuron1 = new InputNeuron();
        public static InputNeuron inputNeuron2 = new InputNeuron();
        public static HidenNeuron hiddenNeuron1 = new HidenNeuron();
        public static HidenNeuron hiddenNeuron2 = new HidenNeuron();
        public static OutputNeuron outputNeuron = new OutputNeuron();

        public static double input1;
        public static double input2;

        static void Assigment()
        {
            inputNeuron1.output = input1;
            inputNeuron2.output = input2;
            hiddenNeuron1.input = inputNeuron1.output * inputNeuron1.weight1 + inputNeuron2.output * inputNeuron1.weight2;
            hiddenNeuron2.input = inputNeuron2.output * inputNeuron2.weight1 + inputNeuron2.output * inputNeuron2.weight2;
            hiddenNeuron1.output = Activation(hiddenNeuron1.input);
            hiddenNeuron2.output = Activation(hiddenNeuron2.input);
            outputNeuron.input = hiddenNeuron1.output * hiddenNeuron1.weight + hiddenNeuron2.output * hiddenNeuron2.weight;
            outputNeuron.output = Activation(outputNeuron.input);

            hiddenNeuron1.exDeltaWeight = hiddenNeuron1.deltaWeight;
            hiddenNeuron2.exDeltaWeight = hiddenNeuron2.deltaWeight;

            inputNeuron1.exDeltaWeight1 = inputNeuron1.deltaWeight1;
            inputNeuron1.exDeltaWeight2 = inputNeuron1.deltaWeight2;

            inputNeuron2.exDeltaWeight1 = inputNeuron2.deltaWeight1;
            inputNeuron2.exDeltaWeight2 = inputNeuron2.deltaWeight2;
        }
        static void Teaching()
        {


            double speed = 0.7;
            double moment = 0.3;

            outputNeuron.delta = (1 - outputNeuron.output) * ((answer - outputNeuron.output) * answer);

            hiddenNeuron1.deltaWeight = ((1 - hiddenNeuron1.output) * hiddenNeuron1.output) * (hiddenNeuron1.weight * outputNeuron.delta);
            hiddenNeuron2.deltaWeight = ((1 - hiddenNeuron2.output) * hiddenNeuron2.output) * (hiddenNeuron2.weight * outputNeuron.delta);

            hiddenNeuron1.grad = hiddenNeuron1.output * outputNeuron.delta;
            hiddenNeuron2.grad = hiddenNeuron2.output * outputNeuron.delta;

            hiddenNeuron1.deltaWeight = speed * hiddenNeuron1.grad + hiddenNeuron1.exDeltaWeight * moment;
            hiddenNeuron2.deltaWeight = speed * hiddenNeuron2.grad + hiddenNeuron2.exDeltaWeight * moment;

            hiddenNeuron1.weight = hiddenNeuron1.weight + hiddenNeuron1.deltaWeight;
            hiddenNeuron2.weight = hiddenNeuron2.weight + hiddenNeuron2.deltaWeight;

            // разделить у InputNeuron input и output

            inputNeuron1.grad1 = inputNeuron1.output * hiddenNeuron1.deltaWeight;
            inputNeuron1.grad2 = inputNeuron1.output * hiddenNeuron2.deltaWeight;
            inputNeuron2.grad1 = inputNeuron2.output * hiddenNeuron1.deltaWeight;
            inputNeuron2.grad2 = inputNeuron2.output * hiddenNeuron2.deltaWeight;

            inputNeuron1.deltaWeight1 = speed * inputNeuron1.grad1 + inputNeuron1.exDeltaWeight1 * moment;
            inputNeuron1.deltaWeight2 = speed * inputNeuron1.grad2 + inputNeuron1.exDeltaWeight2 * moment;
            inputNeuron2.deltaWeight1 = speed * inputNeuron2.grad1 + inputNeuron2.exDeltaWeight1 * moment;
            inputNeuron2.deltaWeight2 = speed * inputNeuron2.grad2 + inputNeuron2.exDeltaWeight2 * moment;

            inputNeuron1.weight1 = inputNeuron1.weight1 + inputNeuron1.deltaWeight1;
            inputNeuron1.weight2 = inputNeuron1.weight2 + inputNeuron1.deltaWeight2;
            inputNeuron2.weight1 = inputNeuron2.weight1 + inputNeuron2.deltaWeight1;
            inputNeuron2.weight2 = inputNeuron2.weight2 + inputNeuron2.deltaWeight2;
        }
        static void Learning()
        {
            Teaching();
            int maxEpoch = 1000000;
            int j = 0;
            for (int i = 0; i < maxEpoch; i++)
            {
                if (j == 2)
                {
                    j = 0;
                }
                else
                {
                    j++;
                }

                if (j == 0)
                {
                    input1 = 0;
                    input2 = 0;
                    answer = 0;
                }
                else if (j == 1)
                {
                    input1 = 1;
                    input2 = 1;
                    answer = 0;
                }
                else if (j == 2)
                {
                    input1 = 1;
                    input2 = 0;
                    answer = 1;
                }
                Assigment();
                Teaching();

                //Console.WriteLine(j + ")");
                //Console.WriteLine("input 1 = " + input1 + " input 2 = " + input2);
                //Console.WriteLine("answer = " + answer);
                //Console.WriteLine("output = " + outputNeuron.output);
                //Console.WriteLine("error = " + Error(answer, outputNeuron.output));
                //Console.WriteLine(" ");
            }
        }

        static void Main(string[] args)
        {
            inputNeuron1.weight1 = random.NextDouble();
            inputNeuron1.weight2 = random.NextDouble();
            inputNeuron2.weight1 = random.NextDouble();
            inputNeuron2.weight2 = random.NextDouble();
            hiddenNeuron1.weight = random.NextDouble();
            hiddenNeuron2.weight = random.NextDouble();
            inputNeuron1.output = 0;
            inputNeuron2.output = 1;
            Assigment();
            Console.WriteLine("output = " + outputNeuron.output);
            Console.WriteLine("error = " + Error(1, outputNeuron.output));
            Console.WriteLine("");
            Console.WriteLine("Wait . . .");
            Console.WriteLine("");
            System.Threading.Thread.Sleep(349);
            Learning();
            inputNeuron1.output = 0;
            inputNeuron2.output = 1;
            Assigment();
            Console.WriteLine("output = " + outputNeuron.output);
            Console.WriteLine("error = " + Error(1, outputNeuron.output));
            Console.Write(answer);
            Console.ReadKey();
        }
    }
}