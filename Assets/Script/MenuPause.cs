using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
   public GameObject Resume,Quit,Retry,Player;

   public void pause (){
       Resume.SetActive(true);
       Quit.SetActive(true);
       Time.timeScale = 0;
   }

   public void resume (){
       Resume.SetActive(false);
       Quit.SetActive(false);
       Time.timeScale = 1;
   }

   public void quit (){
       SceneManager.LoadScene(0);
       Time.timeScale = 1;
   }

   public void retry (){
    //   Player.GetComponent<Movement>().currentHealth = 100;
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
