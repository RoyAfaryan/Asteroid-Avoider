using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject NewBest;
    private float startTime;
    private bool isRunning;
    
    public GameObject spaceship;

    private string finalTime;
    private float elapsedTime;
    private string loadedTimeString;
    private float loadedTimeFloat;

    private bool hasData = true;
    
    
    void Start()
    {
        startTime = Time.time;
        isRunning = true;
        NewBest.SetActive(false);
    }

    
    public async void Awake()
    {
        await UnityServices.InitializeAsync();
        Debug.Log("Player ID:" + AuthenticationService.Instance.PlayerId);

    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime = Time.time - startTime;
            string timerString = FormatTime(elapsedTime);
            timerText.text = timerString;

            if(spaceship == null)
            {
                PauseTimer();
            }
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time - Mathf.FloorToInt(time)) * 100);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public async void PauseTimer()
    {
        isRunning = false;

        finalTime = timerText.text;
        Debug.Log("final time: " + finalTime);

        LoadData();

        //save data if new best score; else score is discarded
        StartCoroutine(WaitFor2Seconds());
       
    }

    public async void SaveData()
    {
        var playerData = new Dictionary<string, object>();
        playerData.Add("BestTimeString", finalTime); // Use a clear key and finalTime value
        playerData.Add("BestTimeFloat", elapsedTime); // Use a clear key and finalTime value
        await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
        
    }

    public async void LoadData()
    {
        var set = new HashSet<string>();
        set.Add("BestTimeString");
        set.Add("BestTimeFloat");
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(set);

        if (playerData.TryGetValue("BestTimeString", out var keyString)) {
            loadedTimeString = keyString.Value.GetAs<string>();
        

        if (playerData.TryGetValue("BestTimeFloat", out var keyFloat)) {
            loadedTimeFloat = keyFloat.Value.GetAs<float>();
            Debug.Log("Loaded Time float: "+ loadedTimeFloat);
        }

        } else
        {
            hasData = false;
        }
    }

    IEnumerator WaitFor2Seconds()
    {
        
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);
        
        Debug.Log("Elapsed Time:" + elapsedTime + "\nLoaded Time" + loadedTimeFloat);
        if(elapsedTime > loadedTimeFloat){
            SaveData();
            NewBest.SetActive(true);

        }
    }

    public void ResumeTimer()
    {
        isRunning = true;
        startTime = Time.time - GetTotalSeconds(timerText.text);
    }

    public void ResetTimer()
    {
        startTime = Time.time;
    }

    private float GetTotalSeconds(string timeString)
    {
        string[] timeParts = timeString.Split(':');
        int minutes = int.Parse(timeParts[0]);
        int seconds = int.Parse(timeParts[1]);
        int milliseconds = int.Parse(timeParts[2]);

        return minutes * 60 + seconds + milliseconds / 1000f;
    }

  
}
