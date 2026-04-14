using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public int checkpointNumber;
    public bool isFinishZone = false;
    public bool isWinZone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (GameManager.instance == null) return;

        if (isFinishZone)
        {
            GameManager.instance.ReachFinishZone();
        }
        else if (isWinZone)
        {
            GameManager.instance.ReachWinZone();
        }
        else
        {
            GameManager.instance.PassCheckpoint(checkpointNumber);
        }
    }
}