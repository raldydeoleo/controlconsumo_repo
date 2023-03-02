using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using ControlConsumo.Droid.Managers;
using ControlConsumo.Shared.Models.Config;
using ControlConsumo.Shared.Models.R;
using ControlConsumo.Shared.Repositories;
using ControlConsumo.Shared.Tables;
using System;
using System.Threading.Tasks;


namespace ControlConsumo.Droid.Activities.Widgets
{
    public class CustomDialog
    {
        private ButtonStyles Buttons;
        private readonly String MaterialCode;
        private readonly String Lot;
        private readonly String Unit;
        private readonly double Ean;
        private readonly double TrayCigarsQuantity;
        private readonly Context context;
        private readonly Status status;
        private readonly Dialog dialog;
        private readonly Button btnAceptDialog1;
        private readonly Button btnAceptDialog2;
        private readonly Button btnAceptDialog3;
        private readonly Button btnCancelDialog;
        private readonly Button btnCancelDialog2;
        private readonly Button btnCancelDialog3;
        private readonly TextView txtShowMessagesBox;
        private readonly TextView txtViewBoxMessage;
        private readonly EditText editScan;
        private readonly RepositoryZ repoz;
        private readonly SoundPool soundPool;
        public readonly EditText editBoxNo;
        private readonly EditText editTrayCigarsQuantity;
        private readonly Int32 soundID;
        private MaterialReport Material { get; set; }
        private readonly RepositoryFactory repo = new RepositoryFactory(Util.GetConnection());
        private Boolean isKeyBoardLocked;
        private ActualConfig ActualConfig;
        private DateTime ProductionDate;
        private int TurnId;
        public enum Status
        {
            Good,
            GoodValidateBox,
            Warning,
            Error,
            ErrorValidateTray,
            EmptyTray
        }

        public enum ButtonStyles
        {
            NoButton,
            OneButton,
            TwoButton,
            TwoButtonWithBoxPeso,
            TwoButtonWithBoxGramo,
            TwoButtonWithBoxCantidad,
            TwoButtonWithAutorize,
            TwoButtonWithAutorizeReprint,
            TwoButtonWithPrint,
            TwoButtonWithContinue,
            OneButtonScanCode,
            TwoButtonWithUpcCode,
            TwoButtonWithTrayCantidad
        }

        public delegate void AcceptPress(Boolean IsCantidad, Single Box, Single Cantidad);
        public delegate void CancelPress();
        public delegate void ValidaSecuencia();
        public delegate void ValidaBandeja();
        public delegate void ScanResult(BarCodeResult result);

        public event AcceptPress OnAcceptPress;
        public event CancelPress OnCancelPress;
        public event ValidaSecuencia ONValidaSecuencia;
        public event ValidaBandeja ONValidaBandeja;
        public event ScanResult ONScanResult;

        public CustomDialog(Context context, Status status = Status.Good, String message = "", ButtonStyles Button = ButtonStyles.OneButton, String MaterialCode = null, String Lot = null, String Unit = null, double Ean = 0, Boolean isKeyBoardLocked = true, double TrayCigarsQuantity = 0
            , ActualConfig actualConfig = null, DateTime? productionDate = null, int turnId = 0 )
        {
            this.soundPool = new SoundPool(1, Stream.Music, 100);
            soundID = soundPool.Load(context, Resource.Raw.beep, 1);
            this.MaterialCode = MaterialCode;
            this.Lot = Lot;
            this.Ean = Ean;
            this.Unit = Unit;
            this.TrayCigarsQuantity = TrayCigarsQuantity;
            this.isKeyBoardLocked = isKeyBoardLocked;
            this.repoz = new RepositoryZ(Util.GetConnection());
            this.context = context;
            this.ActualConfig = actualConfig;
            this.ProductionDate = productionDate ?? new DateTime();
            this.TurnId = turnId;
            dialog = new Dialog(context, Android.Resource.Style.ThemeTranslucentNoTitleBar);
            dialog.SetContentView(Resource.Layout.dialog_messages);
            dialog.SetCancelable(false);

            OnAcceptPress -= null;
            OnCancelPress -= null;

            #region Region de Initialization

            var custom_dialog = dialog.FindViewById<LinearLayout>(Resource.Id.custom_dialog_first_rl);
            var txtShowMessages = custom_dialog.FindViewById<TextView>(Resource.Id.txtShowMessages);
            var flipper = custom_dialog.FindViewById<ViewFlipper>(Resource.Id.flipper_view_message);

            btnAceptDialog1 = custom_dialog.FindViewById<Button>(Resource.Id.btnAceptDialog1);
            btnCancelDialog = custom_dialog.FindViewById<Button>(Resource.Id.btnCancelDialog);
            btnCancelDialog2 = custom_dialog.FindViewById<Button>(Resource.Id.btnCancelDialog2);
            btnCancelDialog3 = custom_dialog.FindViewById<Button>(Resource.Id.btnCancelDialog3);
            btnAceptDialog2 = custom_dialog.FindViewById<Button>(Resource.Id.btnAceptDialog2);
            btnAceptDialog3 = custom_dialog.FindViewById<Button>(Resource.Id.btnAceptDialog3);
            txtViewBoxMessage = custom_dialog.FindViewById<TextView>(Resource.Id.txtViewBoxMessage);
            editScan = custom_dialog.FindViewById<EditText>(Resource.Id.editScan);
            editBoxNo = custom_dialog.FindViewById<EditText>(Resource.Id.editBoxNo);
            editTrayCigarsQuantity = custom_dialog.FindViewById<EditText>(Resource.Id.editTrayCigarsQuantity);
            txtShowMessagesBox = custom_dialog.FindViewById<TextView>(Resource.Id.txtShowMessagesBox);

            editScan.KeyPress += EditScan_KeyPressAsync;

            /*if (Button == ButtonStyles.TwoButtonWithUpcCode)
            {
                //Bloqueo de campo para que el usuario no pueda utilizar el teclado en la digitacion de codigo UPC. 
                editBoxNo.Clickable = false;
                editBoxNo.LongClickable = false;
                editBoxNo.Focusable = true;
                editBoxNo.FocusableInTouchMode = false;

            }*/
            editBoxNo.KeyPress += async (arg, e) =>
            {
                e.Handled = false;
                if (e.KeyCode == Keycode.Enter && !String.IsNullOrEmpty(editBoxNo.Text) && e.Event.Action == KeyEventActions.Down && Buttons == ButtonStyles.TwoButtonWithUpcCode)
                {
                    if (!await ProcessUpcCodeAsync(editBoxNo.Text.GetBarCode(), Ean, editBoxNo))
                    {
                        return;
                    }
                    else
                    {
                        OnAcceptPress.Invoke(false, 0, 0);
                        if (soundPool != null)
                            soundPool.Stop(soundID);
                        dialog.Dismiss();
                        dialog.Dispose();
                    }
                    e.Handled = true;
                }
            };


            btnAceptDialog1.Click += btnAceptDialog1_Click;
            btnAceptDialog2.Click += btnAceptDialog1_Click;
            btnAceptDialog3.Click += btnAceptDialog1_Click;

            btnCancelDialog.Click += btnCancelDialog_Click;
            btnCancelDialog2.Click += btnCancelDialog_Click;
            btnCancelDialog3.Click += btnCancelDialog_Click;

            #endregion

            switch (Button)
            {
                case ButtonStyles.NoButton:
                    flipper.Visibility = ViewStates.Gone;
                    break;

                case ButtonStyles.OneButton:
                    flipper.DisplayedChild = 0;
                    break;

                case ButtonStyles.TwoButton:
                    flipper.DisplayedChild = 1;
                    break;

                case ButtonStyles.TwoButtonWithBoxGramo:
                    flipper.DisplayedChild = 2;
                    txtViewBoxMessage.Text = context.GetString(Resource.String.EntryPesoNo);
                    txtShowMessagesBox.Text = context.GetString(Resource.String.EntryPesoNoWrong);

                    break;

                case ButtonStyles.TwoButtonWithBoxPeso:
                    flipper.DisplayedChild = 2;
                    txtViewBoxMessage.Text = context.GetString(Resource.String.EntryBoxNo);
                    txtShowMessagesBox.Text = context.GetString(Resource.String.EntryBoxNoWrong);

                    break;

                case ButtonStyles.TwoButtonWithTrayCantidad:
                    flipper.DisplayedChild = 2;
                    editBoxNo.Visibility = ViewStates.Gone;
                    editTrayCigarsQuantity.Visibility = ViewStates.Visible;
                    txtViewBoxMessage.Text = String.Format(context.GetString(Resource.String.EntryBoxQuantity), Unit);
                    txtShowMessagesBox.Text = context.GetString(Resource.String.EntryQuantityWrong);

                    break;

                case ButtonStyles.TwoButtonWithBoxCantidad:
                    flipper.DisplayedChild = 2;
                    txtViewBoxMessage.Text = String.Format(context.GetString(Resource.String.EntryBoxQuantity), Unit);
                    txtShowMessagesBox.Text = context.GetString(Resource.String.EntryQuantityWrong);

                    break;

                case ButtonStyles.TwoButtonWithUpcCode:
                    flipper.DisplayedChild = 2;
                    txtViewBoxMessage.Text = "Lea el código UPC: ";
                    txtShowMessagesBox.Text = context.GetString(Resource.String.EntryQuantityWrong);

                    if (isKeyBoardLocked)
                    {
                        //Bloqueo de campo para que el usuario no pueda utilizar el teclado en la digitacion de codigo UPC. 
                        editBoxNo.Clickable = false;
                        editBoxNo.LongClickable = false;
                        editBoxNo.Focusable = true;
                        editBoxNo.FocusableInTouchMode = false;

                        var imm = (InputMethodManager)context.GetSystemService(Context.InputMethodService);
                        imm.HideSoftInputFromWindow(custom_dialog.ApplicationWindowToken, HideSoftInputFlags.None);
                    }

                    break;

                case ButtonStyles.TwoButtonWithAutorize:
                    flipper.DisplayedChild = 1;
                    btnAceptDialog3.Text = context.GetString(Resource.String.Autorize);

                    break;

                case ButtonStyles.TwoButtonWithAutorizeReprint:
                    flipper.DisplayedChild = 1;
                    btnAceptDialog3.Text = context.GetString(Resource.String.Autorize);

                    break;

                case ButtonStyles.TwoButtonWithPrint:
                    flipper.DisplayedChild = 1;
                    btnAceptDialog3.Text = context.GetString(Resource.String.Print);

                    break;

                case ButtonStyles.TwoButtonWithContinue:
                    flipper.DisplayedChild = 1;
                    btnAceptDialog3.Text = context.GetString(Resource.String.Continue);

                    break;

                case ButtonStyles.OneButtonScanCode:
                    flipper.DisplayedChild = 3;

                    break;
            }

            Buttons = Button;

            txtShowMessages.Text = message;

            this.status = status;

            switch (status)
            {
                case Status.GoodValidateBox:
                case Status.Good:
                    custom_dialog.Background = context.Resources.GetDrawable(Resource.Color.green_light);
                    break;

                case Status.Warning:
                    custom_dialog.Background = context.Resources.GetDrawable(Resource.Color.yellow_light);
                    break;

                case Status.EmptyTray:
                    custom_dialog.Background = context.Resources.GetDrawable(Resource.Color.yellow_light);
                    if (Button == ButtonStyles.NoButton && status == Status.EmptyTray)
                    {
                        Task.Run(() =>
                        {
                            Task.Delay(500).Wait();
                            //OnAcceptPress.Invoke(false, 0);
                            dialog.Dismiss();
                            dialog.Dispose();
                        });
                    }
                    break;

                case Status.ErrorValidateTray:
                    custom_dialog.Background = context.Resources.GetDrawable(Resource.Color.red_light);
                    break;

                case Status.Error:
                    custom_dialog.Background = context.Resources.GetDrawable(Resource.Color.red_light);
                    soundPool.LoadComplete += (object sender, SoundPool.LoadCompleteEventArgs e) =>
                    {
                        soundPool.Play(soundID, 1, 1, 1, -1, 1f);
                    };

                    if (Button == ButtonStyles.OneButton)
                        btnAceptDialog1.Text = context.GetString(Resource.String.ButtonCancel);

                    break;
            }

            custom_dialog.Background.SetAlpha(255);

            if (status != Status.GoodValidateBox && status != Status.ErrorValidateTray) dialog.Show();

            if (Button == ButtonStyles.TwoButtonWithBoxGramo || Button == ButtonStyles.TwoButtonWithBoxPeso || Button == ButtonStyles.TwoButtonWithBoxCantidad)
            {
                editBoxNo.RequestFocus();
                editBoxNo.RequestFocusFromTouch();
                var imm = (InputMethodManager)context.GetSystemService(Context.InputMethodService);
                imm.ShowSoftInput(editBoxNo, ShowFlags.Implicit);
                imm.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
            }

            if (Button == ButtonStyles.NoButton)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(500);
                    OnAcceptPress.Invoke(false, 0, 0);
                    dialog.Dismiss();
                    dialog.Dispose();
                });
            }

            if (status == Status.GoodValidateBox) InvokeEvent();

            if (status == Status.ErrorValidateTray) InvokeEventError();
        }

        private async void EditScan_KeyPressAsync(object sender, View.KeyEventArgs e)
        {
            e.Handled = false;
            if (e.KeyCode == Keycode.Enter && !String.IsNullOrEmpty(editScan.Text) && e.Event.Action == KeyEventActions.Down)
            {
                var barcode = editScan.Text.GetBarCode();

                if (barcode == null || barcode.Lot == null)
                {
                    editScan.Text = String.Empty;
                    editScan.RequestFocus();
                    return;
                }

                var material = repoz.GetMaterialByCodeOrReference(barcode.BarCode);

                if (material == null || (material.Group != "L0102" && material.Group != "L0101"))
                {
                    new CustomDialog(context, Status.Error, context.GetString(Resource.String.AlertScanMessageWrong), ButtonStyles.OneButton);
                    editScan.Text = String.Empty;
                    editScan.RequestFocus();
                    return;
                }

                var existe = repoz.ExisteEnBom(Material.ProductCode, material.Code);

                if (!existe)
                {
                    new CustomDialog(context, Status.Error, String.Format(context.GetString(Resource.String.MessageWrongEntry), material._DisplayCode, Material._ProductCode), ButtonStyles.OneButton);
                    editScan.Text = String.Empty;
                    editScan.RequestFocus();
                    return;
                }

                existe = repoz.ExisteLote(material.Code, barcode.Lot);
                var existeLoteParaVarilla = false;
                var loteInterno = await repoz.GetLoteForMaterialAsync(Material.MaterialCode, barcode.Lot);
                if (loteInterno != null && !String.IsNullOrEmpty(loteInterno.Reference))
                {
                    existeLoteParaVarilla = true;
                }
                if (!existeLoteParaVarilla)
                {
                    var msg = String.Format("Lote interno {0} no existe para la varilla {1}", barcode.Lot, Material._MaterialCode);
                    var customDialog = new CustomDialog(context, CustomDialog.Status.Error, msg, CustomDialog.ButtonStyles.OneButton);
                    editScan.Text = String.Empty;
                    editScan.RequestFocus();
                    return;
                }
                if (!existe)
                {
                    new CustomDialog(context, Status.Error, context.GetString(Resource.String.MessageWrongEntryNoBatchExist), ButtonStyles.OneButton);
                    editScan.Text = String.Empty;
                    editScan.RequestFocus();
                    return;
                }

                dialog.Dismiss();
                dialog.Dispose();
                ONScanResult.Invoke(barcode.Clone());

                e.Handled = true;
            }
        }

        private void btnCancelDialog_Click(object sender, EventArgs e)
        {
            if (soundPool != null)
                soundPool.Stop(soundID);

            if (OnCancelPress != null)
            {
                OnCancelPress.Invoke();
            }

            dialog.Dismiss();
            dialog.Dispose();
        }

        public void SetMaterial(MaterialReport Material)
        {
            this.Material = Material;
        }

        private async void btnAceptDialog1_Click(object sender, EventArgs e)
        {
            soundPool.Stop(soundID);

            if (Buttons == ButtonStyles.TwoButtonWithAutorize)
            {
                var pass = new PassDialog(context, PassDialog.Motivos.Necesita_Autorizacion, Shared.Tables.RolsPermits.Permits.AUTORIZAR_VENCIDOS);
                pass.OnAcceptPress += pass_OnAcceptPress;
                pass.OnCancelPress += pass_OnCancelPress;
                dialog.Dismiss();
                dialog.Dispose();
                return;
            }

            if ((Buttons == ButtonStyles.TwoButtonWithBoxPeso || Buttons == ButtonStyles.TwoButtonWithBoxGramo || Buttons == ButtonStyles.TwoButtonWithBoxCantidad) && String.IsNullOrEmpty(editBoxNo.Text))
            {
                txtShowMessagesBox.Visibility = ViewStates.Visible;
                editBoxNo.RequestFocus();
                return;
            }

            if (Buttons == ButtonStyles.TwoButtonWithBoxPeso)
            {
                var exist = await repoz.ValidaBoxNumberAsync(MaterialCode, Lot, Convert.ToInt16(editBoxNo.Text));

                if (exist)
                {
                    txtShowMessagesBox.Visibility = ViewStates.Visible;
                    txtShowMessagesBox.Text = String.Format(context.GetString(Resource.String.MessageExistBoxNumber), editBoxNo.Text);
                    return;
                }
            }

            if ((Buttons == ButtonStyles.TwoButtonWithBoxCantidad || Buttons == ButtonStyles.TwoButtonWithBoxGramo || Buttons == ButtonStyles.TwoButtonWithBoxPeso) && (editBoxNo.Text.CastToSingle() == 0 || editBoxNo.Text.CastToSingle() > 1000))
            {
                editBoxNo.Text = String.Empty;
                editBoxNo.RequestFocus();
                return;
            }

            if (Buttons == ButtonStyles.OneButton && status == Status.Error && OnCancelPress != null)
            {
                OnCancelPress.Invoke();
            }

            if (Buttons == ButtonStyles.TwoButtonWithUpcCode)
            {
                if (!await ProcessUpcCodeAsync(editBoxNo.Text.GetBarCode(), Ean, editBoxNo))
                {
                    return;
                }
            }

            if (Buttons == ButtonStyles.TwoButtonWithTrayCantidad)
            {
                if (String.IsNullOrEmpty(editTrayCigarsQuantity.Text))
                {

                    txtShowMessagesBox.Text = context.GetString(Resource.String.EntryQuantityWrong);
                    txtShowMessagesBox.Visibility = ViewStates.Visible;
                    editTrayCigarsQuantity.Text = String.Empty;
                    editTrayCigarsQuantity.RequestFocus();
                    return;
                }
                else
                {
                    var cantidad = Convert.ToDouble(editTrayCigarsQuantity.Text);

                    if (cantidad <= 0)
                    {

                        txtShowMessagesBox.Text = "Cantidad debe ser mayor a 0 ";
                        txtShowMessagesBox.Visibility = ViewStates.Visible;
                        editTrayCigarsQuantity.Text = String.Empty;
                        editTrayCigarsQuantity.RequestFocus();
                        return;
                    }
                    if (cantidad > TrayCigarsQuantity)
                    {
                        txtShowMessagesBox.Text = String.Format("Cantidad debe ser menor o igual a {0} ", TrayCigarsQuantity);
                        txtShowMessagesBox.Visibility = ViewStates.Visible;
                        editTrayCigarsQuantity.Text = String.Empty;
                        editTrayCigarsQuantity.RequestFocus();
                        return;
                    }

                    //Control de entradas y salidas
                    var resultInOut = await repoz.Get_InputOutputControl_ByCigarsConsumed(ActualConfig, (float) cantidad, ProductionDate, TurnId, Unit);

                    if (!resultInOut)
                    {
                        txtShowMessagesBox.Text = context.GetString(Resource.String.need_new_tray);
                        txtShowMessagesBox.Visibility = ViewStates.Visible;
                        editTrayCigarsQuantity.Text = String.Empty;
                        editTrayCigarsQuantity.RequestFocus();
                        return;
                    }

                }
            }

            if (OnAcceptPress != null)
            {
                OnAcceptPress.Invoke(Buttons == ButtonStyles.TwoButtonWithBoxCantidad, editBoxNo.Text.CastToSingle(), editTrayCigarsQuantity.Text.CastToSingle());
            }

            dialog.Dismiss();
            dialog.Dispose();
        }
        private async Task<Boolean> ProcessUpcCodeAsync(BarCodeResult barcode, Double eanCode, EditText editUpcCode)
        {
            try
            {
                var isCorrect = true;

                if (editUpcCode.Text.Length > 0)
                {
                    if (eanCode != Convert.ToDouble(editUpcCode.Text))
                    {
                        isCorrect = false;
                        txtShowMessagesBox.Visibility = ViewStates.Visible;
                        txtShowMessagesBox.Text = "UPC incorrecto.";

                        var proceso = await repoz.GetProces();
                        var Caching = new CachingManager(context);

                        var ean = await repoz.GetUnitByCode(editBoxNo.Text);
                        var MaterialWrong = new Materials();

                        var barCodeUPC = editBoxNo.Text.GetBarCode();
                        barCodeUPC.Lot = Lot;

                        if (ean != null)
                        {
                            var repoMat = repo.GetRepositoryMaterials();
                            MaterialWrong = await repoMat.GetAsyncByKey(ean.MaterialCode);

                            if (MaterialWrong != null)
                            {
                                await Util.AddError(proceso, await repoz.GetActualConfig(proceso.EquipmentID), MaterialWrong, barCodeUPC, Caching, true, Errors.Messages.WRONG_UPCCODE_MATERIALCONSUMPTION);
                            }
                        }

                        editUpcCode.Text = String.Empty;
                        editUpcCode.RequestFocus();
                    }
                    else
                    {
                        txtShowMessagesBox.Visibility = ViewStates.Visible;
                        txtShowMessagesBox.SetTextColor(Color.Green);
                        txtShowMessagesBox.Text = "UPC correcto.";
                    }
                }
                else
                {
                    isCorrect = false;
                }
                return isCorrect;
            }
            catch (Exception ex)
            {
                Util.CatchException(context, ex);
                return false;
            }
        }
        private void pass_OnCancelPress()
        {
            OnCancelPress.Invoke();
        }

        private void pass_OnAcceptPress(Shared.Models.Process.ProcessList Process)
        {
            SecurityManager.SetProcess(Process);
            OnAcceptPress.Invoke(false, 0, 0);
        }

        private async void InvokeEvent() ///Espero que se termine de referenciar el evento
        {
            await Task.Delay(500);
            if (status == Status.GoodValidateBox) ONValidaSecuencia.Invoke();
        }

        private async void InvokeEventError()
        {
            await Task.Delay(500);
            if (status == Status.ErrorValidateTray) ONValidaBandeja.Invoke();
        }
    }
}