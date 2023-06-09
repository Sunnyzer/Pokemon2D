using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class EnterScene : MonoBehaviour
{
    [SerializeField] UnityEditor.SceneAsset scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerTrainer _player = collision.gameObject.GetComponent<PlayerTrainer>();
        if (!_player) return;
        Debug.Log("Load " + scene.name);
        SceneManager.LoadScene(scene.name);
    }
}
