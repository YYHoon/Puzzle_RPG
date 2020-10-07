using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    
   public void OnTap ()
    {
        Time.timeScale = 0;
    }
    
    public void CloseTap()
    {
        Time.timeScale = 1;
    }

    public void TimeFast()
    {
        Time.timeScale = 2;
    }
    
}