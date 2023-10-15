using System;
using System.Threading; 

class Program
{
    static string[] livros = new string[]
    {
        "1984","Cem Anos de Solidão","Dom Quixote","Grande Sertão: Veredas",
        "Memórias Póstumas de Brás Cubas","Vidas Secas","O Guarani",
        "Quincas Borba","O Cortiço","O Alienista","Iracema",
        "Gabriela, Cravo e Canela", "O Primo Basílio","Senhora",
        "A Moreninha","Dom Casmurro","Memórias de um Sargento de Milícias",
        "Triste Fim de Policarpo Quaresma","O Guarani","Capitães da Areia"
    };
    static int data = 0;
    static int readerCount = 0;
    static readonly object lockObject = new object();

   

    static void Writer()
    {
        while (true)
        {
            Random random = new Random();
            int newIndex = random.Next(livros.Length);

            lock (lockObject)
            {
                data = newIndex;
                Console.WriteLine($"Escritor escreveu: {livros[data]}");
            }

            Thread.Sleep(TimeSpan.FromSeconds(2));
        }
    }

    static void Reader()
    {
        while (true)
        {
            lock (lockObject)
            {
                readerCount++;
                Console.WriteLine($"Leitor {readerCount} leu: {livros[data]}");
            }

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }

    static void Main(string[] args)
    {
        Thread writerThread = new Thread(Writer);//o construtor da classe precisa de um metodo no parametro
        Thread readerThread1 = new Thread(Reader);
        Thread readerThread2 = new Thread(Reader);

        writerThread.Start();
        readerThread1.Start();
        readerThread2.Start();

        writerThread.Join();
        readerThread1.Join();
        readerThread2.Join();
    }
}
