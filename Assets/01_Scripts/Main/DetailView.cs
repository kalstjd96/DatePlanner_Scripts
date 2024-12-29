using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DetailView : MonoBehaviour
{
    public ScrollRect scrollRect; // 전체 화면 스크롤 뷰
    public GameObject imageSliderContent; // 슬라이드 이미지의 부모 오브젝트
    public GameObject imagePrefab; // 이미지 프리팹

    public Text titleText;
    public Text descriptionText;

    private void Start()
    {
        // DetailPageData에서 데이터를 불러옴
        if (DetailPageData.SelectedCourse != null)
        {
            titleText.text = DetailPageData.SelectedCourse.Title;
            descriptionText.text = DetailPageData.SelectedCourse.Description;

            // 이미지를 불러오는 메서드 호출
            //LoadImages(DetailPageData.SelectedCourse.Images);
        }
    }

    private void LoadImages(List<string> imageUrls)
    {
        foreach (string url in imageUrls)
        {
            GameObject imageObject = Instantiate(imagePrefab, imageSliderContent.transform);
            Image imgComponent = imageObject.GetComponent<Image>();

            // URL로 이미지를 로드 (예: 리소스나 URL을 통해 로드)
            StartCoroutine(LoadImageFromUrl(url, imgComponent));
        }
    }

    private IEnumerator LoadImageFromUrl(string url, Image imgComponent)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                imgComponent.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                Debug.LogError("Image loading failed: " + request.error);
            }
        }
    }
}
