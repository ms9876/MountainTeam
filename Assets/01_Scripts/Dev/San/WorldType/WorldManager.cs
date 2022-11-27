using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldState
{
    None = 0,
    Tetris = 1,
    AvoidAndShot = 2,
    BoxMoving = 3,
    Followbullet1 = 4,
    BigBullet = 5, 
    Followbullet2 = 6
}


public class WorldManager : MonoBehaviour
{

    public static WorldManager instance;
    [SerializeField] // ������ �ø��� ������ �ʵ�
    private WorldState _worldState; // ���� ����
    public WorldState WorldState=> _worldState;

    private WorldMove _currentWorld;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple CamaraManager instance is running");
        }
        instance = this;
    }

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
