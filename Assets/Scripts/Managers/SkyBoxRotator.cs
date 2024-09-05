using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeed = 0.1f; // Daha yavaş dönüş hızı

    void Update()
    {
        // Dönüş açısını hesapla
        float rotation = Time.deltaTime * rotationSpeed;

        // Skybox materyalini al
        Material skyboxMaterial = RenderSettings.skybox;

        // Skybox'ın döndürme değerini güncelle
        if (skyboxMaterial != null)
        {
            // Eğer Skybox materyalinde _Rotation parametresi yoksa ekleyin
            if (skyboxMaterial.HasProperty("_Rotation"))
            {
                skyboxMaterial.SetFloat("_Rotation", skyboxMaterial.GetFloat("_Rotation") + rotation);
            }
            else
            {
                Debug.LogError("Skybox materyalinde '_Rotation' parametresi bulunamadı.");
            }
        }
        else
        {
            Debug.LogError("Skybox materyali bulunamadı.");
        }
    }
}