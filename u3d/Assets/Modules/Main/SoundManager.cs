using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 声音管理器
/// </summary>
public class SoundManager
{
    private Canvas cvs;
    /// <summary>
    /// 最小音符
    /// </summary>
    private int PIANO_MIN = 1;
    /// <summary>
    /// 最大音符
    /// </summary>
    private int PIANO_MAX = 88;
    /// <summary>
    /// 允许连续播放钢琴音符的时间间隔
    /// 单位：毫秒
    /// </summary>
    private int PIANO_GAP_MS = 0;
    /// <summary>
    /// 通道字典
    /// </summary>
    private Dictionary<string, int> pianoTrackDict = new Dictionary<string, int>();
    /// <summary>
    /// 上次播放的时间戳
    /// </summary>
    private long lastPlayPianoMS = TimeUtil.nowMS();

    public SoundManager(Canvas cvs)
    {
        this.cvs = cvs;
    }

    /// <summary>
    /// 播放音符
    /// </summary>
    /// <param name="num"></param>
    /// <returns>是否成功播放</returns>
    public bool PlayPiano(int num)
    {
        long now = TimeUtil.nowMS();
        if (now < this.lastPlayPianoMS + this.PIANO_GAP_MS)
        {
            return false;
        }

        GameObject go = new GameObject();
        go.transform.name = "PlayPiano_" + num;
        go.transform.parent = this.cvs.transform;

        string zeroChar = num < 10 ? "0" : "";
        AudioClip clip = (AudioClip)Resources.Load("Sounds/Piano/" + zeroChar + num, typeof(AudioClip));
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        this.lastPlayPianoMS = now;

        this.cvs.StartCoroutine(this.delayDestoryGameObject(go, 5f));
        return true;
    }

    /// <summary>
    /// 按顺序播放声音
    /// </summary>
    /// <param name="track">通道。每个通道独立按顺序播放</param>
    public void PlayPianoNext(string track)
    {
        int num = this.pianoTrackDict.ContainsKey(track) ? this.pianoTrackDict[track] : this.PIANO_MIN;
        bool isPlay = this.PlayPiano(num);

        if (isPlay)
        {
            // 更新通道值
            num++;
            if (num > this.PIANO_MAX)
            {
                num = this.PIANO_MIN;
            }
            this.pianoTrackDict[track] = num;
        }
    }

    /// <summary>
    /// 延迟删除GameObject
    /// </summary>
    /// <param name="go">需要删除的对象</param>
    /// <param name="sec">延迟秒数</param>
    /// <returns></returns>
    private IEnumerator delayDestoryGameObject(GameObject go, float sec)
    {
        yield return new WaitForSeconds(sec);
        this.cvs.DestroyGO(go);
    }
}
