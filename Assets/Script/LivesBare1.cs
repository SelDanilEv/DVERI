using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBare1 : MonoBehaviour
{
    private Transform[] hearts  = new Transform[2];

    private Character character;

    private void Awake()
    {
        character = FindObjectOfType<Character>();

        Debug.Log(hearts.Length);
        for (int i =0; i < hearts.Length ;i++)
        {
            hearts[i] = transform.GetChild(i); //Маленькие сердечки
            Debug.Log(hearts[i]);
        }
        Debug.Log(hearts[0]);
        Debug.Log("sd");
        Debug.Log(hearts[1]);
       

    }

    public void Refresh1(int lives)
    {
        Debug.Log("Маленькие");
        for (int i =0;i<hearts.Length;i++)
        {
            if (i < lives  / 2 )
                hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false);
        }
    }

}
