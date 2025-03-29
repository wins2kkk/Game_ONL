using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Thêm thư viện UI để sử dụng Slider

public class Seting_Music : MonoBehaviour
{
    // Các tham chiếu đến UI
    public GameObject deathUI; // Reference to death UI
    public Slider backgroundMusicSlider; // Slider để điều chỉnh âm thanh nền
    public Slider sfxSlider; // Slider để điều chỉnh âm thanh SFX

    // Các GameObject chứa âm thanh nền và SFX
    public GameObject backgroundMusicObject; // GameObject chứa âm thanh nền
    public GameObject sfxObject; // GameObject chứa âm thanh SFX

    private AudioSource backgroundMusicSource; // AudioSource cho âm thanh nền
    private AudioSource sfxSource; // AudioSource cho âm thanh SFX

    private void Start()
    {
        // Lấy AudioSource từ các GameObject
        backgroundMusicSource = backgroundMusicObject.GetComponent<AudioSource>();
        sfxSource = sfxObject.GetComponent<AudioSource>();

        // Gắn các sự kiện cho Slider
        backgroundMusicSlider.onValueChanged.AddListener(ChangeBackgroundMusicVolume);
        sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);

        // Thiết lập giá trị mặc định cho Slider
        backgroundMusicSlider.value = 1f;  // Âm thanh nền mặc định 100%
        sfxSlider.value = 1f;  // Âm thanh SFX mặc định 100%

        // Ẩn deathUI khi bắt đầu
        deathUI.SetActive(false);
    }

    // Hiển thị UI chết
    public void PlayerDeath()
    {
        // Hiển thị UI chết
        deathUI.SetActive(true);

        // Dừng âm thanh nền khi người chơi chết
        backgroundMusicSource.Stop();

        // Phát âm thanh SFX khi người chơi chết
        sfxSource.Play();
    }

    // Tải cảnh theo tên
    public void LoadSceneByName(string sceneName)
    {
        // Tải cảnh dựa trên tên cảnh
        SceneManager.LoadScene(sceneName);
    }

    // Load lại scene Start (có thể là màn hình chính)
    public void LoadsceneStart(string sceneName)
    {
        // Tải lại scene Start
        SceneManager.LoadScene("Start");
        Time.timeScale = 1f; // Đảm bảo game không bị dừng lại

        // Ẩn deathUI khi quay lại màn hình Start
        deathUI.SetActive(false);
    }

    // Load lại scene khi người chơi die
    public void Loadscenegamedie(string sceneName)
    {
        // Tải lại scene Map
        SceneManager.LoadScene("Map");
        Time.timeScale = 1f; // Đảm bảo game không bị dừng lại

        // Ẩn deathUI khi quay lại scene game
        deathUI.SetActive(false);
    }

    // Hàm điều chỉnh âm lượng âm thanh nền
    public void ChangeBackgroundMusicVolume(float volume)
    {
        // Cập nhật âm lượng cho âm thanh nền
        backgroundMusicSource.volume = volume;
    }

    // Hàm điều chỉnh âm lượng âm thanh SFX
    public void ChangeSFXVolume(float volume)
    {
        // Cập nhật âm lượng cho âm thanh SFX
        sfxSource.volume = volume;
    }


    public void HienSettingsPanel()
    {
        // Đổi trạng thái của settingsPanel (Ẩn/Hiện)
        deathUI.SetActive(true);
    }
    public void AnSettingsPanel()
    {
        // Đổi trạng thái của settingsPanel (Ẩn/Hiện)
        deathUI.SetActive(false);
    }
}
