using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PanelOpener : MonoBehaviour

{
   public GameObject Panel;
   public GameObject PanelX;
    public bool PanelOpen;
   
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Panel.SetActive(false);
        }
    }
    public void OpenPanel()
    {

        if (PanelOpen == false)
        {
            PanelX.SetActive(true);
            Panel.SetActive(true);
            PanelOpen = true;
        }
       
    }
    public void ClosePanel()
    {

        if (PanelOpen == true)
        {
            PanelX.SetActive(false);
            Panel.SetActive(false);
            PanelOpen = false;
        }

    }

}
