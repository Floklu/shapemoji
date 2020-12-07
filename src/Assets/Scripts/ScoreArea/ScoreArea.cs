using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScoreArea
{
    public class ScoreArea : CanHoldHookableObject
    {
        private BoxCollider2D _collider;
        private List<Stone> _stones;

        // Start is called before the first frame update
        private void Start()
        {
            _stones = new List<Stone>();
            _collider = gameObject.GetComponent<BoxCollider2D>();
            _player = gameObject.GetComponentInParent<Player>();
            _team = gameObject.GetComponentInParent<Team>();
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
            //TODO: remove
            var scoreCalculation = new ScoreCalculation();
            scoreCalculation.AnalyzeScoreableView(this);
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
    }
}