using System.Collections;
using UnityEngine;

public class Frogger : MonoBehaviour
{
     
 /*public*/private SpriteRenderer spriteRenderer; //{ get; private set; }

    public Sprite idleSprite;
    public Sprite leapSprite;
    public Sprite deadSprite;

    private Vector3 spawnPosition;
    private float farthestRow;
    //private bool cooldown;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
    }




        private void Update()
        {
           if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {   
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Move(Vector3.up);
            }
           else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {  
                 transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Move(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            { 
                 transform.rotation = Quaternion.Euler(0f, 0f, -90f);
               Move(Vector3.right);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {  
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
               Move(Vector3.down);
            }
        }

        private void Move(Vector3 direction){ 
           

             Vector3 destination = transform.position + direction;

             Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("barrier"));
             Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("platform"));
             Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("obstacle"));

           // Prevent any movement if there is a barrier
             if (barrier != null) {
              return;
              }
          // Attach/detach frogger from the platform
             if (platform != null) {
            transform.SetParent(platform.transform);
              }
             else {
              transform.SetParent(null);
                  }

              // Frogger dies when it hits an obstacle like water ,cars(lma m3mlhash && platform ==null y3ni lma kman mykonsh feh ay trtule aw log 3shan lw mktbtsh b2yet elshart lma frogger tro7 3la ay platform zy log aw trutle htmoot bardo)
             if (obstacle != null && platform == null)
             {
                transform.position = destination;
                Death();
             }

            else
            { 
                if (destination.y > farthestRow)
                {
                    farthestRow = destination.y;
                    FindObjectOfType<GameManger>().AdvancedRow();
                }
                
                StartCoroutine(Leap(destination)); }
           
            }

        private IEnumerator Leap(Vector3 destination)
    {
        Vector3 startPosition = transform.position;

        float elapsed = 0f;
        float duration = 0.125f;

        // Set initial state
        spriteRenderer.sprite = leapSprite;
        //cooldown = true;

        while (elapsed < duration)
        {
            // Move towards the destination over time
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
      

        // Set final state
        transform.position = destination;
        spriteRenderer.sprite = idleSprite;
        //cooldown = false;
    }

    public void Death()
    {
        StopAllCoroutines();
          // Display death sprite
        transform.rotation = Quaternion.identity;
        //
        spriteRenderer.sprite = deadSprite;
        // Disable control
        enabled = false;

        
        FindObjectOfType<GameManger>().Died();
      
    }


    public void Respawn()
    {
        // Stop animations
        StopAllCoroutines();

        // Reset transform to spawn
        transform.rotation = Quaternion.identity;
        transform.position = spawnPosition;
        farthestRow = spawnPosition.y;

        // Reset sprite
        spriteRenderer.sprite = idleSprite;

        // Enable control
        gameObject.SetActive(true);
        enabled = true;
        //cooldown = false;
    }



//The OnTriggerEnter2D() function is called when the frog collides with another collider.
// If the collider belongs to an obstacle and the frog is not attached to a platform, the function calls the Death() function
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*bool hitObstacle = other.gameObject.layer == LayerMask.NameToLayer("Obstacle");
        bool onPlatform = transform.parent != null;

        if (enabled && hitObstacle && !onPlatform) {
            Death();
        }*/

        if (enabled && other.gameObject.layer == LayerMask.NameToLayer ("obstacle") && transform.parent == null)
        {
            Death();
        }
    }

}

