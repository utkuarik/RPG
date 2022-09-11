using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choose : MonoBehaviour
{
    public GameObject[] characters;
    private int p = 0;
    public Text playerName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Accept()
    {
    
        SaveScript.pchar = p;
        SaveScript.pname = playerName.text;
    }

}
