using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;


public class AuthManager : MonoBehaviour
{

    public GameObject loginPanel, signupPanel;

    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);

    }

    public void OpenRegisterPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);

    }

    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;
    public TMP_Text confirmRegisterText;

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("could not resolve all firebase dependencies: " + dependencyStatus);
            }
        });
    }
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }
    public void LoginButton()
    {
        StartCoroutine(login(emailLoginField.text, passwordLoginField.text));

    }
    public void registerButton()
    {
        StartCoroutine(register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }
    private IEnumerator login(string _email, string _password)
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {

            confirmLoginText.text = "";
            Debug.LogWarning(message: $"failed to register task with {loginTask.Exception}");
            FirebaseException firebaseEx = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong password";
                    break;
                case AuthError.InvalidEmail:
                    message = "invalid email";
                    break;
                case AuthError.UserNotFound:
                    message = "User Not Found";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            User = loginTask.Result.User;
            Debug.LogFormat("User signed in succesfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "logged in";
            SceneManager.LoadScene("menu");

        }
    }
    private IEnumerator register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            warningRegisterText.text = "Password does not match!";
        }
        else
        {

            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {

                confirmRegisterText.text = "";
                Debug.LogWarning(message: $"failed to register task with {registerTask.Exception}");
                FirebaseException firebaseEx = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing password";
                        break;
                    case AuthError.WrongPassword:
                        message = "Wrong password";
                        break;
                    case AuthError.InvalidEmail:
                        message = "invalid email";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email already in use";
                        break;
                }
                confirmRegisterText.text = message;
                //warningRegisterText.text = message;
            }
            else
            {
                User = registerTask.Result.User;
                if (User != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = _username };
                    var profileTask = User.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => profileTask.IsCompleted);
                    if (profileTask.Exception != null)
                    {
                        Debug.LogWarning(message: $"failed to register task with {profileTask.Exception}");
                        FirebaseException firebaseEx = profileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username set failed!";

                        SceneManager.LoadScene("menu");
                    }
                }


            }

        }
    }
}