using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        //TODO:
        static void Main(string[] args)
        {
            /*
            //SynchronousStreamCipher("test3.bin", "output3.bin", "11100", "10110");
            CiphertextAutokeyEncrypt("test3.bin", "output3.bin", "11100", "10110");
            Console.ReadKey();
            //SynchronousStreamCipher("output3.bin", "wynik3.bin", "11100", "10110");
            CiphertextAutokeyDecrypt("output3.bin", "wynik3.bin", "11100", "10110");
            Console.ReadKey();
            //PrintBinaryFileContentBitByBit("output3.bin");
            CreateTxtFileFromBinaryBitByBit("test3.bin", "test3.txt");
            CreateTxtFileFromBinaryBitByBit("wynik3.bin", "wynik3.txt");
            Console.ReadKey();
            */

            //po za tym że zadanie 1, 2, 3, to jeszcze dobrze żeby były opcje na konwersji binarnego pliku na tekstowy, tekstowego na binarny i wypisania zawartosci binarnego 

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. ZADANIE 1 - Random Generator");
                Console.WriteLine("2. ZADANIE 2 - Synchronous Stream Cipher");
                Console.WriteLine("3. ZADANIE 3 - Cipher Text Autokey");
                Console.WriteLine("4. Obsługa plików");

                String c = Console.ReadLine();
                switch (c)
                {
                    case "1":
                        Zadanie1();
                        Console.WriteLine("Naciśnij klawisz aby kontynuować...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Zadanie2();
                        break;
                    case "3":
                        Zadanie3();
                        break;
                    case "4":
                        ObslugaPlikow();
                        break;
                    default:
                        break;
                }
            }

        }

        //--------------------------------------------------------- ZADANIE 1 - Random Generator ------------------------------------------------------------------
        //TODO:
        static void Zadanie1()
        {
            Console.WriteLine("Podaj funkcję wielomianową w postaci zero-jedynkowej (np. 101):");
            string function = Console.ReadLine();

            Console.WriteLine("Podaj ziarno (np 110):");
            string seed = Console.ReadLine();

            Console.WriteLine("Podaj ilość kolejnych generacji n (np. 4):");
            int n = int.Parse(Console.ReadLine());

            RandomGenerator(function, seed, n);
        }

        static void RandomGenerator(string function, string seed, int n)
        {

            if (function.Length != seed.Length)
            {
                Console.WriteLine("Błąd! Seed musi być tej samej długości co funkcja.");
                Console.ReadKey();
                return;
            }

            int i, tmp;

            int[] actualMemo = new int[seed.Length];
            List<int> indexesToXOR = new List<int>();
            for (i = 0; i < function.Length; i++)
            {
                if (function[i] == '1')
                {
                    indexesToXOR.Add(i);
                }
            }

            for (i = 0; i < actualMemo.Length; i++)
            {
                actualMemo[i] = (int)Char.GetNumericValue(seed, i);
            }


            for (i = 0; i < n; i++)
            {
                Console.WriteLine(string.Join(" ", actualMemo));
                tmp = 0;
                foreach (int indx in indexesToXOR)
                {
                    tmp ^= actualMemo[indx];
                }
                Array.Copy(actualMemo, 0, actualMemo, 1, actualMemo.Length - 1);
                actualMemo[0] = tmp;
            }
        }

        //--------------------------------------------------------- ZADANIE 2 - Synchronous Stream Cipher ---------------------------------------------------------
        //TODO:
        static void Zadanie2()
        {
            //wybor czy z pliku do pliku (to ewentualnie beda wykorzystywane albo funkcje albo funkcje_file)
            //tutaj pewnie jakies czy chcesz zakodować czy odkodowac
            //    //przy zakodowaniu: chcesz 1. test.bin; 2. test2.bin ; 3. test3.bin ; 4. inny (mozliwosc podaniawlasnej nazwy)
            //        //nastepnie nazwa pliku wyjsciowego
            //    //przy odkodowaniu: podaj nazwe pliku zakodowanego i nazwe pliku wyjsciowego

            //^ przy tych oczywiscie tez pytanie o jakis seed i funkcje
            //^^ do zakodowywania i rozkodowywania bedzie ten sam algorytm wykorzystywany/funkcja czyli SynchronousStreamCipher
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Zadanie 2 - Synchronous Stream Cipher");
                Console.WriteLine("1. Konsolowo");
                Console.WriteLine("2. Z pliku binarnego");
                Console.WriteLine("3. Powrót");
                String c = Console.ReadLine();

                switch (c)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Podaj ciąg do zakodowania/odkodowania (np. 101010):");
                        string X = Console.ReadLine();

                        Console.WriteLine("Podaj funkcję wielomianową w postaci zero-jedynkowej (np. 101):");
                        string function = Console.ReadLine();

                        Console.WriteLine("Podaj ziarno (np 110):");
                        string seed = Console.ReadLine();

                        string function_result = SynchronousStreamCipher(X, function, seed);
                        Console.WriteLine(function_result);

                        Console.WriteLine("Naciśnij klawisz aby kontynuować...");
                        Console.ReadKey();

                        break;
                    case "2":
                        Console.Clear();

                        Console.WriteLine("Podaj nazwę pliku do zakodowania/rozkodowania (wejściowego):");
                        string input = Console.ReadLine();

                        Console.WriteLine("Podaj nazwę pliku - wyjściowego:");
                        string output = Console.ReadLine();

                        Console.WriteLine("Podaj funkcję wielomianową w postaci zero-jedynkowej (np. 101):");
                        function = Console.ReadLine();

                        Console.WriteLine("Podaj ziarno (np 110):");
                        seed = Console.ReadLine();

                        SynchronousStreamCipher_File(input, output, function, seed);

                        Console.WriteLine("Naciśnij klawisz aby kontynuować...");
                        Console.ReadKey();

                        break;
                    case "3":
                        return;
                }

            }
        }

        static void SynchronousStreamCipher_File(string inputFileName, string outputFileName, string function, string seed)
        {
            if (function.Length != seed.Length)
            {
                Console.WriteLine("Błąd! Seed musi być tej samej długości co funkcja.");
                Console.ReadKey();
                return;
            }

            int i, tmp, k;

            int[] actualMemo = new int[seed.Length];
            List<int> indexesToXOR = new List<int>();
            for (i = 0; i < function.Length; i++)
            {
                if (function[i] == '1')
                {
                    indexesToXOR.Add(i);
                }
            }

            for (i = 0; i < actualMemo.Length; i++)
            {
                actualMemo[i] = (int)Char.GetNumericValue(seed, i);
            }

            /*
            //Przy X'sie na wejsciu jako ciag do zakodowania/odkodowania wypluwajacy wynik do Y'eka
            for (i = 0; i < X.Length; i++)
            {
                tmp = 0;
                foreach (int indx in indexesToXOR)
                {
                    tmp ^= actualMemo[indx];
                }

                Y = Y + (tmp ^ (int)Char.GetNumericValue(X, i));

                Array.Copy(actualMemo, 0, actualMemo, 1, actualMemo.Length - 1);
                actualMemo[0] = tmp;
            }
            */

            using (BinaryWriter bw = new BinaryWriter(File.Open(outputFileName, FileMode.Create)))
            using (BinaryReader br = new BinaryReader(File.Open(inputFileName, FileMode.Open)))
            {
                int pos = 0;
                int length = (int)br.BaseStream.Length;


                while (pos < length)
                {
                    byte v = br.ReadByte();

                    byte outByte = 0;
                    for (i = 0; i < 8; i++)
                    {
                        int valueOfBit = ((v & (1 << i)) == 0) ? 0 : 1;

                        tmp = 0;
                        foreach (int indx in indexesToXOR)
                        {
                            tmp ^= actualMemo[indx];
                        }

                        int codedBit = tmp ^ valueOfBit;

                        if (codedBit == 1)
                        {
                            outByte = (byte)(outByte | (1 << i));
                        }

                        Array.Copy(actualMemo, 0, actualMemo, 1, actualMemo.Length - 1);
                        actualMemo[0] = tmp;
                    }
                    bw.Write(outByte);

                    pos += sizeof(byte);
                }
            }

            Console.WriteLine("Zapisano wyniki w pliku \"{0}\".", outputFileName);
        }

        static string SynchronousStreamCipher(string X, string function, string seed)
        {
            if (function.Length != seed.Length)
            {
                Console.WriteLine("Błąd! Seed musi być tej samej długości co funkcja.");
                Console.ReadKey();
                return "error";
            }

            int i, tmp, k;
            string Y = "";

            int[] actualMemo = new int[seed.Length];
            List<int> indexesToXOR = new List<int>();
            for (i = 0; i < function.Length; i++)
            {
                if (function[i] == '1')
                {
                    indexesToXOR.Add(i);
                }
            }

            for (i = 0; i < actualMemo.Length; i++)
            {
                actualMemo[i] = (int)Char.GetNumericValue(seed, i);
            }


            //Przy X'sie na wejsciu jako ciag do zakodowania/odkodowania wypluwajacy wynik do Y'eka
            for (i = 0; i < X.Length; i++)
            {
                tmp = 0;
                foreach (int indx in indexesToXOR)
                {
                    tmp ^= actualMemo[indx];
                }

                Y = Y + (tmp ^ (int)Char.GetNumericValue(X, i));

                Array.Copy(actualMemo, 0, actualMemo, 1, actualMemo.Length - 1);
                actualMemo[0] = tmp;
            }


            return Y;
        }

        //--------------------------------------------------------- ZADANIE 3 - Synchronous Stream Cipher ---------------------------------------------------------
        //TODO:
        static void Zadanie3()
        {
            //wybor czy z pliku do pliku (to ewentualnie beda wykorzystywane albo funkcje albo funkcje_file)
            //tutaj pewnie jakies czy chcesz zakodować czy odkodowac
            //    //przy zakodowaniu: chcesz 1. test.bin; 2. test2.bin ; 3. test3.bin ; 4. inny (mozliwosc podaniawlasnej nazwy)
            //        //nastepnie nazwa pliku wyjsciowego
            //    //przy odkodowaniu: podaj nazwe pliku zakodowanego i nazwe pliku wyjsciowego

            //^ przy tych oczywiscie tez pytanie o jakis seed i funkcje
            //^^ do zakodowywania CiphertextAutokeyEncrypt, do rozkodowania CiphertextAutokeyDecrypt

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Zadanie 3 - Cipher Text Autokey");
                Console.WriteLine("1. Konsolowo");
                Console.WriteLine("2. Z pliku");
                Console.WriteLine("3. Powrót");
                String c = Console.ReadLine();

                switch (c)
                {
                    case "1":
                        {
                            Console.Clear();
                            Console.WriteLine("1. Szyfruj");
                            Console.WriteLine("2. Odszyfruj");
                            Console.WriteLine("3. Powrót");
                            String c1 = Console.ReadLine();
                            switch (c1)
                            {
                                case "1":

                                    Console.Clear();
                                    Console.WriteLine("Podaj ciąg wejściowy (np. 101010):");
                                    string X = Console.ReadLine();

                                    Console.WriteLine("Podaj funkcję wielomianową w postaci zero-jedynkowej (np. 101):");
                                    string function = Console.ReadLine();

                                    Console.WriteLine("Podaj ziarno (np 110):");
                                    string seed = Console.ReadLine();

                                    string function_result = CiphertextAutokeyEncrypt(X, function, seed);
                                    Console.WriteLine(function_result);

                                    Console.WriteLine("Naciśnij klawisz aby kontynuować...");
                                    Console.ReadKey();
                                    break;
                                case "2":

                                    Console.Clear();
                                    Console.WriteLine("Podaj ciąg wejściowy (np. 101010):");
                                    X = Console.ReadLine();

                                    Console.WriteLine("Podaj funkcję wielomianową w postaci zero-jedynkowej (np. 101):");
                                    function = Console.ReadLine();

                                    Console.WriteLine("Podaj ziarno (np 110):");
                                    seed = Console.ReadLine();

                                    function_result = CiphertextAutokeyDecrypt(X, function, seed);
                                    Console.WriteLine(function_result);

                                    Console.WriteLine("Naciśnij klawisz aby kontynuować...");
                                    Console.ReadKey();
                                    break;
                                case "3":
                                    break;
                            }
                        }
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("1. Szyfruj");
                        Console.WriteLine("2. Odszyfruj");
                        Console.WriteLine("3. Powrót");
                        String c2 = Console.ReadLine();
                        switch (c2)
                        {
                            case "1":
                                Console.Clear();

                                Console.WriteLine("Podaj nazwę pliku - wejściowego:");
                                string input = Console.ReadLine();

                                Console.WriteLine("Podaj nazwę pliku - wyjściowego:");
                                string output = Console.ReadLine();

                                Console.WriteLine("Podaj funkcję wielomianową w postaci zero-jedynkowej (np. 101):");
                                string function = Console.ReadLine();

                                Console.WriteLine("Podaj ziarno (np 110):");
                                string seed = Console.ReadLine();

                                CiphertextAutokeyEncrypt_File(input, output, function, seed);

                                Console.WriteLine("Naciśnij klawisz aby kontynuować...");
                                Console.ReadKey();
                                break;
                            case "2":
                                Console.Clear();

                                Console.WriteLine("Podaj nazwę pliku - wejściowego:");
                                input = Console.ReadLine();

                                Console.WriteLine("Podaj nazwę pliku - wyjściowego:");
                                output = Console.ReadLine();

                                Console.WriteLine("Podaj funkcję wielomianową w postaci zero-jedynkowej (np. 101):");
                                function = Console.ReadLine();

                                Console.WriteLine("Podaj ziarno (np 110):");
                                seed = Console.ReadLine();

                                CiphertextAutokeyDecrypt_File(input, output, function, seed);

                                Console.WriteLine("Naciśnij klawisz aby kontynuować...");
                                Console.ReadKey();
                                break;
                            case "3":
                                break;
                        }

                        break;
                    case "3":
                        return;
                }

            }
        }

        static void CiphertextAutokeyEncrypt_File(string inputFileName, string outputFileName, string function, string seed)
        {
            if (function.Length != seed.Length)
            {
                Console.WriteLine("Błąd! Seed musi być tej samej długości co funkcja.");
                Console.ReadKey();
                return;
            }

            int i, tmp, k;

            int[] actualMemo = new int[seed.Length];
            List<int> indexesToXOR = new List<int>();
            for (i = 0; i < function.Length; i++)
            {
                if (function[i] == '1')
                {
                    indexesToXOR.Add(i);
                }
            }

            for (i = 0; i < actualMemo.Length; i++)
            {
                actualMemo[i] = (int)Char.GetNumericValue(seed, i);
            }

            /*
            //Przy X'sie na wejsciu jako ciag do zakodowania wypluwajacy wynik do Y'eka
            for (i = 0; i < X.Length; i++)
            {
                //Console.WriteLine(string.Join(" ", actualMemo));
                tmp = 0;
                foreach (int indx in indexesToXOR)
                {
                    tmp ^= actualMemo[indx];
                }

                tmp ^= (int)Char.GetNumericValue(X, i);
                Y = Y + tmp;

                Array.Copy(actualMemo, 0, actualMemo, 1, actualMemo.Length - 1);
                actualMemo[0] = tmp;
            }
            */

            using (BinaryWriter bw = new BinaryWriter(File.Open(outputFileName, FileMode.Create)))
            using (BinaryReader br = new BinaryReader(File.Open(inputFileName, FileMode.Open)))
            {
                int pos = 0;
                int length = (int)br.BaseStream.Length;


                while (pos < length)
                {
                    byte v = br.ReadByte();

                    byte outByte = 0;
                    for (i = 0; i < 8; i++)
                    {
                        int valueOfBit = ((v & (1 << i)) == 0) ? 0 : 1;

                        tmp = 0;
                        foreach (int indx in indexesToXOR)
                        {
                            tmp ^= actualMemo[indx];
                        }

                        tmp ^= valueOfBit;

                        if (tmp == 1)
                        {
                            outByte = (byte)(outByte | (1 << i));
                        }

                        Array.Copy(actualMemo, 0, actualMemo, 1, actualMemo.Length - 1);
                        actualMemo[0] = tmp;
                    }
                    bw.Write(outByte);

                    pos += sizeof(byte);
                }
            }

            Console.WriteLine("Zapisano wyniki w pliku \"{0}\".", outputFileName);
        }

        static void CiphertextAutokeyDecrypt_File(string inputFileName, string outputFileName, string function, string seed)
        {
            if (function.Length != seed.Length)
            {
                Console.WriteLine("Błąd! Seed musi być tej samej długości co funkcja.");
                Console.ReadKey();
                return;
            }

            int i, tmp, k;

            int[] actualMemo = new int[seed.Length];
            List<int> indexesToXOR = new List<int>();
            for (i = 0; i < function.Length; i++)
            {
                if (function[i] == '1')
                {
                    indexesToXOR.Add(i);
                }
            }

            for (i = 0; i < actualMemo.Length; i++)
            {
                actualMemo[i] = (int)Char.GetNumericValue(seed, i);
            }

            /*
            //Przy Y'eku na wejsciu jako ciag do odkodowania wypluwajacy wynik do X'a
            for (i = 0; i < Y.Length; i++)
            {
                //Console.WriteLine(string.Join(" ", actualMemo));
                tmp = 0;
                foreach (int indx in indexesToXOR)
                {
                    tmp ^= actualMemo[indx];
                }

                tmp ^= (int)Char.GetNumericValue(Y, i);

                X = X + tmp;

                Array.Copy(actualMemo, 0, actualMemo, 1, actualMemo.Length - 1);
                actualMemo[0] = (int)Char.GetNumericValue(Y, i);
            }
            */

            using (BinaryWriter bw = new BinaryWriter(File.Open(outputFileName, FileMode.Create)))
            using (BinaryReader br = new BinaryReader(File.Open(inputFileName, FileMode.Open)))
            {
                int pos = 0;
                int length = (int)br.BaseStream.Length;


                while (pos < length)
                {
                    byte v = br.ReadByte();

                    byte outByte = 0;
                    for (i = 0; i < 8; i++)
                    {
                        int valueOfBit = ((v & (1 << i)) == 0) ? 0 : 1;

                        tmp = 0;
                        foreach (int indx in indexesToXOR)
                        {
                            tmp ^= actualMemo[indx];
                        }

                        tmp ^= valueOfBit;

                        if (tmp == 1)
                        {
                            outByte = (byte)(outByte | (1 << i));
                        }

                        Array.Copy(actualMemo, 0, actualMemo, 1, actualMemo.Length - 1);
                        actualMemo[0] = valueOfBit;
                    }
                    bw.Write(outByte);

                    pos += sizeof(byte);
                }
            }

            Console.WriteLine("Zapisano wyniki w pliku \"{0}\".", outputFileName);
        }

        static string CiphertextAutokeyEncrypt(string X, string function, string seed)
        {
            if (function.Length != seed.Length)
            {
                Console.WriteLine("Błąd! Seed musi być tej samej długości co funkcja.");
                Console.ReadKey();
                return "error";
            }

            int i, tmp, k;
            string Y = "";

            int[] actualMemo = new int[seed.Length];
            List<int> indexesToXOR = new List<int>();
            for (i = 0; i < function.Length; i++)
            {
                if (function[i] == '1')
                {
                    indexesToXOR.Add(i);
                }
            }

            for (i = 0; i < actualMemo.Length; i++)
            {
                actualMemo[i] = (int)Char.GetNumericValue(seed, i);
            }


            //Przy X'sie na wejsciu jako ciag do zakodowania wypluwajacy wynik do Y'eka
            for (i = 0; i < X.Length; i++)
            {
                //Console.WriteLine(string.Join(" ", actualMemo));
                tmp = 0;
                foreach (int indx in indexesToXOR)
                {
                    tmp ^= actualMemo[indx];
                }

                tmp ^= (int)Char.GetNumericValue(X, i);
                Y = Y + tmp;

                Array.Copy(actualMemo, 0, actualMemo, 1, actualMemo.Length - 1);
                actualMemo[0] = tmp;
            }

            return Y;
        }

        static string CiphertextAutokeyDecrypt(string Y, string function, string seed)
        {
            if (function.Length != seed.Length)
            {
                Console.WriteLine("Błąd! Seed musi być tej samej długości co funkcja.");
                Console.ReadKey();
                return "error";
            }

            int i, tmp, k;
            string X = "";

            int[] actualMemo = new int[seed.Length];
            List<int> indexesToXOR = new List<int>();
            for (i = 0; i < function.Length; i++)
            {
                if (function[i] == '1')
                {
                    indexesToXOR.Add(i);
                }
            }

            for (i = 0; i < actualMemo.Length; i++)
            {
                actualMemo[i] = (int)Char.GetNumericValue(seed, i);
            }


            //Przy Y'eku na wejsciu jako ciag do odkodowania wypluwajacy wynik do X'a
            for (i = 0; i < Y.Length; i++)
            {
                //Console.WriteLine(string.Join(" ", actualMemo));
                tmp = 0;
                foreach (int indx in indexesToXOR)
                {
                    tmp ^= actualMemo[indx];
                }

                tmp ^= (int)Char.GetNumericValue(Y, i);

                X = X + tmp;

                Array.Copy(actualMemo, 0, actualMemo, 1, actualMemo.Length - 1);
                actualMemo[0] = (int)Char.GetNumericValue(Y, i);
            }

            return X;
        }

        //--------------------------------------------------------- DODATKOWA FUNKCJONALNOSC---------------------------------------------------------------------

        static void PrintBinaryFileContentBitByBit(string inputFileName)
        {
            using (BinaryReader b = new BinaryReader(File.Open(inputFileName, FileMode.Open)))
            {
                int pos = 0;

                int length = (int)b.BaseStream.Length;
                while (pos < length)
                {
                    byte v = b.ReadByte();
                    for (int i = 0; i < 8; i++)
                    {
                        int valueOfBit = ((v & (1 << i)) == 0) ? 0 : 1;
                        Console.Write(valueOfBit + " ");
                    }
                    Console.Write(" :" + v + "\n");

                    pos += sizeof(byte);
                }
            }
        }

        static void CreateTxtFileFromBinaryBitByBit(string inputFileName, string outputFileName)
        {
            using (StreamWriter wr = new StreamWriter(outputFileName, false))
            using (BinaryReader b = new BinaryReader(File.Open(inputFileName, FileMode.Open)))
            {
                int pos = 0;

                int length = (int)b.BaseStream.Length;
                while (pos < length)
                {
                    byte v = b.ReadByte();
                    int[] bitSeq = new int[8];
                    for (int i = 0; i < 8; i++)
                    {
                        int valueOfBit = ((v & (1 << i)) == 0) ? 0 : 1;
                        bitSeq[i] = valueOfBit;
                    }
                    wr.WriteLine(string.Join(" ", bitSeq));

                    pos += sizeof(byte);
                }
            }
            Console.WriteLine("Przekonwertowano do tekstowego \"{0}\".", outputFileName);
        }

        static void CreateBinaryFileFromTextBitByBit(string inputFileName, string outputFileName)
        {
            using (StreamReader rd = new StreamReader(inputFileName))
            using (BinaryWriter bw = new BinaryWriter(File.Open(outputFileName, FileMode.Create)))
            {
                while (true)
                {
                    string line = rd.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    string[] breakedLine = line.Split(' ');
                    //mamy stringa wiec:
                    byte outByte = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        if (breakedLine[i] == "1")
                        {
                            outByte = (byte)(outByte | (1 << i));
                        }
                    }
                    bw.Write(outByte);
                }
            }
            Console.WriteLine("Przekonwertowano do binarnego \"{0}\".", outputFileName);
        }

        static void ObslugaPlikow()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Wypisz plik binarny bit po bicie");
                Console.WriteLine("2. Stworz plik tekstowy z pliku binarnego bit po bicie");
                Console.WriteLine("3. Stworz plik binarny z pliku tekstowego");
                Console.WriteLine("4. Powrót");
                String c = Console.ReadLine();

                switch (c)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Podaj nazwę pliku, który chcesz wypisać");
                        string inputFileName = Console.ReadLine();
                        PrintBinaryFileContentBitByBit(inputFileName);

                        Console.WriteLine("Naciśnij klawisz aby kontynuować...");
                        Console.ReadKey();
                        break;
                    case "2":

                        Console.Clear();
                        Console.WriteLine("Podaj nazwę pliku wejściowego");
                        inputFileName = Console.ReadLine();

                        Console.WriteLine("Podaj nazwę pliku wyjściowego");
                        string outputFileName = Console.ReadLine();

                        CreateTxtFileFromBinaryBitByBit(inputFileName, outputFileName);
                        Console.WriteLine("Naciśnij klawisz aby kontynuować...");
                        Console.ReadKey();

                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Podaj nazwę pliku wejściowego");
                        inputFileName = Console.ReadLine();

                        Console.WriteLine("Podaj nazwę pliku wyjściowego");
                         outputFileName = Console.ReadLine();

                        CreateBinaryFileFromTextBitByBit(inputFileName, outputFileName);
                        Console.WriteLine("Naciśnij klawisz aby kontynuować...");
                        Console.ReadKey();
                        break;
                    case "4":
                        return;
                }
            }
        }
    }
}
