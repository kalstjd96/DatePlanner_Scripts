using Firebase.Firestore;
using UnityEngine;

namespace Firebase.Database
{
    /// <summary>
    /// Firebase Firestore 데이터베이스 관련 로직
    /// </summary>
    public class FirebaseDatabaseManager
    {
        private FirebaseFirestore _firestore;

        public FirebaseDatabaseManager()
        {
            _firestore = FirebaseFirestore.DefaultInstance;
        }

        public FirebaseFirestore Firestore => _firestore;

        // 데이터베이스 관련 로직 추가 가능
    }
}

