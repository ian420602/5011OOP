namespace Class_Library;


// Factory class for creating instances of OutputData for different case( MySQL, JSON).

public class OutputDataFactory
{

    // Creates an instance of OutputData based on the specified type.
    public SQLiteOutput CreateOutputData(int type)
    {
        // Pre: Type must be valid (1 for SQL, 2 for JSON).
        if (type != 1 && type != 2)
            throw new ArgumentException("Invalid output data type.");

        // Post: Ensure the correct OutputData type is returned.
        if (type == 1)
            return new SQLiteOutput(); //  
        else if (type == 2)
            return new SQLiteOutput(); // Wait for Json Class design

        return null;
    }
}
