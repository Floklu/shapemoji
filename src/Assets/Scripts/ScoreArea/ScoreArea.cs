using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ScoreArea
{
    public class ScoreArea : CanHoldHookableObject
    {
        private List<Stone> _stones;
        private BoxCollider2D _collider;
        private int _teamScore;
        private EmojiSpriteManager _emojiSpriteManager;

        // UI
        private Text teamScoreText;
        private Text emojiScoreText;
        private Toggle _button1;
        private Toggle _button2;
        [SerializeField] private GameObject emojiScoreUI;
        [SerializeField] private GameObject teamScoreUI;
        [SerializeField] private GameObject button1;
        [SerializeField] private GameObject button2;


        // Start is called before the first frame update
        private void Start()
        {
            _stones = new List<Stone>();
            _collider = gameObject.GetComponent<BoxCollider2D>();
            _player = gameObject.GetComponentInParent<Player>();
            _team = gameObject.GetComponentInParent<Team>();
            _emojiSpriteManager = GetComponent<EmojiSpriteManager>();

            // UI
            teamScoreText = teamScoreUI.GetComponent<Text>();
            emojiScoreText = emojiScoreUI.GetComponent<Text>();
            emojiScoreUI.SetActive(false);

            _button1 = button1.GetComponent<Toggle>();
            _button1.onValueChanged.AddListener(delegate { TurnInEmoji(); });
            _button1.onValueChanged.AddListener(delegate { ChangeColorOfButton(_button1); });
            _button1.isOn = false;

            _button2 = button2.GetComponent<Toggle>();
            _button2.onValueChanged.AddListener(delegate { TurnInEmoji(); });
            _button2.onValueChanged.AddListener(delegate { ChangeColorOfButton(_button2); });
            _button2.isOn = false;
        }

        /**
         * implementation of method required by parent class, calls StoneToScoreArea at HookableObjectController
         * 
         * @param stone Stone to add
         * 
         * TODO: do not call Method from HookableGameObject, this has historic reasons and is not necessary with refactored code structure
         */
        public override bool StoneToCanHoldHookableObject(Stone stone)
        {
            HookableObjectController.StoneToScoreArea(stone, this);

            return true;
        }

        /**
     * true if stone is part of _stones
     *
     * @param stone Stone to check
     */
        public override bool IsStoneInCanHoldHookableObject(Stone stone)
        {
            return _stones.Contains(stone);
        }

        /**
     * true if stone is part of _stones
     *
     * @param stone Stone to check
     *
     * TODO: IsStoneInCanHoldHookableObject should be used instead
     */
        public bool ContainsStone(Stone stone)
        {
            return _stones.Contains(stone);
        }

        /**
     * adds stone to _stones and disables draggable on old _stones
     *
     * @param stone Stone to add
     *
     * TODO: should not be needed but part of StoneToCanHoldHookableObject
     */
        public void AddStone(Stone stone)
        {
            _stones.Add(stone);
            //lock n-1th stone and deactivate collider
            if (_stones.Count > 1)
            {
                var oldStone = _stones[_stones.Count - 2];
                HookableObjectController.DisableStoneDraggable(oldStone);
                HookableObjectController.SetHookableObjectColliderState(oldStone, false);
            }
        }

        /**
     * get snapbackposition of stone
     *
     * @param stone Stone to add
     *
     * TODO: maybe try to store information of where stone has been placed before on ScoreArea and return this for snapback when outside ScoreArea but ScoreArea is still parent
     */
        public override Vector3 GetPositionOfStoneChild(Stone stone)
        {
            var currentPosition = HookableObjectController.GetPositionOfHookableObject(stone);
            if (_stones.Contains(stone) && !_collider.bounds.Contains(currentPosition)
            ) // bound only works with rectengular colliders 
                return gameObject.transform.position;
            return currentPosition;
        }

        /**
     * remove stone from ScoreArea, make last stone in list _stones draggable again
     *
     * @param stone Stone to remove
     */
        public override void RemoveStone(Stone stone)
        {
            _stones.Remove(stone);
            // reenable draggable and collider on last stone
            var stoneCount = _stones.Count;
            if (stoneCount > 0)
            {
                var newLastStone = _stones.Last();
                HookableObjectController.ReEnableStoneDraggable(newLastStone);
                HookableObjectController.SetHookableObjectColliderState(stone, true);
            }
        }

        /**
        * AddScore adds the calculated score to the team score
        *
        * @param score calculated Score
        */
        private void AddScore(int score)
        {
            _teamScore += score;
        }

        /**
         * ChangeEmoji calls the EmojiSpriteManager to change the emoji
         */
        private void ChangeEmoji()
        {
            _emojiSpriteManager.ChangeEmoji();
        }

        /**
         * DisplayScore updates the team score and displays the emoji score for one second
         *
         * @param score Score to be displayed
         */
        IEnumerator DisplayScore(int score)
        {
            teamScoreText.text = _teamScore + " P";

            emojiScoreUI.SetActive(true);
            if (score >= 0)
            {
                emojiScoreText.text = "+" + score;
            }
            else
            {
                emojiScoreText.text = "-" + score;
            }


            yield return new WaitForSeconds(3);

            emojiScoreUI.SetActive(false);
        }

        /**
         * TurnInEmoji calls the calculation in the ScoreCalculation script
         */
        private void TurnInEmoji()
        {
            if (_button1.isOn && _button2.isOn)
            {
                HandleScore(_stones.Count);
            }
        }

        /**
         * ChangeColorOfButton changes the color of the button on every click
         *
         * @param button Button to be changed
         */
        private void ChangeColorOfButton(Toggle button)
        {
            var buttonColors = button.colors;
            if (button.isOn)
            {
                buttonColors.normalColor = Color.green;
                buttonColors.selectedColor = Color.green;
                buttonColors.highlightedColor = Color.green;
            }
            else
            {
                buttonColors.normalColor = Color.red;
                buttonColors.selectedColor = Color.red;
                buttonColors.highlightedColor = Color.red;
            }

            button.colors = buttonColors;
        }

        private void ResetScoreArea()
        {
            _button1.isOn = false;
            _button2.isOn = false;

            foreach (var stone in _stones)
            {
                stone.DestroyHookableObject();
            }

            _stones = new List<Stone>();
        }

        /**
         * HandleScore handles the calculated score from the ScoreCalculation script
         *
         * @param score The calculated score
         */
        public void HandleScore(int score)
        {
            AddScore(score);
            StartCoroutine(DisplayScore(score));
            ResetScoreArea();
            ChangeEmoji();
        }
    }
}