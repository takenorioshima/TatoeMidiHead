using UnityEngine;

public class Cam45Script : MonoBehaviour
{  
    public bool isWired = false;
    // Attach this script to a camera, this will make it render in wireframe
    void OnPreRender()
    {   
        if( isWired ) GL.wireframe = true;
    }

    void OnPostRender()
    {
        if( isWired ) GL.wireframe = false;
    }
}
