using System;
using System.Linq;

namespace SICORIS.SGA.Data
{
    namespace Comun
    {
        namespace SISTEMA
        {
            #region "Caso Registro"
            public static class CASO_REGISTRO
            {
                public static void ActualizarEstado(int cod_caso_registro, int cod_catalogo_estado_actual, int cod_usuario_modificacion, string observacion)
                {
                    ComunDataContext cdc = new ComunDataContext();
                    var casoRegistro = cdc.SISTEMA_CASO_REGISTRO.Where(r => r.cod_auto == cod_caso_registro).SingleOrDefault();
                    int cod_catalogo_estado_previo = casoRegistro.cod_catalogo_estado;
                    casoRegistro.cod_catalogo_estado = cod_catalogo_estado_actual;
                    casoRegistro.cod_usuario_modificacion = cod_usuario_modificacion;
                    casoRegistro.fecha_usuario_modificacion = DateTime.Now;
                    cdc.SubmitChanges();
					---prueba
					--como

                    int cod_catalogo_movimiento = Convert.ToInt32(TiposEstados.CambioEstado);
                    CASO_HISTORIAL.Guardar(cod_caso_registro, cod_catalogo_movimiento, cod_catalogo_estado_previo, cod_catalogo_estado_actual, observacion, cod_usuario_modificacion);
                }

                public static SISTEMA_CASO_REGISTRO Seleccionar(int cod_caso_registro)
                {
                    ComunDataContext cdc = new ComunDataContext();
                    var casoRegistro = cdc.SISTEMA_CASO_REGISTRO.Where(a => a.cod_auto == cod_caso_registro).ToList();
                    SISTEMA_CASO_REGISTRO retorno = new SISTEMA_CASO_REGISTRO();

                    if (casoRegistro.Count > 0)
                        retorno = casoRegistro[0];

                    return retorno;
                }

                public static SISTEMA_CASO_REGISTRO Seleccionar(int cod_registro, int cod_proceso)
                {
                    ComunDataContext cdc = new ComunDataContext();
                    var casoRegistro = cdc.SISTEMA_CASO_REGISTRO.Where(a => a.cod_caso_registro == (int?)null && a.cod_registro == cod_registro && a.cod_proceso == cod_proceso).ToList();
                    SISTEMA_CASO_REGISTRO retorno = new SISTEMA_CASO_REGISTRO();

                    if (casoRegistro.Count > 0)
                        retorno = casoRegistro[0];

                    return retorno;
                }

                public static SISTEMA_CASO_REGISTRO Seleccionar(int? cod_caso_registro, int cod_registro, int cod_proceso)
                {
                    ComunDataContext cdc = new ComunDataContext();
                    var casoRegistro = cdc.SISTEMA_CASO_REGISTRO.Where(a => a.cod_registro == cod_registro && a.cod_proceso == cod_proceso && a.cod_caso_registro == cod_caso_registro).ToList();
                    SISTEMA_CASO_REGISTRO retorno = new SISTEMA_CASO_REGISTRO();

                    if (casoRegistro.Count > 0)
                        retorno = casoRegistro[0];

                    return retorno;
                }

                public static int SeleccionarCodigo(int cod_registro, int cod_proceso)
                {
                    ComunDataContext cdc = new ComunDataContext();
                    var casoRegistro = cdc.SISTEMA_CASO_REGISTRO.Where(a => a.cod_caso_registro == (int?)null && a.cod_registro == cod_registro && a.cod_proceso == cod_proceso).ToList();
                    int retorno = 0;

                    if (casoRegistro.Count > 0)
                        retorno = casoRegistro[0].cod_auto;

                    return retorno;
                }

                public static int SeleccionarCodigo(int? cod_caso_registro, int cod_registro, int cod_proceso)
                {
                    ComunDataContext cdc = new ComunDataContext();
                    var casoRegistro = cdc.SISTEMA_CASO_REGISTRO.Where(a => a.cod_registro == cod_registro && a.cod_proceso == cod_proceso && a.cod_caso_registro == cod_caso_registro).ToList();
                    int retorno = 0;

                    if (casoRegistro.Count > 0)
                        retorno = casoRegistro[0].cod_auto;

                    return retorno;
                }
            }
            #endregion "Caso Registro"

            #region "Caso Historial"
            public static class CASO_HISTORIAL
            {
                public static void Guardar(int cod_caso_registro, int cod_catalogo_movimiento, int? cod_catalogo_estado_previo, int? cod_catalogo_estado_actual, string observacion, int cod_usuario_registro)
                {
                    ComunDataContext cdc = new ComunDataContext();
                    var historial = new SISTEMA_CASO_HISTORIAL();
                    historial.cod_caso_registro = cod_caso_registro;
                    historial.cod_catalogo_movimiento = cod_catalogo_movimiento;
                    historial.cod_catalogo_estado_previo = cod_catalogo_estado_previo;
                    historial.cod_catalogo_estado_actual = cod_catalogo_estado_actual;
                    historial.observacion = observacion;
                    historial.cod_usuario_registro = cod_usuario_registro;
                    historial.fecha_usuario_registro = DateTime.Now;
                    cdc.SISTEMA_CASO_HISTORIAL.InsertOnSubmit(historial);
                    cdc.SubmitChanges();
                }
            }
            #endregion "Caso Historial"
        }
    }
}
