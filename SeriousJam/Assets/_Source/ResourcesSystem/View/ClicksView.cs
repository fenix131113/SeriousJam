using TMPro;
using UnityEngine;
using VContainer;

namespace ResourcesSystem.View
{
    public class ClicksView : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyCountPrefab;
        [SerializeField] private float minVerticalForce = 5f;
        [SerializeField] private float maxVerticalForce = 10f;
        [SerializeField] private float minHorizontalForce = 5f;
        [SerializeField] private float maxHorizontalForce = 10f;

        [Inject] private Clicker _clicker;
        [Inject] private Wallet _wallet;

        private void Start() => Bind();

        private void OnDestroy() => Expose();

        private void OnPassiveClickGained(int amount)
        {
            var created = CreateLabel(
                Camera.main!.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)),
                amount);

            created.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(Random.Range(minHorizontalForce, maxHorizontalForce),
                    Random.Range(minVerticalForce, maxVerticalForce)), ForceMode2D.Impulse);
        }

        private void OnClickGained(int amount)
        {
            var created = CreateLabel(
                Camera.main!.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)),
                amount);
            
            created.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(Random.Range(minHorizontalForce, maxHorizontalForce),
                    Random.Range(minVerticalForce, maxVerticalForce)), ForceMode2D.Impulse);
        }

        private TMP_Text CreateLabel(Vector3 position, int amount)
        {
            var spawned = Instantiate(moneyCountPrefab, position, Quaternion.identity);
            spawned.text = "+" + amount.ToString();
            
            var createdPos = spawned.transform.position;
            createdPos.z = 0;
            spawned.transform.position = createdPos;
            return spawned;
        }

        private void Bind()
        {
            _clicker.OnClickGained += OnClickGained;
            _wallet.OnPassiveMoneyGained += OnPassiveClickGained;
        }

        private void Expose()
        {
            _clicker.OnClickGained -= OnClickGained;
            _wallet.OnPassiveMoneyGained -= OnPassiveClickGained;
        }
    }
}