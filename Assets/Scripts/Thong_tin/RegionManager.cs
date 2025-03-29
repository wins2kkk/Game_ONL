using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RegionManager : MonoBehaviour
{
    public TMP_Text regionText;  // Text UI để hiển thị vùng
    private string baseUrl = "https://localhost:7033/";  // URL của API

    private void Start()
    {
        StartCoroutine(GetRegions());
    }

    // Hàm gọi API để lấy danh sách vùng
    IEnumerator GetRegions()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl + "api/APIGame/GetAllRegion"))
        {
            yield return www.SendWebRequest();

            // Kiểm tra lỗi kết nối
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Lỗi kết nối: " + www.error);
                yield break;
            }

            // Xử lý phản hồi từ API
            ResponseRegion response = JsonConvert.DeserializeObject<ResponseRegion>(www.downloadHandler.text);

            if (response.IsSuccess)
            {
                // Lấy danh sách các vùng và hiển thị tên vùng đầu tiên (hoặc tất cả các vùng nếu cần)
                string regionNames = "Danh sách vùng: \n";
                foreach (var region in response.Data)
                {
                    regionNames += region.Name + $" | Region ID: {region.regionId}\n";
                };  // Hiển thị tên vùng
                
                regionText.text = regionNames;  // Cập nhật Text UI
            }
            else
            {
                regionText.text = "Lỗi: " + response.Notification;
            }
        }
    }
    // Lớp Region
    public class Region
    {
        public int regionId;
        public string Name;
    }

    // Lớp ResponseRegion
    public class ResponseRegion
    {
        public bool IsSuccess;
        public string Notification;
        public List<Region> Data;
    }

}
