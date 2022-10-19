using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraMove : MonoBehaviour
{

    [SerializeField] GameObject _vCamObj = null;

    CinemachineVirtualCamera _vCam = null;
    Transform _vCamTransform = null;
    // Sequence seq = DOTween.Sequence();

    private void Awake()
    {
        _vCamTransform = _vCamObj.GetComponent<Transform>();
        _vCam = _vCamObj.GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        DoShakeCam();
    }

    void Update()
    {
        
    }

    public void DoShakeCam()
    {
        StopAllCoroutines();
        StartCoroutine(Shake(0.1f, 1f));
        
    }

    // ī�޶� �ſ� ������ ����
    public IEnumerator Shake(float _amount, float _duration)
    {
        Vector3 originPos = _vCamTransform.position;
        float timer = 0;
        while (timer <= _duration)
        {
            _vCamTransform.position = (Vector3)Random.insideUnitCircle * _amount + originPos;

            timer += Time.deltaTime;
            yield return null;
        }
        _vCamTransform.position = originPos;
    }
    // ī�޶� ���� Ƣ���� ����
    // ī�޶� ȸ����Ű�� ������ ����
    // ī�޶� �ٿ��ٰ� �����Ƿ� �ǵ�����
    // ī�޶� �����̸鼭 ũ�� �ٲٱ�
    // ī�޶� ���̵��� ���̵�ƿ�
}
