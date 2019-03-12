using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class CharacterGaze : MonoBehaviour
{
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    DataManager DT;
    public int player = 0;
    bool state = false;
    // Start is called before the first frame update
    void Start()
    {
        DT = DataManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
       
    }
    //Handle the Over event
    private void HandleOver()
    {
        Debug.Log("Show over state");
        state = true;
      
    }


    //Handle the Out event
    private void HandleOut()
    {
        state = false;
        Debug.Log("Show out state");
      
    }
    void FixedUpdate()
    {
        if (state) DT.frameLooking(player);
        else DT.frameNotLooking(player);
    }
}
