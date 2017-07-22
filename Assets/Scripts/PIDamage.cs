public class PIDamage
{
    private int index = 0;
    private string values = "3141592653589793238462643383279";

    public int GetNext()
    {
        if (index >= values.Length)
            index = 0;
        
        string value = "" + values[index];
        index++;
        return int.Parse(value);
    }
}