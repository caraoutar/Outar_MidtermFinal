using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ended up not using this script, but was following this YT tutorial: https://www.youtube.com/watch?v=DQYj8Wgw3O0&t=11s&ab_channel=AlexanderZotov
public class MovingPlatforms : MonoBehaviour
{

float dirX;

float moveSpeed = 4f;

bool moveRight = true;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.x > -150 && transform.position.x < -140 ) {

            moveRight = true;

        }

        if (transform.position.x > -145 && transform.position.x < -130) {

            moveRight = false;
        }

        if (moveRight == true) {

            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        } else {

            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }


    }
}

    
