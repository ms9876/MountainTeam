using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidWorldManager : WorldForm
{
    [Header("�÷��̾ ������ ���ε��� ��ġ��")]
    [SerializeField] Vector2[] _pos = null;
    int _posIndex;
    private void Start()
    {
        _posIndex = (int)(_pos.Length / 2);
    }

    public override void Move(Rigidbody2D rb, KeyCode left, KeyCode right, KeyCode up, KeyCode down)
    {
        
        //
        if (Input.GetKeyDown(left))
        {
            if(_posIndex - 1 >= 0)
            {
                rb.position = _pos[--_posIndex];
            }

        }
        if (Input.GetKeyDown(right))
        {
            if(_posIndex + 1 <= _pos.Length - 1)
            {
                rb.position = _pos[++_posIndex];
            }
        }
    }

    public override void Pattern()
    {
        //���� ���� ����
    }
}
