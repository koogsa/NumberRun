using UnityEngine;
using System;

public class GoblinController : MonoBehaviour
{
    public Action OnGoblinDestroyed;  // 고블린이 파괴되었을 때 호출되는 이벤트

    // 고블린이 파괴될 때 호출
    void OnDestroy()
    {
        if (OnGoblinDestroyed != null)
        {
            OnGoblinDestroyed.Invoke();  // 파괴 이벤트 호출
        }
    }
}
