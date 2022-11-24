using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    //���� : ��� 2D ���ӿ��� ��밡���� ī�޶� ���� �ڵ�, ��� ��Ʈ���� ���׸����� �ʿ���
    //����2 : ��Ʈ���� ���׸��� ����

    //������Ʈ���� ��� ����
    //ī�޶� ������ � ī�޶� ��������� �����ϴ� �ڵ�
    //ī�޶� �����̴� ���� �޼��带 ���� ��ũ��Ʈ

    private CinemachineVirtualCamera _vCam; // �⺻ ���� ī�޶�
    private CinemachineVirtualCamera _vRigCam; // ���𰡸� ���󰡴� ī�޶�
    private CinemachineVirtualCamera _vCutSceneCam; // �ƾ��� ī�޶�

    private CinemachineBasicMultiChannelPerlin _vCamPerlin;
    private CinemachineBasicMultiChannelPerlin _vRigCamPerlin;
    private CinemachineBasicMultiChannelPerlin _vCutSceneCamPerlin;
    //CinemachineBasicMultiChannelPerlin�� ���ؼ� �����ϱ�, ShakeCam �ϴ� �� �� ����

    private CinemachineBasicMultiChannelPerlin _activePerlin = null; // ���� ����
    private CinemachineVirtualCamera _activeVCam = null; // ���� ������� ī�޶� �־�� ����

    private int backPriority = 10;
    private int frontPriority = 15;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple CamaraManager instance is running");
        }
        instance = this;

        Init();
        ChangeVCam();
    }
    public void Init()
    {
        _vRigCam = GameObject.Find("vRigCam").GetComponent<CinemachineVirtualCamera>();
        _vCam = GameObject.Find("vCam").GetComponent<CinemachineVirtualCamera>();
        _vCutSceneCam = GameObject.Find("vCutSceneCam").GetComponent<CinemachineVirtualCamera>();

        if (_vCam != null)
            _vCamPerlin = _vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (_vRigCam != null)
            _vRigCamPerlin = _vRigCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (_vCutSceneCam != null)
            _vCutSceneCamPerlin = _vCutSceneCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    public void ChangeRigCam()
    {
        ChangeCam(_vRigCam);
        if(_vRigCamPerlin != null)
            _activePerlin = _vRigCamPerlin;
    } // currentCam -> _vRigCam
    public void ChangeVCam()
    {
        ChangeCam(_vCam);
        if(_vCamPerlin != null)
            _activePerlin = _vCamPerlin;
    } // currentCam -> _vCam
    public void ChangeCutSceneCam() // currentCam -> _cutSceneCam
    {
        ChangeCam(_vCutSceneCam);
        if(_vCutSceneCamPerlin != null)
            _activePerlin = _vCutSceneCamPerlin;
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
    public void ResetCam(float zoomValue, float rotationValue)
    {
        _activeVCam.transform.position = new Vector3(0, 0, _activeVCam.transform.position.z);
        _activeVCam.m_Lens.OrthographicSize = zoomValue;
        _activeVCam.transform.rotation = Quaternion.Euler(0, 0, rotationValue);
    }

    public void DOMoveCam(float time, Vector3 moveTransform) //ī�޶� ��ġ �̵�
    {
        if (_activeVCam == null) return;
        Vector3 currentTransform = _activeVCam.transform.position;
        moveTransform.z = currentTransform.z;

        _activeVCam.transform.DOMove(moveTransform, time);
        //time ���� _cam�� ��ġ�� moveTransform��ġ�� ����
    }
    public void RotationCam(float time, float rotationValue) // Ư�� ��"��ŭ" ������
    {
        if (_activeVCam == null) return;
        StartCoroutine(RotationCamcoroutine(time, rotationValue));
    }
    IEnumerator RotationCamcoroutine(float time, float rotationValue)
    {
        float currentTime = 0f;
        float originRotate = _activeVCam.transform.eulerAngles.z;
        originRotate = originRotate >= 180 ? originRotate - 360f : originRotate;
        while (currentTime < time)
        {
            yield return new WaitForEndOfFrame();
            _activeVCam.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(originRotate, originRotate + rotationValue, currentTime / time));
            currentTime += Time.deltaTime;
        }
        _activeVCam.transform.rotation = Quaternion.Euler(0, 0, originRotate + rotationValue);
    }

    public void ValueRotationCam(float time, float rotationValue)
    {
        if (_activeVCam == null) return;
        StartCoroutine(ValueRotationCamcoroutine(time, rotationValue));
        //���� -���� +�� ���� �� ���� ȸ���ϴ� ���� �߻�
    } // Ư�� ��"����" ������
    IEnumerator ValueRotationCamcoroutine(float time, float rotationValue) //���߿� ���ư��� ���� ���� �ڵ� �����
    {
        float currentTime = 0f;
        float originRotate = _activeVCam.transform.eulerAngles.z;
        originRotate = originRotate >= 180 ? originRotate - 360f : originRotate;
        while (currentTime < time)
        {
            yield return new WaitForEndOfFrame();
            _activeVCam.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(originRotate, rotationValue, currentTime / time));
            currentTime += Time.deltaTime;
        }
        _activeVCam.transform.rotation = Quaternion.Euler(0, 0, rotationValue);
    }
    public void WaitValueRotationCam(float time, float rotationValue, float delay) // Ư�� ������ ���ȴٰ� ����ġ��
    {
        if (_activeVCam == null) return;
        StartCoroutine(WaitValueRotationCamcoroutine(time, rotationValue, delay));
    }
    IEnumerator WaitValueRotationCamcoroutine(float time, float rotationValue, float delay)
    {
        float halfTime = time / 2;
        float originRotate = _activeVCam.transform.eulerAngles.z;
        ValueRotationCam(halfTime, rotationValue);
        yield return new WaitForSeconds(delay+halfTime);
        ValueRotationCam(halfTime, originRotate);
    }

    public void ShakeCam(float time, float shakeValue)
    {
        //_activePerlin���
        if (_activeVCam == null || _activePerlin == null) return;
        StartCoroutine(ShakeCamcoroutine(time, shakeValue));
    }
    IEnumerator ShakeCamcoroutine(float time, float shakeValue)
    {
        _activePerlin.m_AmplitudeGain = shakeValue;
        float currentTime = 0f;
        while(currentTime < time)
        {
            yield return new WaitForEndOfFrame();
            if (_activePerlin == null) break;
            _activePerlin.m_AmplitudeGain = Mathf.Lerp(shakeValue, 0, currentTime/time);
            currentTime += Time.deltaTime;
        }
        _activePerlin.m_AmplitudeGain = 0;
    }
    public void ZoomCam(float time, float value)
    {
        if (_activeVCam == null) return;
        //value������ �� �ٲٴ� �ڵ�
        StartCoroutine(ZoomCamcoroutine(time, value));
    }
    IEnumerator ZoomCamcoroutine(float time, float value)
    {
        float currentTime = 0f;
        float originOrthographicSize = _activeVCam.m_Lens.OrthographicSize;
        while (currentTime < time)
        {
            yield return new WaitForEndOfFrame();
            _activeVCam.m_Lens.OrthographicSize = Mathf.Lerp(originOrthographicSize, value, currentTime / time);
            currentTime += Time.deltaTime;
        }
        _activeVCam.m_Lens.OrthographicSize = value;
    }
    public void ZoomSwitchingCam(float time, float value, float delay)
    {
        if (_activeVCam == null) return;
        //value�� ���� �ٲ�ٰ� ���� ������ �ٲٴ� �޼���
        StartCoroutine(ZoomSwitchingCamcoroutine(time, value, delay));
    }
    IEnumerator ZoomSwitchingCamcoroutine(float time, float value, float delay)
    {
        float originOrthographicSize = _activeVCam.m_Lens.OrthographicSize;
        float halfTime = time / 2;
        StartCoroutine(ZoomCamcoroutine(halfTime, value));
        yield return new WaitForSeconds(halfTime + delay);
        StartCoroutine(ZoomCamcoroutine(halfTime, originOrthographicSize));
    }
    public void SetConfiner(PolygonCollider2D confiner)
    {
        _vCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = confiner;
        _vRigCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = confiner;
        _vCutSceneCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = confiner;
    }
    //RIg����
    public void ChangeRigTarget(Transform target)
    {
        if (_activeVCam == null) return;
        //RigCam�� Ÿ���� �ٲٴ� �޼���
        _vRigCam.Follow = target;

    }


    

}
