using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldState
{
    None = 0,
    Tetris = 1,
    AvoidAndShot = 2,
    BoxMoving = 3,
}


public class WorldManager : MonoBehaviour
{
    //World�� �����ϴ� �� ������ ��ũ��Ʈ
    private Rigidbody2D _playerRigidBody; // �÷��̾� ������ٵ�
    [SerializeField] // ������ �ø��� ������ �ʵ�
    private WorldState _worldState; // ���� ����

    private void Awake()
    {
        _playerRigidBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

}
