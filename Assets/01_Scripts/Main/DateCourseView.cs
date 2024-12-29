using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static DateCourseController;


namespace Firebase
{
    public class DateCourseView : MonoBehaviour
    {
        public TextMeshProUGUI TitleText; // 제목 표시 텍스트
        public GameObject itemPrefab; // 개별 아이템 프리팹
        public Transform content; // 스크롤 콘텐츠 영역
        public GameObject loadingImage; // 로딩 이미지
        public ScrollRect scrollRect; // 스크롤 영역

        public Button RegionCategoryButton; // 지역 선택 버튼
        public Button FavoritesButton; // 인기 급상승 버튼
        public Button EventCategoryButton; // 카테고리 버튼

        private List<GameObject> itemPool; // 아이템 풀링 리스트
        private int itemsPerPage = 10; // 한 페이지당 아이템 수
        private int currentPage = 0; // 현재 페이지
        private List<DateCourse> currentCourses; // 현재 표시할 데이터
        private bool isLoading = false; // 로딩 상태 플래그
        private Action<CategoryType> categoryCallback; // 카테고리 콜백

        private void Awake()
        {
            itemPool = new List<GameObject>();
        }

        private void Start()
        {
            // 스크롤 변경 시 이벤트 연결
            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
            loadingImage.SetActive(false);

            // 버튼 클릭 이벤트 연결
            RegionCategoryButton.onClick.AddListener(() => categoryCallback?.Invoke(CategoryType.Main));
            FavoritesButton.onClick.AddListener(() => categoryCallback?.Invoke(CategoryType.Favorite));
            EventCategoryButton.onClick.AddListener(() => categoryCallback?.Invoke(CategoryType.Event));
        }

        // Controller에서 전달받은 콜백 설정
        public void SetButtonCallback(Action<CategoryType> callback)
        {
            categoryCallback = callback;
        }

        /// <summary>
        /// currentCourses 리스트를 초기화하고, 첫 페이지의 아이템을 로드
        /// </summary>
        /// <param name="courses"></param>
        public void DisplayDateCourses(List<DateCourse> courses)
        {
            currentPage = 0;
            currentCourses = courses;

            // 기존 풀링된 아이템을 초기화
            foreach (var item in itemPool)
            {
                item.SetActive(false);
            }

            LoadMoreItems();
        }

        /// <summary>
        /// 현재 페이지의 아이템을 로드하고, 이후 페이지를 불러올 수 있도록 
        /// </summary>
        private void LoadMoreItems()
        {
            if (isLoading) return;
            isLoading = true;

            // 로딩 이미지를 활성화
            loadingImage.SetActive(true);

            int startIndex = currentPage * itemsPerPage;
            int endIndex = Mathf.Min(startIndex + itemsPerPage, currentCourses.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                GameObject item = GetPooledItem();
                DateCourse course = currentCourses[i];

                var itemComponent = item.GetComponent<DateCourseItem>();
                itemComponent.Title.text = course.Title;
                itemComponent.Description.text = course.Description;
                //itemComponent.LikeButton.Set = course.Description;

                item.SetActive(true);

                Button itemButton = item.GetComponent<DateCourseItem>().DetailButton;
                itemButton.onClick.RemoveAllListeners();
                itemButton.onClick.AddListener(() => OnItemClicked(course));
            }

            currentPage++;
            isLoading = false;
            loadingImage.SetActive(false);

        }

        /// <summary>
        /// 풀에 비활성화된 아이템이 있으면 이를 재사용하고, 부족할 경우 새로 생성하여 풀에 추가
        /// </summary>
        /// <returns></returns>
        private GameObject GetPooledItem()
        {
            // 비활성화된 풀링 아이템을 찾아 재사용
            foreach (var item in itemPool)
            {
                if (!item.activeSelf)
                {
                    return item;
                }
            }

            GameObject newItem = Instantiate(itemPrefab, content);
            itemPool.Add(newItem);
            return newItem;
        }

        private void OnScrollValueChanged(Vector2 scrollPosition)
        {
            // 스크롤이 맨 아래에 도달했는지 체크
            if (!isLoading && scrollPosition.y <= 0.1f)
            {
                LoadMoreItems();
            }
        }

        private void OnItemClicked(DateCourse course)
        {
            // 데이터를 Detail 페이지에 전달하는 방법 (예: static 변수를 사용)
            DetailPageData.SelectedCourse = course;

            // Detail 씬으로 이동
            SceneManager.LoadScene("DetailScene");
        }

        public void CleanupListeners()
        {
            // 기존에 등록된 리스너 제거
            RegionCategoryButton.onClick.RemoveAllListeners();
            FavoritesButton.onClick.RemoveAllListeners();
            EventCategoryButton.onClick.RemoveAllListeners();

            // 풀링된 아이템들의 버튼 리스너 초기화
            foreach (var item in itemPool)
            {
                var itemComponent = item.GetComponent<DateCourseItem>();
                if (itemComponent != null && itemComponent.DetailButton != null)
                {
                    itemComponent.DetailButton.onClick.RemoveAllListeners();
                }
            }
        }

    }

}

