public abstract class Weapon
{
    protected float cadence;
    protected int dispersion;
    protected int burst;
    protected float cd;

    public abstract void onPick();

    public abstract void Shoot();
}