using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GravityPivot plane1;
    public GravityPivot plane2;
    public int kills = 0;
    public int killTarget = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (plane1.enabled)
            plane2.enabled = false;
        else if (plane2.enabled)
            plane1.enabled = false;

        if (kills >= killTarget)
            winGame();
    }

    void winGame()
    {

    }
}
