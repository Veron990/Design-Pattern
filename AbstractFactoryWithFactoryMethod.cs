public interface IButton
    {
        public void Paint();
    }
    public interface IWindowsButton : IButton
    {
        public void IsSpecial();
    }

    public class MacButton : IButton
    {
        public void Paint()
        {
            Console.WriteLine("mac button");
        }
    }

    public class WindowsBaseButton : IWindowsButton
    {
        public void IsSpecial()
        {
            Console.WriteLine("non è special");
        }

        public void Paint()
        {
            Console.WriteLine("base windows button");
        }
    }
    public class WindowsSpecialButton : IWindowsButton
    {
        public void IsSpecial()
        {
            Console.WriteLine("è special");
        }

        public void Paint()
        {
            Console.WriteLine("special windows button");
        }
    }

    public interface IGuidFactory
    {
        public IButton CreateButton();
    }

    public abstract class WindowsFactory : IGuidFactory
    {
        public IButton CreateButton()
        {
            return CreateWindowsButton();
        }

        protected abstract IWindowsButton CreateWindowsButton();
    }

    public class WindowsBaseFactory : WindowsFactory
    {
        protected override IWindowsButton CreateWindowsButton()
        {
            return new WindowsBaseButton();
        }
    }
    public class WindowsSpecialFactory : WindowsFactory
    {
        protected override IWindowsButton CreateWindowsButton()
        {
            return new WindowsSpecialButton();
        }
    }
    public class MacFactory : IGuidFactory
    {
        public IButton CreateButton()
        {
            return new MacButton();
        }
    }

    class Program
    {
        static void Main()
        {
           //windows base button
            WindowsFactory windowsBaseFactory = new WindowsBaseFactory();

            IButton baseButton = windowsBaseFactory.CreateButton();
            baseButton.Paint();

            if (baseButton is IWindowsButton)
            {
                (baseButton as IWindowsButton)?.IsSpecial();
            }

            //windows special button
            WindowsFactory windowsSpecialFactory = new WindowsSpecialFactory();

            IButton specialButton = windowsSpecialFactory.CreateButton();
            specialButton.Paint();

            if (specialButton is IWindowsButton)
            {
                (specialButton as IWindowsButton)?.IsSpecial();
            }

            //mac button
            MacFactory macFactory = new MacFactory();
            IButton macButton = macFactory.CreateButton();
            macButton.Paint(); 
        }
    }
