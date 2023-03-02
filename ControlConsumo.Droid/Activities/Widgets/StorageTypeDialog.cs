using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Widget;
using ControlConsumo.Shared.Models.Config;
using ControlConsumo.Shared.Tables;
using static Android.Support.V4.Widget.DrawerLayout;

namespace ControlConsumo.Droid.Activities.Widgets
{
    class TipoAlmacenamientoDialog
    {
        public delegate void StoreType(Boolean IsCold, String ProductType, String Identifier);
        public event StoreType OnStoreType;
        private readonly Context context;
        protected RepositoryFactory repo;
        protected NextConfig config;
        public TipoAlmacenamientoDialog(Context context, RepositoryFactory repo, NextConfig config)
        {
            this.context = context;
            this.repo = repo;
            this.config = config;
        }

        public async void ShowDialogAsync(Times Tiempo)
        {
            if (Tiempo.Producto == Times.ProductTypes.None || Tiempo.Producto == Times.ProductTypes.Validar_Salida)
            {
                OnStoreType.Invoke(false, "",""); ;
                return;
            }

            Dialog dialog = null;

            dialog = new Dialog(context, Android.Resource.Style.ThemeTranslucentNoTitleBar);
            dialog.SetContentView(Resource.Layout.dialog_tipo_almacenamiento);
            dialog.SetCancelable(false);

            var repoTipoProductoTerminados = repo.GetRepositoryTipoProductoTerminados();
            var repoProductoTipoAlmacenamientos = repo.GetRepositoryProductoTipoAlmacenamientos();
            var repoTipoAlmacenamientoProductos = repo.GetRepositoryTipoAlmacenamientoProductos();


            var listaTipoProductoTerminado = await repoTipoProductoTerminados.GetAsyncAll();
            var listaTipoAlmacenamiento = await repoTipoAlmacenamientoProductos.GetAsyncAll();
            var listaProductoTipoAlmacenamiento = await repoProductoTipoAlmacenamientos.GetAsyncAll();

            var detallesProducto = new List<ProductTraceCodeResult>();

            //Obtener tipo de producto terminado asociado al producto que está corriendo.
            var tipoProductoTerminado = listaTipoProductoTerminado.Where(s =>s.alias.Equals(config.ProductType, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (tipoProductoTerminado != null){
                foreach(var item in listaProductoTipoAlmacenamiento.Distinct().Where(s => s.idTipoProductoTerminado == tipoProductoTerminado.id))
                {
                    var detalleProducto = new ProductTraceCodeResult();
                    detalleProducto.identificador = item.identificador;
                    detalleProducto.idTipoAlmacenamiento = item.idTipoAlmacenamientoProducto;
                    detallesProducto.Add(detalleProducto);
                }
            }
            var rgSelection = dialog.FindViewById<RadioGroup>(Resource.Id.rgSelection);
            RadioGroup.LayoutParams rprms;
            var contador = 0;

            
            foreach (var detalleProducto in detallesProducto)
            {
                //Listado de tipo de almacenamiento de acuerdo al tipo de producto que está actualmente corriendo.
                foreach (var item in listaTipoAlmacenamiento.Where(s =>s.id==detalleProducto.idTipoAlmacenamiento))
                {
                    contador++;
                    var radioButton = new RadioButton(context);
                    rprms = new RadioGroup.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
                    radioButton.Id = contador;
                    radioButton.Text = (item.nombre + " (" + detalleProducto.identificador + ") ");
                    radioButton.TextSize = 60;
                    radioButton.SetTextAppearance(context, Android.Resource.Color.Black);
                    radioButton.SetTextAppearance(context, Android.Resource.Style.TextAppearanceLarge);
                    if (config.Identifier != null)
                    {
                        if (config.Identifier.Equals(detalleProducto.identificador, StringComparison.InvariantCultureIgnoreCase))
                        {
                            radioButton.Checked = true;
                        }
                    }
                    if (config.Identifier == null)
                    {
                        if (item.nombre.Contains("Dry") || item.nombre.Contains("Regular"))
                        {
                            radioButton.Checked = true;
                        }
                    }
                    rgSelection.AddView(radioButton, rprms);
                }
            }

            if (tipoProductoTerminado == null && String.IsNullOrEmpty(config.ProductType)) //Si el producto no tiene un trace code asignado, mostrar un mensaje de error
            {
                var dialogTraceCodeError = new CustomDialog(context, CustomDialog.Status.Error,
                           String.Format("Producto no tiene asignado el tipo de Filler. Favor de contactar al Sup. de Calidad \n"));
            }
            if (tipoProductoTerminado == null && !String.IsNullOrEmpty(config.ProductType)) // Si el tipo de filler no existe en la base de datos.
            {
                var dialogFillerError = new CustomDialog(context, CustomDialog.Status.Error,
                 String.Format("Tipo de Filler {0} no existe. Favor de contactar al Sup. de Calidad \n",config.ProductType));

            }
            var btnAceptDialog = dialog.FindViewById<Button>(Resource.Id.btnAceptDialog);
            
            //var rdbSeco = dialog.FindViewById<RadioButton>(Resource.Id.rdbSeco);
            //var rdbFrio = dialog.FindViewById<RadioButton>(Resource.Id.rdbFrio);

            var LayoutPass = dialog.FindViewById<LinearLayout>(Resource.Id.custom_dialog_first_rl);
            LayoutPass.Background = context.Resources.GetDrawable(Resource.Color.gray_base);
            LayoutPass.Background.SetAlpha(175);

            btnAceptDialog.Click += (obj, arg) =>
            {
                RadioButton radioButton = new RadioButton(context);
                if (dialog != null)
                {
                    radioButton = dialog.FindViewById<RadioButton>(rgSelection.CheckedRadioButtonId);
                    dialog.Dismiss();
                    dialog.Dispose();
                }

                if (OnStoreType != null)
                {
                    string identificador = "";
                    string[] arregloDeCadenas;
                    var isCold = false;
                    char[] parametrosDeSeparacion = new char[] { '(', ')' };

                    if (radioButton != null)
                    {
                        arregloDeCadenas = radioButton.Text.Split(parametrosDeSeparacion);
                        if (arregloDeCadenas.Length > 1)
                        {
                            identificador = arregloDeCadenas[1];
                        }
                        if (radioButton.Text.Contains("Frío") || radioButton.Text.Contains("Cold"))
                        {
                            isCold = true;
                        }
                        OnStoreType.Invoke(isCold, config.ProductType,identificador);
                    }
                }
            };

            if (contador > 0)
            {
                dialog.Show();
            }
        }
        public class ProductTraceCodeResult
        {
            public int idTipoAlmacenamiento { get; set; }
            public string identificador { get; set; }
        }
    }
}