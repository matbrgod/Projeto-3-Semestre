using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public GameObject spawnpoint;
    PlayerInteract interact;
    PlayerRespawn respawn;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        interact = FindFirstObjectByType<PlayerInteract>();
        respawn = FindFirstObjectByType<PlayerRespawn>();
    }

    public void GameSave()
    {

    }

    public void GetSavedData()
    {

    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
