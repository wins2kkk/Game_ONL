using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject taskPanel; // Kéo Panel Nhiệm Vụ vào đây từ Unity Editor

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Kiểm tra nếu nhấn phím Q
        {
            ToggleTaskPanel();
        }
    }

    void ToggleTaskPanel()
    {
        taskPanel.SetActive(!taskPanel.activeSelf); // Bật/tắt panel
    }
}
