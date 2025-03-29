using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cointext : MonoBehaviour
{
    public TextMeshProUGUI coinText; // Tham chiếu đến TextMeshProUGUI để hiển thị coin

    private void OnEnable()
    {
        // Lấy số coin từ PlayerPrefs (hoặc có thể từ một hệ thống quản lý điểm khác)
        int score = PlayerPrefs.GetInt("Coins", 0);

        // Cập nhật UI để hiển thị số coin
        coinText.text = "Coin: " + score;
    }
}
