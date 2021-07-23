using System;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace RockScissorsPaper
{
    class Program
    {
        private static int GenerateRandomInt32Value(RandomNumberGenerator randomNumberGenerator)
        {
            var RandomBytes = new byte[16]; // 16 bytes = 128 bits 
            randomNumberGenerator.GetBytes(RandomBytes);
            var randomInt32Value = BitConverter.ToInt32(RandomBytes, 0);
            return randomInt32Value;
        }
        static string[] firstvalues(string[] values)
        {
            int even = values.Length;
            if (even % 2 == 1)
            { return values; }
            else
            {
                Console.WriteLine("Количество вариантов должно быть нечетным, введите новые значения");
                string stroka = Console.ReadLine();
                values = stroka.Split(" ");
                if (values.Length % 2 == 0)
                {
                    firstvalues(values);
                }
                return values;
            }
        }
        static void checkvalues(string[] enteredShapes)
        {
            int shapesCount = enteredShapes.Length;
            Array.Sort(enteredShapes);
            if (shapesCount == 1)
            {
                Console.WriteLine("Количество должно быть больше 1");
                string[] qwe = new string[3];
                EnterNewValues(qwe);
            }
            for (int i = 0; i < shapesCount - 1; i++)
            {
                
                if (enteredShapes[i] == enteredShapes[i + 1])
                {
                    Console.WriteLine("Значения не должны повторяться");
                    EnterNewValues(enteredShapes);
                    break;
                }
                else
                    continue;
            }


            return;
        }
        static void EnterNewValues(string[] NewShapes)
        {
            int shapesCount = NewShapes.Length;
           
            for (int i = 0; i < shapesCount; i++)
            {
                NewShapes[i] = Console.ReadLine();          // ввозможно стоит разделить массив строк
            }
            checkvalues(NewShapes);

            return;

            //Console.WriteLine("Количество вариантов должно быть нечетным, введите новые значения");
            //string stroka = Console.ReadLine();
            //NewShapes = stroka.Split(" ");
            //if (NewShapes.Length % 2 == 0)
            //{
            //    firstvalues(NewShapes);
            //}
            //return NewShapes;

        }


        static int chooseFighter(string[] shapes)
        {
            Console.WriteLine("Выберите вариант ответа.");
            int answer;
            try
            {
                answer = Convert.ToInt32(Console.ReadLine());
                if (answer >= 0 && answer <= (shapes.Length - 1))
                {
                    return answer;

                }
                else
                {
                    Console.WriteLine("Введенно неверное значение. Введите число от 0 до " + (shapes.Length - 1));
                  answer=  chooseFighter(shapes);
                    return answer;
                }
        }
            catch 
            {
                Console.WriteLine("Введенно неверное значение. Введите число от 0 до " + (shapes.Length-1));
                answer = chooseFighter(shapes);
                return answer;
            }

}
        public static string HmacSha256(string message,byte [] key)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] messageBytes = encoding.GetBytes(message);
            HMACSHA256 cryptographer = new HMACSHA256(key);
            byte[] bytes = cryptographer.ComputeHash(messageBytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
        private static byte[] GenerateKey()
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                var key = new byte[16];
                generator.GetBytes(key);
                return key;
            }
        }
        static void Main(string[] args)
        {
            string[] newlist = firstvalues(args);
            int shapesCount = newlist.Length;

            checkvalues(newlist);
            Random rand = new Random();
            int PC_answer = rand.Next(0, newlist.Length);
            string answer = PC_answer.ToString();
            byte[] secretkey = GenerateKey();
            byte[] mess = Encoding.Default.GetBytes(PC_answer.ToString());
            Console.WriteLine(HmacSha256(answer, secretkey));


            for (int i = 0; i < shapesCount; i++)
            {
                Console.WriteLine(i + ". " + newlist[i].ToString());
            }
            int choosenOne = chooseFighter(newlist);

            Console.WriteLine("Вы выбрали " + newlist[choosenOne]);
            Console.WriteLine("Ход противника:" + newlist[PC_answer]);
            Console.WriteLine(BitConverter.ToString(secretkey).Replace("-", "").ToLower());
            string[] WinCondition = new string[(newlist.Length - 1) / 2];
            int WinAnswerCounter = 0;
            for (int i = PC_answer+1; i <= newlist.Length+1; i++)
            {

                if (i == newlist.Length)
                {
                    i = 0;
                }
                WinCondition[WinAnswerCounter] = newlist[i];
                WinAnswerCounter++;
                if (WinAnswerCounter == (newlist.Length - 1) / 2)
                    break;
            }

            string answer1 = "";
            for (int i = 0; i < WinCondition.Length; i++)
            {

                if (PC_answer == choosenOne)
                {
                    answer1="Draw";
                    break;
                }
                if (newlist[choosenOne] == WinCondition[i])
                {
                    answer1 = "Win";
                    break;
                }
                else 
                {
                    answer1 = "Lose";

                }
            }

            Console.WriteLine(answer1 + "\n Введите Y если хотите продолжить.");
            if (Console.ReadLine() == "Y" || Console.ReadLine() == "y")
            { Main(args); }

        }
    }
}
