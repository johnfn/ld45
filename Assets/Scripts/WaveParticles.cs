using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveParticles : MonoBehaviour
{

  public GameObject player;

  private ParticleSystem.ShapeModule sm;

    // Start is called before the first frame update
    void Start()
    {
    ParticleSystem ps = GetComponent<ParticleSystem>();
    sm = ps.shape;
    Vector3 pos = sm.position;
    sm.position = new Vector3(player.transform.position.x, pos.y, pos.z);
        
    }

    // Update is called once per frame
    void Update()
    {
    Vector3 pos = sm.position;
    sm.position = new Vector3(player.transform.position.x, pos.y, pos.z);
  }
}
