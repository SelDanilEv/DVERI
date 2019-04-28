using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// пэрвая
public class Bullet : MonoBehaviour
{
    private GameObject parent; //comment 1
    public GameObject Parent { set { parent = value; } get { return parent; } }
    private float speed = 10.0F;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    private SpriteRenderer sprite;

    public Color Color
    {
        // WE go
        // Here we go!
        set { sprite.color = value; }

    }


    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, 1.4F);
    }
    

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }



private void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet1 bullet1 = collider.GetComponent<Bullet1>();
        
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit.gameObject != parent || bullet1)
        {
            Destroy(gameObject); // comment 2
        }
    }
}
