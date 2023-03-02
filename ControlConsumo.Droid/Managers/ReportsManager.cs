using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using ControlConsumo.Shared.Models.R;
using System.Threading.Tasks;
using OxyPlot.Annotations;
using ControlConsumo.Shared.Repositories;
using ControlConsumo.Droid.Activities.Widgets;

namespace ControlConsumo.Droid.Managers
{
    class ReportsManager
    {
        private readonly Context context;
        private readonly String Equipo;
        private readonly String Material;
        private readonly Byte TurnID;
        private OxyColor Yellow = OxyColor.FromRgb(255, 244, 212);
        private OxyColor Green = OxyColor.FromRgb(215, 232, 204);
        private OxyColor Pink = OxyColor.FromRgb(255, 184, 184);
        private OxyColor DarkGreen = OxyColor.FromRgb(0, 176, 80);

        private enum Zones
        {
            AP,
            AN,
            BP,
            BN,
            CP,
            CN,
            OUTP,
            OUTN,
            LE
        }

        public ReportsManager(Context context, String Equipo, String Material, Byte TurnID)
        {
            this.context = context;
            this.Equipo = Equipo;
            this.Material = Material;
            this.TurnID = TurnID;
        }

        public async Task<PlotModels> GetPesoReport()
        {
            var plotModel1 = new PlotModel();
            var plotModel2 = new PlotModel();

            var r = new RepositoryR(Util.GetConnection());
            var PesoDetails = await r.GetPesoReport(Equipo, Material, TurnID);

            if (PesoDetails == null)
            {
                return null;
            }

            var PesoDetailsResult = PesoDetails.Transactions;

            if (!PesoDetails.Transactions.Any())
            {
                return null;
            }

            plotModel1.LegendTitle = context.GetString(Resource.String.ReportPesoPromedio);
            plotModel1.LegendTitleColor = OxyColors.Blue;
            plotModel1.LegendTitleFontSize = 16;
            plotModel2.LegendTitle = context.GetString(Resource.String.ReportPesoRango);
            plotModel2.LegendTitleColor = plotModel1.LegendTitleColor;
            plotModel2.LegendTitleFontSize = plotModel1.LegendTitleFontSize;

            var linearAxisXPromedio = new DateTimeAxis();
            linearAxisXPromedio.Position = AxisPosition.Bottom;
            linearAxisXPromedio.IntervalType = DateTimeIntervalType.Auto;
            linearAxisXPromedio.Minimum = DateTimeAxis.ToDouble(PesoDetails.Transactions.Min(p => p.Fecha.AddMinutes(-5)));
            linearAxisXPromedio.StringFormat = "hh:mm";

            var linearAxisYPromedio = new LinearAxis();
            linearAxisYPromedio.Maximum = PesoDetailsResult.Max(p => p.Value + p.Value * 0.025);
            linearAxisYPromedio.Minimum = PesoDetailsResult.Min(p => p.Value - p.Value * 0.025);

            plotModel1.Axes.Add(linearAxisXPromedio);
            plotModel1.Axes.Add(linearAxisYPromedio);

            var linearAxisXRango = new DateTimeAxis();
            linearAxisXRango.Position = AxisPosition.Bottom;
            linearAxisXRango.IntervalType = DateTimeIntervalType.Auto;
            linearAxisXRango.Minimum = linearAxisXPromedio.Minimum;
            linearAxisXPromedio.StringFormat = "hh:mm";

            var linearAxisYRango = new LinearAxis();
            linearAxisYRango.Maximum = PesoDetailsResult.Max(p => p.ValueRange + p.ValueRange * 0.05);
            linearAxisYRango.Minimum = PesoDetailsResult.Min(p => p.ValueRange - p.ValueRange * 0.05);

            plotModel2.Axes.Add(linearAxisXRango);
            plotModel2.Axes.Add(linearAxisYRango);

            var lineSeries1 = new LineSeries();
            lineSeries1.Color = OxyColors.Black;
            lineSeries1.MarkerFill = OxyColors.Transparent;
            lineSeries1.DataFieldX = "Time";
            lineSeries1.DataFieldY = "Value";
            lineSeries1.MarkerFill = OxyColors.Black;
            lineSeries1.MarkerSize = 5;

            lineSeries1.MarkerStroke = OxyColors.White;
            lineSeries1.MarkerType = MarkerType.Circle;
            lineSeries1.MarkerStrokeThickness = 1.5;

            var lineSeries2 = new LineSeries();
            lineSeries2.Color = OxyColors.Black;
            lineSeries2.MarkerFill = OxyColors.Transparent;
            lineSeries2.DataFieldX = "Time";
            lineSeries2.DataFieldY = "Value";
            lineSeries2.MarkerFill = OxyColors.Black;
            lineSeries2.MarkerSize = 5;

            lineSeries2.MarkerStroke = OxyColors.White;
            lineSeries2.MarkerType = MarkerType.Circle;
            lineSeries2.MarkerStrokeThickness = 1.5;

            plotModel1.Series.Add(lineSeries1);
            plotModel2.Series.Add(lineSeries2);

            var parametrosPromedioPeso = GetParametrosPeso(PesoDetails.Details.Where(p => p.SubTypeID == "P").ToList(), true);
            var parametrosRangoPeso = GetParametrosPeso(PesoDetails.Details.Where(p => p.SubTypeID == "R").ToList(), false);

            var ParametrosFuera = PesoDetailsResult.OrderByDescending(p => p.Fecha).First();

            if (ParametrosFuera.Values.Any(p => p < parametrosPromedioPeso.LimiteLEI || p > parametrosPromedioPeso.LimiteLES))
            {
                new CustomDialog(context, CustomDialog.Status.Warning, context.GetString(Resource.String.ReportMessageOut));
            }

            var listaPromedio = new ListaZonas();
            var listaRango = new ListaZonas();

            foreach (var item in PesoDetails.Transactions)
            {
                var zonePromedio = GetZonaPeso(parametrosPromedioPeso, item.Value);
                lineSeries1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.Fecha), item.Value));
                lineSeries1.Selectable = false;

                AumentaZona(zonePromedio, ref listaPromedio);

                if (listaPromedio.ListaZonasOUTP > 0 || listaPromedio.ListaZonasAP > 1 || listaPromedio.ListaZonasBP > 2 || listaPromedio.ListaZonasCP > 6)
                {
                    var pointAnnotation1 = new PointAnnotation();
                    pointAnnotation1.X = DateTimeAxis.ToDouble(item.Fecha);
                    pointAnnotation1.Y = item.Value;
                    pointAnnotation1.Shape = MarkerType.Circle;
                    pointAnnotation1.Fill = OxyColors.Red;
                    pointAnnotation1.Stroke = OxyColors.Red;
                    pointAnnotation1.StrokeThickness = 1.5;
                    pointAnnotation1.TextVerticalAlignment = VerticalAlignment.Bottom;
                    pointAnnotation1.Text = listaPromedio.ListaZonasOUTP > 0 ? context.GetString(Resource.String.ReportMessagePesoUpOut) : context.GetString(Resource.String.ReportMessagePesoUp);
                    plotModel1.Annotations.Add(pointAnnotation1);
                }

                if (listaPromedio.ListaZonasOUTN > 0 || listaPromedio.ListaZonasAN > 1 || listaPromedio.ListaZonasBN > 2 || listaPromedio.ListaZonasCN > 6)
                {
                    var pointAnnotation1 = new PointAnnotation();
                    pointAnnotation1.X = DateTimeAxis.ToDouble(item.Fecha);
                    pointAnnotation1.Y = item.Value;
                    pointAnnotation1.Shape = MarkerType.Circle;
                    pointAnnotation1.Fill = OxyColors.Red;
                    pointAnnotation1.Stroke = OxyColors.Red;
                    pointAnnotation1.StrokeThickness = 1.5;
                    pointAnnotation1.Text = listaPromedio.ListaZonasOUTN > 0 ? context.GetString(Resource.String.ReportMessagePesoDownOut) : context.GetString(Resource.String.ReportMessagePesoDown);
                    pointAnnotation1.TextVerticalAlignment = VerticalAlignment.Top;
                    plotModel1.Annotations.Add(pointAnnotation1);
                }

                var ZonaRango = GetZonaPeso(parametrosRangoPeso, item.ValueRange);
                lineSeries2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.Fecha), item.ValueRange));
                lineSeries2.Selectable = false;

                AumentaZona(ZonaRango, ref listaRango);

                if (listaRango.ListaZonasOUTP > 0 || listaRango.ListaZonasAP > 1 || listaRango.ListaZonasBP > 2 || listaRango.ListaZonasCP > 6)
                {
                    var pointAnnotation1 = new PointAnnotation();
                    pointAnnotation1.X = DateTimeAxis.ToDouble(item.Fecha);
                    pointAnnotation1.Y = item.ValueRange;
                    pointAnnotation1.Shape = MarkerType.Circle;
                    pointAnnotation1.Fill = OxyColors.Red;
                    pointAnnotation1.Stroke = OxyColors.Red;
                    pointAnnotation1.StrokeThickness = 1.5;
                    pointAnnotation1.TextVerticalAlignment = VerticalAlignment.Bottom;
                    pointAnnotation1.Text = context.GetString(Resource.String.ReportMessageRangoUp);
                    plotModel2.Annotations.Add(pointAnnotation1);
                }

                if (listaRango.ListaZonasOUTN > 0 || listaRango.ListaZonasAN > 1 || listaRango.ListaZonasBN > 2 || listaRango.ListaZonasCN > 6)
                {
                    var pointAnnotation1 = new PointAnnotation();
                    pointAnnotation1.X = DateTimeAxis.ToDouble(item.Fecha);
                    pointAnnotation1.Y = item.ValueRange;
                    pointAnnotation1.Shape = MarkerType.Circle;
                    pointAnnotation1.Fill = OxyColors.Red;
                    pointAnnotation1.Stroke = OxyColors.Red;
                    pointAnnotation1.StrokeThickness = 1.5;
                    pointAnnotation1.Text = context.GetString(Resource.String.ReportMessageRangoDown);
                    pointAnnotation1.TextVerticalAlignment = VerticalAlignment.Top;
                    plotModel2.Annotations.Add(pointAnnotation1);
                }
            }

            foreach (var item in PesoDetails.Details.Where(p => p.SubTypeID == "P" && p.IsVisible))
            {
                var lineAnnotation1 = new LineAnnotation();
                lineAnnotation1.Type = LineAnnotationType.Horizontal;
                lineAnnotation1.ClipByXAxis = false;
                lineAnnotation1.Y = (double)item.Value;                

                switch (item.ParametroID)
                {
                    case "LCS":
                    case "LCI":
                        lineAnnotation1.Color = OxyColors.Red;
                        lineAnnotation1.StrokeThickness = 3;
                        lineAnnotation1.LineStyle = LineStyle.Solid;

                        break;

                    case "LE":
                        lineAnnotation1.Color = DarkGreen;
                        lineAnnotation1.StrokeThickness = 3;
                        lineAnnotation1.LineStyle = LineStyle.Solid;

                        break;

                    case "+1SP":
                    case "-1SP":
                        lineAnnotation1.Color = OxyColors.Green;
                        lineAnnotation1.LineStyle = LineStyle.DashDot;

                        break;
                    case "+2SP":
                    case "-2SP":
                        lineAnnotation1.Color = OxyColors.Yellow;
                        lineAnnotation1.LineStyle = LineStyle.DashDot;

                        break;
                }

                plotModel1.Annotations.Add(lineAnnotation1);
            }

            #region RectangleAnnotation for grafica promedio
            var rectangleAnnotation1 = new RectangleAnnotation();
            rectangleAnnotation1.MinimumY = parametrosPromedioPeso.LimiteSPP2;
            rectangleAnnotation1.MaximumY = parametrosPromedioPeso.LimiteLCS;
            rectangleAnnotation1.Fill = Pink;
            rectangleAnnotation1.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation2 = new RectangleAnnotation();
            rectangleAnnotation2.MinimumY = parametrosPromedioPeso.LimiteSPP1;
            rectangleAnnotation2.MaximumY = parametrosPromedioPeso.LimiteSPP2;
            rectangleAnnotation2.Fill = Yellow;
            rectangleAnnotation2.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation3 = new RectangleAnnotation();
            rectangleAnnotation3.MinimumY = parametrosPromedioPeso.LimiteLE;
            rectangleAnnotation3.MaximumY = parametrosPromedioPeso.LimiteSPP1;
            rectangleAnnotation3.Fill = Green;
            rectangleAnnotation3.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation4 = new RectangleAnnotation();
            rectangleAnnotation4.MinimumY = parametrosPromedioPeso.LimiteSPN1;
            rectangleAnnotation4.MaximumY = parametrosPromedioPeso.LimiteLE;
            rectangleAnnotation4.Fill = Green;
            rectangleAnnotation4.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation5 = new RectangleAnnotation();
            rectangleAnnotation5.MinimumY = parametrosPromedioPeso.LimiteSPN2;
            rectangleAnnotation5.MaximumY = parametrosPromedioPeso.LimiteSPN1;
            rectangleAnnotation5.Fill = Yellow;
            rectangleAnnotation5.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation6 = new RectangleAnnotation();
            rectangleAnnotation6.MinimumY = parametrosPromedioPeso.LimiteLCI;
            rectangleAnnotation6.MaximumY = parametrosPromedioPeso.LimiteSPN2;
            rectangleAnnotation6.Fill = Pink;
            rectangleAnnotation6.Layer = AnnotationLayer.BelowSeries;

            plotModel1.Annotations.Add(rectangleAnnotation1);
            plotModel1.Annotations.Add(rectangleAnnotation2);
            plotModel1.Annotations.Add(rectangleAnnotation3);
            plotModel1.Annotations.Add(rectangleAnnotation4);
            plotModel1.Annotations.Add(rectangleAnnotation5);
            plotModel1.Annotations.Add(rectangleAnnotation6);
            #endregion

            #region RectangleAnnotation for grafica rango
            var rectangleAnnotation1R = new RectangleAnnotation();
            rectangleAnnotation1R.MinimumY = parametrosRangoPeso.LimiteSPP2;
            rectangleAnnotation1R.MaximumY = parametrosRangoPeso.LimiteLCS;
            rectangleAnnotation1R.Fill = Pink;
            rectangleAnnotation1R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation2R = new RectangleAnnotation();
            rectangleAnnotation2R.MinimumY = parametrosRangoPeso.LimiteSPP1;
            rectangleAnnotation2R.MaximumY = parametrosRangoPeso.LimiteSPP2;
            rectangleAnnotation2R.Fill = Yellow;
            rectangleAnnotation2R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation3R = new RectangleAnnotation();
            rectangleAnnotation3R.MinimumY = parametrosRangoPeso.LimiteLE;
            rectangleAnnotation3R.MaximumY = parametrosRangoPeso.LimiteSPP1;
            rectangleAnnotation3R.Fill = Green;
            rectangleAnnotation3R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation4R = new RectangleAnnotation();
            rectangleAnnotation4R.MinimumY = parametrosRangoPeso.LimiteSPN1;
            rectangleAnnotation4R.MaximumY = parametrosRangoPeso.LimiteLE;
            rectangleAnnotation4R.Fill = Green;
            rectangleAnnotation4R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation5R = new RectangleAnnotation();
            rectangleAnnotation5R.MinimumY = parametrosRangoPeso.LimiteSPN2;
            rectangleAnnotation5R.MaximumY = parametrosRangoPeso.LimiteSPN1;
            rectangleAnnotation5R.Fill = Yellow;
            rectangleAnnotation5R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation6R = new RectangleAnnotation();
            rectangleAnnotation6R.MinimumY = parametrosRangoPeso.LimiteLCI;
            rectangleAnnotation6R.MaximumY = parametrosRangoPeso.LimiteSPN2;
            rectangleAnnotation6R.Fill = Pink;
            rectangleAnnotation6R.Layer = AnnotationLayer.BelowSeries;

            plotModel2.Annotations.Add(rectangleAnnotation1R);
            plotModel2.Annotations.Add(rectangleAnnotation2R);
            plotModel2.Annotations.Add(rectangleAnnotation3R);
            plotModel2.Annotations.Add(rectangleAnnotation4R);
            plotModel2.Annotations.Add(rectangleAnnotation5R);
            plotModel2.Annotations.Add(rectangleAnnotation6R);
            #endregion

            foreach (var item in PesoDetails.Details.Where(p => p.SubTypeID == "R" && p.IsVisible))
            {
                var lineAnnotation1 = new LineAnnotation();
                lineAnnotation1.Type = LineAnnotationType.Horizontal;
                lineAnnotation1.ClipByXAxis = false;
                lineAnnotation1.Y = (double)item.Value;

                switch (item.ParametroID)
                {
                    case "LCS":
                    case "LCI":
                        lineAnnotation1.Color = OxyColors.Red;
                        lineAnnotation1.StrokeThickness = 3;
                        lineAnnotation1.LineStyle = LineStyle.Solid;

                        break;

                    case "LE":
                        lineAnnotation1.Color = DarkGreen;
                        lineAnnotation1.StrokeThickness = 3;
                        lineAnnotation1.LineStyle = LineStyle.Solid;

                        break;

                    case "+1SP":
                    case "-1SP":
                        lineAnnotation1.Color = OxyColors.Green;
                        lineAnnotation1.LineStyle = LineStyle.DashDot;

                        break;
                    case "+2SP":
                    case "-2SP":
                        lineAnnotation1.Color = OxyColors.Yellow;
                        lineAnnotation1.LineStyle = LineStyle.DashDot;

                        break;
                }

                plotModel2.Annotations.Add(lineAnnotation1);
            }

            return new PlotModels()
            {
                plotModel1 = plotModel1,
                plotModel2 = plotModel2
            };
        }

        public async Task<PlotModels> GetDiametroReport()
        {
            var plotModel1 = new PlotModel();
            var plotModel2 = new PlotModel();

            var r = new RepositoryR(Util.GetConnection());
            var DiametroDetails = await r.GetDiametroReport(Equipo, Material, TurnID);

            if (DiametroDetails == null)
            {
                return null;
            }

            var DiametroDetailsResult = DiametroDetails.Transactions;

            if (!DiametroDetails.Transactions.Any())
            {
                return null;
            }

            plotModel1.LegendTitle = context.GetString(Resource.String.ReportDiametroPromedio);
            plotModel1.LegendTitleColor = OxyColors.Blue;
            plotModel1.LegendTitleFontSize = 16;
            plotModel2.LegendTitle = context.GetString(Resource.String.ReportDiametroRango);
            plotModel2.LegendTitleColor = plotModel1.LegendTitleColor;
            plotModel2.LegendTitleFontSize = plotModel1.LegendTitleFontSize;

            var linearAxisXPromedio = new DateTimeAxis();
            linearAxisXPromedio.Position = AxisPosition.Bottom;
            linearAxisXPromedio.IntervalType = DateTimeIntervalType.Auto;
            linearAxisXPromedio.Minimum = DateTimeAxis.ToDouble(DiametroDetails.Transactions.Min(p => p.Fecha).AddMinutes(-5));
            linearAxisXPromedio.StringFormat = "hh:mm";

            var linearAxisYPromedio = new LinearAxis();
            linearAxisYPromedio.Maximum = DiametroDetailsResult.Max(p => p.Value + p.Value * 0.005);
            linearAxisYPromedio.Minimum = DiametroDetailsResult.Min(p => p.Value - p.Value * 0.005);

            plotModel1.Axes.Add(linearAxisXPromedio);
            plotModel1.Axes.Add(linearAxisYPromedio);

            var linearAxisXRango = new DateTimeAxis();
            linearAxisXRango.Position = AxisPosition.Bottom;
            linearAxisXRango.IntervalType = DateTimeIntervalType.Auto;
            linearAxisXRango.Minimum = linearAxisXPromedio.Minimum;
            linearAxisXPromedio.StringFormat = "hh:mm";

            var linearAxisYRango = new LinearAxis();
            linearAxisYRango.Maximum = DiametroDetailsResult.Max(p => p.ValueRange + p.ValueRange * 0.05);
            linearAxisYRango.Minimum = DiametroDetailsResult.Min(p => p.ValueRange - p.ValueRange * 0.05);

            plotModel2.Axes.Add(linearAxisXRango);
            plotModel2.Axes.Add(linearAxisYRango);

            var lineSeries1 = new LineSeries();
            lineSeries1.Color = OxyColors.Black;
            lineSeries1.MarkerFill = OxyColors.Transparent;
            lineSeries1.DataFieldX = "Time";
            lineSeries1.DataFieldY = "Value";
            lineSeries1.MarkerFill = OxyColors.Black;
            lineSeries1.MarkerSize = 5;

            lineSeries1.MarkerStroke = OxyColors.White;
            lineSeries1.MarkerType = MarkerType.Circle;
            lineSeries1.MarkerStrokeThickness = 1.5;

            var lineSeries2 = new LineSeries();
            lineSeries2.Color = OxyColors.Black;
            lineSeries2.MarkerFill = OxyColors.Transparent;
            lineSeries2.DataFieldX = "Time";
            lineSeries2.DataFieldY = "Value";
            lineSeries2.MarkerFill = OxyColors.Black;
            lineSeries2.MarkerSize = 5;

            lineSeries2.MarkerStroke = OxyColors.White;
            lineSeries2.MarkerType = MarkerType.Circle;
            lineSeries2.MarkerStrokeThickness = 1.5;

            plotModel1.Series.Add(lineSeries1);
            plotModel2.Series.Add(lineSeries2);

            var parametrosPromedioDiametro = GetParametrosDiametro(DiametroDetails.Details.Where(p => p.SubTypeID == "P").ToList(), true);
            var parametrosRangoDiametro = GetParametrosDiametro(DiametroDetails.Details.Where(p => p.SubTypeID == "R").ToList(), false);

            var ParametrosFuera = DiametroDetailsResult.OrderByDescending(p => p.Fecha).First();

            if (ParametrosFuera.Values.Any(p => p < parametrosPromedioDiametro.LimiteLEI || p > parametrosPromedioDiametro.LimiteLES))
            {
                new CustomDialog(context, CustomDialog.Status.Warning, context.GetString(Resource.String.ReportMessageOut));
            }

            var listaPromedio = new ListaZonas();
            var listaRango = new ListaZonas();

            foreach (var item in DiametroDetails.Transactions)
            {
                var zonePromedio = GetZonaDiametro(parametrosPromedioDiametro, item.Value);
                lineSeries1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.Fecha), item.Value));
                lineSeries1.Selectable = false;

                AumentaZona(zonePromedio, ref listaPromedio);

                if (listaPromedio.ListaZonasOUTP > 0 || listaPromedio.ListaZonasAP > 1 || listaPromedio.ListaZonasBP > 2 || listaPromedio.ListaZonasCP > 6)
                {
                    var pointAnnotation1 = new PointAnnotation();
                    pointAnnotation1.X = DateTimeAxis.ToDouble(item.Fecha);
                    pointAnnotation1.Y = item.Value;
                    pointAnnotation1.Shape = MarkerType.Circle;
                    pointAnnotation1.Fill = OxyColors.Red;
                    pointAnnotation1.Stroke = OxyColors.Red;
                    pointAnnotation1.StrokeThickness = 1.5;
                    pointAnnotation1.TextVerticalAlignment = VerticalAlignment.Bottom;
                    pointAnnotation1.Text = listaPromedio.ListaZonasOUTP > 0 ? context.GetString(Resource.String.ReportMessageDiametroUpOut) : context.GetString(Resource.String.ReportMessageDiametroUp);
                    plotModel1.Annotations.Add(pointAnnotation1);
                }

                if (listaPromedio.ListaZonasOUTN > 0 || listaPromedio.ListaZonasAN > 1 || listaPromedio.ListaZonasBN > 2 || listaPromedio.ListaZonasCN > 6)
                {
                    var pointAnnotation1 = new PointAnnotation();
                    pointAnnotation1.X = DateTimeAxis.ToDouble(item.Fecha);
                    pointAnnotation1.Y = item.Value;
                    pointAnnotation1.Shape = MarkerType.Circle;
                    pointAnnotation1.Fill = OxyColors.Red;
                    pointAnnotation1.Stroke = OxyColors.Red;
                    pointAnnotation1.StrokeThickness = 1.5;
                    pointAnnotation1.Text = listaPromedio.ListaZonasOUTN > 0 ? context.GetString(Resource.String.ReportMessageDiametroDownOut) : context.GetString(Resource.String.ReportMessageDiametroDown);
                    pointAnnotation1.TextVerticalAlignment = VerticalAlignment.Top;
                    plotModel1.Annotations.Add(pointAnnotation1);
                }

                var ZonaRango = GetZonaDiametro(parametrosRangoDiametro, item.ValueRange);
                lineSeries2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.Fecha), item.ValueRange));
                lineSeries2.Selectable = false;

                AumentaZona(ZonaRango, ref listaRango);

                if (listaRango.ListaZonasOUTP > 0 || listaRango.ListaZonasAP > 1 || listaRango.ListaZonasBP > 2 || listaRango.ListaZonasCP > 6)
                {
                    var pointAnnotation1 = new PointAnnotation();
                    pointAnnotation1.X = DateTimeAxis.ToDouble(item.Fecha);
                    pointAnnotation1.Y = item.ValueRange;
                    pointAnnotation1.Shape = MarkerType.Circle;
                    pointAnnotation1.Fill = OxyColors.Red;
                    pointAnnotation1.Stroke = OxyColors.Red;
                    pointAnnotation1.StrokeThickness = 1.5;
                    pointAnnotation1.TextVerticalAlignment = VerticalAlignment.Bottom;
                    pointAnnotation1.Text = context.GetString(Resource.String.ReportMessageRangoUp);
                    plotModel2.Annotations.Add(pointAnnotation1);
                }

                if (listaRango.ListaZonasOUTN > 0 || listaRango.ListaZonasAN > 1 || listaRango.ListaZonasBN > 2 || listaRango.ListaZonasCN > 6)
                {
                    var pointAnnotation1 = new PointAnnotation();
                    pointAnnotation1.X = DateTimeAxis.ToDouble(item.Fecha);
                    pointAnnotation1.Y = item.ValueRange;
                    pointAnnotation1.Shape = MarkerType.Plus;
                    pointAnnotation1.Fill = OxyColors.Red;
                    pointAnnotation1.Stroke = OxyColors.Red;
                    pointAnnotation1.StrokeThickness = 1.5;
                    pointAnnotation1.Text = context.GetString(Resource.String.ReportMessageRangoDown);
                    pointAnnotation1.TextVerticalAlignment = VerticalAlignment.Top;
                    plotModel2.Annotations.Add(pointAnnotation1);
                }
            }

            foreach (var item in DiametroDetails.Details.Where(p => p.SubTypeID == "P" && p.IsVisible))
            {
                var lineAnnotation1 = new LineAnnotation();
                lineAnnotation1.Type = LineAnnotationType.Horizontal;
                lineAnnotation1.ClipByXAxis = false;
                lineAnnotation1.Y = (double)item.Value;               

                switch (item.ParametroID)
                {
                    case "LCS":
                    case "LCI":
                        lineAnnotation1.Color = OxyColors.Red;
                        lineAnnotation1.StrokeThickness = 3;
                        lineAnnotation1.LineStyle = LineStyle.Solid;

                        break;

                    case "LE":
                        lineAnnotation1.Color = DarkGreen;
                        lineAnnotation1.StrokeThickness = 3;
                        lineAnnotation1.LineStyle = LineStyle.Solid;

                        break;

                    case "+1SD":
                    case "-1SD":
                        lineAnnotation1.Color = OxyColors.Green;
                        lineAnnotation1.LineStyle = LineStyle.DashDot;

                        break;
                    case "+2SD":
                    case "-2SD":
                        lineAnnotation1.Color = OxyColors.Yellow;
                        lineAnnotation1.LineStyle = LineStyle.DashDot;

                        break;
                }

                plotModel1.Annotations.Add(lineAnnotation1);
            }

            #region RectangleAnnotation for grafica promedio
            var rectangleAnnotation1 = new RectangleAnnotation();
            rectangleAnnotation1.MinimumY = parametrosPromedioDiametro.LimiteSPP2;
            rectangleAnnotation1.MaximumY = parametrosPromedioDiametro.LimiteLCS;
            rectangleAnnotation1.Fill = Pink;
            rectangleAnnotation1.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation2 = new RectangleAnnotation();
            rectangleAnnotation2.MinimumY = parametrosPromedioDiametro.LimiteSPP1;
            rectangleAnnotation2.MaximumY = parametrosPromedioDiametro.LimiteSPP2;
            rectangleAnnotation2.Fill = Yellow;
            rectangleAnnotation2.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation3 = new RectangleAnnotation();
            rectangleAnnotation3.MinimumY = parametrosPromedioDiametro.LimiteLE;
            rectangleAnnotation3.MaximumY = parametrosPromedioDiametro.LimiteSPP1;
            rectangleAnnotation3.Fill = Green;
            rectangleAnnotation3.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation4 = new RectangleAnnotation();
            rectangleAnnotation4.MinimumY = parametrosPromedioDiametro.LimiteSPN1;
            rectangleAnnotation4.MaximumY = parametrosPromedioDiametro.LimiteLE;
            rectangleAnnotation4.Fill = Green;
            rectangleAnnotation4.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation5 = new RectangleAnnotation();
            rectangleAnnotation5.MinimumY = parametrosPromedioDiametro.LimiteSPN2;
            rectangleAnnotation5.MaximumY = parametrosPromedioDiametro.LimiteSPN1;
            rectangleAnnotation5.Fill = Yellow;
            rectangleAnnotation5.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation6 = new RectangleAnnotation();
            rectangleAnnotation6.MinimumY = parametrosPromedioDiametro.LimiteLCI;
            rectangleAnnotation6.MaximumY = parametrosPromedioDiametro.LimiteSPN2;
            rectangleAnnotation6.Fill = Pink;
            rectangleAnnotation6.Layer = AnnotationLayer.BelowSeries;

            plotModel1.Annotations.Add(rectangleAnnotation1);
            plotModel1.Annotations.Add(rectangleAnnotation2);
            plotModel1.Annotations.Add(rectangleAnnotation3);
            plotModel1.Annotations.Add(rectangleAnnotation4);
            plotModel1.Annotations.Add(rectangleAnnotation5);
            plotModel1.Annotations.Add(rectangleAnnotation6);
            #endregion

            #region RectangleAnnotation for grafica rango
            var rectangleAnnotation1R = new RectangleAnnotation();
            rectangleAnnotation1R.MinimumY = parametrosRangoDiametro.LimiteSPP2;
            rectangleAnnotation1R.MaximumY = parametrosRangoDiametro.LimiteLCS;
            rectangleAnnotation1R.Fill = Pink;
            rectangleAnnotation1R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation2R = new RectangleAnnotation();
            rectangleAnnotation2R.MinimumY = parametrosRangoDiametro.LimiteSPP1;
            rectangleAnnotation2R.MaximumY = parametrosRangoDiametro.LimiteSPP2;
            rectangleAnnotation2R.Fill = Yellow;
            rectangleAnnotation2R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation3R = new RectangleAnnotation();
            rectangleAnnotation3R.MinimumY = parametrosRangoDiametro.LimiteLE;
            rectangleAnnotation3R.MaximumY = parametrosRangoDiametro.LimiteSPP1;
            rectangleAnnotation3R.Fill = Green;
            rectangleAnnotation3R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation4R = new RectangleAnnotation();
            rectangleAnnotation4R.MinimumY = parametrosRangoDiametro.LimiteSPN1;
            rectangleAnnotation4R.MaximumY = parametrosRangoDiametro.LimiteLE;
            rectangleAnnotation4R.Fill = Green;
            rectangleAnnotation4R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation5R = new RectangleAnnotation();
            rectangleAnnotation5R.MinimumY = parametrosRangoDiametro.LimiteSPN2;
            rectangleAnnotation5R.MaximumY = parametrosRangoDiametro.LimiteSPN1;
            rectangleAnnotation5R.Fill = Yellow;
            rectangleAnnotation5R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation6R = new RectangleAnnotation();
            rectangleAnnotation6R.MinimumY = parametrosRangoDiametro.LimiteLCI;
            rectangleAnnotation6R.MaximumY = parametrosRangoDiametro.LimiteSPN2;
            rectangleAnnotation6R.Fill = Pink;
            rectangleAnnotation6R.Layer = AnnotationLayer.BelowSeries;

            plotModel2.Annotations.Add(rectangleAnnotation1R);
            plotModel2.Annotations.Add(rectangleAnnotation2R);
            plotModel2.Annotations.Add(rectangleAnnotation3R);
            plotModel2.Annotations.Add(rectangleAnnotation4R);
            plotModel2.Annotations.Add(rectangleAnnotation5R);
            plotModel2.Annotations.Add(rectangleAnnotation6R);
            #endregion

            foreach (var item in DiametroDetails.Details.Where(p => p.SubTypeID == "R" && p.IsVisible))
            {
                var lineAnnotation1 = new LineAnnotation();
                lineAnnotation1.Type = LineAnnotationType.Horizontal;
                lineAnnotation1.ClipByXAxis = false;
                lineAnnotation1.Y = (double)item.Value;

                switch (item.ParametroID)
                {
                    case "LCS":
                    case "LCI":
                        lineAnnotation1.Color = OxyColors.Red;
                        lineAnnotation1.StrokeThickness = 3;
                        lineAnnotation1.LineStyle = LineStyle.Solid;

                        break;

                    case "LE":
                        lineAnnotation1.Color = DarkGreen;
                        lineAnnotation1.StrokeThickness = 3;
                        lineAnnotation1.LineStyle = LineStyle.Solid;

                        break;

                    case "+1SD":
                    case "-1SD":
                        lineAnnotation1.Color = OxyColors.Green;
                        lineAnnotation1.LineStyle = LineStyle.DashDot;

                        break;
                    case "+2SD":
                    case "-2SD":
                        lineAnnotation1.Color = OxyColors.Yellow;
                        lineAnnotation1.LineStyle = LineStyle.DashDot;

                        break;
                }

                plotModel2.Annotations.Add(lineAnnotation1);
            }

            return new PlotModels()
            {
                plotModel1 = plotModel1,
                plotModel2 = plotModel2
            };
        }

        public async Task<PlotModels> GetTiroReport()
        {
            var plotModel1 = new PlotModel();
            var plotModel2 = new PlotModel();

            var r = new RepositoryR(Util.GetConnection());
            var TiroDetails = await r.GetTiroReport(Equipo, Material, TurnID);

            if (TiroDetails == null)
            {
                return null;
            }

            var TiroDetailsResult = TiroDetails.Transactions;

            if (!TiroDetails.Transactions.Any())
            {
                return null;
            }

            plotModel1.LegendTitle = context.GetString(Resource.String.ReportTiroPromedio);
            plotModel1.LegendTitleColor = OxyColors.Blue;
            plotModel1.LegendTitleFontSize = 16;
            plotModel2.LegendTitle = context.GetString(Resource.String.ReportTiroRango);
            plotModel2.LegendTitleColor = plotModel1.LegendTitleColor;
            plotModel2.LegendTitleFontSize = plotModel1.LegendTitleFontSize;

            var linearAxisXPromedio = new DateTimeAxis();
            linearAxisXPromedio.Position = AxisPosition.Bottom;
            linearAxisXPromedio.IntervalType = DateTimeIntervalType.Auto;
            linearAxisXPromedio.Minimum = DateTimeAxis.ToDouble(TiroDetailsResult.Min(p => p.Fecha).AddMinutes(-5));
            linearAxisXPromedio.StringFormat = "hh:mm";

            var linearAxisYPromedio = new LinearAxis();
            linearAxisYPromedio.Maximum = TiroDetailsResult.Max(p => p.Value + p.Value * 0.025);
            linearAxisYPromedio.Minimum = TiroDetailsResult.Min(p => p.Value - p.Value * 0.025);

            plotModel1.Axes.Add(linearAxisXPromedio);
            plotModel1.Axes.Add(linearAxisYPromedio);

            var linearAxisXRango = new DateTimeAxis();
            linearAxisXRango.Position = AxisPosition.Bottom;
            linearAxisXRango.IntervalType = DateTimeIntervalType.Auto;
            linearAxisXRango.Minimum = linearAxisXPromedio.Minimum;
            linearAxisXPromedio.StringFormat = "hh:mm";

            var linearAxisYRango = new LinearAxis();
            linearAxisYRango.Maximum = TiroDetailsResult.Max(p => p.ValueRange + p.ValueRange * 0.05);
            linearAxisYRango.Minimum = TiroDetailsResult.Min(p => p.ValueRange - p.ValueRange * 0.05);

            plotModel2.Axes.Add(linearAxisXRango);
            plotModel2.Axes.Add(linearAxisYRango);

            var lineSeries1 = new LineSeries();
            lineSeries1.Color = OxyColors.Black;
            lineSeries1.MarkerFill = OxyColors.Transparent;
            lineSeries1.DataFieldX = "Time";
            lineSeries1.DataFieldY = "Value";
            lineSeries1.MarkerFill = OxyColors.Black;
            lineSeries1.MarkerSize = 5;

            lineSeries1.MarkerStroke = OxyColors.White;
            lineSeries1.MarkerType = MarkerType.Circle;
            lineSeries1.MarkerStrokeThickness = 1.5;

            var lineSeries2 = new LineSeries();
            lineSeries2.Color = OxyColors.Black;
            lineSeries2.MarkerFill = OxyColors.Transparent;
            lineSeries2.DataFieldX = "Time";
            lineSeries2.DataFieldY = "Value";
            lineSeries2.MarkerFill = OxyColors.Black;
            lineSeries2.MarkerSize = 5;

            lineSeries2.MarkerStroke = OxyColors.White;
            lineSeries2.MarkerType = MarkerType.Circle;
            lineSeries2.MarkerStrokeThickness = 1.5;

            plotModel1.Series.Add(lineSeries1);
            plotModel2.Series.Add(lineSeries2);

            var parametrosPromedioTiro = GetParametrosTiro(TiroDetails.Details.Where(p => p.SubTypeID == "P" && p.IsVisible).ToList());
            var parametrosRangoTiro = GetParametrosTiro(TiroDetails.Details.Where(p => p.SubTypeID == "R" && p.IsVisible).ToList());

            var listaPromedio = new ListaZonas();
            var listaRango = new ListaZonas();

            foreach (var item in TiroDetails.Transactions)
            {
                var zonePromedio = GetZonaTiro(parametrosPromedioTiro, item.Value);
                lineSeries1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.Fecha), item.Value));

                AumentaZona(zonePromedio, ref listaPromedio);

                if (listaPromedio.ListaZonasOUTP > 0 || listaPromedio.ListaZonasAP > 1 || listaPromedio.ListaZonasBP > 2 || listaPromedio.ListaZonasCP > 6)
                {
                    var pointAnnotation1 = new PointAnnotation();
                    pointAnnotation1.X = DateTimeAxis.ToDouble(item.Fecha);
                    pointAnnotation1.Y = item.Value;
                    pointAnnotation1.Shape = MarkerType.Circle;
                    pointAnnotation1.Fill = OxyColors.Red;
                    pointAnnotation1.Stroke = OxyColors.Red;
                    pointAnnotation1.StrokeThickness = 1.5;
                    pointAnnotation1.TextVerticalAlignment = VerticalAlignment.Bottom;
                    pointAnnotation1.Text = listaPromedio.ListaZonasOUTP > 0 ? context.GetString(Resource.String.ReportMessageTiroOut) : context.GetString(Resource.String.ReportMessageTiro);
                    plotModel1.Annotations.Add(pointAnnotation1);
                }

                if (listaPromedio.ListaZonasOUTN > 0 || listaPromedio.ListaZonasAN > 1 || listaPromedio.ListaZonasBN > 2 || listaPromedio.ListaZonasCN > 6)
                {
                    var pointAnnotation1 = new PointAnnotation();
                    pointAnnotation1.X = DateTimeAxis.ToDouble(item.Fecha);
                    pointAnnotation1.Y = item.Value;
                    pointAnnotation1.Shape = MarkerType.Circle;
                    pointAnnotation1.Fill = OxyColors.Red;
                    pointAnnotation1.Stroke = OxyColors.Red;
                    pointAnnotation1.StrokeThickness = 1.5;
                    pointAnnotation1.Text = listaPromedio.ListaZonasOUTN > 0 ? context.GetString(Resource.String.ReportMessageTiroOut) : context.GetString(Resource.String.ReportMessageTiro);
                    pointAnnotation1.TextVerticalAlignment = VerticalAlignment.Top;
                    plotModel1.Annotations.Add(pointAnnotation1);
                }

                var ZonaRango = GetZonaTiro(parametrosRangoTiro, item.ValueRange);
                lineSeries2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.Fecha), item.ValueRange));
                lineSeries2.Selectable = false;

                AumentaZona(ZonaRango, ref listaRango);

                if (listaRango.ListaZonasOUTP > 0 || listaRango.ListaZonasAP > 1 || listaRango.ListaZonasBP > 2 || listaRango.ListaZonasCP > 6)
                {
                    var pointAnnotation1 = new PointAnnotation();
                    pointAnnotation1.X = DateTimeAxis.ToDouble(item.Fecha);
                    pointAnnotation1.Y = item.ValueRange;
                    pointAnnotation1.Shape = MarkerType.Circle;
                    pointAnnotation1.Fill = OxyColors.Red;
                    pointAnnotation1.Stroke = OxyColors.Red;
                    pointAnnotation1.StrokeThickness = 1.5;
                    pointAnnotation1.TextVerticalAlignment = VerticalAlignment.Bottom;
                    pointAnnotation1.Text = context.GetString(Resource.String.ReportMessageRangoUp);
                    plotModel2.Annotations.Add(pointAnnotation1);
                }

                if (listaRango.ListaZonasOUTN > 0 || listaRango.ListaZonasAN > 1 || listaRango.ListaZonasBN > 2 || listaRango.ListaZonasCN > 6)
                {
                    var pointAnnotation1 = new PointAnnotation();
                    pointAnnotation1.X = DateTimeAxis.ToDouble(item.Fecha);
                    pointAnnotation1.Y = item.ValueRange;
                    pointAnnotation1.Shape = MarkerType.Circle;
                    pointAnnotation1.Fill = OxyColors.Red;
                    pointAnnotation1.Stroke = OxyColors.Red;
                    pointAnnotation1.StrokeThickness = 1.5;
                    pointAnnotation1.Text = context.GetString(Resource.String.ReportMessageRangoDown);
                    pointAnnotation1.TextVerticalAlignment = VerticalAlignment.Top;
                    plotModel2.Annotations.Add(pointAnnotation1);
                }
            }

            foreach (var item in TiroDetails.Details.Where(p => p.SubTypeID == "P" && p.IsVisible))
            {
                var lineAnnotation1 = new LineAnnotation();
                lineAnnotation1.Type = LineAnnotationType.Horizontal;
                lineAnnotation1.ClipByXAxis = false;
                lineAnnotation1.Y = (double)item.Value;            

                switch (item.ParametroID)
                {
                    case "LCS":
                    case "LCI":
                        lineAnnotation1.Color = OxyColors.Red;
                        lineAnnotation1.StrokeThickness = 3;
                        lineAnnotation1.LineStyle = LineStyle.Solid;

                        break;

                    case "LE":
                        lineAnnotation1.Color = DarkGreen;
                        lineAnnotation1.StrokeThickness = 3;
                        lineAnnotation1.LineStyle = LineStyle.Solid;

                        break;

                    case "+1ST":
                    case "-1ST":
                        lineAnnotation1.Color = OxyColors.Green;
                        lineAnnotation1.LineStyle = LineStyle.DashDot;

                        break;
                    case "+2ST":
                    case "-2ST":
                        lineAnnotation1.Color = OxyColors.Yellow;
                        lineAnnotation1.LineStyle = LineStyle.DashDot;

                        break;
                }

                plotModel1.Annotations.Add(lineAnnotation1);
            }

            foreach (var item in TiroDetails.Details.Where(p => p.SubTypeID == "R" && p.IsVisible))
            {
                var lineAnnotation1 = new LineAnnotation();
                lineAnnotation1.Type = LineAnnotationType.Horizontal;
                lineAnnotation1.ClipByXAxis = false;
                lineAnnotation1.Y = (double)item.Value;

                switch (item.ParametroID)
                {
                    case "LCS":
                    case "LCI":
                        lineAnnotation1.Color = OxyColors.Red;
                        lineAnnotation1.StrokeThickness = 3;
                        lineAnnotation1.LineStyle = LineStyle.Solid;

                        break;

                    case "LE":
                        lineAnnotation1.Color = DarkGreen;
                        lineAnnotation1.StrokeThickness = 3;
                        lineAnnotation1.LineStyle = LineStyle.Solid;

                        break;

                    case "+1ST":
                    case "-1ST":
                        lineAnnotation1.Color = OxyColors.Green;
                        lineAnnotation1.LineStyle = LineStyle.DashDot;

                        break;
                    case "+2ST":
                    case "-2ST":
                        lineAnnotation1.Color = OxyColors.Yellow;
                        lineAnnotation1.LineStyle = LineStyle.DashDot;

                        break;
                }

                plotModel2.Annotations.Add(lineAnnotation1);
            }

            #region RectangleAnnotation for grafica promedio
            var rectangleAnnotation1 = new RectangleAnnotation();
            rectangleAnnotation1.MinimumY = parametrosPromedioTiro.LimiteSPP2;
            rectangleAnnotation1.MaximumY = parametrosPromedioTiro.LimiteLCS;
            rectangleAnnotation1.Fill = Pink;
            rectangleAnnotation1.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation2 = new RectangleAnnotation();
            rectangleAnnotation2.MinimumY = parametrosPromedioTiro.LimiteSPP1;
            rectangleAnnotation2.MaximumY = parametrosPromedioTiro.LimiteSPP2;
            rectangleAnnotation2.Fill = Yellow;
            rectangleAnnotation2.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation3 = new RectangleAnnotation();
            rectangleAnnotation3.MinimumY = parametrosPromedioTiro.LimiteLE;
            rectangleAnnotation3.MaximumY = parametrosPromedioTiro.LimiteSPP1;
            rectangleAnnotation3.Fill = Green;
            rectangleAnnotation3.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation4 = new RectangleAnnotation();
            rectangleAnnotation4.MinimumY = parametrosPromedioTiro.LimiteSPN1;
            rectangleAnnotation4.MaximumY = parametrosPromedioTiro.LimiteLE;
            rectangleAnnotation4.Fill = Green;
            rectangleAnnotation4.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation5 = new RectangleAnnotation();
            rectangleAnnotation5.MinimumY = parametrosPromedioTiro.LimiteSPN2;
            rectangleAnnotation5.MaximumY = parametrosPromedioTiro.LimiteSPN1;
            rectangleAnnotation5.Fill = Yellow;
            rectangleAnnotation5.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation6 = new RectangleAnnotation();
            rectangleAnnotation6.MinimumY = parametrosPromedioTiro.LimiteLCI;
            rectangleAnnotation6.MaximumY = parametrosPromedioTiro.LimiteSPN2;
            rectangleAnnotation6.Fill = Pink;
            rectangleAnnotation6.Layer = AnnotationLayer.BelowSeries;

            plotModel1.Annotations.Add(rectangleAnnotation1);
            plotModel1.Annotations.Add(rectangleAnnotation2);
            plotModel1.Annotations.Add(rectangleAnnotation3);
            plotModel1.Annotations.Add(rectangleAnnotation4);
            plotModel1.Annotations.Add(rectangleAnnotation5);
            plotModel1.Annotations.Add(rectangleAnnotation6);
            #endregion

            #region RectangleAnnotation for grafica rango
            var rectangleAnnotation1R = new RectangleAnnotation();
            rectangleAnnotation1R.MinimumY = parametrosRangoTiro.LimiteSPP2;
            rectangleAnnotation1R.MaximumY = parametrosRangoTiro.LimiteLCS;
            rectangleAnnotation1R.Fill = Pink;
            rectangleAnnotation1R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation2R = new RectangleAnnotation();
            rectangleAnnotation2R.MinimumY = parametrosRangoTiro.LimiteSPP1;
            rectangleAnnotation2R.MaximumY = parametrosRangoTiro.LimiteSPP2;
            rectangleAnnotation2R.Fill = Yellow;
            rectangleAnnotation2R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation3R = new RectangleAnnotation();
            rectangleAnnotation3R.MinimumY = parametrosRangoTiro.LimiteLE;
            rectangleAnnotation3R.MaximumY = parametrosRangoTiro.LimiteSPP1;
            rectangleAnnotation3R.Fill = Green;
            rectangleAnnotation3R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation4R = new RectangleAnnotation();
            rectangleAnnotation4R.MinimumY = parametrosRangoTiro.LimiteSPN1;
            rectangleAnnotation4R.MaximumY = parametrosRangoTiro.LimiteLE;
            rectangleAnnotation4R.Fill = Green;
            rectangleAnnotation4R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation5R = new RectangleAnnotation();
            rectangleAnnotation5R.MinimumY = parametrosRangoTiro.LimiteSPN2;
            rectangleAnnotation5R.MaximumY = parametrosRangoTiro.LimiteSPN1;
            rectangleAnnotation5R.Fill = Yellow;
            rectangleAnnotation5R.Layer = AnnotationLayer.BelowSeries;

            var rectangleAnnotation6R = new RectangleAnnotation();
            rectangleAnnotation6R.MinimumY = parametrosRangoTiro.LimiteLCI;
            rectangleAnnotation6R.MaximumY = parametrosRangoTiro.LimiteSPN2;
            rectangleAnnotation6R.Fill = Pink;
            rectangleAnnotation6R.Layer = AnnotationLayer.BelowSeries;

            plotModel2.Annotations.Add(rectangleAnnotation1R);
            plotModel2.Annotations.Add(rectangleAnnotation2R);
            plotModel2.Annotations.Add(rectangleAnnotation3R);
            plotModel2.Annotations.Add(rectangleAnnotation4R);
            plotModel2.Annotations.Add(rectangleAnnotation5R);
            plotModel2.Annotations.Add(rectangleAnnotation6R);
            #endregion

            return new PlotModels()
            {
                plotModel1 = plotModel1,
                plotModel2 = plotModel2
            };
        }

        private Zones GetZonaPeso(Detalle parametros, Single value)
        {
            if (value == parametros.LimiteLE)
            {
                return Zones.LE;
            }
            else if (value > parametros.LimiteLCS)
            {
                return Zones.OUTP;
            }
            else if (value < parametros.LimiteLCI)
            {
                return Zones.OUTN;
            }
            else if (value < parametros.LimiteLCS && value > parametros.LimiteSPP2)
            {
                return Zones.AP;
            }
            else if (value > parametros.LimiteLCI && value < parametros.LimiteSPN2)
            {
                return Zones.AN;
            }
            else if (value < parametros.LimiteSPP2 && value > parametros.LimiteSPP1)
            {
                return Zones.BP;
            }
            else if (value > parametros.LimiteSPN2 && value < parametros.LimiteSPN1)
            {
                return Zones.BN;
            }
            else if (value < parametros.LimiteSPP1 && value > parametros.LimiteLE)
            {
                return Zones.CP;
            }
            else
            {
                return Zones.CN;
            }
        }

        private Zones GetZonaDiametro(Detalle parametros, Single value)
        {
            if (value == parametros.LimiteLE)
            {
                return Zones.LE;
            }
            else if (value > parametros.LimiteLCS)
            {
                return Zones.OUTP;
            }
            else if (value < parametros.LimiteLCI)
            {
                return Zones.OUTN;
            }
            else if (value < parametros.LimiteLCS && value > parametros.LimiteSPP2)
            {
                return Zones.AP;
            }
            else if (value > parametros.LimiteLCI && value < parametros.LimiteSPN2)
            {
                return Zones.AN;
            }
            else if (value < parametros.LimiteSPP2 && value > parametros.LimiteSPP1)
            {
                return Zones.BP;
            }
            else if (value > parametros.LimiteSPN2 && value < parametros.LimiteSPN1)
            {
                return Zones.BN;
            }
            else if (value < parametros.LimiteSPP1 && value > parametros.LimiteLE)
            {
                return Zones.CP;
            }
            else
            {
                return Zones.CN;
            }
        }

        private Zones GetZonaTiro(Detalle parametros, Single value)
        {
            if (value == parametros.LimiteLE)
            {
                return Zones.LE;
            }
            else if (value > parametros.LimiteLCS)
            {
                return Zones.OUTP;
            }
            else if (value < parametros.LimiteLCI)
            {
                return Zones.OUTN;
            }
            else if (value < parametros.LimiteLCS && value > parametros.LimiteSPP2)
            {
                return Zones.AP;
            }
            else if (value > parametros.LimiteLCI && value < parametros.LimiteSPN2)
            {
                return Zones.AN;
            }
            else if (value < parametros.LimiteSPP2 && value > parametros.LimiteSPP1)
            {
                return Zones.BP;
            }
            else if (value > parametros.LimiteSPN2 && value < parametros.LimiteSPN1)
            {
                return Zones.BN;
            }
            else if (value < parametros.LimiteSPP1 && value > parametros.LimiteLE)
            {
                return Zones.CP;
            }
            else
            {
                return Zones.CN;
            }
        }

        private Detalle GetParametrosPeso(IEnumerable<GraficsReport.DetailsResult> detalle, Boolean Message)
        {
            var parametros = new Detalle();
            parametros.LimiteSPP1 = detalle.First(p => p.ParametroID == "+1SP").Value;
            parametros.LimiteSPP2 = detalle.First(p => p.ParametroID == "+2SP").Value;
            parametros.LimiteSPN1 = detalle.First(p => p.ParametroID == "-1SP").Value;
            parametros.LimiteSPN2 = detalle.First(p => p.ParametroID == "-2SP").Value;
            parametros.LimiteLCI = detalle.First(p => p.ParametroID == "LCI").Value;
            parametros.LimiteLCS = detalle.First(p => p.ParametroID == "LCS").Value;
            parametros.LimiteLE = detalle.First(p => p.ParametroID == "LE").Value;

            if (Message)
            {
                parametros.LimiteLEI = detalle.First(p => p.ParametroID == "LEI").Value;
                parametros.LimiteLES = detalle.First(p => p.ParametroID == "LES").Value;
            }

            return parametros;
        }

        private Detalle GetParametrosDiametro(IEnumerable<GraficsReport.DetailsResult> detalle, Boolean Message)
        {
            var parametros = new Detalle();
            parametros.LimiteSPP1 = detalle.First(p => p.ParametroID == "+1SD").Value;
            parametros.LimiteSPP2 = detalle.First(p => p.ParametroID == "+2SD").Value;
            parametros.LimiteSPN1 = detalle.First(p => p.ParametroID == "-1SD").Value;
            parametros.LimiteSPN2 = detalle.First(p => p.ParametroID == "-2SD").Value;
            parametros.LimiteLCI = detalle.First(p => p.ParametroID == "LCI").Value;
            parametros.LimiteLCS = detalle.First(p => p.ParametroID == "LCS").Value;
            parametros.LimiteLE = detalle.First(p => p.ParametroID == "LE").Value;

            if (Message)
            {
                parametros.LimiteLEI = detalle.First(p => p.ParametroID == "LEI").Value;
                parametros.LimiteLES = detalle.First(p => p.ParametroID == "LES").Value;
            }

            return parametros;
        }

        private Detalle GetParametrosTiro(IEnumerable<GraficsReport.DetailsResult> detalle)
        {
            var parametros = new Detalle();
            parametros.LimiteSPP1 = detalle.First(p => p.ParametroID == "+1ST").Value;
            parametros.LimiteSPP2 = detalle.First(p => p.ParametroID == "+2ST").Value;
            parametros.LimiteSPN1 = detalle.First(p => p.ParametroID == "-1ST").Value;
            parametros.LimiteSPN2 = detalle.First(p => p.ParametroID == "-2ST").Value;
            parametros.LimiteLCI = detalle.First(p => p.ParametroID == "LCI").Value;
            parametros.LimiteLCS = detalle.First(p => p.ParametroID == "LCS").Value;
            parametros.LimiteLE = detalle.First(p => p.ParametroID == "LE").Value;

            return parametros;
        }

        private void AumentaZona(Zones zona, ref ListaZonas lista)
        {
            if (zona != Zones.AP) lista.ListaZonasAP = 0;
            if (zona != Zones.AN) lista.ListaZonasAN = 0;
            if (zona != Zones.BP) lista.ListaZonasBP = 0;
            if (zona != Zones.BN) lista.ListaZonasBN = 0;
            if (zona != Zones.CP) lista.ListaZonasCP = 0;
            if (zona != Zones.CN) lista.ListaZonasCN = 0;
            if (zona != Zones.LE) lista.ListaZonasLE = 0;
            if (zona != Zones.OUTP) lista.ListaZonasOUTP = 0;
            if (zona != Zones.OUTN) lista.ListaZonasOUTN = 0;

            if (zona == Zones.AP) lista.ListaZonasAP++;
            if (zona == Zones.AN) lista.ListaZonasAN++;
            if (zona == Zones.BP) lista.ListaZonasBP++;
            if (zona == Zones.BN) lista.ListaZonasBN++;
            if (zona == Zones.CP) lista.ListaZonasCP++;
            if (zona == Zones.CN) lista.ListaZonasCN++;
            if (zona == Zones.LE) lista.ListaZonasLE++;
            if (zona == Zones.OUTP) lista.ListaZonasOUTP++;
            if (zona == Zones.OUTN) lista.ListaZonasOUTN++;
        }

        public class PlotModels
        {
            public PlotModel plotModel1 { get; set; }
            public PlotModel plotModel2 { get; set; }
        }

        public class Detalle
        {
            public Single LimiteSPP1 { get; set; }
            public Single LimiteSPP2 { get; set; }
            public Single LimiteSPN1 { get; set; }
            public Single LimiteSPN2 { get; set; }
            public Single LimiteLCI { get; set; }
            public Single LimiteLCS { get; set; }
            public Single LimiteLE { get; set; }
            public Single LimiteLEI { get; set; }
            public Single LimiteLES { get; set; }
        }

        private class ListaZonas
        {
            public Byte ListaZonasAP;
            public Byte ListaZonasAN;
            public Byte ListaZonasBP;
            public Byte ListaZonasBN;
            public Byte ListaZonasCP;
            public Byte ListaZonasCN;
            public Byte ListaZonasLE;
            public Byte ListaZonasOUTP;
            public Byte ListaZonasOUTN;
        }
    }
}