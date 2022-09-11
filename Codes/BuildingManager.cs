using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    [SerializeField] private Transform pfHouse;
    private static Camera mainCamera;
    private static Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(GetMouseWorldPosition());
            Instantiate(pfHouse, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    

    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            pos =  raycastHit.point;
        }
        //mouseWorl;
        //dPosition.z = 0f;
        return pos;
    }
}
