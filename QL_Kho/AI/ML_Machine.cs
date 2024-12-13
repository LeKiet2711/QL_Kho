using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace QL_Kho.AI
{
    public class ML_Machine
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public ML_Machine()
        {
            _mlContext = new MLContext();
        }

        public void TrainModel()
        {
            string filePath = "wwwroot/CSVData/TrainingData.csv";
            var trainingData = LoadTrainingData(filePath);
            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            Console.WriteLine("Starting training...");

            var pipeline = _mlContext.Transforms.Concatenate("Features", nameof(XuatKhoData.SlXuat), nameof(XuatKhoData.DonGiaXuat), nameof(XuatKhoData.NgayXuatKho))
                .Append(_mlContext.BinaryClassification.Trainers.FastTree());

            var stopwatch = Stopwatch.StartNew();
            _model = pipeline.Fit(dataView);
            stopwatch.Stop();

            Console.WriteLine($"Training completed in {stopwatch.ElapsedMilliseconds} ms");
        }


        public bool Predict(XuatKhoData newData)
        {
            if (_model == null)
                throw new InvalidOperationException("Model has not been trained.");

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<XuatKhoData, XuatKhoPrediction>(_model);
            var prediction = predictionEngine.Predict(newData);
            Console.WriteLine($"Prediction for SlXuat: {newData.SlXuat}, DonGiaXuat: {newData.DonGiaXuat}, NgayXuatKho: {newData.NgayXuatKho} is {prediction.IsFraud}");

            return prediction.IsFraud;
        }

        private IEnumerable<XuatKhoData> LoadTrainingData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                return csv.GetRecords<XuatKhoData>().ToList();
            }
        }

        public void PrintTrainingData()
        {
            string filePath = "wwwroot/CSVData/TrainingData.csv";
            var trainingData = LoadTrainingData(filePath);

            foreach (var record in trainingData)
            {
                Console.WriteLine($"SlXuat: {record.SlXuat}, DonGiaXuat: {record.DonGiaXuat}, NgayXuatKho: {record.NgayXuatKho}, Label: {record.Label}");
            }
        }

    }
    public class XuatKhoData
    {
        public float SlXuat { get; set; }
        public float DonGiaXuat { get; set; }
        public float NgayXuatKho { get; set; }
        public bool Label { get; set; } // True nếu là gian lận, False nếu không
    }

    public class XuatKhoPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool IsFraud { get; set; }
    }
}
