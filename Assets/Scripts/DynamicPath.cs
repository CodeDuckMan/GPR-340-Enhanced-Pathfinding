using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class DynamicPath : MonoBehaviour
{
    List<Point2D> path = new List<Point2D>();
    Point2D start;
    Point2D end;
    public World world; // get this somehow
    Path pathf;

    public Button pathingButton;
    public TMP_InputField inputField;



    // Start is called before the first frame update
    void Start()
    {
        pathingButton.onClick.AddListener(getNewPath);   
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null) 
        { 
            getNewPath();
        }
    }

    void getNewPath() 
    {
        if (start == null || end == null) 
        {
            return;
        }
        path = pathf.FindPath(start, end, world);
    }
}
