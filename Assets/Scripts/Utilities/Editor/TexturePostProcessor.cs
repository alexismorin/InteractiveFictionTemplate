using UnityEditor;

class TextureProcessor : AssetPostprocessor
{
    void OnPreprocessTexture()
    {

        TextureImporter textureImporter = (TextureImporter)assetImporter;

        if (assetPath.Contains("UI_"))
        {
            textureImporter.textureType = TextureImporterType.Sprite;
        }
        else
        {

            if (assetPath.Contains("M_") || assetPath.Contains("H_") || assetPath.Contains("FL_") || assetPath.Contains("Mask_"))
            {
                textureImporter.sRGBTexture = false;
            }

            if (assetPath.Contains("N_"))
            {
                textureImporter.textureType = TextureImporterType.NormalMap;
            }

            if (assetPath.Contains("HDR_"))
            {
                textureImporter.textureShape = TextureImporterShape.TextureCube;
                textureImporter.mipmapEnabled = false;
            }
            else
            {

                if (assetPath.Contains("_NoStream") == false)
                {
                    textureImporter.streamingMipmaps = true;
                }

            }


        }


    }
}