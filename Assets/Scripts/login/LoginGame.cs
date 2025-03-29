using Newtonsoft.Json;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginGame : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public GameObject notfication;
    public GameObject Register;
    private string baseUrl = "https://localhost:7033/"; // URL của API

    private void Awake()
    {
        // Xóa tất cả dữ liệu đăng nhập khi mở ứng dụng
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }


    public void Registerr()
    {
        Register.SetActive(true);
        gameObject.SetActive(false);
    }

    IEnumerator login()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            ShowNotification("Vui lòng nhập đầy đủ thông tin!");
            yield break;
        }

        RequestLoginData loginData = new RequestLoginData(email, password);
        string body = JsonConvert.SerializeObject(loginData);

        using (UnityWebRequest www = new UnityWebRequest(baseUrl + "api/APIGame/Login", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(body);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                ShowNotification($"Lỗi kết nối: {www.error}");
            }
            else
            {
                ResponseLogin response = JsonConvert.DeserializeObject<ResponseLogin>(www.downloadHandler.text);
                if (response.IsSuccess)
                {
                    PlayerPrefs.SetString("UserId", response.Data.user.id);
                    PlayerPrefs.SetString("email", response.Data.user.email);
                    PlayerPrefs.SetString("name", response.Data.user.name);
                    PlayerPrefs.SetString("LinkAvatar", baseUrl + "uploads/avatars/" + response.Data.user.avatar);
                    PlayerPrefs.SetInt("regionId", response.Data.user.regionId);
                    PlayerPrefs.Save();

                    ShowNotification("Đăng nhập thành công!");
                    SceneManager.LoadScene(1);
                }
                else
                {
                    ShowNotification(response.Notification);
                }
            }
        }
    }

    public void OnLoginButtonClicked()
    {
        StartCoroutine(login());
    }

    public void anotfication()
    {
        notfication.SetActive(false);
    }

    private void ShowNotification(string message)
    {
        notfication.SetActive(true);
        notfication.GetComponentsInChildren<TMP_Text>()[1].text = message;
    }
}

// Class mô phỏng request và response
public class RequestLoginData
{
    public string Email;
    public string Password;

    public RequestLoginData(string email, string password)
    {
        Email = email;
        Password = password;
    }
}

public class ResponseLogin
{
    public bool IsSuccess;
    public string Notification;
    public ResponseUserData Data;
}

public class ResponseUserData
{
    public UserData user;
}

public class UserData
{
    public string id;
    public string email;
    public string name;
    public string avatar;
    public int regionId;
}
