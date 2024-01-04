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

   public void Setting()
   {
          SceneManager.LoadScene("Scenes/Setting");
   }
   public void Back()
   {
      SceneManager.LoadScene("Scenes/Menu");
   }
   public void QuitGame()
   {
      Application.Quit();
   }
}
