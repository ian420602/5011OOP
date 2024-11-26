namespace Class_Library;

public interface OutputData
{
    void Write();
}

public class MySQL : OutputData
{
    public void Write()
    {
        // Write Data to DataBase
        Console.WriteLine("Data written to MySQL database.");
    }
}

public class JSON : OutputData
{
    public void Write()
    {
        // Write Data to JSON 
        Console.WriteLine("Data written to JSON file.");
    }
}
