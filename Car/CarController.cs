using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    public Car car;
    public float MoveSpeed = 2;
    public float RotateSpeed = 90;
    private void Start()
    {
        car = GameObject.Find("遥控汽车").GetComponent<Car>();
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * Time.deltaTime * MoveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(Vector3.down * Time.deltaTime * RotateSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.up* Time.deltaTime * RotateSpeed);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            car.UnUse();
        }
    }
}