using UnityEngine;

public class InputHandler : MonoBehaviour
{

    bool keyBoardInput = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckDownPressed() 
    { 
        if(keyBoardInput == true) 
        { }
        else { if (Input.GetKeyDown("s")) { return true; } }


        return false;
    }

    public bool CheckRightPressed()
    {
        if (keyBoardInput == true)
        { }
        else { if (Input.GetKeyDown("d")) { return true; } }


        return false;
    }

    public bool CheckLeftPressed()
    {
        if (keyBoardInput == true)
        { }
        else { if (Input.GetKeyDown("a")) { return true; } }


        return false;
    }

    public bool CheckUpPressed() 
    {
        if (keyBoardInput == true)
        { }
        else { if (Input.GetKeyDown("w")) { return true; } }

        return false;
    }

    public bool CheckAPressed() 
    {
        if (keyBoardInput == true)
        { }
        else { if (Input.GetKeyDown(KeyCode.RightArrow)) { return true; } }

        return false;
    }

}
