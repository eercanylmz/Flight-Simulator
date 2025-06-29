using UnityEngine;

public class DirectionnalArrow : MonoBehaviour
{
    public Transform Cargo1;
    public Transform Cargo2;
    public Transform Cargo3;

    public  TagController tagController1;
    public  TagController2 tagController2;
    public  TagController3 tagController3;
     

    void Update()
    {
        transform.LookAt(Cargo1);
        if (tagController1 .active1== true  )
        {
            transform.LookAt(Cargo2);
        }
        if (tagController1.active1 == true && tagController2.active2 == true)
        {
            transform.LookAt(Cargo3);
        }
    }
}