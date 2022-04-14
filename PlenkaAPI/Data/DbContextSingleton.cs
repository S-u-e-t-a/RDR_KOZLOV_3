namespace PlenkaAPI.Data;

public class DbContextSingleton
{
    private static MembraneContext instance;

    private static readonly object syncRoot = new();


    public static MembraneContext GetInstance()
    {
        if (instance == null)
            lock (syncRoot)
            {
                if (instance == null) instance = new MembraneContext();
            }

        return instance;
    }
}