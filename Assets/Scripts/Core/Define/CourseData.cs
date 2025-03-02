using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FirestoreData]
public class CourseData
{
    [FirestoreProperty] public string Title { get; set; }
    [FirestoreProperty] public int LikesCount { get; set; }
    [FirestoreProperty] public string Description { get; set; }
}

