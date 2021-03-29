using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

//player renderer
public SpriteRenderer myRenderer;
//cloud renderers
public SpriteRenderer cloud1Renderer;
public SpriteRenderer cloud1reflectRenderer;
public SpriteRenderer cloud2Renderer;
public SpriteRenderer cloud2reflectRenderer;
public SpriteRenderer cloud3Renderer;
public SpriteRenderer cloud3reflectRenderer;
//platform renderers
public SpriteRenderer platform1Renderer;
public SpriteRenderer platform2Renderer;
public SpriteRenderer platform3Renderer;
public SpriteRenderer platform4Renderer;
//flag renderers
public SpriteRenderer OFlag1Renderer;
public SpriteRenderer OFlag2Renderer;
public SpriteRenderer OFlag3Renderer;
public SpriteRenderer OFlag4Renderer;
public SpriteRenderer OFlag5Renderer;
public SpriteRenderer OFlag6Renderer;

public SpriteRenderer TFlag1Renderer;
public SpriteRenderer TFlag2Renderer;
public SpriteRenderer TFlag3Renderer;
public SpriteRenderer TFlag4Renderer;
public SpriteRenderer TFlag5Renderer;
public SpriteRenderer TFlag6Renderer;


//colours
public Color floorColour;
public Color FireColour;
public Color yellowColour;
public Color blueColour;
public Color hinge2Colour;
public Color gateColour2;
public Color greenColour;
public Color purpleColour;

public Color flagcolour1;
public Color flagcolour2;
public Color flagcolour3;
public Color flagcolour4;
public Color flagcolour5;
public Color flagcolour6;








public Rigidbody2D myBody;

public GameObject firstCam;
float MoveDirection = 1;
public float speed; 
public float jumpHeight;
public float GravityMultiplier;
public float jumpMultiplier;
//boolean to check if player is on floor/platform or not
bool onFloor = true;

public GameObject gate1;
public GameObject gate2;
public GameObject gate3;
public GameObject minigate1;
public GameObject minigate2;
public GameObject greykeytransparent; 
public GameObject redkeytransparent;
public GameObject redkey;

public GameObject cloudlvl1;
public GameObject cloudlvl1reflect; 
public GameObject secondcloudlvl1;
public GameObject secondcloudlvl1reflect; 

//platform game objects

public GameObject cloud1;
public GameObject cloud1reflection;
public GameObject platform1;
public GameObject platform2;
public GameObject platform3;
public GameObject platform4;
public GameObject cloud2;
public GameObject cloud2reflection;
public GameObject cloud3;
public GameObject cloud3reflection;

public GameObject arrow; 
public GameObject arrow2;

public GameObject day;
public GameObject night; 

public GameObject firstsign; 
public GameObject portal1;
public GameObject portal2; 
//private bool reachedPos = true;
//private bool stopMovement = false;
//private Vector3 dir; 
private Vector3 nextPos; 
public float sightDist; 
public LayerMask SignLayer; 
public LayerMask SignLayer2;

public GameManager gameManager; 

public bool cloud1check = false;
public bool cloud2check = false;
public bool cloud3check = false;

public bool keyactive = false;

public float power;

//flag objects

public GameObject flagopaque1;
public GameObject flagopaque2;
public GameObject flagopaque3;
public GameObject flagopaque4;
public GameObject flagopaque5;
public GameObject flagopaque6; 

public GameObject flagtransparent1;
public GameObject flagtransparent2;
public GameObject flagtransparent3;
public GameObject flagtransparent4;
public GameObject flagtransparent5;
public GameObject flagtransparent6; 

public GameObject minigatetransparent;
public GameObject greykeytransparent2; 
public GameObject greykeytransparent3; 

public GameObject white; 
public GameObject endtitle;




    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myBody = gameObject.GetComponent<Rigidbody2D>();

        //cloudrenderers
        cloud1Renderer = cloud1.GetComponent<SpriteRenderer>();
        cloud1reflectRenderer = cloud1reflection.GetComponent<SpriteRenderer>();
        cloud2Renderer = cloud2.GetComponent<SpriteRenderer>();
        cloud2reflectRenderer = cloud2reflection.GetComponent<SpriteRenderer>();
        cloud3Renderer = cloud3.GetComponent<SpriteRenderer>();
        cloud3reflectRenderer = cloud3reflection.GetComponent<SpriteRenderer>();
        //platformrenderers
        platform1Renderer = platform1.GetComponent<SpriteRenderer>();
        platform2Renderer = platform2.GetComponent<SpriteRenderer>();
        platform3Renderer = platform3.GetComponent<SpriteRenderer>();
        platform4Renderer = platform4.GetComponent<SpriteRenderer>();
        //flag opaque renderers

        OFlag1Renderer = flagopaque1.GetComponent<SpriteRenderer>();
        OFlag2Renderer = flagopaque2.GetComponent<SpriteRenderer>();
        OFlag3Renderer = flagopaque3.GetComponent<SpriteRenderer>();
        OFlag4Renderer = flagopaque4.GetComponent<SpriteRenderer>();
        OFlag5Renderer = flagopaque5.GetComponent<SpriteRenderer>();
        OFlag6Renderer = flagopaque6.GetComponent<SpriteRenderer>();

        //flag transparent renderers

        TFlag1Renderer = flagtransparent1.GetComponent<SpriteRenderer>();
        TFlag2Renderer = flagtransparent2.GetComponent<SpriteRenderer>();
        TFlag3Renderer = flagtransparent3.GetComponent<SpriteRenderer>();
        TFlag4Renderer = flagtransparent4.GetComponent<SpriteRenderer>();
        TFlag5Renderer = flagtransparent5.GetComponent<SpriteRenderer>();
        TFlag6Renderer = flagtransparent6.GetComponent<SpriteRenderer>();

        



        
    }

    // Update is called once per frame
    void Update()
    {
        //returns true once

        //struct; creates a ray in particular direction
        //created a layer for the sign object
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), sightDist, SignLayer);
        
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right), Color.green);
        //if we hit something
        if (hit.collider != null) {
            
            Debug.Log("hit sign");

            gameManager.ShowPopText("The key hides itself above. Let the clouds guide you.");

        }

        CheckCloudColours();
        
       
    }

    void FixedUpdate() {
        //physics code runs in fixed update instead of update!

        //make on floor false if our velocity is at a certain rate
      
        if(onFloor && myBody.velocity.y > 0) {
            onFloor = false; 
        }
        CheckKeys();
        HandleMovement();
        JumpPhysics();


    }

    void CheckKeys() {

        if (Input.GetKey(KeyCode.D)) {

            MoveDirection = 1;
        }

        else if (Input.GetKey(KeyCode.A)) {

            MoveDirection = -1;
        }

        else {

            MoveDirection = 0;
        }

        if (Input.GetKey(KeyCode.W)) {

            //we want to keep the x velocity as is
            //but change y value thru jump height
            myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);
            //if we are not pressing W or on the floor 
        } else if(!Input.GetKey(KeyCode.W) && myBody.velocity.y > 0) {
            //little boost, then slow down 
            myBody.velocity += Vector2.up * Physics.gravity.y * (jumpMultiplier - 1f) * Time.deltaTime; 
        }

    }

    void JumpPhysics() {
        //if we are in descent
        if(myBody.velocity.y < 0) {
            myBody.velocity += Vector2.up * Physics.gravity.y * (GravityMultiplier - 1f) * Time.deltaTime;
        }
    }

    void HandleMovement() {

        //only messing w/ x values
        myBody.velocity = new Vector3(MoveDirection * speed, myBody.velocity.y);


    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {

        //tag for group of objects
         //if (collisionInfo.gameObject.name == "picketsign (2)") {

           // Debug.Log("hit sign 2");
           // gameManager.ShowPopText("Traverse the clouds and turn them all gold to acquire the key for the next floor. Beware of touching fire, or the clouds will turn back to normal.");
        //}

        if(collisionInfo.gameObject.tag == "Floor") {

            onFloor = true;
        }

        if(collisionInfo.gameObject.tag == "Floor") {

            myRenderer.color = floorColour;
            cloud1.SetActive(true);
            platform1.SetActive(true);
            platform2.SetActive(true);
            platform3.SetActive(true);
            platform4.SetActive(true);
            cloud2.SetActive(true);
            cloud3.SetActive(true);
        }

       

        if (collisionInfo.gameObject.name == "floor (4)") {

            Debug.Log("collided w first platform");

            //cloud1.SetActive(false); 


    } 

    if (collisionInfo.gameObject.name == "platform (12)") {
            cloud1Renderer.color = yellowColour;
            cloud1reflectRenderer.color = yellowColour;
            cloud1check = true; 
    }


    if (collisionInfo.gameObject.name == "bigcloudopaque") {
            cloud3Renderer.color = yellowColour;
            cloud3reflectRenderer.color = yellowColour;
            cloud3check = true; 
    }

    if (collisionInfo.gameObject.name == "floor (26)") {

            //platform1.SetActive(false); 

    } 

    if (collisionInfo.gameObject.name == "floor (27)") {

            //platform2.SetActive(false); 

    } 

    if (collisionInfo.gameObject.name == "floor (28)") {

            //platform3.SetActive(false); 

    } 

    if (collisionInfo.gameObject.name == "smallcloudopaque") {

            cloud2Renderer.color = yellowColour;
            cloud2reflectRenderer.color = yellowColour;
            cloud2check = true; 

    } 

    

        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "firstkey") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up first level key");
            Destroy(other.gameObject);
            Destroy(gate1);
            Destroy(greykeytransparent);
            Destroy(cloudlvl1);
            Destroy(cloudlvl1reflect);
            Destroy(secondcloudlvl1);
            Destroy(secondcloudlvl1reflect); 
            Destroy(firstsign);

            arrow.SetActive(true);
            arrow2.SetActive(true);

        }

        if (other.gameObject.name == "secondkey") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up second level key");
            Destroy(other.gameObject);
            Destroy(gate2);
            Destroy(redkeytransparent);
            
        }

         if (other.gameObject.name == "bluekey") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up key for mini gate");
            Destroy(other.gameObject);
            Destroy(greykeytransparent3);
            Destroy(minigate1);
            Destroy(minigatetransparent);
            
        }

        if (other.gameObject.name == "thirdkey") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up third level key");
            Destroy(other.gameObject);
            Destroy(minigate2);
            Destroy(gate3);
            Destroy(greykeytransparent2);
        }

        if(other.gameObject.tag == "Fire") {
            Debug.Log("fire");
            myRenderer.color = FireColour;
            cloud1.SetActive(true);
            platform1.SetActive(true);
            platform2.SetActive(true);
            platform3.SetActive(true);
            platform4.SetActive(true);
            cloud2.SetActive(true);
            cloud3.SetActive(true);
            

            cloud1Renderer.color = floorColour;
            cloud1reflectRenderer.color = floorColour;
            cloud1check = false; 

            cloud2Renderer.color = floorColour;
            cloud2reflectRenderer.color = floorColour;
            cloud2check = false; 

            cloud3Renderer.color = floorColour;
            cloud3reflectRenderer.color = floorColour;
            cloud3check = false; 

        }

        if(other.gameObject.name == "picketsign (2)") {

        Debug.Log("hit sign 2");
        gameManager.ShowPopText("Traverse the clouds and paint them gold to make the next key known. Beware of fire's touch, or the clouds will turn back to normal.");
        }

        if(other.gameObject.name == "picketsign (3)") {

        gameManager.ShowPopText("Brave the waters and find the light.");

        }

        if(other.gameObject.name == "picketsign (4)") {

            gameManager.ShowPopText("Scale the stars both high and low.");
        }

        if(other.gameObject.name == "picketsign (5)") {

            gameManager.ShowPopText("Time passes and reflections grow closer.");
        }

        if(other.gameObject.name == "picketsign (7)") {

            gameManager.ShowPopText("Time to wake up.");
        }

        if (other.gameObject.name == "ocean") {

        myRenderer.color = blueColour; 
        }
    
        if(other.gameObject.name == "lighthouse") {

            day.SetActive(false);
            night.SetActive(true);
        }   

        if(other.gameObject.name == "flag1") {

            OFlag1Renderer.color = flagcolour1;
            TFlag1Renderer.color = flagcolour1;
        }  

         if(other.gameObject.name == "flag2") {

            OFlag2Renderer.color = flagcolour2;
            TFlag2Renderer.color = flagcolour2;
        }

         if(other.gameObject.name == "flag3") {

            OFlag3Renderer.color = flagcolour3;
            TFlag3Renderer.color = flagcolour3;
        }

         if(other.gameObject.name == "flag4") {

            OFlag4Renderer.color = flagcolour4;
            TFlag4Renderer.color = flagcolour4;
        }

         if(other.gameObject.name == "flag5") {

            OFlag5Renderer.color = flagcolour5;
            TFlag5Renderer.color = flagcolour5;
        }

         if(other.gameObject.name == "flag6") {

            OFlag6Renderer.color = flagcolour6;
            TFlag6Renderer.color = flagcolour6;
        }


        if(other.gameObject.name == "portal1 (1)") {

            white.SetActive(true);
            endtitle.SetActive(true);
        }

        if(other.gameObject.name == "portal1") {

            gameManager.ShowPopText("In a dream I see a world reflected, as if cast on water's surface - where up is down, and down is up. Heaven at the ground and Hell in the sky, all else ripples between.");
        }
    }

    void CheckCloudColours() {

        redkey.SetActive(false);
        redkeytransparent.SetActive(false);

        if (cloud1check == true && cloud2check == true && cloud3check == true) {

            Debug.Log("all clouds have been collided with");
            redkey.SetActive(true);
            redkeytransparent.SetActive(true);
        }
    }
}
    


