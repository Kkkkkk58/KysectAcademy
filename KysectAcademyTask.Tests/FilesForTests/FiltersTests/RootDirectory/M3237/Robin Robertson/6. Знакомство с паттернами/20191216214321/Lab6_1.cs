using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifmolab
{
    public enum EnumType
    {
        ALL,
        SHOES,
        CLOTHING,
        ACCESSORIES
    }
    public class Thing
    {
        public String Name { get; set; }
        public int Cost { get; set; }
        public String Color { get; set; }
        public int Size { get; set; }
        public EnumType Type { get; set; }
    }
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handler(Thing request);
    }

    abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;

            return handler;
        }

        public virtual object Handler(Thing request)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handler(request);
            }
            else
            {
                return null;
            }
        }
    }

    class CostHandler : AbstractHandler
    {
        int minCost;
        int maxCost;
        public CostHandler(int minCost, int maxCost)
        {                
            this.minCost = minCost;
            if (maxCost != 0)
            {
                this.maxCost = maxCost;
            }
            else
            {
                this.maxCost = Int32.MaxValue;
            }
        }
        public override object Handler(Thing request)
        {
            if (request.Cost<minCost || request.Cost > maxCost)
            {
                return false;
            }
            return base.Handler(request);
        }
    }
    class ColorHandler : AbstractHandler
    {
        String color;
        public ColorHandler(String color)
        {
            this.color = color;
        }
        public override object Handler(Thing request)
        {
            if (color != "" && request.Color != color)
            {
                return false;
            }
            return base.Handler(request);
        }
    }
    class SizeHandler : AbstractHandler
    {
        int size;
        public SizeHandler(int size)
        {
            this.size = size;
        }
        public override object Handler(Thing request)
        {
            if (size != 0 && request.Size != size )
            {
                return false;
            }
            return base.Handler(request);
        }
    }
    class TypeHandler : AbstractHandler
    {
        EnumType type;
        public TypeHandler(int type)
        {
            this.type = (EnumType)type;
        }
        public override object Handler(Thing request)
        {
            if (type != EnumType.ALL && request.Type != type)
            {
                return false;
            }
            return base.Handler(request);
        }
    }

    class Lab6_1
    {
        public List<Thing> things;

        public Lab6_1()
        {
            things = new List<Thing>();
            int n = 10;
            Console.WriteLine("Введите {n} вещей характеристики перечислить через пробел, пример: Имя Цвет Стоимость Размер Тип(1. Обувь 2. Одежка 3. Аксесуары)");
            for (int i = 0; i < n; i++)
            {
                String[] s = Console.ReadLine().Split(new char[]{' '}).ToArray();
                Thing thing = new Thing
                {
                    Name = s[0],
                    Color = s[1],
                    Cost = int.Parse(s[2]),
                    Size = int.Parse(s[3]),
                    Type = (EnumType)int.Parse(s[4])
                };
                things.Add(thing);
            }

            Console.WriteLine("Теперь будем фильтровать");
            while (true)
            {
                Console.WriteLine("Напишите максимум и минимум цены ");
                String[] ct = Console.ReadLine().Split(new char[] { ' ' }).ToArray();
                var cost = new CostHandler(int.Parse(ct[1]), int.Parse(ct[0]));
                Console.WriteLine("Напишите цвет или оставьте строчку пустой");
                String cl = Console.ReadLine();
                var color = new ColorHandler(cl);
                Console.WriteLine("Напишите размер или напишите 0 ");
                String sz = Console.ReadLine();
                var size = new SizeHandler(int.Parse(sz));
                Console.WriteLine("Напишите номер типа или напишите 0");
                String tp = Console.ReadLine();
                var type = new TypeHandler(int.Parse(tp));
                cost.SetNext(color).SetNext(size).SetNext(type);
                List<Thing> ts = CodeFilter(cost);
                foreach (Thing t in ts)
                {
                    Console.WriteLine(t.Name);
                }
            }
        }

        public List<Thing> CodeFilter(AbstractHandler handler)
        {
            List<Thing> result = new List<Thing>();
            foreach (var thing in things)
            {
                var r = handler.Handler(thing);
                if (r == null)
                {
                    result.Add(thing);
                }
            }
            return result;
        }
    }
}
