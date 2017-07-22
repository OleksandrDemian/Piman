using System.Collections.Generic;

public delegate void OnDialogEnd();

public class Dialog
{
    public OnDialogEnd onDialogEnd;
    private List<Frase> frases;
    private int currentIndex;

    public Dialog()
    {
        frases = new List<Frase>();
        currentIndex = 0;
    }

    public void AddFrase(Frase frase)
    {
        frases.Add(frase);
    }

    public Frase GetNextFrase()
    {
        if (currentIndex >= frases.Count)
        {
            onDialogEnd();
            return null;
        }

        Frase current = frases[currentIndex];
        currentIndex++;
        return current;
    }

    public override string ToString()
    {
        return "Dialog f" + frases.Count;
    }
}
