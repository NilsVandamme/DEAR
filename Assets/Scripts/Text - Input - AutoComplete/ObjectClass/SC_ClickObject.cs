public class SC_ClickObject
{
    private int posStart;
    private string mot;

    public SC_ClickObject(int pos, string val)
    {
        this.posStart = pos;
        this.mot = val;
    }

    public int getPosStart()
    {
        return this.posStart;
    }

    public string getMot()
    {
        return this.mot;
    }

    public void setPosStart(int val)
    {
        this.posStart = val;
    }

    public void setMot(string val)
    {
        this.mot = val;
    }
}
