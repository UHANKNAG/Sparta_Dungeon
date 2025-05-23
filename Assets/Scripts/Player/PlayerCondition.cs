using System;
using System.Collections;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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

    public float dashStamina;

    public bool isDash = false;
    public bool canDash = true;
    
    public bool isZeroGravity = false;
    // Delegate
    public event Action onTakeDamage;

    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue == 0f) 
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        } 

        if (health.curValue == 0f)
        {
            Die();
        }

        if (isDash)
        {
            stamina.Subtract(dashStamina * Time.deltaTime);
        }

        if (stamina.curValue <= 0f)
        {
            CantDash();
            canDash = false;
        }
        else
        {
            canDash = true;            
        }
    }

    public void Heal(float amout) 
    {
        health.Add(amout);
    }

    public void Fast(float amout)
    {
        StartCoroutine(SpeedUp(amout));
    }

    public void Eat(float amout)
    {
        hunger.Add(amout);
    }

    public void NoGravity()
    {
        StartCoroutine(Gravity());
    }

    public void CantDash()
    {
        Debug.Log("Dash 불가능");
    }

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

    private IEnumerator Gravity()
    {
        Debug.Log("중력 OFF");
        isZeroGravity = true;
        CharacterManager.Instance.Player.GetComponent<Rigidbody>().useGravity = false;

        yield return new WaitForSeconds(5f);

        Debug.Log("중력 ON");
        isZeroGravity = false;
        CharacterManager.Instance.Player.GetComponent<Rigidbody>().useGravity = true;
    }
}
