using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int currentHp;
    
    [SerializeField] private int damage;
    private bool isDead;
    public bool IsDead => isDead;
    void Start()
    {
        currentHp = maxHp;
        damage = 10;
    }

    public void TakeDamage(int dmg)
    {
        if (dmg < 0) return;
        if (isDead)
        {
            Debug.Log($"Enemy is dead");
            return;
        }
        currentHp -= dmg;
        Debug.Log($"Taking Damage : {dmg}");
        if (currentHp <= 0)
        {
            currentHp = 0;
        }
        if (currentHp == 0)
        {
            Die();

        }
    }
    public void Die()
    {
        if (isDead) return;
        isDead = true;
        Debug.Log($"Enemy is dead");
    }
   
    public void ShowStatus()
    {
        string status = "";
        if (isDead)
        {
            status = "Dead";
        }
        else
        {
            status = "Alive";
        }
        Debug.Log($" CurrentHp = {currentHp} | MaxHp = {maxHp} | Status: {status}");
    }
}
