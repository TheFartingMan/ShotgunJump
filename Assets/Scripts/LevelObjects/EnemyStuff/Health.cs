using UnityEngine;
//  Old health script that I was going to use for a potential enemy. Currently unused but could maybe be used for a boss
//or something like that.
public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    public void subtractHealth(int amount)
    {
        if (currentHealth - amount > 0)
        {
            currentHealth -= amount;
        }

        else
        {
            currentHealth = 0;
        }
    }

    public void addHealth(int amount)
    {
        if (currentHealth + amount < maxHealth)
        {
            currentHealth += amount;
        }

        else
        {
            currentHealth = maxHealth;
        }
    }

    public void setHealth(int amount)
    {
        currentHealth = amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void setMaxHealth(int amount)
    {
        maxHealth = amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
