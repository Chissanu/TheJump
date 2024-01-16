using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{

    public Rigidbody rb;
    public bool onFloor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<game>().gameState == true)
        {
            // Movements
            if (Input.GetKey(KeyCode.A))
            {
                //Do something
                rb.AddForce(new Vector3(-5, 0, 0));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(new Vector3(5, 0, 0));
            }
            else if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(new Vector3(0, 0, 5));
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(new Vector3(0, 0, -5));
            }
            else if (Input.GetKey(KeyCode.Space) && onFloor)
            {
                rb.AddForce(new Vector3(0, 0.5f, 0), ForceMode.Impulse);

            }
        } else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                FindObjectOfType<game>().RestartGame();
                this.gameObject.transform.position = new Vector3 (-0.17f, 3.44f, 0);
            }
        }

        if (this.gameObject.GetComponent<Transform>().position.y < -1)
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        }

        // Check if player fall off
        if (this.gameObject.GetComponent<Transform>().position.y < -3)
        {
            //FindObjectOfType<game>().GameOver();
            this.GetComponent<Renderer>().enabled = false;
            StartCoroutine(Reposition());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = false;
        }
    }

    IEnumerator Reposition()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.transform.position = new Vector3(-0.17f, 3.44f, 0);
        this.gameObject.transform.rotation = Quaternion.identity;
        this.GetComponent<Renderer>().enabled = true;
        rb.velocity = Vector3.zero;
    }
}
