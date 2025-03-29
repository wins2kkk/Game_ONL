using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Register;
using UnityEngine.Networking;
using System.Linq;

public class GameRegister : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField nameInput;
    public GameObject notification;
    public static int selectedRegionId;
    public GameObject Login;

    public void login()
    {
        Login.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnButtonClickRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        selectedRegionId = GameRegion.selectedRegionId;
        RegisterRequestData requestData = new RegisterRequestData(
            emailInput.text,
            passwordInput.text,
            nameInput.text,
            " ",
            selectedRegionId
        );

        string body = JsonUtility.ToJson(requestData);

        // Kiểm tra các trường nhập
        if (string.IsNullOrWhiteSpace(emailInput.text) || string.IsNullOrWhiteSpace(passwordInput.text) || string.IsNullOrWhiteSpace(nameInput.text))
        {
            notification.SetActive(true);
            var textComponent = notification.GetComponentsInChildren<TMP_Text>();
            if (textComponent.Length > 1)
            {
                textComponent[1].text = "Vui lòng nhập đầy đủ thông tin!";
            }
            yield break;
        }

        using (UnityWebRequest www = new UnityWebRequest("https://localhost:7033/api/APIGame/Register", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(body);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Network or HTTP Error: " + www.error);  
                string responseJson = www.downloadHandler.text;
                Debug.Log("Server Response: " + responseJson);

                try
                {
                    ResponseUserError responseErr = JsonConvert.DeserializeObject<ResponseUserError>(responseJson);

                    if (responseErr != null && responseErr.data != null && responseErr.data.Count > 0)
                    {
                        notification.SetActive(true);
                        var textComponent = notification.GetComponentsInChildren<TMP_Text>();
                        string error = string.Join("\n", responseErr.data.Select(e => e.description));

                        if (textComponent.Length > 1)
                        {
                            textComponent[1].text = error;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Error parsing error response: " + ex.Message);
                }
            }
            else
            {
                string json = www.downloadHandler.text;
                Debug.Log("Success Response: " + json);

                try
                {
                    ResponUserSuccess response = JsonConvert.DeserializeObject<ResponUserSuccess>(json);

                    if (response != null && response.isSuccess)
                    {
                        notification.SetActive(true);
                        var textComponent = notification.GetComponentsInChildren<TMP_Text>();

                        if (textComponent.Length > 1)
                        {
                            textComponent[1].text =
                                "Đăng ký thành công, vui lòng kiểm tra trong tài khoản " + response.data.name;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Error parsing success response: " + ex.Message);
                }
            }
        }
    }
}

