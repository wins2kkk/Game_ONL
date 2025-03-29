using Newtonsoft.Json;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class EditPlayerInfo : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField regionInput; // Input cho Region
    public TMP_InputField oldPasswordInput;  // Mật khẩu cũ
    public TMP_InputField newPasswordInput;  // Mật khẩu mới
    public GameObject notification;
    public GameObject PlayerInfoPanel;
    public GameObject EditPlayerPanel;

    private string baseUrl = "https://localhost:7033/";

    private void Start()
    {
        LoadCurrentInfo();
    }

    // Hàm load thông tin người chơi hiện tại
    void LoadCurrentInfo()
    {
        nameInput.text = PlayerPrefs.GetString("name");
        regionInput.text = PlayerPrefs.GetInt("regionId").ToString();
    }

    // Hàm khi nhấn nút "Save"
    public void OnSaveButtonClicked()
    {
        StartCoroutine(UpdatePlayerInfo());
        LoadCurrentInfo();
    }

    // Hàm cập nhật thông tin người chơi và mật khẩu
    IEnumerator UpdatePlayerInfo()
    {
        string name = nameInput.text;
        string regionStr = regionInput.text;
        string oldPassword = oldPasswordInput.text;
        string newPassword = newPasswordInput.text;

        // Kiểm tra nếu thông tin nhập vào không hợp lệ
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(regionStr) || !int.TryParse(regionStr, out int region))
        {
            ShowNotification("Vui lòng nhập đầy đủ thông tin hợp lệ!");
            yield break;
        }

        // Kiểm tra mật khẩu cũ và mới
        if (!string.IsNullOrWhiteSpace(oldPassword) && string.IsNullOrWhiteSpace(newPassword))
        {
            ShowNotification("Vui lòng nhập mật khẩu mới.");
            yield break;
        }

        // Lấy UserId từ PlayerPrefs
        string userId = PlayerPrefs.GetString("UserId");

        // Tạo request với UserId, mật khẩu cũ và mật khẩu mới
        RequestUpdatePlayerData requestData = new RequestUpdatePlayerData(name, region, userId, oldPassword, newPassword);
        string body = JsonUtility.ToJson(requestData);

        using (UnityWebRequest www = new UnityWebRequest(baseUrl + "api/APIGame/UpdatePlayer", "PUT"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(body);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                ShowNotification("Lỗi kết nối: " + www.error);
            }
            else
            {
                // Debug logs để kiểm tra phản hồi
                Debug.Log("Response: " + www.downloadHandler.text);

                ResponseUpdatePlayer response = JsonConvert.DeserializeObject<ResponseUpdatePlayer>(www.downloadHandler.text);
                if (response.IsSuccess)
                {
                    PlayerPrefs.SetString("name", name);
                    PlayerPrefs.SetInt("regionId", region);

                    // Nếu mật khẩu được thay đổi, lưu mật khẩu mới vào PlayerPrefs
                    if (!string.IsNullOrWhiteSpace(newPassword))
                    {
                        PlayerPrefs.SetString("password", newPassword);  // Lưu mật khẩu mới
                    }

                    PlayerPrefs.Save();

                    ShowNotification("Cập nhật thành công!");

                    // Set PlayerInfoPanel active trước khi gọi LoadPlayerInfo
                    PlayerInfoPanel.SetActive(true);
                    EditPlayerPanel.SetActive(false);

                    // Đảm bảo gọi LoadPlayerInfo sau khi PlayerInfoPanel đã được kích hoạt
                    yield return new WaitForEndOfFrame();  // Đợi 1 frame để UI cập nhật
                                                           //LoadPlayerInfo();  // Gọi LoadPlayerInfo để cập nhật thông tin người chơi
                }
                else
                {
                    ShowNotification("Lỗi: " + response.Notification);
                }
            }
        }
    }


    // Hiển thị thông báo
    void ShowNotification(string message)
    {
        notification.SetActive(true);
        TMP_Text textComponent = notification.GetComponentInChildren<TMP_Text>();
        textComponent.text = message;
    }

    // Hàm khi nhấn nút "Hủy"
    public void OnCancelButtonClicked()
    {
        PlayerInfoPanel.SetActive(true);
        EditPlayerPanel.SetActive(false);
    }
}

// Class để định nghĩa dữ liệu yêu cầu và phản hồi từ server
public class RequestUpdatePlayerData
{
    public string Name;
    public int RegionId;
    public string UserId;
    public string OldPassword;
    public string NewPassword;

    public RequestUpdatePlayerData(string name, int regionId, string userId, string oldPassword, string newPassword)
    {
        Name = name;
        RegionId = regionId;
        UserId = userId;
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }
}

public class ResponseUpdatePlayer
{
    public bool IsSuccess;
    public string Notification;
}
