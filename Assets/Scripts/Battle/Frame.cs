using System.Collections.Generic;

public class Frame
{
    public int id;
    public List<string> userInput;
    public Frame(int id)
    {
        this.id = id;
        userInput = new();
    }
}