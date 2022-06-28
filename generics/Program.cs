//Box<Doll> caja1 = new Box<Doll>();
//Doll barbie = new Doll() { SKU = new Guid(), Price = 100 };
//caja1.Put(barbie);
//caja1.Extract();

//Box<Car> caja2 = new Box<Car>();
//Car hotwheel = new Car { SKU = new Guid(), Price = 50 };
//caja2.Put(hotwheel);
//caja2.Extract();

iFactory<Doll> factory = new DollFactory();
var boxes = BoxHelper.GenerateBoxes<Doll>(50, factory.MakeElement);


#region declarations
class Box<T>
{
    public T Content { get; private set; }
    private bool hasContent;

    public void Put(T content)
    {
        if (hasContent)
        {
            throw new Exception("Ya hay algo en la caja.");
        }
        this.Content = content;
        hasContent = true;
    }

    public T Extract()
    {
        if (!hasContent)
        {
            throw new Exception("No hay nada en la caja para extraer.");
        }
        hasContent = false;
        return this.Content;
    }
}

class Doll
{
    public Guid SKU { get; set; }
    public decimal Price { get; set; }
}

class Car
{ 
    public Guid SKU { get; set; }
    public decimal Price { get; set; }
}

interface iFactory<T>
{
    T MakeElement();
}

class DollFactory : iFactory<Doll>
{
    public Doll MakeElement() => new Doll();
}

static class BoxHelper
{
    public static List<Box<T>> GenerateBoxes<T>(int numberOfBoxes, Func<T> getContent)
    {
        var lstBoxes = new List<Box<T>>();
        for (int i = 0; i < numberOfBoxes; i++)
        {
            var box = new Box<T>();
            box.Put(getContent());
            lstBoxes.Add(box);
        }
        return lstBoxes;
    }
}

#endregion