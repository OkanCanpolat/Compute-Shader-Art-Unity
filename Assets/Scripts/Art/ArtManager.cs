using System.Collections;
using UnityEngine;

public class ArtManager : MonoBehaviour
{
    private RenderTexture renderTexture;
    public ComputeShader shader;
    private string time = "Time";
    [Range(0, 1)] public float TimeInterval;
    private WaitForSeconds delay;
    private void Start()
    {
        renderTexture = new RenderTexture(Screen.width, Screen.height, 1);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();
        shader.SetTexture(0, "Result", renderTexture);
        shader.SetFloats("Resolution", renderTexture.width, renderTexture.height);
        delay = new WaitForSeconds(TimeInterval);
        StartCoroutine(Dispatch());
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(renderTexture, destination);
    }
    private IEnumerator Dispatch()
    {
        while (true)
        {
            shader.SetFloat(time, Time.time);
            shader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
            yield return delay;
        }
    }
}
