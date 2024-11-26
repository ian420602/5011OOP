namespace Class_Library;

public class OutputDataFactory
{
    public OutputData CreateOutputData(int type)
    {
        return type == 0 ? new MySQL() : new JSON();
    }
}
