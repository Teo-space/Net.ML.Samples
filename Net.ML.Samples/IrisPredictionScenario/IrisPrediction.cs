using Microsoft.ML.Data;
namespace IrisPredictionScenario
{
	// IrisPrediction является результатом, возвращенным из операций прогнозирования
	public class IrisPrediction
	{
		[ColumnName("PredictedLabel")]
		public string PredictedLabels;
	}
}
