using Scripts.Game.Buildings;

namespace Scripts.Game
{
    public class IncomeCounter
    {
        public IncomeCounter()
        {
            Office.Finished += OnOfficeFinished;
        }

        ~IncomeCounter()
        {
            Office.Finished -= OnOfficeFinished;
        }

        private void OnOfficeFinished(int income) => PlayerCoins.Instance.Add(income);
    }
}