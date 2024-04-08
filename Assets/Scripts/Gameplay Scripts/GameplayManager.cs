using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{

    public GameObject EndGameScreen;

    public GameObject Spaceship;

    // Start is called before the first frame update
    void Start()
    {
        EndGameScreen.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Spaceship == null)
        {
            EndGameScreen.SetActive(true);
        }
        
    }

    // Method to be called when the "Play Again" button is pressed
    public void OnPlayAgainButtonPressed()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
