using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SceneChange : MonoBehaviour
{
  
    public void ChangeToScene (string sceneToChangeto)
    {
        Application.LoadLevel (sceneToChangeto);

    }


}
