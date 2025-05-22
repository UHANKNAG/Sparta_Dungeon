using System;
using System.Collections;
using Unity.Profiling;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour //, IDamagable
{
    public UICondition uiCondition;

    Condition health {get {return uiCondition.health;}}
    Condition hunger {get {return uiCondition.hunger;}}
    Condition stamina {get {return uiCondition.stamina;}}

    public float noHungerHealthDecay;
    
    // Delegate
    public event Action onTakeDamage;

    void Update()
    {
        // hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        // stamina.Add(stamina.passiveValue * Time.deltaTime);

        // if (hunger.curValue == 0f) 
        // {
        //     health.Subtract(noHungerHealthDecay * Time.deltaTime);
        // } 

        // if (health.curValue == 0f)
        // {
        //     Die();
        // }
    }

    public void Heal(float amout) 
    {
        health.Add(amout);
    }

    public void Fast(float amout)
    {
        StartCoroutine(SpeedUp(amout));
    }

    // public void Eat(float amout)
    // {
    //     hunger.Add(amout);
    // }

    public void Die()
    {
        Debug.Log("Die");
    }

    // public void TakePhysicalDamage(int damage)
    // {
    //     health.Subtract(damage);
    //     onTakeDamage?.Invoke();
    // }

    private IEnumerator SpeedUp(float amout)
    {
        float Speed = CharacterManager.Instance.Player.controller.moveSpeed;
        CharacterManager.Instance.Player.controller.moveSpeed *= amout;

        yield return new WaitForSeconds(5f);

        CharacterManager.Instance.Player.controller.moveSpeed = Speed;
    }
}
