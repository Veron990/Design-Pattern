public abstract class Color
    {
        public abstract void ValidateSpace(double[] components);

        public abstract void PrintComponents();

    }
    public class RGBColor : Color
    {
        private double _r;
        private double _g;
        private double _b;

        public override void ValidateSpace(double[] components)
        {
            _r = components[0];
            _g = components[1];
            _b = components[2];
        }

        public override void PrintComponents()
        {
            Console.WriteLine($"valori RGB R : {_r} G : {_g} B : {_b}\n");
        }

        public void RgbOperation()
        {
            Console.WriteLine("operazione con Rgb");
        }
    }

    public class HSVColor : Color
    {
        private double _h;
        private double _s;
        private double _v;

        public override void ValidateSpace(double[] components)
        {
            _h = components[0];
            _s = components[1];
            _v = components[2];
        }

        public override void PrintComponents()
        {
            Console.WriteLine($"valori HSV H : {_h} S : {_s} V : {_v}\n");
        }

        public void HsvOperation()
        {
            Console.WriteLine("operazione con Hsv");
        }
    }


    public abstract class ColorFactory 
    {
       
        public Color GetColor(double[] components) //questo è il factory method, nonchè il punto di ingresso 
        {
            Color color = MakeColor();

            color.ValidateSpace(components);
            return color;
        }


        //il metodo MakeColor sarà SPECIFICO per OGNI tipologia di classe factory concreta definita 
        protected abstract Color MakeColor();
    }


    public class ColorFactoryRGB : ColorFactory
    {
        protected override Color MakeColor()
        {
            RGBColor color = new RGBColor();
            color.RgbOperation();
            return color;
        }
    }

    public class ColorFactoryHSV : ColorFactory
    {
        protected override Color MakeColor()
        {
            HSVColor color = new HSVColor();
            color.HsvOperation();
            return color;
        }
    }

    class Program
    {
        static void Main()
        {

            ColorFactory factoryRgb = new ColorFactoryRGB();

            ColorFactory factoryHsv = new ColorFactoryHSV();


            double[] comp = { 0.1, 0.2, 0.3 };
            Color color = factoryRgb.GetColor(comp);
            color.PrintComponents();

            Color color2 = factoryHsv.GetColor(comp);
            color.PrintComponents();

        }
    }
