using System.Collections.Generic;

public class Frame
{
    public int Id;
    public List<string> UserInput;
    public Frame(int id)
    {
        Id = id;
        UserInput = new();
    }
}