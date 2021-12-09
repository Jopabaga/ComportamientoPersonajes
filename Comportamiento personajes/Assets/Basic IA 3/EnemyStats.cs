public class EnemyStats : CharacterStats
{
    public bool hurt;

    public int auxHealth;

    private void Start()
    {
        maxHealth = 1;
        hp = maxHealth;
        auxHealth = hp;

        maxAmmo = 30;
        ammo = maxAmmo;
    }

    public void Update()
    {
        checkIfHurt();
    }

    public void checkIfHurt()
    {
        if (auxHealth != hp)
        {
            hurt = true;
        }
        else
        {
            hurt = false;
        }
    }
}
