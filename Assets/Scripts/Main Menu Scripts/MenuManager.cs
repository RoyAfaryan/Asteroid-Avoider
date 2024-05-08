using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject LeaderboardScreen;
    public GameObject ProfilePopUp;
    public TextMeshProUGUI Stats;


    private string username;
    private string score;
    private bool hasData = true;

    public async void Start(){
        await UnityServices.InitializeAsync();
        LeaderboardScreen.SetActive(false);
        ProfilePopUp.SetActive(false);
        LoadData();
    }
    
    public void OnPlayButtonPressed()
    {
        // Load the Gameplay scene
        SceneManager.LoadScene("Gameplay");
    }

    public void OnLeaderboardCloseButtonPressed()
    {
        LeaderboardScreen.SetActive(false);
    }

    public void OnLeaderboardButtonPressed()
    {
        LeaderboardScreen.SetActive(true);
    }

    public void OnProfileButtonPressed()
    {
        
        ProfilePopUp.SetActive(true);
        statDisplay();
    }

    public void OnProfileCloseButtonPressed()
    {
        ProfilePopUp.SetActive(false);
    }

    public void statDisplay()
    {
        if (hasData == true){
            Stats.text = "Username: " + username + "\nBest Time: " + score;
        } else
        {
            Stats.text = "Username: " + username + "\nBest Time: N/A";
        }

    }

    public async void LoadData()
    {
        var set = new HashSet<string>();
        set.Add("Username");
        set.Add("BestTimeString");
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(set);

        if (playerData.TryGetValue("Username", out var firstKey)) {
            username = firstKey.Value.GetAs<string>();
        }

        if (playerData.TryGetValue("BestTimeString", out var secondKey)) {
            score = secondKey.Value.GetAs<string>();
        
        } else
        {
            hasData = false;
        }
    }

    public void OnSignOutButtonPressed()
    {
        SignOut();
        SceneManager.LoadScene("Login");
    }

    public async void SignOut()
    {
        AuthenticationService.Instance.SignOut();
    }

   
}

