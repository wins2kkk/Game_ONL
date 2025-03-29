using UnityEngine;
using UnityEngine.UI; // Thêm thư viện UI để sử dụng Slider

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float CurrentHealth;

    [Header("Health UI")]
    public Slider HealthSlider; // Tham chiếu đến thanh slider trong UI

    [Header("Game Over UI")]
    public GameObject gameOverPanel; // Tham chiếu đến Game Over panel

    [Header("Camera Settings")]
    public Camera mainCamera; // Tham chiếu đến camera chính, nếu cần dừng hoặc tạm dừng camera

    private void Start()
    {
        // Đặt giá trị ban đầu cho máu
        CurrentHealth = MaxHealth;

        // Cập nhật slider nếu nó được gắn
        if (HealthSlider != null)
        {
            HealthSlider.maxValue = MaxHealth;
            HealthSlider.value = CurrentHealth;
        }

        // Ẩn game over panel khi bắt đầu trò chơi
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Nếu cần, đảm bảo camera di chuyển hoặc hành động bình thường khi game chưa kết thúc
        if (mainCamera != null)
        {
            mainCamera.enabled = true; // Camera sẽ hoạt động bình thường
        }
    }

    public void TakeDamage(float damage)
    {
        // Trừ máu và giới hạn trong khoảng từ 0 đến MaxHealth
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        Debug.Log($"Player took {damage} damage. Current health: {CurrentHealth}");

        // Cập nhật slider
        if (HealthSlider != null)
        {
            HealthSlider.value = CurrentHealth;
        }

        // Kiểm tra nếu máu bằng 0 thì chết
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");

        // Dừng game (dừng toàn bộ hoạt động của trò chơi)
        Time.timeScale = 0f; // Tạm dừng toàn bộ trò chơi

        // Tạm dừng camera hoặc bất kỳ đối tượng nào khác nếu cần
        if (mainCamera != null)
        {
            mainCamera.enabled = false; // Tắt camera chính, nếu bạn muốn ngừng di chuyển camera
        }

        // Hiển thị game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    // Hàm hồi máu (tuỳ chọn)
    public void Heal(float healAmount)
    {
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        Debug.Log($"Player healed {healAmount}. Current health: {CurrentHealth}");

        if (HealthSlider != null)
        {
            HealthSlider.value = CurrentHealth;
        }
    }

    // Tạm dừng game và camera (nếu cần)
    public void PauseGame()
    {
        Time.timeScale = 0f; // Tạm dừng game
        if (mainCamera != null)
        {
            mainCamera.enabled = false; // Tắt camera
        }
    }

    // Tiếp tục game và camera (nếu cần)
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Tiếp tục game
        if (mainCamera != null)
        {
            mainCamera.enabled = true; // Bật lại camera
        }
    }
}
