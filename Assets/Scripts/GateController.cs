using UnityEngine;

public class GateController : MonoBehaviour
{
    private bool opened = false;

    public void OpenGate()
    {
        if (opened) return;

        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            90f,
            transform.eulerAngles.z
        );

        opened = true;
    }
}