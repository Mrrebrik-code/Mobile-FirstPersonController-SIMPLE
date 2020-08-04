using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCastHit : MonoBehaviour
{
    public Camera player;
    private RaycastHit hit;
    public Text t;
    public Animator anim;
    bool opened = false;
    [HideInInspector]
    public float distanceHit;
    public GameObject flashlight;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(player.transform.position, transform.forward, out hit, 4f))
            t.text = "Предмет: " + hit.collider.gameObject.name.ToUpper();
        if(Input.GetKey(KeyCode.F) && Physics.Raycast(player.transform.position, transform.forward, out hit, 3f))
        {
            if(hit.transform.tag == "Door")
            {
                anim = hit.transform.GetComponentInParent<Animator>();
                opened = !opened;
                anim.SetBool("isOpen", opened);
            }
            if (hit.transform.tag == "item")
            {


                Destroy(hit.collider.gameObject);
            }
        }
    }

    public void Take()
    {
        if (Physics.Raycast(player.transform.position, transform.forward, out hit, 3f))
        {
            if(hit.transform.tag == "Door")
            {
                anim = hit.transform.GetComponentInParent<Animator>();
                opened = !opened;
                anim.SetBool("isOpen", opened);
            }
            if (hit.transform.tag == "item")
            {
                if(hit.collider.gameObject.name == "flashlight")
                    flashlight.SetActive(true);
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
