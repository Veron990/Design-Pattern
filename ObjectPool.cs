public class Persona
    {
        public int Id { get; private set; }

        public string Nome { get; private set; }

        public Persona(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
        public void Reset()
        {
            Nome = "";
        }

        public void SetPersona(string nome)
        {
            Nome = nome;
        }

        public void Stampa()
        {
            Console.WriteLine($"Id : {Id}\tNome : {Nome}");
        }

        public class ObjectPool
        {
            public static int _nextId = 1;

            private Queue<Persona> pool = new Queue<Persona>();
            private int _poolSize;

            private ObjectPool() { }

            public static ObjectPool Istance = new ObjectPool();

            public void SetPoolSize(int size)
            {
                _poolSize = size;
            }
            public Persona GetPersonaFromPool()
            {
                if (pool.Count == 0) //pool vuoto => creo nuovo oggetto
                {
                    Console.WriteLine("\nCreo nuova istanza");
                    return new Persona(_nextId++, "");
                }
                else
                {
                    Persona p = pool.Dequeue(); //toglie l' oggetto all' inizio della coda
                    int vecchioId = p.Id;
                    p.Id = _nextId;
                    Console.WriteLine($"\nIstanza ottenuta dal pool da Id Persona => {vecchioId}");
                    return p;
                }
            }

            public void RitornaPersonaToPool(Persona p)
            {
                string nomePersona = p.Nome;
                if (pool.Count < _poolSize)
                {
                    p.Reset();
                    pool.Enqueue(p); //aggiunge un oggetto alla fine della coda
                    Console.WriteLine($"\n{nomePersona} è stato restituito al pool");
                }
                else
                {
                    Console.WriteLine($"\nIl pool è pieno, impossibile restituire {nomePersona} al pool.");
                }
            }

            public void SvuotaPool()
            {
                pool.Clear();
                Console.WriteLine("\nPool svuotato");
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                ObjectPool pool = ObjectPool.Istance;
                pool.SetPoolSize(3);

                Persona p1 = pool.GetPersonaFromPool();
                p1.SetPersona("Veronica");
                p1.Stampa();

                Persona p2 = pool.GetPersonaFromPool();
                p2.SetPersona("Laura");
                p2.Stampa();

                Persona p3 = pool.GetPersonaFromPool();
                p3.SetPersona("Luisa");
                p3.Stampa();

               

                Persona p4 = pool.GetPersonaFromPool();
                p4.SetPersona("Giacomo");
                p4.Stampa();

                pool.RitornaPersonaToPool(p1);
                pool.RitornaPersonaToPool(p2);
                pool.RitornaPersonaToPool(p3);
                pool.RitornaPersonaToPool(p4); //non può è pieno (max 3)

                pool.SvuotaPool();
                pool.RitornaPersonaToPool(p4); //ora si

                Persona p5 = pool.GetPersonaFromPool();
                p5.SetPersona("Alessio");
                p5.Stampa();
               
                pool.RitornaPersonaToPool(p5);

                
            }
        }
    }
