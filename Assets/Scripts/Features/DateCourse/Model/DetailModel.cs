using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FirestoreData]
public class DetailModel
{
    [FirestoreProperty]
    public string CourseDescription { get; set; }

    [FirestoreProperty]
    public List<DetailStep> DetailedSteps { get; set; }
}
