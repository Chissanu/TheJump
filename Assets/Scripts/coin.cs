using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class coin : MonoBehaviour
{
    public GameObject coinObj;
    public Collider coinCollider;
    public Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        coinObj = this.gameObject;
        coinCollider = GetComponent<Collider>();
        coinObj.transform.Rotate(0, 90, 90);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0,1,0), Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("HIT");
            coinObj.GetComponent<Renderer>().enabled = false;
            coinCollider.enabled = false;
            this.gameObject.GetComponent<AudioSource>().Play();
            FindObjectOfType<game>().AddScore();

            StartCoroutine(RespawnCoins());
        }
    }

    IEnumerator RespawnCoins()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.GetComponent<ParticleSystem>().Emit(2);
        this.transform.SetPositionAndRotation(new Vector3(UnityEngine.Random.Range(-3f,3f),2f,0f), this.transform.rotation);
        Debug.Log("Coin Spawn");
        coinObj.GetComponent<Renderer>().enabled = true;
        coinCollider.enabled = true;

    }
}
