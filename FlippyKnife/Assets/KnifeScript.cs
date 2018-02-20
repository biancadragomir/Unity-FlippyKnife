using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KnifeScript : MonoBehaviour {


    public Rigidbody rb;
    private Vector2 startSwipe;
    private Vector2 endSwipe;
    public float force = 5;
    public float torque = 20f;
    private float timeStartFly;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Swipe();
        }
	}

    void Swipe()
    {
        rb.isKinematic = false;
        timeStartFly = Time.time;

        Vector2 swipe = endSwipe - startSwipe;
        rb.AddForce(force*swipe, ForceMode.Impulse);
        rb.AddTorque(0f, 0f, torque, ForceMode.Impulse);
        //Debug.Log(startSwipe + "|" + endSwipe);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Plank")
        {
            rb.isKinematic = true;

        }
        else
        {
            Restart();
        }

    }
    void OnCollisionEnter()
    {
        float timeInAir = Time.time - timeStartFly;
        if (!rb.isKinematic && timeInAir >= .05f)
        {
            Restart();
        }
    }
    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
