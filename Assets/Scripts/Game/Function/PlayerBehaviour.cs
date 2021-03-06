using DG.Tweening;
using Model;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// �����Ϊ��
    /// </summary>
    public class PlayerBehaviour : IPlayerBehaviour
    {
        private readonly Transform _playerTrans;
        private readonly PlayerDataModel _model;
        private Vector3 _faceDirection;
        private bool _isFaceDirectionChange;

        public PlayerBehaviour(Transform player, PlayerDataModel model)
        {
            _playerTrans = player;
            _model = model;
            IsAttack = false;
            _faceDirection = Vector3.zero;
            _isFaceDirectionChange = false;
        }

        public void Idle()
        {
            IsAttack = false;
        }

        public void TurnForward()
        {
            if (IsAttack)
                return;
            SetDirectionData(Vector3.zero);
        }

        public void TurnBack()
        {
            if (IsAttack)
                return;
            SetDirectionData(Vector3.up * 180);
        }

        public void TurnLeft()
        {
            if (IsAttack)
                return;
            SetDirectionData(Vector3.up * -90);
        }

        public void TurnRight()
        {
            if (IsAttack)
                return;
            SetDirectionData(Vector3.up*90);
        }

        private void SetDirectionData(Vector3 direction)
        {
            if (_faceDirection != direction)
            {
                _faceDirection = direction;
                _isFaceDirectionChange = true;
            }
        }

        public void Move()
        {
            if (_isFaceDirectionChange)
            {
                IsCollideWall = false;
            }

            RotateFace();
            
            PlayerMove();
        }

        private void PlayerMove()
        {
            if (IsCollideWall)
                return;
            _playerTrans.Translate(Time.deltaTime * _model.Speed * Vector3.forward, Space.Self);
        }

        private void RotateFace()
        {
            if (_isFaceDirectionChange)
            {
                _isFaceDirectionChange = false;
                PlayerOrientation(_faceDirection);
            }
        }

        public bool IsRun { get; set; }
        public bool IsCollideWall { get; set; }
        public bool IsAttack { get; private set; }

        public void Attack(int skillCode)
        {
            IsAttack = true;
        }

        private void PlayerOrientation(Vector3 direction)
        {
            var value = Mathf.Abs((_playerTrans.eulerAngles - direction).y);
            if (value == 90)
            {
                _playerTrans.DORotate(direction, 0.3f);
            }
            else
            {
                _playerTrans.eulerAngles = direction;
            }
        }
    }
}
