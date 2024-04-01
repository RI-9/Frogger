//for moving trtules w elkhshb ofokii mn edge le edge el default bta3 el moving mn el edge el ymen w spead 1 w default size object 1

using UnityEngine;
public class MovmentCycle : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public float speed = 1f;
    public int size = 1;
    private Vector3 leftEdge;
    private Vector3 rightEdge;


     private void Start()
    {    //viewportworldpoint is a fun that takes a Vector3 in viewport coordinates as input and returns a Vector3 in world coordinates. 
        //This is useful for determining the edges of the screen relative to camera View 
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);       //Viewport coordinates in Unity are a normalized coordinate system 
                                                                        //that represents the position of a point on the screen as a percentage of the screen's width and height.
                                                                        //The bottom-left corner of the screen is represented by (0,0) and the top-right corner is represented by (1,1) , f kda hya bt7wly eldge mn 0(left) to 1 right 
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    private void Update()
    {
        // Check if the object is past the right edge of the screen and if it has hy7rk position bta3o lel left edge 
        if (direction.x > 0 && (transform.position.x - size) > rightEdge.x)
          { Vector3 position =transform.position;
            position.x = leftEdge.x - size;
            transform.position = position;  
          }
        // Check if the object is past the left edge of the screen w
        //x da object lw hwa fi left edge
        else if (direction.x < 0 && (transform.position.x + size) < leftEdge.x)
          {  
            Vector3 position =transform.position;
            position.x = rightEdge.x + size;
            transform.position = position; 
          }
        // w lw object msh 3nd left or right edge ,Move it in specified direction, spead and time 
        else 
           {
            transform.Translate(direction * speed * Time.deltaTime);
            }
    }

} 

