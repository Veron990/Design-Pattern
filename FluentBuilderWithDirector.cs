public class Pizza
    {
        public string Impasto { get; set; }
        public string Salsa { get; set; }
        public string Condimenti { get; set; }

        public void Stampa()
        {
            Console.WriteLine($"Pizza con impasto di {Impasto} e salsa al {Salsa} con {Condimenti} ");
        }
    }

    public interface IPizzaBuilder
    {
        IPizzaBuilder SetImpasto();
        IPizzaBuilder SetSalsa();
        IPizzaBuilder SetCondimenti();
        Pizza Build();
    }

    public class MargheritaBuilder : IPizzaBuilder //Fluent Builder
    {
        private Pizza _pizza = new Pizza();
        public IPizzaBuilder SetCondimenti()
        {
            _pizza.Condimenti = "Mozzarella";
            return this;
        }

        public IPizzaBuilder SetImpasto()
        {
            _pizza.Impasto = "Farina 00";
            return this;

        }

        public IPizzaBuilder SetSalsa()
        {
            _pizza.Salsa = "Pomodoro";
            return this;
        }

        public Pizza Build()
        {
            return _pizza;
        }
    }

    public class PizzaChef //Director
    {
        private IPizzaBuilder _builder;

        public PizzaChef(IPizzaBuilder builder)
        {
            _builder = builder;
        }

        public void MakePizza()
        {
            _builder
                .SetImpasto()
                .SetSalsa()
                .SetCondimenti();
        }
    }
    class Program
    {
        public static void Main(string[] args) 
        {
            IPizzaBuilder builder = new MargheritaBuilder();
            PizzaChef chef = new PizzaChef(builder);
            chef.MakePizza();
            Pizza pizza = builder.Build();
            pizza.Stampa();
        }
    }
