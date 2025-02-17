using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FirestoreData]
public class DetailStep
{
    [FirestoreProperty]
    public string StepTitle { get; set; }

    [FirestoreProperty]
    public string Description { get; set; }

    [FirestoreProperty]
    public int EstimatedTime { get; set; }

    [FirestoreProperty]
    public string Image { get; set; }

    public DetailStep() { }

    public DetailStep(string stepTitle, string description, int estimatedTime, string image)
    {
        StepTitle = stepTitle;
        Description = description;
        EstimatedTime = estimatedTime;
        Image = image;
    }
}
