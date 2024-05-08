using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour
{
    public GameObject LoginUI;
    public GameObject RegistrationUI;
    public GameObject ErrorUI;
    public TextMeshProUGUI ErrorMessage;
    public TMP_InputField LoginUsernameInput;
    public TMP_InputField RegistrationUsernameInput;

    public TMP_InputField LoginPasswordInput;
    public TMP_InputField RegistrationPasswordInput;
    public TMP_InputField ConfirmRegistrationPasswordInput;




    async void Awake()
	{
		try
		{
			await UnityServices.InitializeAsync();
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}

        ErrorUI.SetActive(false);
        RegistrationUI.SetActive(false);
        LoginUI.SetActive(true);
	}

    // Setup authentication event handlers if desired
    void SetupEvents() {
        AuthenticationService.Instance.SignedIn += () => {
            // Shows how to get a playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            // Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

        };

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log("Player signed out.");
        };

        AuthenticationService.Instance.Expired += () =>
            {
                Debug.Log("Player session could not be refreshed and expired.");
            };
    }


    public void OnBackButtonPressed()
    {
        RegistrationUI.SetActive(false);
        LoginUI.SetActive(true);
    }

    public void OnLoginButtonPressed()
    {
        SignInWithUsernamePasswordAsync(LoginUsernameInput.text, LoginPasswordInput.text);

    }

    public void OnNewUserButtonPressed()
    {
        RegistrationUI.SetActive(true);
        LoginUI.SetActive(false);
    }

    public void OnCreateAccountButtonPressed()
    {
        if (RegistrationPasswordInput.text == ConfirmRegistrationPasswordInput.text){
            SignUpWithUsernamePasswordAsync(RegistrationUsernameInput.text, RegistrationPasswordInput.text);
        } else
        {
            ErrorScreen("Passwords must match.");
        }
    }

    async Task SignUpWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("SignUp is successful.");

            // store username
            var playerData = new Dictionary<string, object>();
            playerData.Add("Username", username); // Use a clear key and finalTime value
            await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);

            // load main menu
            SceneManager.LoadScene("Main Menu");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            ErrorScreen("Sign-up failed. Invalid username OR Ensure password has at least 1 uppercase, 1 loserecase, 1 digit, and 1 symbol.");
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            //ErrorScreen("Sign-up failed. Please make sure password.");
            ErrorScreen("Sign-up failed. Invalid username OR Ensure password has at least 1 uppercase, 1 loserecase, 1 digit, and 1 symbol.");
        }
    }

    async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("SignIn is successful.");
            Debug.Log("Player ID:" + AuthenticationService.Instance.PlayerId);
            SceneManager.LoadScene("Main Menu");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            ErrorScreen("Login failed. Please use valid credentials.");
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            ErrorScreen("Login failed. Please use valid credentials.");
        }
    }

    public void ErrorScreen(string ErrorText)
    {
        ErrorUI.SetActive(true);
        ErrorMessage.text = ErrorText;
    }

    public void OnCloseErrorScreen()
    {
        ErrorUI.SetActive(false);
    }
    


}
