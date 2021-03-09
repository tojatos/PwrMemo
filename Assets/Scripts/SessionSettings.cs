using UnityEngine;
public class SessionSettings : CreatableSingletonMonoBehaviour<SessionSettings>
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void DDOL() => DontDestroyOnLoad(Instance);

    public int tilesWidth;
    public int tilesHeight;
}