using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using System.Threading;
using System.Threading.Tasks;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Droid.Activities.Widgets;
using Android.Bluetooth;
using System.IO;
using Zebra.Android.Comm;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Printer.Internal;
using Android.Hardware.Usb;
using Android.App;
using Zebra.Sdk.Comm;
using PrinterLanguage = Zebra.Sdk.Printer.PrinterLanguage;
using PrinterStatus = Zebra.Sdk.Printer.PrinterStatus;
using IConnection = Zebra.Sdk.Comm.IConnection;
using LinkOS.Plugin;
using LinkOS.Plugin.Abstractions;

namespace ControlConsumo.Droid.Managers
{
    class PrinterManager : IDisposable
    {
        public enum _PrinterStatus
        {
            Configured = 0,
            No_Paired = 1,
            Connecting = 2,
            Error = 3,
            Connected = 4
        }

        public delegate void Get_Status(_PrinterStatus _Status);
        public event Get_Status ON_Get_Status;

        private List<BufferClass> Buffer = new List<BufferClass>();
        public _PrinterStatus LastStatus { get; private set; }

        private readonly RepositoryFactory repo = new RepositoryFactory(Util.GetConnection());
        private const String SPP_UUID = "00001101-0000-1000-8000-00805F9B34FB";
        public const String ACTION_USB_PERMISSION = "Lemo.ControlConsumo.Droid.USB_PERMISSION";
        private const int UsbPermissionTimeout = 30000;
        private static readonly object UsbConnectionLock = new object();
        private IConnection Connection { get; set; }
        private ZebraPrinterZpl printer { get; set; }
        public String PrinterName
        {
            get
            {
                return repo.GetRepositoryZ().GetSetting<String>(Settings.Params.PrinterName, null);
            }
            private set { }
        }
        public String PrinterAddress
        {
            get
            {
                return repo.GetRepositoryZ().GetSetting<String>(Settings.Params.PrinterAddress, null);
            }
        }
        public string PrinterConnectivity
        {
            get
            {
                return repo.GetRepositoryZ().GetSetting<String>(Settings.Params.PrinterConnectivity, null);
            }
        }
        private Boolean Printer4X3 { get; set; }

        private static PrinterManager SinglePrinter;

        #region Metodos Privados

        private void Connect()
        {
            Thread t;
            if (PrinterConnectivity.Equals("Usb", StringComparison.InvariantCultureIgnoreCase))
            {
                t = new Thread(doConnectUsb);
            }
            else
            {
                t = new Thread(()=> { doConnectBT(PrinterAddress); });
            }
            t.Start();
        }

        private void doConnectBT(String deviceAddress)
        {
            try
            {
                Connection = new BluetoothPrinterConnection(deviceAddress).ConvertedNewStyleConnection;
                Connection.Open();
            }
            catch (ZebraPrinterConnectionException ex)
            {
                disconnect();
            }
            catch (Exception ex)
            {
                disconnect();
            }

            threadedConnect(deviceAddress);
        }

        private void doConnectUsb()
        {
            try
            {
                Connection = GetUsbConnection(this.PrinterAddress);
                Connection.Open();
            }
            catch (ZebraPrinterConnectionException ex)
            {
                disconnect();
            }
            catch (Exception ex)
            {
                disconnect();
            }

            threadedConnect(this.PrinterAddress);
        }


        private UsbDevice GetUsbDevice(string deviceAddress)
        {
            try
            {
                var UsbDevices = new List<UsbDevice>();
                var usbReceiver = new UsbReceiver();
                var usbFilter = new IntentFilter(ACTION_USB_PERMISSION);
                Application.Context.RegisterReceiver(usbReceiver, usbFilter);

                UsbManager usbManager = (UsbManager)Application.Context.GetSystemService(Context.UsbService);
                IEnumerable<UsbDevice> deviceIterator = usbManager.DeviceList.Values.AsEnumerable();

                var usbDevice = deviceIterator.FirstOrDefault(s => s.SerialNumber == deviceAddress);

                return usbDevice;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                UsbReceiver.Reset();
            }
        }

        private UsbConnection GetUsbConnection(string deviceAddress)
        {
            lock (UsbConnectionLock)
            {
                try
                {
                    var usbReceiver = new UsbReceiver();
                    var usbFilter = new IntentFilter(ACTION_USB_PERMISSION);
                    Application.Context.RegisterReceiver(usbReceiver, usbFilter);

                    UsbManager usbManager = (UsbManager) Application.Context.GetSystemService(Context.UsbService);

                    IEnumerable<UsbDevice> deviceIterator = usbManager.DeviceList.Values.AsEnumerable();

                    var usbDevice = deviceIterator.FirstOrDefault(s => s.SerialNumber == deviceAddress);

                    if (usbDevice != null)
                    {
                        return new UsbConnection(usbManager, usbDevice);
                    }
                    else
                    {
                        throw new Exception($"USB device '{deviceAddress}' was not found.");
                    }
                }
                catch(Exception)
                {
                    throw;
                }
                finally
                {
                    UsbReceiver.Reset();
                }
            }
        }

        private void disconnect()
        {
            Thread t = new Thread(doDisconnect);
            t.Start();
        }

        private void doDisconnect()
        {
            try
            {
                if (Connection != null && Connection.IsConnected)
                {
                    Connection.Close();
                }
            }
            catch (Exception)
            {

            }
            Thread.Sleep(1000);
            Connection = null;
        }

        private void threadedConnect(String addressName)
        {
            printer = null;
            if (Connection != null && Connection.IsConnected)
            {
                try
                {
                    printer = new ZebraPrinterZpl(Connection);
                    PrinterLanguage pl = printer.PrinterControlLanguage;
                    Thread.Sleep(2000);
                }
                catch (ZebraPrinterConnectionException)
                {
                    printer = null;
                    disconnect();
                }
                catch (ZebraPrinterLanguageUnknownException)
                {
                    printer = null;
                    disconnect();
                }
            }
        }

        private Boolean ExecuteHeader(Boolean Printer4X3)
        {
            this.Printer4X3 = Printer4X3;
            Connect();
            Thread.Sleep(10000);

            while (Connection != null && Connection.IsConnected)
            {
                PrinterStatus printerStatus = null;

                try
                {
                    printerStatus = printer.CurrentStatus;
                }
                catch (NullReferenceException ex)
                {
                    return false;
                }

                if (printerStatus.IsReadyToPrint)
                {
                    return true;
                }
            }

            return false;
        }

        private String GetBegin()
        {
            return "^XA";
        }

        private String GetEnd()
        {
            return "^XZ";
        }

        private String GetCode128(String Contenido, Decimal x, Decimal y, Decimal dots, Decimal w, Boolean Normal)
        {
            //Int32 Resta = (Int32)Contenido.Length - 25;
            //Int32 Suma = 0;

            //if (Resta > 0)
            //    Suma = 10 * Resta;

            StringBuilder str = new StringBuilder();
            str.AppendLine(String.Format("^FO{0},{1}^BY{2}", x, y, w)); // the second command sets the field origin at 100 dots across the x-axis and 75 dots down the y-axis from the upper-left corner.
            str.AppendLine(String.Format("^BC{1},{0},Y,N,N", dots, Normal ? "N" : "R")); // the third command calls for a Code 128 bar code to be printed with no rotation (N) and a height of 100 dots. An interpretation line is printed (Y) below the bar code (N). No UCC check digit is used (N).
            str.AppendLine(String.Format("^FD{0}^FS", Contenido)); // the field data command specifies the content of the bar code.            
            return str.ToString();
        }

        private String GetCode93(String Contenido, Decimal x, Decimal y, Decimal dots, Decimal w, Boolean Normal)
        {
            Int32 Resta = (Int32)Contenido.Length - 25;
            Int32 Suma = 0;

            if (Resta > 0)
                Suma = 10 * Resta;

            StringBuilder str = new StringBuilder();
            str.AppendLine(String.Format("^FO{0},{1}^BY{2}", x - Suma, y, w)); // the second command sets the field origin at 100 dots across the x-axis and 75 dots down the y-axis from the upper-left corner.
            str.AppendLine(String.Format("^B3{1},N,{0},N,N", dots, Normal ? "N" : "R")); // the third command calls for a Code 128 bar code to be printed with no rotation (N) and a height of 100 dots. An interpretation line is printed (Y) below the bar code (N). No UCC check digit is used (N).
            str.AppendLine(String.Format("^FD{0}^FS", Contenido)); // the field data command specifies the content of the bar code.            
            return str.ToString();
        }

        private String GetFont(String Contenido, Decimal x, Decimal y, Decimal dots, Decimal w)
        {
            return String.Format("^FO{1},{2}^A0,{3},{4}^FD{0}^FS", Contenido, x, y, dots, w);
        }

        private String GetFont(String Contenido, Decimal x, Decimal y, Decimal dots, Decimal w, Decimal w2)
        {
            return String.Format("^FO{1},{2}^A0,{3},{4}^FB{5},1,0,C^FD{0}^FS", Contenido, x, y, dots, w, w2);
        }

        private String GetFont(String Contenido, Decimal x, Decimal y, Decimal dots, Decimal w, Boolean Grade90)
        {
            return String.Format("^FO{1},{2}^A0,{3},{4}^FW{5}^FD{0}^FS", Contenido, x, y, dots, w, Grade90 ? "R" : "N");
        }

        private String GetFont(String Contenido, Decimal x, Decimal y, Decimal dots, Decimal w, Decimal w2, Boolean Grade90)
        {
            return String.Format("^FO{1},{2}^A0,{3},{4}^FB{5},1,0,C^FW{6}^FD{0}^FS", Contenido, x, y, dots, w, w2, Grade90 ? "R" : "N");
        }

        #endregion

        #region Propiedades Estaticas

        private BluetoothSocket socket;
        private Stream mmInStream;
        private Stream mmOutStream;

        #endregion

        #region Metodos Publicos

        private async Task<bool> Print3X1(List<Etiquetas> etiquetas)
        {
            try
            {
                if (ExecuteHeader(false))
                {
                    Decimal Suma = -10;

                    int Control = 0;

                    foreach (var etiqueta in etiquetas)
                    {
                        String Temp = String.Empty;
                        Control++;

                        var report = new StringBuilder();
                        report.AppendLine(GetBegin());

                        Temp = String.Concat("Material: ", etiqueta._Material);

                        report.AppendLine(GetFont(Temp, 1, 45 + Suma, 30, 25));

                        Temp = String.Concat("Lote Supl: ", etiqueta.LoteSuplidor);

                        report.AppendLine(GetFont(Temp, 325, 45 + Suma, 30, 25));

                        report.AppendLine(GetFont(etiqueta._Descripcion, 1, 70 + Suma, 30, 25));

                        Temp = String.Concat("Qty: ", etiqueta._Medida, " ", etiqueta.Unidad);

                        report.AppendLine(GetFont(Temp, 393, 70 + Suma, 30, 25));

                        Temp = String.Concat("Lote Int: ", etiqueta._LoteInterno);

                        report.AppendLine(GetFont(Temp, 1, 95 + Suma, 30, 25));

                        Temp = String.Concat("Fecha Exp: ", etiqueta.Fecha.HasValue ? etiqueta.Fecha.Value.ToString("dd/MM/yy") : "");
                        report.AppendLine(GetFont(Temp, 326, 95 + Suma, 30, 25));

                        report.AppendLine(GetCode93(etiqueta.CodigoBarra, 70, 125 + Suma, 50, 1, true));

                        report.AppendLine(GetFont(etiqueta.CodigoBarra.Replace("-000000", ""), 180, 178 + Suma, 20, 16));

                        report.AppendLine(GetEnd());

                        for (int i = 0; i < etiqueta.Cantidad; i++)
                        {
                            Connection.Write(report.ToString().ToByte());
                            Thread.Sleep(350);
                        }

                        // if (etiquetas.Count > 1 && Control != etiquetas.Count) utils.Util.showInf("Favor Presione OK para Continuar con el siguiente Grupo");
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                await Util.SaveException(ex);
            }
            finally
            {
                disconnect();
            }

            return false;
        }

        /// <summary>
        /// Metodo para imprimir equiquetas standares
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <returns></returns>
        private async Task<bool> Print4X3(List<Etiquetas> etiquetas)
        {
            try
            {
                if (ExecuteHeader(true))
                {
                    Decimal Suma = 15;

                    int Control = 0;

                    var Proceso = await repo.GetRepositoryZ().GetProces();

                    var usuario = SecurityManager.CurrentProcess != null ? SecurityManager.CurrentProcess.Logon : Proceso.Logon;

                    foreach (var etiqueta in etiquetas)
                    {
                        Control++;
                        String Temp = String.Empty;

                        var report = new StringBuilder();
                        report.AppendLine(GetBegin());

                        report.AppendLine(GetFont("LA AURORA, S.A.", 240, 40 + Suma, 60, 50));

                        Temp = String.Concat("Material: ", etiqueta._Material);

                        report.AppendLine(GetFont(Temp, 30, 130 + Suma, 45, 35));

                        Temp = String.Concat("Qty: ", etiqueta._Medida, " ", etiqueta.Unidad);

                        report.AppendLine(GetFont(Temp, 500, 130 + Suma, 45, 35));

                        report.AppendLine(GetFont(etiqueta._Descripcion, 30, 170 + Suma, 45, 35));

                        Temp = String.Concat("Lote Supl: ", etiqueta.LoteSuplidor);

                        report.AppendLine(GetFont(Temp, 30, 210 + Suma, 45, 35));

                        Temp = String.Concat("No. Caja: @Secuencia");

                        report.AppendLine(GetFont(Temp, 30, 248 + Suma, 45, 35));

                        report.AppendLine(GetFont("Expiracion", 560, 210 + Suma, 45, 35));

                        Temp = "";

                        if (etiqueta.Fecha != null)
                        {
                            if (etiqueta.Fecha.Value.Year != 0001)
                            {
                                Temp = etiqueta.Fecha.Value.ToString("dd/MM/yy");
                            }
                        }

                        report.AppendLine(GetFont(Temp, 580, 248 + Suma, 45, 35));

                        Temp = String.Concat("Lote Int: ", etiqueta._LoteInterno);

                        report.AppendLine(GetFont(Temp, 75, 305 + Suma, 80, 65));

                        report.AppendLine(GetCode128(etiqueta.CodigoBarra, 110, 390 + Suma, 150, 2, true));

                        report.AppendLine(GetFont(usuario, 30, 570, 40, 25));

                        //report.AppendLine(GetFont(etiqueta.CodigoBarra.Replace("-000000", ""), 1, 175, 30, 16, 600));                       

                        report.AppendLine(GetEnd());

                        for (int i = 0; i < etiqueta.Cantidad; i++)
                        {
                            Connection.Write(report.ToString().Replace("@Secuencia", etiqueta.Secuencia.ToString("00000")).ToByte());
                            Thread.Sleep(350);
                        }

                        // if (etiquetas.Count > 1 && Control != etiquetas.Count) utils.Util.showInf("Favor Presione OK para Continuar con el siguiente Grupo");
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                await Util.SaveException(ex);
            }
            finally
            {
                disconnect();
            }

            return false;
        }

        /// <summary>
        /// Etiqueta Standard
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <returns></returns>
        public async Task<bool> Print4X3EtiquetasUnica(List<Etiquetas> etiquetas)
        {
            try
            {
                if (ExecuteHeader(true))
                {
                    Decimal Suma = 15;

                    int Control = 0;

                    var Proceso = await repo.GetRepositoryZ().GetProces();

                    var usuario = SecurityManager.CurrentProcess != null ? SecurityManager.CurrentProcess.Logon : Proceso.Logon;

                    foreach (var etiqueta in etiquetas)
                    {
                        Control++;
                        String Temp = String.Empty;

                        var report = new StringBuilder();
                        report.AppendLine(GetBegin());

                        report.AppendLine(GetFont("LA AURORA, S.A.", 240, 40 + Suma, 60, 50));

                        Temp = String.Concat("Material: ", etiqueta._Material);

                        report.AppendLine(GetFont(Temp, 30, 130 + Suma, 45, 35));

                        Temp = String.Concat("Qty: ", etiqueta._Medida, " ", etiqueta.Unidad);

                        report.AppendLine(GetFont(Temp, 500, 130 + Suma, 45, 35));

                        report.AppendLine(GetFont(etiqueta._Descripcion, 30, 170 + Suma, 45, 35));

                        Temp = String.Concat("Lote Supl: ", etiqueta.LoteSuplidor);

                        report.AppendLine(GetFont(Temp, 30, 210 + Suma, 45, 35));

                        Temp = String.Concat("No. Caja: @Secuencia");

                        report.AppendLine(GetFont(Temp, 30, 248 + Suma, 45, 35));

                        report.AppendLine(GetFont("Expiracion", 560, 210 + Suma, 45, 35));

                        Temp = "";

                        if (etiqueta.Fecha != null)
                        {
                            if (etiqueta.Fecha.Value.Year != 0001)
                            {
                                Temp = etiqueta.Fecha.Value.ToString("dd/MM/yy");
                            }
                        }

                        report.AppendLine(GetFont(Temp, 580, 248 + Suma, 45, 35));

                        Temp = String.Concat("Lote Int: ", etiqueta._LoteInterno);

                        report.AppendLine(GetFont(Temp, 75, 305 + Suma, 80, 65));

                        report.AppendLine(GetCode128(etiqueta.CodigoBarra, 50, 390 + Suma, 150, 2, true));

                        report.AppendLine(GetFont(usuario, 30, 570, 40, 25));

                        report.AppendLine(GetEnd());

                        Connection.Write(report.ToString().Replace("@Secuencia", etiqueta.Secuencia.ToString("00000")).ToByte());
                        Thread.Sleep(350);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                await Util.SaveException(ex);
            }
            finally
            {
                disconnect();
            }

            return false;
        }
        
        public async Task ExecutePrint(Context context, List<Etiquetas> lista)
        {
            try
            {
                if (String.IsNullOrEmpty(PrinterAddress))
                {
                    new CustomDialog(context, CustomDialog.Status.Warning, context.GetString(Resource.String.AlertNoConfig));
                    return;
                }

                var printmanager = new PrinterManager();

                if (lista.Any(a => a.Secuencia == 0))
                {
                    var result = await printmanager.Print4X3(lista.Where(w => w.Secuencia == 0).ToList());
                  
                    if (!result)
                    {
                        new CustomDialog(context, CustomDialog.Status.Warning, context.GetString(Resource.String.AlertNoConnection));
                    }
                }

                if (lista.Any(a => a.Secuencia > 0))
                {
                    var etiquetas = lista.Where(w => w.Secuencia > 0).ToList();

                    var result2 = await printmanager.Print4X3EtiquetasUnica(etiquetas);
                   
                    if (!result2)
                    {
                        new CustomDialog(context, CustomDialog.Status.Warning, context.GetString(Resource.String.AlertNoConnection));
                    }
                    //else
                    //{
                    //    var repoz = new RepositoryFactory(Util.GetConnection()).GetRepositoryZ();
                    //    var proceso = await repoz.GetProces();
                    //    await repoz.Post_Secuences(etiquetas.Select(s => new PostSecuenceRequest
                    //    {
                    //        MATNR = s.Material,
                    //        CHARG = s.LoteInterno,
                    //        LICHA = s.LoteSuplidor,
                    //        WERKS = proceso.Centro,
                    //        ESTADO = "A",
                    //        CREADOR = SecurityManager.CurrentProcess.Logon,
                    //        EMPAQUENO = s.Secuencia
                    //    }).ToList());
                    //}
                }
            }
            catch (Exception ex)
            {
                await Util.SaveException(ex);
            }
        }

        public async Task<Boolean> PrintLastEtiqueta(Context context, Elaborates salida, ProductsRoutes traza, Byte Copias)
        {
            try
            {
                if (! await SingleConnect()) return false;

                ///  Se crea un buffer para almacenar las impresiones por si se esta conectando
                if (!Buffer.Any(a => a.salida.CustomFecha == salida.CustomFecha && a.salida.CustomID == salida.CustomID))
                {
                    Buffer.Add(new BufferClass
                    {
                        CustomID = salida.CustomID,
                        CustomFecha = salida.CustomFecha,
                        salida = salida,
                        traza = traza,
                        Copias = Copias
                    });
                }

                var repoz = new RepositoryFactory(Util.GetConnection()).GetRepositoryZ();

                foreach (var item in Buffer.Where(w => !w.Printed).OrderBy(o => o.CustomID))
                {
                    item.Printed = true;
                    Buffer.RemoveAll(r => r.CustomFecha == item.CustomFecha && r.CustomID == item.CustomID);
                    var elaborate = item.salida ?? await repoz.GetLastSalidaAsync();
                    var producto = await repoz.GetMaterialByCodeAsync(elaborate.ProductCode);
                    var categories = await repoz.GetCategorias(elaborate.ProductCode);

                    var categoria = String.Empty;

                    var _categories = categories.Where(w => w._Value > 0).OrderByDescending(d => d._Value).ToList();

                    if (_categories.Count() > 1)
                    {
                        if (_categories.Count() == 2)
                        {
                            var IPACK = _categories.FirstOrDefault(f => f.Category == Categories.TypesCategories.IPDM_IPACK).Value;
                            var NOIPACK = _categories.FirstOrDefault(f => f.Category != Categories.TypesCategories.IPDM_IPACK).Value;
                            categoria = String.Format("{0}/{1}", NOIPACK, IPACK);
                        }
                        else
                        {
                            var IPACK = _categories.FirstOrDefault(f => f.Category == Categories.TypesCategories.IPDM_IPACK).Value;
                            var PCKSC = _categories.FirstOrDefault(f => f.Category == Categories.TypesCategories.IPDM_PCKSC).Value;
                            var PCKCT = _categories.FirstOrDefault(f => f.Category == Categories.TypesCategories.IPDM_PCKCT).Value;
                            categoria = String.Format("{0}-{1}/{2}", PCKCT, PCKSC, IPACK);
                        }
                    }
                    var report = new StringBuilder();

                    var secuenciaEmpaque = elaborate.PackSequence == 0 ? item.traza.SecuenciaEmpaque : elaborate.PackSequence; 

                    var eanCode = String.Format("1{0}{1}{2}{3}     {4}", elaborate.Identifier, elaborate.PackID.Replace("-", "").PadRight(8, ' '), elaborate._Produccion.ToString("hhmmss"), producto.Reference.PadRight(13, ' '), secuenciaEmpaque.ToString("0000"));

                    report.Append("^XA");
                    report.Append("^PON");
                    report.Append("^CF0,50");
                    report.Append(String.Format("^FO45,25^FD{0}^FS", producto.Name));
                    report.Append("^CF0,45");

                    if ((await repoz.GetProces()).Process.Equals("2304", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var actualConfig = await repoz.GetActualConfig(elaborate.EquipmentID);
                        var unit = actualConfig.units.First(p => p.Unit == "CS" || p.Unit == "CJ");
                        var bandeja = await repoz.GetBandejaConfig(elaborate.ProcessID, actualConfig.TimeID, unit.Unit);
                        bandeja.Quantity = Util.CtoUnidad(unit, 1, actualConfig.units.Single(p => p.Unit == "OZ"), actualConfig.units);

                        report.Append(String.Format("^FO45,90^FD{0} OUNCES^FS", bandeja.Quantity));
                    }
                    else
                    {
                        report.Append(String.Format("^FO45,90^FD{0} CIGARS^FS", elaborate.Quantity * 1000));
                    }
                    report.Append(String.Format("^FO590,90^FD{0}^FS", categoria));
                    report.Append("^CF0,53");
                    report.Append(String.Format("^FO45,170^FD{0}^FS", producto.Reference));
                    report.Append("^CF0,65");
                    report.Append(String.Format("^FO45,265^FD{0}{1}^FS", elaborate.Identifier, elaborate.PackID.Replace("-", "").PadRight(8, ' ')));
                    report.Append("^CF0,53");
                    report.Append(String.Format("^FO326,275^FD{0}^FS", elaborate._Produccion.ToString("hhmmss")));
                    report.Append(String.Format("^FO495,275^FD{0}^FS", secuenciaEmpaque.ToString("0000")));
                    //report.Append(String.Format("^FO45,275^FB540,1,0,R,0^FD{0}^FS", item.traza.SecuenciaEmpaque.ToString("0000")));
                    report.Append("^CF0,30");
                    report.Append(String.Format("^FO45,350^FD{0}^FS", eanCode));
                    report.Append(String.Format("^FO610,160^BQN,2,8^FH\\^FDMA,{0}^FS", eanCode));
                    report.Append("^XZ");

                    if (!PrinterConnectivity.Equals("Usb", StringComparison.InvariantCultureIgnoreCase))
                    {
                        mmInStream = socket.InputStream;
                        mmOutStream = socket.OutputStream;

                        for (int i = 0; i < item.Copias + 1; i++)
                        {
                            var bbite = report.ToString().ToByte();
                            mmOutStream.Write(bbite, 0, bbite.Length);
                            await Task.Delay(400);
                        }

                        mmInStream.Flush();
                        mmOutStream.Flush();
                    }
                    else
                    {
                        doConnectUsb();
                        for (int i = 0; i < item.Copias + 1; i++)
                        {
                            Connection.Write(Encoding.ASCII.GetBytes(report.ToString()));
                        }
                    }
                }

                Buffer.RemoveAll(r => r.Printed);

                ThrowStatus(_PrinterStatus.Connected);
            }
            catch (Java.IO.IOException ex)
            {
                Task.Delay(1000).Wait();
                ThrowStatus(_PrinterStatus.Error);
                Dispose();
                await Util.SaveException(ex);
            }
            catch (Exception ex)
            {
                ThrowStatus(_PrinterStatus.Error);
                Dispose();
                await Util.SaveException(ex);
                return false;
            }

            finally
            {
                try
                {
                    if (Connection != null)
                    {
                        Connection.Close();
                    }
                }
                catch(ConnectionException ce)
                {
                    await Util.SaveException(ce);
                    throw;
                }
            }
            return true;
        }

        private void ThrowStatus(_PrinterStatus _Error)
        {
            LastStatus = _Error;
            ON_Get_Status.Invoke(_Error);
        }

        public async void Dispose()
        {
            if (mmOutStream != null)
            {
                try
                {
                    mmOutStream.Close();
                    mmOutStream.Dispose();
                }
                catch (NullReferenceException)
                { }
                catch (Exception ex)
                {
                    await Util.SaveException(ex);
                }
                finally
                {
                    try
                    {
                        mmOutStream = null;
                    }
                    catch (Exception)
                    { }
                }
            }

            if (mmInStream != null)
            {
                try
                {
                    mmInStream.Close();
                    mmInStream.Dispose();
                }
                catch (NullReferenceException)
                { }
                catch (Exception ex)
                {
                    await Util.SaveException(ex);
                }
                finally
                {
                    try
                    {
                        mmInStream = null;
                    }
                    catch (Exception)
                    { }
                }
            }

            if (socket != null)
            {
                try
                {
                    socket.Close();
                    socket.Dispose();
                }
                catch (NullReferenceException)
                { }
                catch (Exception ex)
                {
                    await Util.SaveException(ex);
                }
                finally
                {
                    try
                    {
                        socket = null;
                    }
                    catch (Exception)
                    { }
                }
            }

            if (printer != null)
            {
                try
                {
                    printer.Dispose();
                    printer = null;
                }
                catch (NullReferenceException)
                { }
                catch (Exception ex)
                {
                    await Util.SaveException(ex);
                }
                finally
                {
                    try
                    {
                        printer = null;
                    }
                    catch (Exception)
                    { }
                }
            }

            if (Connection != null)
            {
                try
                {
                    if (Connection.IsConnected) Connection.Close();

                    Connection.Dispose();
                }
                catch (NullReferenceException)
                { }
                catch (Exception ex)
                {
                    await Util.SaveException(ex);
                }
                finally
                {
                    try
                    {
                        Connection = null;
                    }
                    catch (Exception)
                    { }
                }
            }
        }

        #endregion

        public async Task<bool> SingleConnect()
        {
            if (PrinterConnectivity.Equals("Bluetooth"))
            {
                if (LastStatus == _PrinterStatus.Connected && socket.IsConnected) return true;
            }

            var conteo = 0;

            VolverAConnectar:

            conteo++;

            if (conteo == 3)
            {
                ThrowStatus(_PrinterStatus.Error);
                return false;
            }

            try
            {
                if (PrinterConnectivity.Equals("Usb", StringComparison.InvariantCultureIgnoreCase))
                {
                    try
                    {
                        UsbDevice usbDevice = GetUsbDevice(PrinterAddress);
                        ThrowStatus(_PrinterStatus.Connecting);
                        if (usbDevice != null)
                        {
                            Connect();
                            ThrowStatus(_PrinterStatus.Connected);

                        }
                    }
                    catch(Exception ex)
                    {
                        await Util.SaveException(ex);
                        ThrowStatus(_PrinterStatus.Error);
                        return false;
                    }
                }
                else
                {
                    var adapter = BluetoothAdapter.DefaultAdapter;

                    if (!adapter.IsEnabled) adapter.Enable();

                    if (adapter.IsDiscovering) adapter.CancelDiscovery();

                    if (socket == null || !socket.IsConnected)
                    {
                        if (LastStatus == _PrinterStatus.Connecting) return false; /// Evito mas forces de conexión
                        var device = adapter.BondedDevices.SingleOrDefault(s => s.Address.Equals(PrinterAddress));
                        if (device == null)
                        {
                            ThrowStatus(_PrinterStatus.No_Paired);
                            return false;
                        }

                        PrinterName = device.Name;
                        socket = device.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString(SPP_UUID));

                        ThrowStatus(_PrinterStatus.Connecting);
                        socket.Connect();
                        ThrowStatus(_PrinterStatus.Connected);
                    }

                    //if (mmInStream == null) mmInStream = socket.InputStream;

                    //if (mmOutStream == null) mmOutStream = socket.OutputStream;
                }
            }
            catch (Java.IO.IOException ex)
            {
                Task.Delay(500).Wait();
                ThrowStatus(_PrinterStatus.Error);
                Dispose();
                await Util.SaveException(ex);
                goto VolverAConnectar;
            }
            catch (Exception ex)
            {
                ThrowStatus(_PrinterStatus.Error);
                Dispose();
                await Util.SaveException(ex);
                return false;
            }

            return true;
        }

        public static PrinterManager GetUniqueInstance()
        {
            if (SinglePrinter == null)
            {
                SinglePrinter = new PrinterManager();
            }

            return SinglePrinter;
        }

        private class BufferClass
        {
            public Boolean Printed { get; set; }
            public Int32 CustomFecha { get; set; }
            public Int32 CustomID { get; set; }
            public Elaborates salida { get; set; }
            public ProductsRoutes traza { get; set; }
            public Byte Copias { get; set; }
        }

        public class UsbDiscoveryHandler: IDiscoveryHandler
        {
            public bool DiscoveryComplete { get; private set; } = false;
            public List<IDiscoveredPrinter> DiscoveredPrinters { get; set; } = new List<IDiscoveredPrinter>();

            public void DiscoveryError(String message)
            {
                Console.WriteLine($"An error occured during discovery: {message}.");
                DiscoveryComplete = true;
            }

            public void DiscoveryFinished()
            {
                DiscoveryComplete = true;
            }
            
            public void FoundPrinter(IDiscoveredPrinter discoveredPrinter)
            {
                DiscoveredPrinters.Add(discoveredPrinter);
            }
        }

        [BroadcastReceiver]
        [IntentFilter(new[] { UsbManager.ExtraDevice })]
        public class UsbReceiver : BroadcastReceiver
        {
            public UsbReceiver()
            {
                Reset();
            }

            public static bool HasPermission { get; private set; } = false;

            public static Result Result { get; private set; } = Result.Canceled;

            public static void Reset()
            {
                HasPermission = false;
                Result = Result.Canceled;
            }

            public override void OnReceive(Context context, Intent intent)
            {   
                if (ACTION_USB_PERMISSION.Equals(intent.Action))
                {
                    UsbDevice device = (UsbDevice)intent.GetParcelableExtra(UsbManager.ExtraDevice);
                    if (intent.GetBooleanExtra(UsbManager.ExtraPermissionGranted, false))
                    {
                        if (device != null)
                        {
                            HasPermission = true;
                        }
                    }
                    Result = Result.Ok;
                }
            }
        }
    }
}