using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Camera cam;
    public float speed;
    public float scale;
    public GameObject crossHair;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) && cam.transform.position.x < 19)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.A) && cam.transform.position.x > 0)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S) && cam.transform.position.y > 0)
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.W) && cam.transform.position.y < 19)
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.Q) && cam.orthographicSize <= 5.5)
        {
            cam.orthographicSize += 0.1f;
            speed *= 1.05f;
            crossHair.transform.localScale += new Vector3(.022f, .022f, .022f);
        }
        if (Input.GetKey(KeyCode.E) && cam.orthographicSize >= 0.8)
        {
            cam.orthographicSize -= 0.1f;
            speed /= 1.05f;
            crossHair.transform.localScale -= new Vector3(.022f, .022f, .022f);
        }
    }
}
