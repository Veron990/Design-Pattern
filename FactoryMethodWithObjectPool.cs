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

    public class FactoryRgb : IFactoryColore
    {
        public IColore CreaColore()
        {
            return new ColoreRgb();
        }
    }

    class ObjectPool
    {
        private List<IColore> _list;
        private IFactoryColore _factory;

        public ObjectPool(IFactoryColore factory)
        {
            _list = new List<IColore>();
            _factory = factory;
        }

        public IColore GetColor()
        {
            if(_list.Count == 0)
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
    }

    public class Program
    {
        static void Main(string[] args)
        {
            IFactoryColore factory = new FactoryRgb();
            ObjectPool pool = new ObjectPool(factory);

            IColore colore1 = pool.GetColor();
            colore1.ValidaColore();

            IColore colore2 = pool.GetColor();
            colore2.ValidaColore();

            pool.Restituisci(colore1); //restituisco l' oggetto al pool quando non mi serve più cosicchè io possa riutilizzarlo
            pool.Restituisci(colore2);

            IColore colore3 = pool.GetColor(); //nel colore 3 e 4 riutilizzo l' istanza del colore 1 e 2
            colore3.ValidaColore();

            IColore colore4 = pool.GetColor();
            colore4.ValidaColore();

            IColore colore5 = pool.GetColor();
            colore5.ValidaColore();
        }
    }
