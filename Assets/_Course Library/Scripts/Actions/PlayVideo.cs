using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// Play a single video or play from a list of videos 
/// </summary>
[RequireComponent(typeof(VideoPlayer))]
public class PlayVideo : MonoBehaviour
{
    [Tooltip("Whether video should play on load")]
    public bool playAtStart = false;

    [Tooltip("Material used for playing the video (Uses URP/Unlit by default)")]
    public Material videoMaterial = null;

    [Tooltip("List of video clips to pull from")]
    public List<VideoClip> videoClips = new List<VideoClip>();

    private VideoPlayer videoPlayer = null;
    private MeshRenderer meshRenderer = null;

    private int index = 0;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoClips.Count > 0)
            videoPlayer.clip = videoClips[0];
    }

    private void OnEnable()
    {
        videoPlayer.prepareCompleted += ApplyVideoMaterial;
    }

    private void OnDisable()
    {
        videoPlayer.prepareCompleted -= ApplyVideoMaterial;
    }

    private void Start()
    {
        if (playAtStart)
        {
            Play();
        }
        else
        {
            Stop();
        }
    }

    public void NextClip()
    {
        //index = ++index % videoClips.Count;
        Play();
        index += 1;
    }

    public void NextClipAndStop() //가장 마지막 영상에서 트리거 버튼 누를 시 꺼지는 기능 추가
    {
            if (!(index == videoClips.Count)) // 가장 마지막 영상이 아닐 시
            {
            Debug.Log((index + 1) + "번째 동영상 재생");
            NextClip();
            }
            else // 가장 마지막 영상일 시
            {
            Debug.Log("마지막 동영상 재생");
            Stop();
                index = 0;
            Debug.Log($"인덱스 {index}으로 초기화");
            }
    }

    public void PreviousClip()
    {
        index = --index % videoClips.Count;
        Play();
    }

    public void RandomClip()
    {
        if (videoClips.Count > 0)
        {
            index = Random.Range(0, videoClips.Count);
            Play();
        }
    }

    public void PlayAtIndex(int value)
    {
        if (videoClips.Count > 0)
        {
            index = Mathf.Clamp(value, 0, videoClips.Count);
            Play();
        }
    }

    public void Play()
    {
        videoPlayer.clip = videoClips[index];
        videoMaterial.color = Color.white;
        videoPlayer.Play();
    }

    public void Stop()
    {
        videoMaterial.color = Color.black;
        videoPlayer.Stop();
    }

    public void TogglePlayStop()
    {
        bool isPlaying = !videoPlayer.isPlaying;
        SetPlay(isPlaying);
    }

    public void TogglePlayPause()
    {
        if (videoPlayer.isPlaying)
            videoPlayer.Pause();
        else
            Play();
    }

    public void SetPlay(bool value)
    {
        if (value)
        {
            Play();
        }
        else
        {
            Stop();
        }
    }

    private void ApplyVideoMaterial(VideoPlayer source)
    {
        meshRenderer.material = videoMaterial;
    }

    private void OnValidate()
    {
        var mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        videoMaterial = mat;
    }
}