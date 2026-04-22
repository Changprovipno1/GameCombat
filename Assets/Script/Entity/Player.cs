using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int currentHp;
    private int damage;
    public int Damage => damage;
    private bool isDead;
    public bool IsDead => isDead;
    void Start()
    {
        currentHp = maxHp;
        damage = 20;
    }
    public void TakeDamage(int dmg)
    {
        if (dmg < 0) return;
        if (isDead)
        {
            Debug.Log($"Player is dead");
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
        Debug.Log($"Player is dead");
    }
    public void Heal(int amountHp)
    {
        if (isDead)
        {
            Debug.Log("Heal is error because player is dead");
            return;
        }
        else if (amountHp < 0)
        {
            Debug.Log("Heal is error because amount Hp < 0");
            return;
        }

        currentHp += amountHp;
        Debug.Log($"Heal : {amountHp}");
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

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
