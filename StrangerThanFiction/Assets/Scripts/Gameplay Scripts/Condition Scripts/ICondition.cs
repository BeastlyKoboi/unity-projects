public interface ICondition 
{
    void OnAdd();
    void OnTrigger();
    void OnSurplus (ICondition surplus);
    void OnRemove();
}
