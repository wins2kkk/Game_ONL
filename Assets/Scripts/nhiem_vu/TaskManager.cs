using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public Toggle collectCoinToggle; // Toggle nhặt coin
    public Toggle killEnemyToggle;  // Toggle giết Dragon

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            collectCoinToggle.isOn = true; // Đánh dấu nhiệm vụ nhặt coin hoàn thành
            Destroy(other.gameObject); // Xóa coin sau khi nhặt
        }
    }

    public void CompleteKillDragonTask()
    {
        if (!killEnemyToggle.isOn) // Nếu nhiệm vụ giết Dragon chưa hoàn thành
        {
            killEnemyToggle.isOn = true; // Đánh dấu nhiệm vụ giết Dragon hoàn thành
            Debug.Log("Nhiệm vụ giết Dragon hoàn thành!");
        }
    }
}
