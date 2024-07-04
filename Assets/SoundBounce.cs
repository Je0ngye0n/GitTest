using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SoundBounce : MonoBehaviour
{
    [SerializeField]
    public AudioSource bounceAudio;
    public Rigidbody rb;

    public float maxSpeed = 10f; // �ִ� �ӵ�
    public float maxVolume = 1f; // �ִ� ����

    float volume;

    void Start()
    {
        bounceAudio.AddComponent<AudioSource>();
        rb.AddComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        // Rigidbody�� �ӵ� ũ�⸦ ����մϴ�
        float speed = rb.velocity.magnitude;

        // �ӵ��� ����Ͽ� ������ ����մϴ�
        volume = Mathf.Lerp(0f, maxVolume, speed / maxSpeed);

        // AudioSource�� ������ �����մϴ�
        bounceAudio.volume = volume;

        bounceAudio.Play();
    }
}
