using System.Collections;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitAndPrint());
    }

    IEnumerator WaitAndPrint()
    {
        Debug.Log("시작!");
        yield return new WaitForSeconds(2f); // 2초 대기
        Debug.Log("2초 후 실행됨!");
    }
}
