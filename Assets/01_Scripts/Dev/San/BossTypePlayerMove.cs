using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    None = 0,
    Avoid = 1,
    Shooting = 2,
    Tetris = 3,
    PacMan = 4,

}

public class BossTypePlayerMove : MonoBehaviour
{
    // �÷��̾� ������ٵ�2D
    Rigidbody2D _playerRb = null;

    [Header("�÷��̾��� ���� ����")]
    [SerializeField]
    State _playerState = State.None;

    [Header("�÷��̾� �̵�Ű")]
    [SerializeField] KeyCode _leftKey = KeyCode.None;
    [SerializeField] KeyCode _rightKey = KeyCode.None;
    [SerializeField] KeyCode _upKey = KeyCode.None;
    [SerializeField] KeyCode _downKey = KeyCode.None;

    // ���庰 �̵����, ���ϵ��� �����ص� ��ũ��Ʈ��
    AvoidWorldManager _avoidWorld = null;
    TetrisWorldManager _tetrisWorld = null;
    ShootingWorldManager _platformerWorld = null;

    private void Awake()
    {
        _playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();

        _avoidWorld = GetComponentInChildren<AvoidWorldManager>();
        _tetrisWorld = GetComponentInChildren<TetrisWorldManager>();
        _platformerWorld = GetComponentInChildren<ShootingWorldManager>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        _playerRb.velocity = Vector2.zero;
        if(_playerState == State.None)
        {
            
        }
        else if(_playerState == State.Avoid)
        {
            _avoidWorld.Move(_playerRb, _leftKey, _rightKey, _upKey, _downKey);
        }
        else if(_playerState == State.Shooting)
        {
            _platformerWorld.Move(_playerRb, _leftKey, _rightKey, _upKey, _downKey);
        }
        else if(_playerState == State.Tetris)
        {
            _tetrisWorld.Move(_playerRb, _leftKey, _rightKey, _upKey, _downKey);
        }

        
    }
}
