public class ClickObject
{
    private int posStart, lenght;

    public ClickObject(int pos, int len)
    {
        this.posStart = pos;
        this.lenght = len;
    }

    public int getPosStart()
    {
        return this.posStart;
    }

    public int getLenght()
    {
        return this.lenght;
    }

    public void setPosStart(int val)
    {
        this.posStart = val;
    }

    public void setLenght(int val)
    {
        this.lenght = val;
    }
}
