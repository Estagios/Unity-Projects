using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_AI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int XMoveDirecrion;
    private float moveSpeed = 1;
    private bool movingRight = false;
    public Transform groundDetetcion;
    public Transform wallDetetcion;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }

        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetetcion.position, Vector2.down, 2f);

        bool wallInfo = Physics2D.Linecast(transform.position, wallDetetcion.position, 1 << LayerMask.NameToLayer("Wall"));
       
        if (groundInfo == false || wallInfo == true)
        {
            if (movingRight == false)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = true;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = false;
            }
        }     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool hit = collision.collider.GetComponent<Enemies_AI>() != null;
        if (hit)
        {
            if (movingRight == false)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = true;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = false;
            }
        }

    }
}
