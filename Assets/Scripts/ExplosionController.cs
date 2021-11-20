using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float seconds;
    public AudioSource Sound_Death;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        Sound_Death.PlayOneShot(Sound_Death.clip, 0.5f);
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
