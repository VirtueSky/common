using DG.Tweening;
using UnityEngine;

namespace Virtuesky.common
{
    public class CurrencyCounter : MonoBehaviour
    {
        // public TextMeshProUGUI CurrencyAmountText;
        // public int StepCount = 10;
        // public float DelayTime = .01f;
        // public CurrencyGenerate CurrencyGenerate;
        //
        // private int currentCoin;
        //
        // private void Start()
        // {
        //     EventManager.SaveCurrencyTotal += SaveCurrency;
        //     EventManager.CurrencyTotalChanged += UpdateCurrencyAmountText;
        //     CurrencyAmountText.text = Data.CurrencyTotal.ToString();
        // }
        //
        // private void SaveCurrency()
        // {
        //     currentCoin = Data.CurrencyTotal;
        // }
        //
        // private void UpdateCurrencyAmountText()
        // {
        //     if (Data.CurrencyTotal > currentCoin)
        //     {
        //         IncreaseCurrency();
        //     }
        //     else
        //     {
        //         DecreaseCurrency();
        //     }
        // }
        //
        // private void IncreaseCurrency()
        // {
        //     bool isPopupUIActive = PopupManager.Instance.Get<PopupUI>().isActiveAndEnabled;
        //     if (!isPopupUIActive) PopupManager.Instance.Show<PopupUI>();
        //     bool isFirstMove = false;
        //     CurrencyGenerate.GenerateCoin(() =>
        //     {
        //         if (!isFirstMove)
        //         {
        //             isFirstMove = true;
        //             int currentCurrencyAmount = int.Parse(CurrencyAmountText.text);
        //             int nextAmount = (Data.CurrencyTotal - currentCurrencyAmount) / StepCount;
        //             int step = StepCount;
        //             CurrencyTextCount(currentCurrencyAmount, nextAmount, step);
        //         }
        //     }, () =>
        //     {
        //         EventManager.CoinMove?.Invoke();
        //         if (!isPopupUIActive) PopupManager.Instance.Hide<PopupUI>();
        //     });
        // }
        //
        // private void DecreaseCurrency()
        // {
        //     int currentCurrencyAmount = int.Parse(CurrencyAmountText.text);
        //     int nextAmount = (Data.CurrencyTotal - currentCurrencyAmount) / StepCount;
        //     int step = StepCount;
        //     CurrencyTextCount(currentCurrencyAmount, nextAmount, step);
        // }
        //
        // private void CurrencyTextCount(int currentCurrencyValue, int nextAmountValue, int stepCount)
        // {
        //     if (stepCount == 0)
        //     {
        //         CurrencyAmountText.text = Data.CurrencyTotal.ToString();
        //         return;
        //     }
        //
        //     int totalValue = (currentCurrencyValue + nextAmountValue);
        //     DOTween.Sequence().AppendInterval(DelayTime).SetUpdate(isIndependentUpdate: true)
        //         .AppendCallback(() => { CurrencyAmountText.text = totalValue.ToString(); }).AppendCallback(() =>
        //         {
        //             CurrencyTextCount(totalValue, nextAmountValue, stepCount - 1);
        //         });
        // }
    }
}