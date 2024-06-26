using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to define whether a card is a unit or a spell, 
/// especially when dealing with different behavior. 
/// </summary>
public enum CardType { Unit, Spell }

/// <summary>
/// Defines the basic members and behaviors for all cards. Meant to be 
/// extended into card-specific scripts rather than used as is. 
/// </summary>
public abstract class CardModel : MonoBehaviour
{
    // ----------------------------------------------------------------------------
    // Physical Descriptors of the card, that will effect how it is viewed.
    // ----------------------------------------------------------------------------
    public virtual string Title { get; }
    public virtual string Description { get; }
    public virtual string FlavorText { get; }
    public virtual CardType Type { get; set; }

    // These 3 paths are unused now, but intended for the ability to have card skins.
    public virtual string CardbackPath { get; } = "Cardback_Placeholder.png";
    public virtual string UnitFramePath { get; } = "UnitCardFrontFrame.png"; //
    public virtual string SpellFramePath { get; } = "SpellCardFrontFrame.png"; //
    public virtual string PortraitPath { get; set; }
    public virtual bool IsHidden { get; set; } = false;

    // ----------------------------------------------------------------------------
    // Future place to add reqs for targeting ally/enemy units, and ally cards.
    // ----------------------------------------------------------------------------


    // ----------------------------------------------------------------------------
    // Stats that will not be changed - Consider making children implement this as static somehow?
    // ----------------------------------------------------------------------------
    public virtual int BaseCost { get; }
    public virtual int BasePower { get; } = 0;
    public virtual int BasePlotArmor { get; } = 0;

    // ----------------------------------------------------------------------------
    // Stats that reflect gameplay and can be changed.
    //  - Max properties will automatically increase when current goes above it.
    // ----------------------------------------------------------------------------
    public virtual int MaxCost { get; set; }
    public virtual int MaxPower { get; set; }
    public virtual int MaxPlotArmor { get; set; }


    private int _currentCost;
    public virtual int CurrentCost
    {
        get { return _currentCost; }
        set
        {
            _currentCost = value;
            UpdateCardStatText();

            if (unitView)
                UpdateUnitStatText();
        }
    }
    private int _currentPower;
    public virtual int CurrentPower
    {
        get { return _currentPower; }
        set
        {
            _currentPower = value;
            UpdateCardStatText();

            if (Type == CardType.Unit)
                UpdateUnitStatText();
        }
    }
    private int _currentPlotArmor;
    public virtual int CurrentPlotArmor
    {
        get { return _currentPlotArmor; }
        set
        {
            _currentPlotArmor = value;
            UpdateCardStatText();

            if (Type == CardType.Unit)
                UpdateUnitStatText();
        }
    }

    // Used in conditions like Resilient
    public virtual int DamageResistence { get; set; } = 0;

    /// <summary>
    /// Holds labeled objects for the conditions applied to a card: Resilient, Poisoned, etc.
    /// </summary>
    private Dictionary<string, ICondition> conditions = new Dictionary<string, ICondition>();

    /// <summary>
    /// Holds play requirements, if any: Target 1, Ally 1, etc. 
    /// </summary>
    public Dictionary<string, int> PlayRequirements { get; set; }

    // Used to set whether the card is playable and if it should indicate as such.
    [SerializeField] private bool _playable = true;
    public bool Playable
    {
        get { return _playable; }
        set
        {
            //if (_playable == value) 
            //    return; 
            _playable = value;

            if (IsHidden && _playable)
                return;

            cardView.Find("Glow").gameObject.SetActive(value);
            GetComponent<Draggable>().enabled = value;
        }
    }

    // Quick Ref to use in stuff like conditions and internal behavior
    public Player Owner { get; set; }
    public BoardManager Board { get; set; }

    // Unit specific placement info
    public UnitRow SelectedArea { get; set; }

    // Ref to update view.
    public Transform cardView;
    private TextMeshProUGUI cardTextCost;
    private TextMeshProUGUI cardTextPower;
    private TextMeshProUGUI cardTextPlotArmor;

    public Transform unitView;
    private TextMeshProUGUI unitTextPower;
    private TextMeshProUGUI unitTextPlotArmor;


    // ----------------------------------------------------------------------------
    // All Card Events 
    // ----------------------------------------------------------------------------

    // Card Events - common to both units and spells.
    public event Action OnPlay;
    public event Action OnDraw;
    public event Action OnDiscard;
    public event Action OnDestroy;

    // Unit Events - only called when in play, otherwise never.
    public event Action OnSummon;
    public event Action OnRoundStart;
    public event Action OnRoundEnd;
    public event Action OnTakeDamage;
    public event Action OnGrantPower;
    public event Action OnGrantPlotArmor;
    public event Action OnHeal;


    private void Awake()
    {
        CurrentCost = BaseCost;
        CurrentPower = BasePower;
        CurrentPlotArmor = BasePlotArmor;

        MaxCost = CurrentCost;
        MaxPower = CurrentPower;
        MaxPlotArmor = CurrentPlotArmor;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        //OverwriteCardPrefab();

        //if (Type == CardType.Unit)
        //{
        //    OverwriteUnitPrefab();
        //}
    }

    // ----------------------------------------------------------------------------
    // Card Behaviors
    // ----------------------------------------------------------------------------

    /// <summary>
    /// Method to play this card.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public virtual async Task Play(Player player)
    {
        Owner.CurrentMana -= CurrentCost;

        OnPlay?.Invoke();

        if (Type == CardType.Unit)
        {
            await Summon();
        }

    }

    public virtual async Task Summon()
    {
        cardView.gameObject.SetActive(false);
        unitView.gameObject.SetActive(true);
        Board.SummonUnit(this, SelectedArea);
        OnSummon?.Invoke();
    }

    /// <summary>
    /// Method to discard this card.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public virtual async Task Discard(Player player)
    {
        OnDiscard?.Invoke();
    }

    /// <summary>
    /// Method to destroy this card. 
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public virtual async Task Destroy()
    {
        OnDestroy?.Invoke();
        Destroy(cardView.gameObject);
    }

    public void RoundStart()
    {
        OnRoundStart?.Invoke();
    }

    /// <summary>
    /// Method to damage the unit and return the actual amount of damage given. 
    ///  - This excludes overkill damage
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="ignorePlotArmor">Whether the damage is affected by plot armor.</param>
    /// <returns></returns>
    public int TakeDamage(int damage, bool ignorePlotArmor = false)
    {
        // Should NOT be called if in card form.
        if (Type != CardType.Unit) return 0;

        // Applies damage mitigation effects, and separate conditions.
        damage -= DamageResistence;

        // TODO: Make ifs for helpless or invincible

        // Now that final damage is calculated, 
        // Plot armor and then power are affected in that order
        if (!ignorePlotArmor)
        {
            if (CurrentPlotArmor <= damage)
            {
                damage -= CurrentPlotArmor;
                CurrentPlotArmor = 0;
            }
            else
            {
                CurrentPlotArmor -= damage;
                damage = 0;
            }
        }

        // Damage does its worst, the OnTakeDamage event triggers,
        // and the amount is finally returned
        if (CurrentPower > damage)
            CurrentPower -= damage;
        else
            CurrentPower = 0;

        OnTakeDamage?.Invoke();

        return damage;
    }

    /// <summary>
    /// Method to grant Power to this card and return the amount granted. 
    /// </summary>
    /// <param name="powerAmount"></param>
    /// <returns></returns>
    public int GrantPower(int powerAmount)
    {
        MaxPower += powerAmount;
        CurrentPower += powerAmount;

        OnGrantPower?.Invoke();

        return powerAmount;
    }

    /// <summary>
    /// Method to grant Plot Armor to this card and return the amount granted.
    /// </summary>
    /// <param name="armorAmount"></param>
    /// <returns></returns>
    public int GrantPlotArmor(int armorAmount)
    {
        CurrentPlotArmor += armorAmount;

        OnGrantPlotArmor?.Invoke();

        return armorAmount;
    }

    /// <summary>
    /// Method to heal this unit by aan amount up to the max power.
    /// </summary>
    /// <param name="healAmount"></param>
    /// <returns></returns>
    public int Heal(int healAmount)
    {
        // Should NOT be called if in card form.
        if (Type != CardType.Unit) return 0;

        // should never be negative
        int totalHealPossible = MaxPower - CurrentPower;

        if (totalHealPossible == 0) return 0;

        healAmount = Math.Min(totalHealPossible, healAmount);

        CurrentPower += healAmount;

        OnHeal?.Invoke();

        return healAmount;
    }

    // ----------------------------------------------------------------------------
    // Unit and Card Conditions
    // ----------------------------------------------------------------------------

    /// <summary>
    /// Method to add a condition. If the condition is already added, 
    /// calls surplus implementation instead. 
    /// </summary>
    /// <param name="conditionName"></param>
    /// <param name="condition"></param>
    public void ApplyCondition(string conditionName, ICondition condition)
    {
        if (!conditions.ContainsKey(conditionName))
        {
            conditions.Add(conditionName, condition);
            condition.OnAdd();
        }
        else
        {
            conditions[conditionName].OnSurplus(condition);
        }
    }

    /// <summary>
    /// Method to remove a condition if possible
    /// </summary>
    /// <param name="conditionName"></param>
    public void RemoveCondition(string conditionName)
    {
        if (conditions.ContainsKey(conditionName))
        {
            conditions[conditionName].OnRemove();
            conditions.Remove(conditionName);
        }
    }

    /// <summary>
    /// Method to trigger a condition, if possible
    /// </summary>
    /// <param name="conditionName"></param>
    public void TriggerCondition(string conditionName)
    {
        if (conditions.ContainsKey(conditionName))
        {
            conditions[conditionName].OnTrigger();
        }
    }

    /// <summary>
    /// Method to find out if this unit has a condition
    /// </summary>
    /// <param name="conditionName"></param>
    /// <returns></returns>
    public bool HasCondition(string conditionName)
    {
        return conditions.ContainsKey(conditionName);
    }

    // ----------------------------------------------------------------------------
    // Loading Assets & Overwriting Card Prefabs
    // ----------------------------------------------------------------------------

    /// <summary>
    /// Loads a sprite and returns it from a path string. 
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    private static Sprite LoadSprite(string filename)
    {
        if (string.IsNullOrEmpty(filename)) return null;

        string path = Path.Combine(Application.streamingAssetsPath, filename);

        if (System.IO.File.Exists(path))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(100, 100, TextureFormat.RGB24, false);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }

        return null;
    }

    /// <summary>
    /// At instantiation will be used to overwrite placeholder card gameobject
    /// with correct sprites and initial values.
    /// </summary>
    public void OverwriteCardPrefab()
    {
        cardView = transform.Find("CardPrefab(Clone)");
        if (cardView == null)
            return;

        // Load portrait picture
        Sprite sprite = LoadSprite(PortraitPath);
        Transform portrait = cardView.Find("Portrait");

        if (sprite != null)
            portrait.GetComponent<Image>().sprite = sprite;

        cardTextCost = cardView.Find("Cost").GetComponent<TextMeshProUGUI>();
        cardTextCost.text = CurrentCost.ToString();

        // Placeholder has Unit Card frame automatically, so replace it if needed
        if (Type == CardType.Spell)
        {
            Sprite spellCardFrame = LoadSprite("SpellCardFrontFrame.png");
            Transform background = cardView.Find("Background");
            background.GetComponent<Image>().sprite = spellCardFrame;

            cardView.Find("Power").gameObject.SetActive(false);
            cardView.Find("PlotArmor").gameObject.SetActive(false);

        }
        else
        {
            cardTextPower = cardView.Find("Power").GetComponent<TextMeshProUGUI>();
            cardTextPower.text = CurrentPower.ToString();
            cardTextPlotArmor = cardView.Find("PlotArmor").GetComponent<TextMeshProUGUI>();
            cardTextPlotArmor.text = CurrentPlotArmor.ToString();

        }

        cardView.Find("Name").GetComponent<TextMeshProUGUI>().text = Title;
        cardView.Find("Description").GetComponent<TextMeshProUGUI>().text = Description;

        if (IsHidden)
            cardView.Find("Cardback").gameObject.SetActive(true);
    }

    /// <summary>
    /// At instantiation will be used to overwrite placeholder unit gameobject
    /// with correct sprites and initial values. 
    /// </summary>
    public void OverwriteUnitPrefab()
    {
        unitView = transform.Find("UnitPrefab(Clone)");

        if (unitView == null)
            return;

        // Load portrait picture
        Sprite sprite = LoadSprite(PortraitPath);
        Transform portrait = unitView.Find("Portrait");

        if (sprite != null)
            portrait.GetComponent<Image>().sprite = sprite;

        unitTextPower = unitView.Find("Power").GetComponent<TextMeshProUGUI>();
        unitTextPower.text = CurrentPower.ToString();

        unitTextPlotArmor = unitView.Find("PlotArmor").GetComponent<TextMeshProUGUI>();
        unitTextPlotArmor.text = CurrentPlotArmor.ToString();


        unitView.Find("Name").GetComponent<TextMeshProUGUI>().text = Title;

        unitView.gameObject.SetActive(false);
    }

    private void UpdateCardStatText()
    {
        if (!cardView) return;
        cardTextCost.text = CurrentCost.ToString();

        // Spells need to be updated, but will not have these saved. 
        if (cardTextPower)
            cardTextPower.text = CurrentPower.ToString();
        if (cardTextPlotArmor)
            cardTextPlotArmor.text = CurrentPlotArmor.ToString();
    }

    private void UpdateUnitStatText()
    {
        if (!unitView) return;
        unitTextPower.text = CurrentPower.ToString();
        unitTextPlotArmor.text = CurrentPlotArmor.ToString();
    }
}
