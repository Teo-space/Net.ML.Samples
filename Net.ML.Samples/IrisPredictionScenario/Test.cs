using Microsoft.ML;
using IrisPredictionScenario;


namespace Net.ML.Samples.IrisPredictionScenario
{

	public record Key<TSource, TDest>();


	internal class Test : iRunnable
	{
		readonly string dataPath = "iris.data";
		readonly string modelPath = "iris_model.zip";
		public void Run()
		{
			var predictionEngine = PredictionEngine<IrisData, IrisPrediction>(modelPath, dataPath);

			predictionEngine
			.Predict(new IrisData()
			{
				SepalLength = 3.3f,
				SepalWidth = 1.6f,
				PetalLength = 0.2f,
				PetalWidth = 5.1f,
			}).Print(x => $"Predicted flower type is: {x.PredictedLabels}");
			
			predictionEngine
			.Predict(new IrisData()
			{
				SepalLength = 4f,
				SepalWidth = 1.6f,
				PetalLength = 0.2f,
				PetalWidth = 5.1f,
			}).Print(x => $"Predicted flower type is: {x.PredictedLabels}");
			
			predictionEngine
			.Predict(new IrisData()
			{
				SepalLength = 4f,
				SepalWidth = 3f,
				PetalLength = 1f,
				PetalWidth = 1f,
			}).Print(x => $"Predicted flower type is: {x.PredictedLabels}");

		}

		static PredictionEngine<TSrc, TDst> PredictionEngine<TSrc, TDst>(string modelPath, string dataPath)
			where TSrc : class, new()
			where TDst : class, new()
		{
			var mlContext = new MLContext();

			ITransformer model;
			if (File.Exists(modelPath))
			{
				model = mlContext.Model.Load(modelPath, out var schema);
				print($"Loaded");
			}
			else
			{
				var reader = mlContext.Data.CreateTextLoader<TSrc>(separatorChar: ',', hasHeader: true);
				IDataView trainingDataView = reader.Load(dataPath).Print(x => $"Load IDataView");

				model = mlContext.Transforms.Conversion
					.MapValueToKey("Label")
					.Append(mlContext.Transforms.Concatenate("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth"))
					.Append(mlContext.MulticlassClassification.Trainers.SdcaNonCalibrated(labelColumnName: "Label", featureColumnName: "Features"))
					.Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"))
					.Fit(trainingDataView);
					
				print($"Trained");
				mlContext.Model.Save(model, reader, modelPath);
				print($"Saved");
			}
			
			var engine = mlContext.Model.CreatePredictionEngine<TSrc, TDst>(model);
			print($"Engine Created");
			return engine;
		}
	}
}




