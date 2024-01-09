public interface IColore
    {
        void ValidaColore();
    }

    public interface IFactoryColore
    {
        IColore CreaColore();
    }

    public class ColoreRgb : IColore
    {
        public ColoreRgb() { }

        public void ValidaColore()
        {
            Console.WriteLine("validato colore rgb");
        }
    }
    public class ColoreHsv : IColore
    {
        public ColoreHsv() { }

        public void ValidaColore()
        {
            Console.WriteLine("validato colore hsv");
        }
    }
    public class FactoryRgb : IFactoryColore
    {
        public IColore CreaColore()
        {
            return new ColoreRgb();
        }
    }

    public class FactoryHsv : IFactoryColore
    {
        public IColore CreaColore()
        {
            return new ColoreHsv();
        }
    }

    class ObjectPool
    {
        private List<IColore> _list;
        private IFactoryColore _factory;

        private ObjectPool() {
            _list = new List<IColore>();
        }

        public static ObjectPool Istance = new ObjectPool();

        public void SetFactory(IFactoryColore factory) { 
            _factory = factory;
        }

        public IColore GetColor()
        {
            if (_list.Count == 0)
            {
                Console.WriteLine("\n--creo nuova istanza");
                return _factory.CreaColore();
            }
            else
            {
                Console.WriteLine("\n--riutilizzo istanza");
                IColore color = _list[0];
                _list.RemoveAt(0);
                return color;
            }
        }

        public void Restituisci(IColore colore)
        {
            _list.Add(colore);
        }

        public void PulisciPool()
        {
            _list.Clear();
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            IFactoryColore factoryRgb = new FactoryRgb();
            IFactoryColore factoryHsv = new FactoryHsv();

            ObjectPool pool = ObjectPool.Istance;
            pool.SetFactory(factoryRgb);

            IColore colore1 = pool.GetColor();
            colore1.ValidaColore();

            IColore colore2 = pool.GetColor();
            colore2.ValidaColore();

            pool.Restituisci(colore1); //restituisco l' istanza al pool cosicchè posso usarla per un altra variabile ( color3 )

            IColore colore3 = pool.GetColor();
            colore3.ValidaColore();

            pool.Restituisci(colore2);

            pool.PulisciPool(); //pulisco tutto il pool quando voflio cambiare factory
            pool.SetFactory(factoryHsv);

            IColore colore4 = pool.GetColor();
            colore4.ValidaColore();

            IColore colore5 = pool.GetColor();
            colore5.ValidaColore();
        }
    }
