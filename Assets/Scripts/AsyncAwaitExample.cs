using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncAwaitExample : MonoBehaviour
{
    private CancellationTokenSource _cancellationTokenSource;

    void Start()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        RunAsyncOperations();
    }

    async void RunAsyncOperations()
    {
        Debug.Log("Start Test!!!!");
        await TaskWithCancellation(_cancellationTokenSource.Token);
    }

    async Task TaskWithCancellation(CancellationToken cancellationToken)
    {
        Debug.Log("5초간 작업 수행 중... (취소 가능)");

        try
        {
            for (int i = 0; i < 5; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Debug.LogWarning("작업 취소됨!");
                    return;
                }

                Debug.Log($"진행 중임... {i + 1}초 경과");
                await Task.Delay(1000, cancellationToken);
            }

            Debug.Log("작업 끝!");
        }
        catch (TaskCanceledException)
        {
            Debug.LogWarning("작업이 강제로 취소됨!");
        }
    }

    void OnDestroy()
    {
        // 게임 종료 시 작업 취소
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }
}
