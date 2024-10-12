using UnityEngine;
using System;

public class GoblinController : MonoBehaviour
{
    public Action OnGoblinDestroyed;  // ����� �ı��Ǿ��� �� ȣ��Ǵ� �̺�Ʈ

    // ����� �ı��� �� ȣ��
    void OnDestroy()
    {
        if (OnGoblinDestroyed != null)
        {
            OnGoblinDestroyed.Invoke();  // �ı� �̺�Ʈ ȣ��
        }
    }
}
