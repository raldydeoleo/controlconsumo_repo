using System;

using Android.App;
using Android.Content;
using Android.Widget;
using static Android.Support.V4.Widget.DrawerLayout;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Droid.Managers;
using System.Collections.Generic;

namespace ControlConsumo.Droid.Activities.Widgets
{
    class PrintConfirmationDialog
    {
        public delegate void PrintLabel(Byte cantidadReimpresiones, Byte idMotivoReimpresion);
        public event PrintLabel OnPrintLabel;
        private readonly Context context;
        protected RepositoryFactory repo;
        public DateTime printedDate { get; set; }
        public string packId { get; set; }
        public int packSequence { get; set; }
        private Elaborates salida { get; set; }
        private ProductsRoutes traza { get; set; }

        public PrintConfirmationDialog(Context context, RepositoryFactory repo, Elaborates elaborate, ProductsRoutes traza)
        {
            this.context = context;
            this.repo = repo;
            this.salida = elaborate;
            this.traza = traza;
        }

        public async void ShowDialog()
        {
            Dialog dialog = null;

            dialog = new Dialog(context, Android.Resource.Style.ThemeTranslucentNoTitleBar);
            dialog.SetContentView(Resource.Layout.dialog_print_confirmation);
            dialog.SetCancelable(false);

            var LayoutPass = dialog.FindViewById<LinearLayout>(Resource.Id.custom_dialog_first_rl);
            LayoutPass.Background = context.Resources.GetDrawable(Resource.Color.gray_base);
            LayoutPass.Background.SetAlpha(175);

            var rgCantidadReimpresiones = dialog.FindViewById<RadioGroup>(Resource.Id.rgCantidadImpresiones);
            var btnCancelDialog = dialog.FindViewById<Button>(Resource.Id.btnCancelPrintConfirmationDialog);
            var spnMotivoReimpresionDialog = dialog.FindViewById<Spinner>(Resource.Id.spnMotivoReimpresion);
            var imgBtnAceptDialog = dialog.FindViewById<ImageButton>(Resource.Id.imgButtonPrint);
            var txtViewSecuencia = dialog.FindViewById<TextView>(Resource.Id.txtViewSecuencia);
            var txtViewAlmacenamientoFiller = dialog.FindViewById<TextView>(Resource.Id.txtViewAlmacenamiento_Filler);
            var txtViewEmpaque = dialog.FindViewById<TextView>(Resource.Id.txtViewEmpaque);
            var txtViewHora = dialog.FindViewById<TextView>(Resource.Id.txtViewHora);

            int[] array = { 1, 2 };
            RadioGroup.LayoutParams rprms;
            var options = new List<string>();

            for (int i = 0; i < array.Length; i++)
            {
                var radioButton = new RadioButton(context);
                rprms = new RadioGroup.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
                radioButton.Id = i+1;
                radioButton.Text = array[i].ToString();
                radioButton.TextSize = 250;                
                radioButton.SetWidth(150);
                radioButton.SetTextAppearance(context, Android.Resource.Color.Black);
                radioButton.SetTextAppearance(context, Android.Resource.Style.TextAppearanceLarge);
                if (array[i] == 1)
                {
                    radioButton.Checked = true;
                }
                rgCantidadReimpresiones.AddView(radioButton, rprms);
            }

            var repoLabelPrintingReasons = repo.GetRepositoryLabelPrintingReasons();
            var listaMotivosReimpresion = new List<LabelPrintingReason>(await repoLabelPrintingReasons.GetAsyncAll());

            try
            {
                options.Add("Seleccione un motivo");

                foreach (var item in listaMotivosReimpresion)
                {
                    if (item.Active)
                    {
                        options.Add(item.Description);
                    }
                }
                spnMotivoReimpresionDialog.Adapter = new ArrayAdapter<String>(context, Android.Resource.Layout.SimpleDropDownItem1Line, options.ToArray());
                spnMotivoReimpresionDialog.ScrollBarSize = 600;
            }
            catch (Exception ex)
            {
                await Util.SaveException(ex);
            }
                       
            var repoZ = repo.GetRepositoryZ();

            var secuenciaEmpaque = salida.PackSequence == 0 ? traza.SecuenciaEmpaque : salida.PackSequence;

            var cantidadReimpresionesRealizadas = await repoZ.GetCantidadReimpresionesEtiquetaAsync(salida._Produccion, salida.TurnID, salida.PackID, secuenciaEmpaque);
            
            txtViewSecuencia.Text = secuenciaEmpaque.ToString("0000");

            txtViewAlmacenamientoFiller.Text = salida.Identifier;

            txtViewEmpaque.Text = salida.PackID;

            txtViewHora.Text = salida.Produccion.ToLocalTime().ToString("hh:mm");

            btnCancelDialog.Click += (obj, arg) =>
            {
                if (dialog != null)
                {
                    dialog.Dismiss();
                    dialog.Dispose();
                }
            };

            imgBtnAceptDialog.Click += async (obj, arg) =>
            {
                RadioButton radioButton = new RadioButton(context);

                if (OnPrintLabel != null)
                {
                    if (dialog != null)
                    {
                        radioButton = dialog.FindViewById<RadioButton>(rgCantidadReimpresiones.CheckedRadioButtonId);
                        byte idMotivoReimpresion = 0;
                        try
                        {
                            var cantidadReimpresiones = Convert.ToByte(radioButton.Text);

                            if (spnMotivoReimpresionDialog.SelectedItemPosition > 0)
                            {
                                idMotivoReimpresion = listaMotivosReimpresion[spnMotivoReimpresionDialog.SelectedItemPosition - 1].ID;
                                OnPrintLabel.Invoke(cantidadReimpresiones, idMotivoReimpresion);
                                dialog.Dismiss();
                                dialog.Dispose();
                            }
                            else
                            {
                                var errorDialog = new CustomDialog(context, CustomDialog.Status.Error, "Debe seleccionar un motivo de reimpresión.");
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            await Util.SaveException(ex, "Selección de cantidad de reimpresiones - Case Packer.");
                        }
                    }
                }
            };
             
            dialog.Show();
        }

    }
}