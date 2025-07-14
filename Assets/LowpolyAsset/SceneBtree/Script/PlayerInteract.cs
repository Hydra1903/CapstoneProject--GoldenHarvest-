using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 3f;
    public LayerMask npcLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactRange, npcLayer))
            {
                var npc = hit.collider.GetComponent<NPCAI>();
                if (npc != null)
                    npc.Interact();
            }
        }
    }
}
