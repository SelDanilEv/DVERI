using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_add : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();
        if (character)
        {
            character.farCount += 5;
            character.farCountText.text = "FarBullet: " + character.farCount;
            Destroy(gameObject);
        }
    }
}
