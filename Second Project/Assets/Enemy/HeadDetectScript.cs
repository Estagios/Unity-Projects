using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetectScript : MonoBehaviour
{
    GameObject flip;

    Enemies_AI Enemy;

    void Start()
    {
        Enemy = GetComponent<Enemies_AI>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            Destroy(gameObject);       
    }
}
