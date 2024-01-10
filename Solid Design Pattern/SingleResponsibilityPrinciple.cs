public class Diario //Si occupa solo di gestire la scrittura dei pensieri sul diario
    {
        private readonly List<string> _pensieri = new List<string>();

        private static int _count = 0;

        public int AddPensiero(string pensiero)
        {
            _pensieri.Add($"{++_count}: {pensiero}");
            return _count;
        }

        public void DeletePensiero(int indice)
        {
            _pensieri.RemoveAt(indice);
        }

        public override string ToString()
        {
            return string.Join("\n", _pensieri);
        }

        //Questa operazione rompe il Single Responsability Principle! => Trasferisco il metodo salva all' interno della classe apposita Persistenza
        //public void Salva(Diario diario, string nomeFile)
        //{
        //    File.WriteAllText(nomeFile, diario.ToString());
        //}
    }

    public class Persistenza //Si occupa di salvare le informazioni contenute nel diario sul disco
    {
        //L' operazione di salvataggio è un operazione DIFFERENTE rispetto alla scrittura di un diario.
        public void Salva(Diario diario, string nomeFile)
        {
            File.WriteAllText(nomeFile, diario.ToString());
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var diario = new Diario();
            diario.AddPensiero("Oggi sono andato al cinema.");
            diario.AddPensiero("Questa sera ho mangiato una pizza.");

            Console.WriteLine(diario);
            var nomeFile = "C:\\Users\\veron\\Desktop\\ProveProg\\FileProva.txt";

            //diario.Salva(diario, nomeFile); => rompe il SRP
            Persistenza persistenza = new Persistenza(); //Uso del SRP
            persistenza.Salva(diario, nomeFile);
        }
    }
