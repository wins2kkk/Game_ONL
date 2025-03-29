using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Seting : MonoBehaviour
{
    // Hàm này sẽ được gọi khi nhấn nút
    public GameObject deathUI; // Reference to your UI
    public void PlayerDeath()
    {
        // Hiển thị UI chết
        deathUI.SetActive(true);
    }
    public void LoadSceneByName(string sceneName)
    {
        // Tải cảnh dựa trên tên cảnh
        SceneManager.LoadScene(sceneName);
    }


    public void LoadsceneStart(string sceneName)
    {
        // Tải cảnh dựa trên tên cảnh
        SceneManager.LoadScene("Start");
        Time.timeScale = 1f;
    }
    public void Loadscenegamedie(string sceneName)
    {
        // Tải cảnh dựa trên tên cảnh
        SceneManager.LoadScene("Map");
        Time.timeScale = 1f;
    }
}