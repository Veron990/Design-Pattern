public interface IButton //classe di un prodotto  base
    {
        void Paint();
    }

public class WindowsButton : IButton //classe di un prodotto specifica per la tipologia Windows
    {
        public void Paint()
        {
            Console.WriteLine("windows button");
        }
    }

    public class MacButton : IButton //classe di un prodotto specifica per la tipologia Mac
    {
        public void Paint()
        {
            Console.WriteLine("mac button");
        }
    }

public interface IGuiFactory //Factory base
    {
        IButton CreateButton(); //Factory method
    }

public class WindowsFactory : IGuiFactory //Factory della tipologia Windows
    {
        //Rendo questa classe un Singleton in quanto è una pratica consigliata per i Factory
        private WindowsFactory(){ }

        private static WindowsFactory instance = new WindowsFactory();
      
        public static WindowsFactory GetIstance()
        {
            return instance;
        }

        public IButton CreateButton() //Factory method specifico per Windows
        {
            return new WindowsButton();
        }
    }

public class MacFactory : IGuiFactory //Factory della tipologia Mac
    {
        public IButton CreateButton()  //Factory method specifico per Mac
        {
            return new MacButton();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            //Implementazione Factory Windows
            IGuiFactory factory = WindowsFactory.GetIstance();
            IButton windowsButton = factory.CreateButton();
            windowsButton.Paint();
            
            //Implementazione Factory Mac
            IGuiFactory factory2 = new MacFactory();
            factory2.CreateButton().Paint();
        }
    }
