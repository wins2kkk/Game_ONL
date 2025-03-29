using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Seting_hương_dan : MonoBehaviour
{
    public GameObject deathUI; // Reference to death UI

    private void Start()
    {
        // Ẩn deathUI khi bắt đầu
        deathUI.SetActive(false);
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
