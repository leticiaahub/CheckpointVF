using UnityEngine;
using System.Collections;

public class PoliInteractionScript : MonoBehaviour

{
    [Header("Pontos de Referência")]
    public Transform waterTargetPosition;
    public Transform islandCenter;

    [Header("Configurações")]
    public float jumpDuration = 1.0f;
    public float jumpHeight = 2.0f;
    public float swimRadius = 4.0f;
    public float swimSpeed = 50.0f;

    // 1. PRIMEIRO VOID (vincular no On Target Found)
    public void PularNaAgua()
    {
        StopAllCoroutines();
        StartCoroutine(JumpToWaterRoutine());
    }

    // 2. SEGUNDO VOID (vincular no On Target Lost)
    public void IniciarNadando()
    {
        StopAllCoroutines();
        StartCoroutine(SwimRoutine());
    }

    private IEnumerator JumpToWaterRoutine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = waterTargetPosition.position;
        float elapsed = 0f;

        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;
            float percent = Mathf.Clamp01(elapsed / jumpDuration);

            Vector3 currentPos = Vector3.Lerp(startPos, endPos, percent);
            currentPos.y += Mathf.Sin(percent * Mathf.PI) * jumpHeight;

            transform.position = currentPos;
            yield return null;
        }

        transform.position = endPos;
        transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, 0f);
    }

    private IEnumerator SwimRoutine()
    {
        transform.position = waterTargetPosition.position;
        transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, 0f);

        float currentSwimAngle = 0f;
        Vector3 center = islandCenter != null ? islandCenter.position : Vector3.zero;

        while (true)
        {
            currentSwimAngle += swimSpeed * Time.deltaTime;
            float radians = currentSwimAngle * Mathf.Deg2Rad;

            float x = center.x + Mathf.Cos(radians) * swimRadius;
            float z = center.z + Mathf.Sin(radians) * swimRadius;

            transform.position = new Vector3(x, waterTargetPosition.position.y, z);

            Vector3 direction = new Vector3(-Mathf.Sin(radians), 0, Mathf.Cos(radians));
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation * Quaternion.Euler(90f, 0, 0);
            }

            yield return null;
        }
    }
}
