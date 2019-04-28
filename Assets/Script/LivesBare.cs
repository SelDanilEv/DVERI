using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBare : MonoBehaviour
{
    private Transform[] hearts  = new Transform[10];

    private Character character;

    private void Awake()
    {
        character = FindObjectOfType<Character>();

        for (int i =0; i < hearts.Length;i++)
        {
            hearts[i] = transform.GetChild(i); //sdhsdTHUS
            Debug.Log(hearts[i]);
        }

    }

    public void Refresh()
    {
        for (int i =0;i<hearts.Length;i++)
        {
            if (i < character.Lives)
                hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false);
        }
    }

}
