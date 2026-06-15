using UnityEngine;

public class CutsceneFinal : MonoBehaviour
{
    public GameObject cameraCutscene;
    public GameObject cameraGameplay;
    public GameObject player;
    public Transform targetPoint;
    public bool cutsceneActive = false;
    CameraCutscene cameraCutsceneScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       cameraCutsceneScript = cameraCutscene.GetComponent<CameraCutscene>();
    }

    void Update()
    {
        if(cutsceneActive)
        {
            cameraCutscene.SetActive(true);
            cameraGameplay.SetActive(false); 
            cameraCutsceneScript.ChangeTarget(targetPoint);
        }
          
    }
}
