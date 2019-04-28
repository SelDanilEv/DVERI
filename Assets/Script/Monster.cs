using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    protected virtual void Awake() { }
    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {

        Bullet1 bullet1 = collider.GetComponent<Bullet1>();
        Bullet bullet = collider.GetComponent<Bullet>();

        if (bullet || bullet1)
        {
            ReceiveDamage();
        }

        Character Character = collider.GetComponent<Character>();
        if (Character)
        {
            Character.ReceiveDamage();
        }
    }
}

