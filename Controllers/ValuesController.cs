using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using systemMantenimiento;
using systemMantenimiento.Util;

namespace systemMantenimiento
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // we want json  not those weird string ? Ohh Microsoft ?
        [HttpGet]
        public ActionResult Get()
        {
            CategoriaRepository categoriaRepository = new();

            List<CategoriaModel> data = categoriaRepository.Read();

            return Ok(new { data });
        }
        // what if we need post ? we don't some idiot playing with mvc / / / / ?
        [HttpPost]
        public ActionResult Post()
        {
            var status = false;
            // wait // anybody tell this before ? 
            var mode = Request.Form["mode"];

            var nombre = Request.Form["nombre"];
            var tipo = Request.Form["tipo"];
            var descripcion = Request.Form["descripcion"];
            var categoriaId = Request.Form["categoriaId"];

            CategoriaRepository categoriaRepository = new();

            List<CategoriaModel> data = new();

            var code = "";
            // but we think something missing .. what ya ? 
            switch (mode)
            {
                case "create":
                    // for ide like php storm maybe they will angry ? wher's my catch ?
                    try
                    {
                        categoriaRepository.Create(nombre, tipo, descripcion);

                        code = ((int)ReturnCode.CREATE_SUCCESS).ToString();
                        status = true;

                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;

                    }
                    break;
                case "read":
                    try
                    {
                        data = categoriaRepository.Read();
                        code = ((int)ReturnCode.CREATE_SUCCESS).ToString();
                        status = true;

                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;

                    }

                    break;
                case "update":
                    try
                    {
                        categoriaRepository.Update(nombre,tipo, descripcion, Convert.ToInt32(categoriaId));
                        code = ((int)ReturnCode.UPDATE_SUCCESS).ToString();
                        status = true;

                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;

                    }
                    break;
                case "delete":
                    try
                    {
                        categoriaRepository.Delete(Convert.ToInt32(categoriaId));

                        code = ((int)ReturnCode.DELETE_SUCCESS).ToString();
                        status = true;

                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;

                    }
                    break;
                default:
                    // do remmeber hacker always exist anywhere ?
                    code = ((int)ReturnCode.ACCESS_DENIED_NO_MODE).ToString();
                    break;
            }

            return Ok(new { status, code, data });
        }

    }
}
