using UnityEngine; 

public class FPSLimiter : MonoBehaviour 

{ 

    [SerializeField] 

    int target = 60; 

 

    Void Awake() 

    { 

        QualitySettings.vSyncCount = 0; //remove the V-sync 

        Application.targetFrameRate = target; 

    } 

} 