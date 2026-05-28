namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    [System.Serializable]
    public class ParallaxLayer
    {
        [SerializeField] private Transform background;
        [SerializeField] private float parallaxMultiplier;
        [SerializeField] private float imageWidthOffset = 10f;

        private float imageFullWidth;
        private float ImageHalfWidth;

        public void CalculateImageWidth()
        {
            imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
            ImageHalfWidth = imageFullWidth / 2f;
        }

        public void Move(float distanceToMove)
        {
            background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
        }

        public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)
        {
            float imageRightEdge = (background.position.x + ImageHalfWidth) - 10f;
            float imageLeftEdge = (background.position.x - ImageHalfWidth) + 10f;

            if (imageRightEdge < cameraLeftEdge)
                background.position += Vector3.right * imageFullWidth;
            else if (imageLeftEdge > cameraRightEdge)
                background.position += Vector3.right * -imageFullWidth;
        }
    }
}