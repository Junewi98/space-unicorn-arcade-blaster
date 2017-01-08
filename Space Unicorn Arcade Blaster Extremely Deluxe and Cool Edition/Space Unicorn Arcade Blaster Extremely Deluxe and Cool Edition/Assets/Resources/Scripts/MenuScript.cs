using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
    {

    public float maxRayDistance = 25;
    public string sceneName;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            Debug.DrawLine(transform.position, transform.forward * maxRayDistance, Color.red);

            if (Physics.Raycast(ray, out hit, maxRayDistance))
            {
                sceneName = hit.collider.gameObject.name;
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
        }
    }

}
