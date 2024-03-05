public class Poisoned : ICondition
{
    private readonly CardModel card;
    public uint amount;

    public Poisoned(CardModel card, uint amount)
    {
        this.card = card;
        this.amount = amount;
    }

    public void OnAdd()
    {
        card.OnRoundEnd += OnTrigger;
    }
    public void OnTrigger()
    {
        card.TakeDamage(amount, true);
    }
    public void OnSurplus(ICondition surplus)
    {
        if (surplus is Poisoned poisonSurplus)
        {
            amount += poisonSurplus.amount;
        }
    }
    public void OnRemove()
    {
        card.OnRoundEnd -= OnTrigger;
    }
}
