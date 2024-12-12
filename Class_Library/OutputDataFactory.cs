namespace Class_Library
{
    public class OutputDataFactory
    {
        public IOrderRepository CreateOutputData(int type)
        {
            if (type != 1 && type != 2)
                throw new ArgumentException("Invalid output data type.");

            if (type == 1)
                return new SQLiteOutput();
            else if (type == 2)  //if database closed, use json
                return new JsonOutput();

            return null;
        }
    }
}
