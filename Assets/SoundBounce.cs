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

    public float maxSpeed = 10f; // 최대 속도
    public float maxVolume = 1f; // 최대 볼륨

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
        // Rigidbody의 속도 크기를 계산합니다
        float speed = rb.velocity.magnitude;

        // 속도에 비례하여 볼륨을 계산합니다
        volume = Mathf.Lerp(0f, maxVolume, speed / maxSpeed);

        // AudioSource의 볼륨을 설정합니다
        bounceAudio.volume = volume;

        bounceAudio.Play();
    }
}
