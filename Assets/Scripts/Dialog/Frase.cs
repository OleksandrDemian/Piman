public class Frase
{
    public Frase(string personName, string text)
    {
        this.PersonName = personName;
        this.Text = text;
    }

    public string PersonName
    {
        get;
        private set;
    }

    public string Text
    {
        get;
        private set;
    }
}
