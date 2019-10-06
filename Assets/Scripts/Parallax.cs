using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
  public GameObject player;
  public static int numLayers = 2;
  public GameObject[] layers = new GameObject[numLayers];
  private Renderer[] layerRenderers = new Renderer[numLayers];

  // Start is called before the first frame update
  void Start() {
    for (int i = 0; i < numLayers; i++) {
      layerRenderers[i] = layers[i].GetComponent<Renderer>();
      layerRenderers[i].material.mainTextureOffset = new Vector2(Random.Range(0, layerRenderers[i].material.mainTexture.width), 0);
    }
  }

  // Update is called once per frame
  void Update() {
    for (int i = 0; i < numLayers; i++) {
      Vector2 textureOffset = new Vector2(player.transform.position.x / ((i + 1) * 100), 0);
      layerRenderers[i].material.mainTextureOffset = textureOffset;
      Debug.Log(layerRenderers[i].material);

      
      Color c = layerRenderers[i].material.color;
      layerRenderers[i].material.color = new Color(c.r, c.b, c.g, c.a * Mathf.Max(0.1f, (numLayers - i) / numLayers));
      
      
    }
  }
}
