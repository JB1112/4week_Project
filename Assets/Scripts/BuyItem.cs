using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    private Health playerHealth;
    void Start()
    {
        playerHealth = GameManager.Instance.playerHealth;
    }
    public void Drink()
    {
        playerHealth.Heal(25);
    }

    public void Eat()
    {
        playerHealth.Heal(70);
    }

    public void Polish()
    {
        //공격력 증가.
    }

    public void SharpPolish()
    { 
        //공격력 증가
    }

    public void AttackBoost()
    {
        //공격속도 증가
    }
}
