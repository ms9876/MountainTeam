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
    [SerializeField] // ������ �ø��� ������ �ʵ�
    private WorldState _worldState; // ���� ����

    private WorldMove _currentWorld;

    private void Update()
    {
        if(_currentWorld != null)
        {
            _currentWorld.WorldPlay();
            _currentWorld.DamageCheck();

        }
    }

    public void ChangeWorld(WorldMove game, WorldState state)
    {
        _currentWorld = game;
        _worldState = state;
    }

}
