using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public void PlayGame()
   {
      SceneManager.LoadScene("Scenes/SampleScene");
   }

   public void Por()
   {
      SceneManager.LoadScene("Scenes/Main");
   }
   public void QuitGame()
   {
      Application.Quit();
   }
}
