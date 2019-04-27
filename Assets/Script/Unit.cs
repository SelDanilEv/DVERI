using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public virtual void ReceiveDamage()
    {
        Die();    // чувак умирает
    }
    //123
    //hhhhhhhhh

    protected virtual void Die()
    {

        Destroy(gameObject);

    }
}
