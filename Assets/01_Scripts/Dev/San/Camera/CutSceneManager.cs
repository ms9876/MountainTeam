using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    //�ƾ��� �����Ѵ�

    CameraManager _vCamManager = null; // ī�޶� �����
    List<CutSceneData> _cutSceneList = new List<CutSceneData>(); // �ƾ��� ������


    //�������
    // 1. �ؽ�Ʈ ��� ���
    //�ƺ��� ä���� Ư�� ��ġ�� �������� �Ѵ�. / �� �����Ե� ����
    // 2. �ƺ� ȭ�� ȿ��
    //�ƺ��� ī�޶� ������ �������� �Ѵ�. / ���̵� ����
    // 3. �ƺ� �׼�
    //�� : �ִϸ��̼� ���, �Ǵ� ĳ���͸� �����δٵ簡

    // Start is called before the first frame update
    void Awake()
    {
        _vCamManager = FindObjectOfType<CameraManager>();
    }

    
}
