using System.Collections.Generic;

namespace Main.User
{
    public static class UserData
    {
        // 사용자 기본 정보
        public static string Email { get; private set; }
        public static string Name { get; private set; }
        public static string Nickname { get; private set; }

        // 즐겨찾기 항목 (ID나 제목으로 관리 가능)
        private static List<string> favoriteItems = new List<string>();
        public static IReadOnlyList<string> FavoriteItems => favoriteItems;

        // 작성한 글 목록
        private static List<string> writtenPosts = new List<string>();
        public static IReadOnlyList<string> WrittenPosts => writtenPosts;

        // 작성한 댓글 목록
        private static List<string> writtenComments = new List<string>();
        public static IReadOnlyList<string> WrittenComments => writtenComments;

        // 로그인 여부 확인
        public static bool IsLoggedIn => !string.IsNullOrEmpty(Email);

        /// <summary>
        /// 사용자 정보를 설정합니다. 로그인 시 호출됩니다.
        /// </summary>
        public static void SetUserInfo(string email, string name = null, string nickname = null)
        {
            Email = email;
            Name = name;
            Nickname = nickname;
        }

        /// <summary>
        /// 즐겨찾기 항목 추가
        /// </summary>
        public static void AddFavoriteItem(string item)
        {
            if (!favoriteItems.Contains(item))
            {
                favoriteItems.Add(item);
            }
        }

        /// <summary>
        /// 즐겨찾기 항목 제거
        /// </summary>
        public static void RemoveFavoriteItem(string item)
        {
            favoriteItems.Remove(item);
        }

        /// <summary>
        /// 게시물 추가
        /// </summary>
        public static void AddPost(string post)
        {
            writtenPosts.Add(post);
        }

        /// <summary>
        /// 댓글 추가
        /// </summary>
        public static void AddComment(string comment)
        {
            writtenComments.Add(comment);
        }

        /// <summary>
        /// 로그아웃 시 호출하여 사용자 정보를 초기화합니다.
        /// </summary>
        public static void Logout()
        {
            Email = null;
            Name = null;
            Nickname = null;
            favoriteItems.Clear();
            writtenPosts.Clear();
            writtenComments.Clear();
        }
    }

}
