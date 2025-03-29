using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logout : MonoBehaviour
{
    // Tên Scene muốn chuyển đến
    public string sceneName;

    // Phương thức gọi khi nhấn vào Button
    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
