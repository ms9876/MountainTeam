using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CutSceneData : MonoBehaviour
{
    //�ƾ��� ���κ��� ������
    //�ƾ��Ŵ��� > �ƾ� > �ƾ�������
    //�ƾ��Ŵ��� �ȿ� ��� �ƾ��� �����Ѵ�.
    //�ƾ� �ȿ� ��� �ƾ������͸� �����Ѵ�.
    public float Delay = 0;


    public abstract void CutAction(); //�߻�޼���
    
}
