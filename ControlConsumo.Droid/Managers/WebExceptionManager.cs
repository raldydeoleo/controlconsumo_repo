using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ControlConsumo.Droid.Managers
{
    public class WebExceptionManager
    {
        public WebException WebException { get; set; }

        public WebExceptionManager(WebException webException)
        {
            WebException = webException;
        }

        public String ClasificarExcepcionWeb()
        {
            var mensajeError = "";
            try
            {
                if (WebException.Response == null)//Si no hubo respuesta del servidor, determino la causa a través del estatus de Web Exception
                {
                    switch (WebException.Status)
                    {
                        case WebExceptionStatus.NameResolutionFailure:
                        case WebExceptionStatus.ConnectFailure:
                        case WebExceptionStatus.ProxyNameResolutionFailure:
                        case WebExceptionStatus.RequestProhibitedByProxy:
                        case WebExceptionStatus.Timeout:
                            mensajeError = "Error de comunicación.";
                            break;
                        default:
                            mensajeError = "Error interno. Favor de intentarlo nuevamente.";
                            break;
                    }
                }
                else
                {
                    var httpWebResponse = (HttpWebResponse) WebException.Response; //Si hubo respuesta del servidor, determino la causa a través del código de estatus HTTP 

                    switch (httpWebResponse.StatusCode)
                    {
                        case HttpStatusCode.Unauthorized:
                            mensajeError = "Recurso solicitado requiere Autenticación.";
                            break;
                        case HttpStatusCode.NotFound:
                            mensajeError = "Recurso no encontrado. Contacte a Tecnología de Información.";
                            break;
                        case HttpStatusCode.RequestTimeout:
                            mensajeError = "Conexión cerrada inesperadamente por Servidor. Favor de intentarlo nuevamente.";
                            break;
                        case HttpStatusCode.InternalServerError:
                            mensajeError = "El servidor encontró un error interno o desconfiguración y no pudo procesar su solicitud.";
                            break;
                        case HttpStatusCode.NotImplemented:
                            mensajeError = "Funcionalidad no implementada. Contacte a Tecnología de Información.";
                            break;
                        case HttpStatusCode.BadGateway:
                            mensajeError = "Puerta de enlace no válida. Contacte a Tecnología de Información.";
                            break;
                        case HttpStatusCode.ServiceUnavailable:
                            mensajeError = "Servicio no disponible. Contacte a Tecnología de Información.";
                            break;
                        case HttpStatusCode.GatewayTimeout:
                            mensajeError = "El Servidor encontró un error temporal y no pudo completar la solicitud. Intente de nuevo en 30 segundos.";
                            break;
                        case HttpStatusCode.HttpVersionNotSupported:
                            mensajeError = "Versión de HTTP no soportada. Contacte a Tecnología de Información.";
                            break;
                        default:
                            mensajeError = "Error interno. Favor de intentarlo nuevamente.";
                            break;
                    }
                }

                //Guardar log de error en Aplicación.
                //Util.SaveException(WebException);

                return mensajeError;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}