using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotMovement : MonoBehaviour {

    public float speed;
    public float lim;

    private Vector3 targetPosition;
	// Use this for initialization
	void Start () {
        targetPosition = new Vector3(Random.Range(-lim, lim), Random.Range(-lim, lim), transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, targetPosition) <= 0.05f)
            targetPosition = new Vector3(Random.Range(-lim, lim), Random.Range(-lim, lim), transform.position.z);
   }
}
