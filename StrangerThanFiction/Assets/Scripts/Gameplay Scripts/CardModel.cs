using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CardType { Unit, Spell }

public class CardModel : MonoBehaviour
{
    public virtual string Title { get; }
    public virtual string Description { get; }
    public virtual string FlavorText { get; }
    public virtual CardType Type { get; set; }

    public virtual bool IsHidden { get; set; } = false;

    // Add reqs for targeting ally/enemy units, and ally cards.

    public virtual uint BaseCost { get; }
    public virtual uint BasePower { get; }
    public virtual uint BasePlotArmor { get; }

    public virtual string PortraitPath { get; set; } 
    public virtual string CardbackPath { get; } = "Cardback_Placeholder.png";
    public virtual string UnitFramePath { get; } = "UnitCardFrontFrame.png";
    public virtual string SpellFramePath { get; } = "SpellCardFrontFrame.png";

    public virtual uint CurrentCost { get; set; }
    public virtual uint CurrentDepth { get; set; }
    public virtual uint CurrentPlotArmor { get; set; }

    private Dictionary<string, ICondition> conditions = new Dictionary<string, ICondition>();
    public Dictionary<string, int> PlayRequirements { get; set; }

    [SerializeField] private bool _playable = true;
    public bool Playable
    {
        get { return _playable; }
        set
        {
            //if (_playable == value) 
            //    return; 
            _playable = value;

            if (IsHidden & _playable) 
                return;

            cardView.Find("Glow").gameObject.SetActive(value);
            GetComponent<Draggable>().enabled = value;
        }
    }

    public Player Owner { get; set; }
    public BoardManager Board { get; set; }

    public event Action OnPlay;
    public event Action OnSummon;
    public event Action OnDraw;
    public event Action OnDiscard;
    public event Action OnDestroy;

    public Transform cardView;
    public Transform unitView;

    // Unit specific placement info
    public UnitRow SelectedArea { get; set; }

    private void Awake()
    {
        CurrentCost = BaseCost;
        CurrentDepth = BasePower;
        CurrentPlotArmor = BasePlotArmor;

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

    public virtual async Task Play(Player player) 
    {
        Owner.CurrentMana -= CurrentCost;

        if (Type == CardType.Unit)
        {
            cardView.gameObject.SetActive(false);
            unitView.gameObject.SetActive(true);
            Board.SummonUnit(this, SelectedArea);
        }

        OnPlay?.Invoke();
    }

    public virtual async Task Discard(Player player)
    {
        OnDiscard?.Invoke();
    }

    public virtual async Task Destroy(Player player)
    {
        OnDestroy?.Invoke();
    }


    // Method to add a condition
    public void ApplyCondition(string conditionName, ICondition condition)
    {
        if (!conditions.ContainsKey(conditionName))
        {
            conditions.Add(conditionName, condition);
            condition.OnAdd(this);
        }
        else
        {
            conditions[conditionName].OnSurplus(this, condition);
        }
    }

    // Method to remove a condition
    public void RemoveCondition(string conditionName)
    {
        if (conditions.ContainsKey(conditionName))
        {
            conditions[conditionName].OnRemove(this);
            conditions.Remove(conditionName);
        }
    }

    // Method to trigger a condition
    public void TriggerCondition(string conditionName)
    {
        if (conditions.ContainsKey(conditionName))
        {
            conditions[conditionName].OnTrigger(this);
        }
    }

    // Method to find out if this unit has a condition
    public bool HasCondition(string conditionName)
    {
        return conditions.ContainsKey(conditionName);
    }

    private static Sprite LoadSprite(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        if (System.IO.File.Exists(path))
        {
            int sprite_width = 100; 
            int sprite_height = 100;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(sprite_width, sprite_height, TextureFormat.RGB24, false);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        return null;
    }

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

        cardView.Find("Cost").GetComponent<TextMeshProUGUI>().text = CurrentCost.ToString();

        //  
        if (Type == CardType.Spell)
        {
            Sprite spellCardFrame = LoadSprite("Assets/Textures/SpellCardFrame.png");
            Transform background = cardView.Find("Background");
            background.GetComponent<Image>().sprite = spellCardFrame;

            cardView.Find("Power").gameObject.SetActive(false);
            cardView.Find("PlotArmor").gameObject.SetActive(false);

        }
        else
        {
            cardView.Find("Power").GetComponent<TextMeshProUGUI>().text = CurrentDepth.ToString();
            cardView.Find("PlotArmor").GetComponent<TextMeshProUGUI>().text = CurrentPlotArmor.ToString();
        }

        cardView.Find("Name").GetComponent<TextMeshProUGUI>().text = Title;
        cardView.Find("Description").GetComponent<TextMeshProUGUI>().text = Description;

        if (IsHidden)
            cardView.Find("Cardback").gameObject.SetActive(true);
    }

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

        unitView.Find("Cost").GetComponent<TextMeshProUGUI>().text = CurrentCost.ToString();
        unitView.Find("Power").GetComponent<TextMeshProUGUI>().text = CurrentDepth.ToString();
        unitView.Find("PlotArmor").GetComponent<TextMeshProUGUI>().text = CurrentPlotArmor.ToString();
        unitView.Find("Name").GetComponent<TextMeshProUGUI>().text = Title;

        unitView.gameObject.SetActive(false);
    }
}
