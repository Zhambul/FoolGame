namespace FoolGame.Bll.Game
{
    interface IGameCallback
    {
        void OnNotSuitableCard();
        void OnPassButtonVisible();
        void OnGetCardsButtonVisible();
        void OnRoleSwith(bool isAttacking);
        void OnGetCardsButtonHidden();
        void OnTrumpCardChosen();
    }
}
