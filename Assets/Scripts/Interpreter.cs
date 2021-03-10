using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BASIC;

public class Interpreter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        Parser parser = new Parser();
        parser.ParseLine("PRINT 155");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
