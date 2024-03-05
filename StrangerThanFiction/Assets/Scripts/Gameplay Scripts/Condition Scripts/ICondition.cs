public interface ICondition 
{
    void OnAdd(CardModel card);
    void OnRemove(CardModel card);
    void OnTrigger(CardModel card);
    void OnSurplus (CardModel card, ICondition surplus);
}
