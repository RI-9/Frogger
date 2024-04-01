//da b2a 3shan manage mechanism bta3t el le3ba

using System.Collections;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManger : MonoBehaviour
{
   private void Start()
    {  
        NewGame();
    }
    private Frogger frogger;  //frogger w home dol objects
    private Home[] homes;

    public GameObject gameOverMenu;  //ui element 
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text timeText;

    private int score;
    private int lives;
    private int time; 

    // finds and initializes the Home and Frogger objects in the scene.
    private void Awake()
    {
        //Application.targetFrameRate = 60;

        homes = FindObjectsOfType<Home>();
        frogger = FindObjectOfType<Frogger>();
    }
    //It resets the game state, hides the game over menu, sets the score and lives to their starting values, and starts a new level
    private void NewGame()
    {
        gameOverMenu.SetActive(false);

        SetScore(0);
        SetLives(3);
        NewLevel();
        
    }
  //called at the start of a new level. It disables all homes and respawns(btrg3) el frog lel scene
    private void NewLevel()
    {
        for (int i = 0; i < homes.Length; i++) {
            homes[i].enabled = false;
        }
    
        Respawn();
     
    }

    //respawns el frogger and starts a timer coroutine w bt counts down from 30 seconds. If the timer reaches 0, the player dies.
    private void Respawn ()
    {
         frogger.Respawn();
         StopAllCoroutines();
         StartCoroutine(Timer(30));
    }

    //lw time 5ls frggger tmot
    private IEnumerator Timer(int duration)
   {
       time= duration;
        timeText.text = time.ToString();
      while (time > 0)
        {
            yield return new WaitForSeconds(1);

            time--;
            timeText.text = time.ToString();
        }

        frogger.Death();
   }

 //It decrements the lives count w btrg3 frogger if there are still lives left. w lw frogger has no more lives, the game ends
   public void Died()
   {
      SetLives(lives -1);
      if (lives > 0){
        Invoke(nameof(Respawn), 1f);
      }
      else {
        Invoke(nameof(GameOver), 1f);

      }
   }

    //when the player has no more lives. It displays the game over menu and waits for the player to press the Enter key to start a new game
    private void GameOver(){

        frogger.gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(PlayAgain());
    }
     //for pressing enter to play again
    private IEnumerator PlayAgain()
    {
        bool playAgain = false;

        while (!playAgain)
        {
            if(Input.GetKeyDown(KeyCode.Return)){
                 playAgain=true;
            }
            yield return null;
        }
        NewGame();
    }

    //when the player advances to a new row. It adds 10 points to the player's score
    public void AdvancedRow()
        {
           SetScore(score +10);
        }
    
      
     public void HomeOccupied()
    {   //when the player occupies a home. It stops the frogger's movement 
        frogger.gameObject.SetActive(false);
//calculates a bonus score based on the remaining time,
// adds the bonus score and a fixed amount to the player's score
        int bonusPoints = time * 20;
        SetScore(score + bonusPoints + 50);

//and checks if all homes are occupied to progress to the next level.
        if (Cleared())
        {
            SetLives(lives + 1);
            SetScore(score + 1000);
            Invoke(nameof(NewLevel), 1f);
        }
        else {
            Invoke(nameof(Respawn), 1f);
        }
    }  


 private bool Cleared()
    {
        for (int i = 0; i < homes.Length; i++)
        {
            if (!homes[i].enabled) {
                return false;
            }
        }

        return true;
    }



//checks if all homes are occupied
    private void SetScore(int score)
    {
       this.score = score;
       scoreText.text = score.ToString();
      
    }
     
    private void SetLives (int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }

}
