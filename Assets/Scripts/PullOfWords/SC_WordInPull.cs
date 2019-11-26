using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WordInPull
{
    private string champLexical;
    private Word word;
    private bool[] used;

    public SC_WordInPull(string cl, Word word, bool[] use)
    {
        this.champLexical = cl;
        this.word = word;
        this.used = use;
    }

    public string GetCL()
    {
        return this.champLexical;
    }
    public Word GetWord()
    {
        return this.word;
    }
    public bool[] GetUsed()
    {
        return this.used;
    }
}
