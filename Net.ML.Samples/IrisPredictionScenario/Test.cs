using Microsoft.ML;
using IrisPredictionScenario;


namespace Net.ML.Samples.IrisPredictionScenario
{
	internal class Test : iRunnable
	{
		readonly string dataPath = "iris.data";
		readonly string modelPath = "iris_model.zip";


		public void Run()
		{
			var mlContext = new MLContext();

			ITransformer model;
			if (File.Exists(modelPath)) model = mlContext.Model.Load(modelPath, out var schema);
			else model = TrainAndSave(mlContext, dataPath, modelPath);


			var predictionEngine = mlContext.Model.CreatePredictionEngine<IrisData, IrisPrediction>(model);
			print($"CreatePredictionEngine", ConsoleColor.DarkGray);

			var prediction = predictionEngine
			.Predict(new IrisData()
			{
				SepalLength = 3.3f,
				SepalWidth = 1.6f,
				PetalLength = 0.2f,
				PetalWidth = 5.1f,
			})
			.Print(x => $"Predicted flower type is: {x.PredictedLabels}");

		}



		static ITransformer TrainAndSave(MLContext mlContext, string dataPath, string savePath)
		{
			var reader = mlContext.Data.CreateTextLoader<IrisData>(separatorChar: ',', hasHeader: true);
			IDataView trainingDataView = reader.Load(dataPath).Print(x => $"Load IDataView");

			var pipeline =
				mlContext.Transforms.Conversion
				.MapValueToKey("Label")
				.Append(mlContext.Transforms
				.Concatenate("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth"))
				.Append(mlContext.MulticlassClassification.Trainers
				.SdcaNonCalibrated(
					labelColumnName: "Label",
					featureColumnName: "Features"))
				.Append(mlContext.Transforms.Conversion
				.MapKeyToValue("PredictedLabel"))
				;

			var model = pipeline.Fit(trainingDataView).Print("Trained");
			mlContext.Model.Save(model, reader, savePath);
			print($"Saved");

			return model;
		}

	}
}
