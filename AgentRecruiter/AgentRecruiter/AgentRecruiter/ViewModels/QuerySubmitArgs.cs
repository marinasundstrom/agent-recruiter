namespace AgentRecruiter.ViewModels
{
    public class QuerySubmitArgs
    {
        public QuerySubmitArgs(object chosenSuggestion, string queryText)
        {
            ChosenSuggestion = chosenSuggestion;
            QueryText = queryText;
        }

        public object ChosenSuggestion { get; }
        public string QueryText { get; }
    }
}