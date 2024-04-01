// for enabling and disabling the frog object which will be displayed in home and checking if all homes are occupied to progress to the next level

using UnityEngine;
using UnityEngine.SceneManagement;


public class Home : MonoBehaviour
{
    public GameObject frog;
    private static int occupiedHomes = 0;
    
    //It activates the frog object and increments the occupied Homes variable
    private void OnEnable()
    {
        frog.SetActive(true);
        occupiedHomes++;
    }

    //b3mlha call lma home object ykon inactive f scene+ It deactivates the frog object (y3ni active frog ht5tfy) and decrements the occupiedHomes variable.
    private void OnDisable()
    {
        frog.SetActive(false);
        occupiedHomes--;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {    //lw another object with a 2D collider d5l 3la collider el home object w other object da tag bta3o "Player", then enable the home object
        if (/*!enabled &&*/ other.tag/*gameObject.CompareTag*/ == "Player")
        {   
            enabled = true;
           
           FindObjectOfType<GameManger>().HomeOccupied();
      //w lw homeoccupied =4 7ml scene el tani
           if (occupiedHomes == 4)
        {
            SceneManager.LoadScene("Level 2");
        }
           
        }
    }

}
