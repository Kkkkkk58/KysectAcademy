import entity.Product;
import filter.ColorPredicate;
import filter.Predicate;
import filter.PricePredicate;
import filter.TypePredicate;
import service.Service;

public class Main
{
    public static void main(String[] args)
    {
        Service service = Service.instance;

        service.add(new Product(10, "Blue", 32, Product.Type.jacket));
        service.add(new Product(100, "Blue", 42, Product.Type.shoes));
        service.add(new Product(50, "Blue", 42, Product.Type.shoes));
        service.add(new Product(15, "Red", 32, Product.Type.shirt));
        service.add(new Product(16, "Yellow", 32, Product.Type.shirt));
        service.add(new Product(18, "Black", 40, Product.Type.shirt));
        service.add(new Product(18, "Blue", 40, Product.Type.shirt));


        Predicate p = new PricePredicate(16, 50).and(new ColorPredicate("Blue"));
        Predicate p2 = new ColorPredicate("Blue").and(new TypePredicate(Product.Type.shirt).or(new TypePredicate(Product.Type.shoes)));

        System.out.println(service.find(p2));
    }
}
