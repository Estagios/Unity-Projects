using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tux_Script : MonoBehaviour
{
    public float moveSpeed = 5;
    public bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Vector3 movment = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += movment * Time.deltaTime * moveSpeed;
        if (transform.position.x < -46)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
    }
}
