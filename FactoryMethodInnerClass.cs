 public interface IColore
    {
        void ValidaColore();
    }

    public interface FactoryColore
    {
        IColore CreaColore();
    }

    public class ColoreRgb : IColore
    {
        private ColoreRgb() { } //costruttore dell' oggetto reso privato

        public void ValidaColore()
        {
            Console.WriteLine("validato colore rgb");
        }

        //classe interna
        public class FactoryRgb : FactoryColore
        {
            public  IColore CreaColore()
            {
                return new ColoreRgb();
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var rgbColor = new ColoreRgb.FactoryRgb().CreaColore();
            rgbColor.ValidaColore();
          
            //ho reso il costruttore privato quindi non posso più creare l' oggetto senza l' utilizzo del factory
            // var rgb = new ColoreRgb();     //non possibile!   

        }
    }
