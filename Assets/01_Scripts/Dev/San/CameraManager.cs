using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    //���� : ��� 2D ���ӿ��� ��밡���� ī�޶� ���� �ڵ�, ��� ��Ʈ���� ���׸����� �ʿ���
    //����2 : ��Ʈ���� ���׸��� ����

    //������Ʈ���� ��� ����
    //ī�޶� ������ � ī�޶� ��������� �����ϴ� �ڵ�
    //ī�޶� �����̴� ���� �޼��带 ���� ��ũ��Ʈ



    [SerializeField]
    private CinemachineVirtualCamera _vCam; // �⺻ ���� ī�޶�
    [SerializeField]
    private CinemachineVirtualCamera _vRigCam; // ���𰡸� ���󰡴� ī�޶�
    [SerializeField]
    private CinemachineVirtualCamera _vCutSceneCam; // �ƾ��� ī�޶�

    //CinemachineBasicMultiChannelPerlin�� ���ؼ� �����ϱ� ShakeCam �ϴ� �� �� ����
    private CinemachineBasicMultiChannelPerlin _activePerlin = null; // ���� ����
    
    private CinemachineVirtualCamera _activeVCam = null; // ���� ������� ī�޶� �־�� ����

    private int backPriority = 10;
    private int frontPriority = 15;

    public void ChangeRigCam()
    {
        ChangeCam(_vRigCam);
    } // currentCam -> _vRigCam
    public void ChangeVCam()
    {
        ChangeCam(_vCam);
    } // currentCam -> _vCam
    public void ChangeCutSceneCam() // currentCam -> _cutSceneCam
    {
        ChangeCam(_vCutSceneCam);
    }
    private void ChangeCam(CinemachineVirtualCamera _cam) // currentCam -> _cam
    {
        _vCam.Priority = backPriority;
        _vRigCam.Priority = backPriority;
        _vCutSceneCam.Priority = backPriority;

        _cam.Priority = frontPriority;
        _activeVCam = _cam;
    }

    // ���� ���
    public void MoveCam(float time, Vector3 moveTransform) //ī�޶� ��ġ �̵�
    {
        Vector3 currentTransform = _activeVCam.transform.position;
        moveTransform.z = currentTransform.z;
        //time ���� _cam�� ��ġ�� moveTransform��ġ�� ����
    }
    public void RotationCam()
    {

    }
    public void ShakeCam()
    {

    }
    public void ZoomCam(float value, float time)
    {
        //value������ �� �ٲٴ� �ڵ�
    }
    public void ZoomAndSwitchingCam(float value, float time, float delay)
    {
        //value�� ���� �ٲ�ٰ� ���� ������ �ٲٴ� �޼���

    }
    public void ChangeRigTarget()
    {
        //RigCam�� Ÿ���� �ٲٴ� �޼���
    }

}
