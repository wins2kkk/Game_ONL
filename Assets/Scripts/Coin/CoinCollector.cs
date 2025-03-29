using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public TextMeshProUGUI coinText; // Text UI để hiển thị điểm coin
    public Toggle collectCoinToggle; // Toggle hiển thị trạng thái nhiệm vụ nhặt coin
    private int Coins = 0; // Biến lưu điểm hiện tại
    private bool taskCompleted = false; // Kiểm tra xem nhiệm vụ nhặt coin đã hoàn thành chưa
    public Toggle killDragonToggle; // Toggle hiển thị trạng thái nhiệm vụ giết Dragon
    private bool dragonTaskCompleted = false; // Kiểm tra xem nhiệm vụ giết Dragon đã hoàn thành chưa

    internal static object Instance;

    private void Start()
    {
        // Xóa điểm từ PlayerPrefs mỗi khi game bắt đầu
        

        // Hoặc nếu muốn xóa tất cả PlayerPrefs:
        // PlayerPrefs.DeleteAll();

        // Đảm bảo khởi tạo điểm ban đầu là 0
        //score = 0;

        UpdateScoreText(); // Cập nhật điểm trên UI
    }


    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nhặt coin
        if (other.CompareTag("Coin"))
        {
            Coins++; // Cộng 1 coin
            Destroy(other.gameObject); // Xóa coin
            FindObjectOfType<AudioManager>().Play("coin"); // Phát âm thanh khi nhặt coin

            // Nếu nhiệm vụ chưa hoàn thành và người chơi nhặt 1 coin
            if (!taskCompleted)
            {
                Coins += 5; // Cộng thêm 5 coin khi hoàn thành nhiệm vụ
                taskCompleted = true; // Đánh dấu nhiệm vụ nhặt coin đã hoàn thành
                collectCoinToggle.isOn = true; // Đánh dấu Toggle nhiệm vụ nhặt coin đã hoàn thành
            }


            // Lưu điểm vào PlayerPrefs
          //  PlayerPrefs.SetInt("Coin", score);
            //PlayerPrefs.Save(); // Đảm bảo lưu ngay lập tức
            SaveScore();
            UpdateScoreText(); // Cập nhật điểm sau mỗi lần cộng
        }

        // Kiểm tra giết Dragon
        if (other.CompareTag("Dragon"))
        {
            Coins += 20; // Cộng 20 coin khi giết Dragon
            Destroy(other.gameObject); // Xóa Dragon
            FindObjectOfType<AudioManager>().Play("dragonKill"); // Phát âm thanh khi giết Dragon

            // Nếu nhiệm vụ giết Dragon chưa hoàn thành
            if (!dragonTaskCompleted)
            {
                dragonTaskCompleted = true; // Đánh dấu nhiệm vụ giết Dragon đã hoàn thành
                killDragonToggle.isOn = true; // Đánh dấu Toggle giết Dragon đã hoàn thành
            }


            // Lưu điểm vào PlayerPrefs
          //  PlayerPrefs.SetInt("Coin", score);
            //PlayerPrefs.Save(); // Đảm bảo lưu ngay lập tức
            //SaveScore();
           // UpdateScoreText(); // Cập nhật điểm sau mỗi lần giết Dragon

        }

        // Kiểm tra coin2
        if (other.CompareTag("Coin2"))
        {
            Coins += 5; // Cộng 5 coin
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("coin");


            // Lưu điểm vào PlayerPrefs
            // PlayerPrefs.SetInt("Coin", score);
            // PlayerPrefs.Save(); // Đảm bảo lưu ngay lập tức
            SaveScore();
            UpdateScoreText(); // Cập nhật điểm sau khi cộng coin

        }

        // Kiểm tra coin3
        if (other.CompareTag("Coin3"))
        {
            Coins += 20; // Cộng 20 coin
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("coin");


            // Lưu điểm vào PlayerPrefs
           // PlayerPrefs.SetInt("Coin", Coin );
            SaveScore(); // Đảm bảo lưu ngay lập tức
            UpdateScoreText(); // Cập nhật điểm sau khi cộng coin
        }
    }

    // Hàm cập nhật điểm trên UI
    private void UpdateScoreText()
    {
        coinText.text = "Coin: " + Coins; // Hiển thị điểm lên Text UI
    }
    private void SaveScore()
    {
        PlayerPrefs.SetInt("Coins", Coins); // Lưu số coin vào PlayerPrefs với key "CoinScore"
                                            //PlayerPrefs.Save(); // Lưu thay đổi vào bộ nhớ
    }
    // Hàm kiểm tra và cộng 20 coin khi hoàn thành nhiệm vụ giết Dragon và Toggle đã được bật
    public void CompleteKillDragonTask()
    {
        // Kiểm tra nếu nhiệm vụ giết Dragon đã hoàn thành và Toggle đã bật
        if (killDragonToggle.isOn && !dragonTaskCompleted)
        {
            Coins += 20; // Cộng 20 coin
            dragonTaskCompleted = true; // Đánh dấu nhiệm vụ giết Dragon đã hoàn thành
            Debug.Log("Nhiệm vụ giết Dragon đã hoàn thành, +20 coin!");

            // Lưu điểm vào PlayerPrefs
           // PlayerPrefs.SetInt("Coin", Coin);
           // SaveScore(); // Đảm bảo lưu ngay lập tức
//UpdateScoreText(); // Cập nhật điểm sau khi cộng 20 coin
        }
    }
}
