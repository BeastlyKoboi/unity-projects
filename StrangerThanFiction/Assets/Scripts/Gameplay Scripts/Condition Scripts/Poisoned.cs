public class Poisoned : ICondition
{
    private readonly CardModel card;
    public int amount;

    public Poisoned(CardModel card, int amount)
    {
        this.card = card;
        this.amount = amount;
    }

    public static string GetName()
    {
        return "Poisoned";
    }

    public void OnAdd()
    {
        card.OnRoundEnd += OnTrigger;
    }
    public void OnTrigger()
    {
        card.TakeDamage(amount, true);
        amount -= 1;
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
