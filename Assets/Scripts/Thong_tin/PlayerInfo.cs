using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text emailText;
    public TMP_Text regionText;
    public RawImage avatarImage;
    public GameObject PlayerInfoPanel;
    public GameObject EditPlayerPanel;

    public Texture2D defaultAvatarTexture; // Avatar mặc định

    private void Start()
    {
        LoadPlayerInfo(); // Gọi hàm để load thông tin khi bắt đầu

    }

    // Hàm LoadPlayerInfo để lấy và cập nhật thông tin từ PlayerPrefs
    public void LoadPlayerInfo()
    {
        nameText.text = PlayerPrefs.GetString("name");
        emailText.text = PlayerPrefs.GetString("email");
        regionText.text = $"Region ID: {PlayerPrefs.GetInt("regionId")}";
        StartCoroutine(LoadAvatar(PlayerPrefs.GetString("LinkAvatar")));
    }

    // Hàm tải avatar
    IEnumerator LoadAvatar(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            Debug.LogWarning("URL avatar không hợp lệ hoặc không có avatar.");
            avatarImage.texture = defaultAvatarTexture; // Gán avatar mặc định
            yield break;
        }

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            avatarImage.texture = texture;
        }
        else
        {
            avatarImage.texture = defaultAvatarTexture; // Gán avatar mặc định nếu có lỗi
        }
    }

    public void OnEditButtonClicked()
    {
        PlayerInfoPanel.SetActive(false);
        EditPlayerPanel.SetActive(true);
    }

    public void OnLogoutButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("LoginScene");
    }
}
