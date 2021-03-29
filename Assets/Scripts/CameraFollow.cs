using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour

{

    public Transform followTransform;
    public BoxCollider2D worldBounds;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    //camera positions
    float camX;
    float camY;
    //width of camera
    float camRatio;
    //height of camera
    float camSize;

    public Camera mainCam;
    //public Camera2 secondCam;

    Vector3 smoothPos;

    //speed (how quick the camera catches up to player)
    public float smoothRate;

    // Start is called before the first frame update
    void Start()
    {
        xMin = worldBounds.bounds.min.x;
        xMax = worldBounds.bounds.max.x;
        yMin = worldBounds.bounds.min.y;
        yMax = worldBounds.bounds.max.y;

        mainCam = gameObject.GetComponent<Camera>();
        //secondCam = gameObject.GetComponent<Camera2>();

        camSize = mainCam.orthographicSize;
        //makes sure ball is in the center
        camRatio = (xMax + camSize) / 8.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        //code from class
        //called every fixed frame rate frame
        //helps keep physics as stable as possible
        camY = Mathf.Clamp(followTransform.position.y, yMin + camSize, yMax - camSize);
        camX = Mathf.Clamp(followTransform.position.x, xMin + camRatio, xMax - camRatio);

        smoothPos = Vector3.Lerp(gameObject.transform.position, new Vector3(camX, camY, gameObject.transform.position.z), smoothRate);

        gameObject.transform.position = smoothPos;
    }
}
