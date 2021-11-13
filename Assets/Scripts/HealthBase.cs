public class HealthBase
{
    private int health;
    public HealthBase(int h)
    {
        health = h;
    }

    public void setHealth(int h)
    {
        health = h;
    }

    public int getHealth()
    {
        return health;
    }

    public void takeHealth(int h)
    {
        health -= h;
    }
}
