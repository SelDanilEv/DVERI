using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBare : MonoBehaviour
{
    private Transform[] hearts  = new Transform[2];

    private Character character;

    private void Awake()
    {
        character = FindObjectOfType<Character>();
        Debug.Log(hearts.Length);
        for (int i =0; i < hearts.Length ;i++)
        {
            hearts[i] = transform.GetChild(i); //Большие сердечки
            Debug.Log(hearts[i]);
        }
    }

    public void Refresh(int lives)
    {
        Debug.Log("Большие");
        for (int i =0;i<hearts.Length;i++)
        {
            if (i < lives / 2)
                hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false);
        }
    }

}
