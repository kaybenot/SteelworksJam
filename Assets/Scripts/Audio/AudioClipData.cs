using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AudioClipDatabase
{
    public List<AudioClipData> clipDatas;

    public AudioClip GetClip(AudioClipType clipType, int index = -1)
    {
        AudioClipData clipData = null;

        clipData = clipDatas.Where(x => x.clipType == clipType).FirstOrDefault();

        if (clipData == null || clipData.clips == null || clipData.clips.Count == 0)
        {
            Debug.LogError($"No clips found for {clipType}");
            return null;
        }

        if (index == -1)
        {
            return clipData.clips[Random.Range(0, clipData.clips.Count - 1)];
        }
        else
        {
            if (index > 0 & index < clipData.clips.Count)
            {
                return clipData.clips[index];

            }
            else
            {
                Debug.LogWarning("Sound index out of range");
                return null;
            }
        }
    }
}

[System.Serializable]
public class AudioClipData
{
    public AudioClipType clipType;
    public List<AudioClip> clips;
}

public enum AudioClipType
{
    BossMusic,
    AmbientMusic,
    SFX //Divide to movement, hit, interact etc.
}